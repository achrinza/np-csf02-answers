#include <iostream>   	// for input/output
#include <string>	 	// for string class
#include "BST.h"

void displayMenu();

int main()
{
	BST bst;
	ItemType target;

	int dataArray[10] = { 55, 28, 87, 32, 90, 16, 45, 79, 66, 30 };

	for (int i = 0; i < 10; i++)
	{
		bst.insert(dataArray[i]);
	}

	int option = 1;

	while (option != 0)
	{
		displayMenu();
		std::cin >> option;

		if (option == 1) {
			bst.inorder();
		}
		else if (option == 2) {
			std::cout << "Enter a number to search : ";
			std::cin >> target;
			BinaryNode* p = bst.search(target);
			if (p != NULL)
				std::cout << "Found" << endl;
			else
				std::cout << "Not found" << endl;
		}
		else if (option == 3) {
			int n;
			std::cout << "Enter a number to add : ";	
			std::cin >> n;
			if (bst.search(n) == NULL) // check if n already exists
				bst.insert(n);
		}
		else if (option == 4) {
			int n;
			std::cout << "Enter a number to delete : ";
			std::cin >> n;
			bst.remove(n);
		}
		else if (option == 5) {
			std::cout << bst.countNodes() << std::endl;
		}
		else if (option == 6) {
			std::cout << bst.getHeight() << std::endl;
		}
		else if (option == 7) {
			if (bst.isBalanced())
				std::cout << "The tree is balanced" << std::endl;
			else
				std::cout << "The tree is not balanced" << std::endl;
		}
		else if (option == 0) {
			std::cout << "Bye! \n";
			break;
		}
		else {
			std::cout << "Sorry. You have entered an invalid option. \n";
		}
	}

	return 0;
}

void displayMenu()
{
	cout << endl;
	cout << "Binary Search Tree Demo \n";
	cout << "--------------------------------\n";
	cout << "1 Display all the items in order\n";
	cout << "2 Search for an item            \n";
	cout << "3 Add a new item                \n";
	cout << "4 Delete an item                \n";
	cout << "5 Display total number of items \n";
	cout << "6 Display the height            \n";
	cout << "7 Check if tree is balanced     \n";
	cout << "0 Exit							 \n";
	cout << "--------------------------------\n";
	cout << "Enter option : ";
}




