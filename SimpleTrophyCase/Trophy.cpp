// TODO: implement the Trophy class

#include <string>
#include <iostream>
using namespace std;

// This is the enum of the colors, where Bronze = 0, Silver = 1, and Gold = 2
enum class ColorOfTrophies
{
	BRONZE,
	SILVER,
	GOLD
};

class Trophy 
{
public:
	//intialize trophy to safe values
	Trophy() 
	{
		name = "";
		level = 0;
		color = ColorOfTrophies::BRONZE;
	}
	
	//actual constructor containing 3 needed parameters
	Trophy(string name, int level, ColorOfTrophies color) 
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
	ColorOfTrophies GetColor() 
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
	void SeTrophyColor(ColorOfTrophies inputColor) 
	{
		color = inputColor;
	}

	//prints out formatted response after checking the case of the trophy's color
	void Print() 
	{
		string trophyColor;
		switch (color)
		{
		case ColorOfTrophies::BRONZE:
			trophyColor = "BRONZE";
			break;
		case ColorOfTrophies::SILVER:
			trophyColor = "SILVER";
			break;
		case ColorOfTrophies::GOLD:
			trophyColor = "GOLD";
			break;
		}

		cout << "[ " << name << " : " << level << " : " << trophyColor << " ]" << endl;
	}

private:
	//variables to just be used by this file
	string name;
	int level;
	ColorOfTrophies color;
};