#include <iostream>
#include <string>
#include <vector>
#include "Trophy.h"


using namespace std;

// Reusable strings
const string PROMPT_FOR_NAME = "Please enter the name of the Trophy";
const string PROMPT_FOR_LEVEL = "Please enter the level of your Trophy (between 1 and 10)";
const string PROMPT_FOR_COLOR = "Please enter the color of your Trophy (GOLD, SILVER, or BRONZE)";

// Menu choice handlers
void addTrophy(/* TODO: vector of trophies */ vector<Trophy> &trophies);
void copyTrophy(/* TODO: vector of trophies */ vector<Trophy> &trophies);
void deleteTrophy(/* TODO: vector of trophies */ vector<Trophy> &trophies);
void renameTrophy(/* TODO: vector of trophies */ vector<Trophy> &trophies);
void relevelTrophy(/* TODO: vector of trophies */ vector<Trophy> &trophies);
void recolorTrophy(/* TODO: vector of trophies */ vector<Trophy> &trophies);
void printTrophies(/* TODO: vector of trophies */ vector<Trophy> trophies);

// Input handlers
int printMenu();
Trophy /* TODO: Return a Trophy instead of void */ promptForTrophy();
string promptForString(const string& message);
int promptForInt(const string& message, int minimum, int maximum);
Color /* TODO: Return a color instead of void */ promptForColor(const string& message);

// Useful helper methods
string stringToUpper(string value);
int searchForTrophy(/* TODO: vector of trophies, name of a trophy  */ vector<Trophy> trophies, const string& nameOfTrophy);

// This application allows for the management of a trophy collection, has a switch statement used to select menu option that user wants to choose.
int main()
{
	cout << "***********************************************" << endl
		<< "Welcome to the Trophy editor!" << endl
		<< "With this application, you can manage your trophy" << endl
		<< "collection - add, modify, or remove trophies by" << endl
		<< "using this simple menu!" << endl
		<< "***********************************************" << endl;

	// TODO: Create a vector of Trophy objects
	vector<Trophy> trophies;

	// Loop the menu, allowing the user to select an action each time
	int input;
	do
	{
		input = printMenu();
		switch (input)
		{
		case 1:		// Add a new Trophy
			addTrophy(/* collection of trophies  */ trophies);
			break;
		case 2:		// Copy an existing Trophy
			copyTrophy(/* collection of trophies  */ trophies);
			break;
		case 3:		// Delete an existing Trophy
			deleteTrophy(/* collection of trophies  */ trophies);
			break;
		case 4:		// Rename a Trophy
			renameTrophy(/* collection of trophies  */ trophies);
			break;
		case 5:		// Change the level of a Trophy
			relevelTrophy(/* collection of trophies  */ trophies);
			break;
		case 6:		// Change the color of a Trophy
			recolorTrophy(/* collection of trophies  */ trophies);
			break;
		case 7:		// Print all Trophies
			printTrophies(/* collection of trophies  */ trophies);
			break;
		case 8:		// Exit
			cout << "You have chosen to exit the application, good-bye!" << endl;
			break;
		default:
			cout << "That is not a recognized menu selection, choose again." << endl;
			break;
		}

	} while (input != 8);

	return 0;
}

// Print the menu to the user and accept their menu choice
int printMenu()
{
	int input;
	cout << "-----------------------------------------" << endl
		<< "Please select an option :" << endl
		<< "1 - Add a new Trophy" << endl
		<< "2 - Copy a Trophy" << endl
		<< "3 - Delete a Trophy" << endl
		<< "4 - Rename a Trophy" << endl
		<< "5 - Change the level of a Trophy" << endl
		<< "6 - Change the color of a Trophy" << endl
		<< "7 - Print All the Trophies" << endl
		<< "8 - Exit the program" << endl
		<< "-----------------------------------------" << endl;
	cin >> input;
	cin.ignore();
	return input;
}

// Add a new Trophy to the collection, prompts the user for a trophy, once trophy is created it is then pushed into the vector holding the trophies
void addTrophy(/* TODO: vector of trophies */ vector<Trophy> &trophies)
{
	cout << "You have chosen to add a trophy." << endl;
	// TODO: Ask the user for the Trophy info (hint: there's a function for this...) and add it to the vector
	Trophy trophyToAdd = promptForTrophy();
	trophies.push_back(trophyToAdd);
}

// Delete an existing Trophy from the collection
void deleteTrophy(/* TODO: vector of trophies */ vector<Trophy> &trophies)
{
	//used to determine which index the trophy we want to delete is held at.
	int trophyLoc = 0;
	cout << "You have chosen to delete an existing trophy." << endl;
	string name = promptForString(PROMPT_FOR_NAME);
	// TODO: Find the trophy and if it exists, erase it to the vector
	trophyLoc = searchForTrophy(trophies, name);
	//checks to see if there is an actual trophy wanting to be deleted.
	if (trophyLoc >= 0)
	{
		int i = 0;
		//loops through the vector of trophies
		for (; i < trophies.size(); i++)
		{
			//once specific name is found
			if (name == trophies[i].GetName())
			{
				break;
			}
		}
		// trophy is erased
		trophies.erase(trophies.begin() + i);
	}
}

// Copy an existing Trophy in the collection, and then adds copy to collection
void copyTrophy(/* TODO: vector of trophies */ vector<Trophy> &trophies)
{
	int trophyLocation = 0;
	cout << "You have chosen to copy an existing trophy." << endl;
	string name = promptForString(PROMPT_FOR_NAME);

	// TODO: Find the trophy and if it exists, copy it and add the copy to the vector
	trophyLocation = searchForTrophy(trophies, name);
	if (trophyLocation >=0)
	{
		//creates a new copy of trophy at designated trophy location
		Trophy newTrophy = trophies[trophyLocation];
		//pushes trophy into vector
		trophies.push_back(newTrophy);
	}
}

// Rename an existing Trophy (change the name)
void renameTrophy(/* TODO: vector of trophies */ vector<Trophy> &trophies)
{
	int trophyLocation = 0;
	cout << "You have chosen to rename an existing trophy." << endl;
	string name = promptForString(PROMPT_FOR_NAME);
	// TODO: Find the trophy and if it exists, change its name
	string newName = promptForString("Please enter the new name of the Trophy");
	trophyLocation = searchForTrophy(trophies, name);
	if (trophyLocation >= 0)
	{
		//for the trophy at the specified location, the set name func is called to change the name
		trophies[trophyLocation].SetName(newName);
	}

}

// Relevel an existing Trophy (change the level)
void relevelTrophy(/* TODO: vector of trophies */ vector<Trophy> &trophies)
{
	int trophyLocation = 0;
	cout << "You have chosen to change the level of an existing trophy." << endl;
	string name = promptForString(PROMPT_FOR_NAME);
	// TODO: Find the trophy and if it exists, change its level
	int newLevel = promptForInt(PROMPT_FOR_LEVEL, 1, 10);
	trophyLocation = searchForTrophy(trophies, name);
	if (trophyLocation >= 0)
	{
		//calls set level to change the level of trophy at trophy Location
		trophies[trophyLocation].SetLevel(newLevel);
	}
}

// Recolor an existing Trophy (change the color)
void recolorTrophy(/* TODO: vector of trophies */ vector<Trophy> &trophies)
{
	int trophyLocation = 0;
	cout << "You have chosen to change the color of an existing trophy." << endl;
	string name = promptForString(PROMPT_FOR_NAME);
	// TODO: Find the trophy and if it exists, change its color
	Color newColor = promptForColor(PROMPT_FOR_COLOR);
	trophyLocation = searchForTrophy(trophies, name);
	if (trophyLocation >= 0)
	{
		//sets the color using set color
		trophies[trophyLocation].SetTrophyColor(newColor);
	}
}

// Print all of the Trophies in the collection
void printTrophies(/* TODO: vector of trophies */ vector<Trophy> trophies)
{
	int i = 0;
	cout << "You have chosen to print all of the trophies." << endl;
	// TODO: Print all the trophies in the order they are stored in the vector
	//iterates through the vector
	for (vector<Trophy>::iterator iter = trophies.begin(); iter != trophies.end(); iter++)
	{
		//every interation the current trophy is printed and then the index counter i is incremented to change index of trophies
		trophies[i].Print();
		i++;
	}
}

// Ask the user for a Trophy, validate their responses
// Return the Trophy
Trophy /* TODO: Return a Trophy instead of void */ promptForTrophy()
{
	//prompts for the name, level and color
	string name = promptForString(PROMPT_FOR_NAME);
	int level = promptForInt(PROMPT_FOR_LEVEL, 1, 10);
	/* TODO: Store the color the user selected = */ 
	Color trophyColor = promptForColor(PROMPT_FOR_COLOR);

	// TODO: Create a new trophy with the above info
	//creates new trophy using the constructors parameters
	Trophy newTrophy(name, level, trophyColor);

	// TODO: Return a Trophy
	return newTrophy;
}

// Ask the user for a string, validate their response
// Return the string
string promptForString(const string& message)
{
	string value;
	cout << message << endl;
	// TODO: read in the trophy name
	getline(cin, value);
	// TODO: while the user has not entered any characters
	while (value.empty())
	{
		cout << "That is not a valid name.  Try again.";
		// TODO: read in the trophy name
		getline(cin, value);
	} 
	return value;
}

// Ask the user for an int, validate their response by
// checking that it is between minimum and maximum values
// Return the int
int promptForInt(const string& message, int minimum, int maximum)
{
	int value = 0;
	cout << message << endl;
	// TODO: read in a level number
	cin >> value;
	cin.ignore();
	// TODO: while the level number is not between the minimum and maximum (inclusive)
	while (value < minimum || value > maximum)
	{
		cout << "That value is outside the acceptable range.  Try again." << endl;
		// TODO: read in a level number
		cin >> value;
		cin.ignore();
	}
	return value;
}

// Convert a string to all uppercase (so that we can compare the
// user's entered color to the official color)
// returns uppercased input for color choice
string stringToUpper(string value)
{
	// TODO: Convert the string parameter into all uppercase
	string upperCaseInput;
	//loops for every character within the string of value
	for (int i = 0; i < value.length(); i++)
	{
		//adds the uppercased letters into the string upperCaseInput
		upperCaseInput += toupper(value[i]);
	}
	return upperCaseInput;
}

// Ask the user for a color, validate the response
// Return the color
Color /* TODO: Return a color instead of void */ promptForColor(const string& message)
{
	/* TODO: Create a Color variable */
	Color trophyColor;
	string value;
	bool isValidColor = false;
	cout << message << endl;
	
	// TODO: while the color is not acceptable
	// loops until we recieve a valid color input from user
	while (isValidColor != true)
	{
		// TODO: read in the color
		getline(cin, value);
		// TODO: If the color is  GOLD, SILVER, or BRONZE (hint: case insensitive!)
		// TODO:    convert the string color into the enumerated type Color
		// TODO: otherwise, print an error
		// Turns the string of value into an uppercase string
		string upperCase = stringToUpper(value);
		//determines whether the string fits in the color criteria or needs to loop again
		if (upperCase == "BRONZE")
		{
			trophyColor = BRONZE;
			isValidColor = true;
		}
		else if (upperCase == "SILVER")
		{
			trophyColor = SILVER;
			isValidColor = true;
		}
		else if (upperCase == "GOLD")
		{
			trophyColor = GOLD;
			isValidColor = true;
		}
		else
		{
			cout << "That is not an acceptable color.  Try again." << endl;
		}
	}
	/* TODO: Return the Color that the user selected */
	return trophyColor;
}

// Search for a trophy in the collection by name
int searchForTrophy(/* TODO: vector of trophies, name of a trophy  */ vector<Trophy> trophies, const string& nameOfTrophy)
{
	// TODO: Find the trophy in the collection by its name
	// TODO: If you find the trophy, return its position in the collection
	// TODO: if the trophy was not found, print this error, return -1
	// loops through the trophies vector and checks to see if the name of trophy matches any within the vector
	for (int i = 0; i < trophies.size(); i++)
	{
		if (nameOfTrophy == trophies[i].GetName())
		{
			// returns the index location of the trophy 
			return i;
		}
	}

	//prints error and returns -1 if trophy isnt found
	cout << "ERROR: The Trophy was not found" << endl;
	return -1;
}