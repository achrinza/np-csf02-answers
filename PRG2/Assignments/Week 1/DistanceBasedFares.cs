//
// (c) 2019 Rifa I. Achrinza
// This code is licensed under MIT license (See LICENSE.txt for details)
// SPDX-Short-Identifier: MIT
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DistanceBasedFares
{
    class BusStop
    {
        public string Code { get; set; }
        public float Distance { get; set; }
        public string Road { get; set; }
        public string Description { get; set; }

        public BusStop(string busStopCode, float distance, string road, string description)
        {
            Code = busStopCode;
            Distance = distance;
            Road = road;
            Description = description;
        }
    }

    class DistanceBasedFare
    {
        public float Distance { get; set; }

        public int Cost { get; set; }

        public DistanceBasedFare(float distance, int cost)
        {
            Distance = distance;
            Cost = cost;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<BusStop> BusStopList = new List<BusStop>();
            List<DistanceBasedFare> DistanceBasedFareList = new List<DistanceBasedFare>();

            Console.Write("bus_174.csv path (default: ./bus_174.csv): ");
            string BusStopFilePath = Console.ReadLine();
            if (BusStopFilePath == "") BusStopFilePath = @"./bus_174.csv";

            bool IsFileValidFormat = true;

            try
            {
                using (StreamReader reader = new StreamReader(BusStopFilePath))
                {

                    int i = 0;
                    while (!reader.EndOfStream)
                    {
                        string Line = reader.ReadLine();

                        if (i > 0)
                        {
                            string[] LineArray = Line.Split(',');

                            try
                            {
                                BusStopList.Add(new BusStop(
                                   LineArray[1],
                                   Convert.ToSingle(LineArray[0]),
                                   LineArray[2],
                                   LineArray[3]
                               ));
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Error: Invalid file format.");
                                IsFileValidFormat = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error: File not found.");
                IsFileValidFormat = false;
            }


            if (!IsFileValidFormat)
            {
                Console.ReadKey();
                Environment.Exit(13);
            }

            Console.Write("distance-based-fare.csv file path (default: ./distance-based-fare.csv): ");
            string DBFFilePath = Console.ReadLine();
            if (DBFFilePath == "") DBFFilePath = "./distance-based-fare.csv";
            IsFileValidFormat = true;

            try
            {
                using (StreamReader reader = new StreamReader(DBFFilePath))
                {
                    int i = 0;
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();

                        if (i > 0)
                        {
                            string[] LineArray = line.Split(',');

                            try
                            {
                                DistanceBasedFareList.Add(new DistanceBasedFare(
                                    Convert.ToSingle(LineArray[0]),
                                    Convert.ToInt32(LineArray[1])
                                ));
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Error: Invalid file format.");
                                IsFileValidFormat = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error: File not found.");
                IsFileValidFormat = false;
            }

            if (!IsFileValidFormat)
            {
                Console.ReadKey();
                Environment.Exit(13);
            }

            Console.WriteLine($"{"Distance (km)".PadRight(13)} {"BusStopCode".PadRight(11)} {"Road".PadRight(19)} Bus Stop Destination");

            BusStopList.ForEach(x => { Console.WriteLine($"{x.Distance.ToString().PadRight(13)} {x.Code.PadRight(11)} {x.Road.PadRight(19)} {x.Description}"); });

            Console.Write("Enter boarding bus stop: ");
            string BusStopBoardingInput = Console.ReadLine();

            BusStop BusStopBoarding = BusStopList.Find((BusStop bs) => bs.Code == BusStopBoardingInput);

            if (BusStopBoarding == default)
            {
                Console.WriteLine("Error: Boarding bus stop not found.");
                Console.ReadKey();
                Environment.Exit(11);
            }

            Console.Write("Enter alighting bus stop: ");
            string BusStopAlightingInput = Console.ReadLine();

            BusStop BusStopAlighting = BusStopList.Find((BusStop bs) => bs.Code == BusStopAlightingInput);

            if (BusStopAlighting == default)
            {
                Console.WriteLine("Error: Alighting bus stop not found.");
                Console.ReadKey();
                Environment.Exit(12);
            }

            float TravelDistance = Math.Abs(BusStopAlighting.Distance - BusStopBoarding.Distance);

            DistanceBasedFare LastComparedDistanceBasedFare = new DistanceBasedFare(0, 0);

            foreach (DistanceBasedFare x in DistanceBasedFareList.OrderBy(x => x.Distance))
            {
                if (x.Distance > TravelDistance)
                {
                    LastComparedDistanceBasedFare = x;
                    break;
                }

            }

            Console.WriteLine($"Distance travelled: {TravelDistance.ToString("0.00")} km");
            Console.WriteLine($"Fare to pay: ${LastComparedDistanceBasedFare.Distance.ToString("0.00")}");
            Console.WriteLine($"Estimated duration: {Math.Round(TravelDistance * 4, 2).ToString("0.00")} mins");

            Console.ReadKey();
        }
    }
}
