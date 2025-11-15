// TODO: Guard the header
#ifndef TROPHY_HEADER

// TODO: include the libraries this class will need
#include <string>
#include <iostream>
using namespace std;

//holds the values of Color, bronze = 0, silver = 1, gold = 2
enum Color 
{
	BRONZE,
	SILVER,
	GOLD
};		// TODO: create the color values

class Trophy {
public:
	//intialize trophy to safe values
	Trophy()
	{
		name = "";
		level = 0;
		color = BRONZE;
	}

	//actual constructor containing 3 needed parameters
	Trophy(string name, int level, Color color)
	{
		this->name = name;
		this->level = level;
		this->color = color;
	}

	//returns the name
	string GetName()
	{
		return name;
	}

	//returns the level
	int GetLevel()
	{
		return level;
	}

	//returns the color
	Color GetColor()
	{
		return color;
	}

	//changes name to new input
	void SetName(string inputName)
	{
		name = inputName;
	}

	//changes level to new input
	void SetLevel(int inputLevel)
	{
		level = inputLevel;
	}

	//changes color to new input
	void SetTrophyColor(Color inputColor)
	{
		color = inputColor;
	}

	//prints out formatted response after checking the case of the trophy's color
	void Print()
	{
		string trophyColor;
		switch (color)
		{
		case BRONZE:
			trophyColor = "BRONZE";
			break;
		case SILVER:
			trophyColor = "SILVER";
			break;
		case GOLD:
			trophyColor = "GOLD";
			break;
		}

		cout << "[ " << name << " : " << level << " : " << trophyColor << " ]" << endl;
	}

private:
	//variables to just be used by this file
	string name;
	int level;
	Color color;
};;	// TODO: complete the Trophy class

// TODO: end the guard
#endif // !1