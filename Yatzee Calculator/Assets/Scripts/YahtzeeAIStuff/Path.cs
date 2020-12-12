using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{

	/* I got these number by doing (sum)! / (product of each (number)!)
	* 1, 1,  2, 1,   6,  3, 1,   24,  12,  6,  4, 1,   120,   60,  30,  20,  5, 10, 1
	* 0, 1, 11, 2, 111, 12, 3, 1111, 112, 22, 13, 4, 11111, 1112, 122, 113, 14, 23, 5
	*/

	/// <summary>
	/// This list is the multipliers used for each type of dice arrangements shown above that is used to calculate the probability
	/// </summary>
	readonly static int[] chanceMultipliers = { 1, 1, 2, 1, 6, 3, 1, 24, 12, 6, 4, 1, 120, 60, 30, 20, 5, 10, 1 };

	/// <summary>
	/// This is the size of all starting dice combinations
	/// </summary>
	readonly static int DICE_COMBINATION_ARRAY_SIZE = 462;

	/// <summary>
	/// This is the size of the roll combinations from one hand to another that does not result in 0
	/// </summary>
	readonly static int PATH_LIST_SIZE = 166176;

	/// <summary>
	/// This is the name of the file that stores the data for the path probabilities
	/// </summary>
	readonly static string PATH_DATA_FILE_NAME = "PathProbabilitiesNormalFloat";

	/// <summary>
	/// This is the array of sorted dice roll options
	/// </summary>
	static string[] diceRollOptions;

	/// <summary>
	/// This is the array of sorted dice roll option probabilities
	/// </summary>
	static float[] diceRollProbabilities;

	/// <summary>
	/// This is the list of all unique numericalphabetical dice combinations in the order 123456G
	/// </summary>
	static string[] diceCombinations = InitializeRollCombinations();

	/// <summary>
	/// This is the array that keeps track of whether dice combinations have already been calculated
	/// </summary>
	static bool[] diceRollCombinationsAlreadyCalculated;

	/// <summary>
	/// This is the array that keeps track of dice combinations of the dice roll combination average point values for each iteration of 252 verticies in a column
	/// </summary>
	static float[] diceRollCombinationsAveragePointValues;

	/// <summary>
	/// This gets the index of the dice combination that is given in the corresponding lists
	/// </summary>
	/// <param name="dice">The dice combination of 5 dice characters in numericalphabetical order 123456G</param>
	/// <returns>The index of the given dice in the corresponding list</returns>
	static int GetDiceCombinationIndex(string dice)
	{
		// These are the min and max indexes of possible ID indexes
		int minIndex = 0;
		int maxIndex = DICE_COMBINATION_ARRAY_SIZE - 1;

		// These are the ID values at the min and max indexes
		string minID = diceCombinations[minIndex];
		string maxID = diceCombinations[maxIndex];

		// This is the index and ID value that split the searching in half using a binary search
		int currentIndex;
		string currentID;

		// These handle if the ID is at the beginning or end of the array or if the array is length 1
		if (minID.Equals(dice) || minID.CompareTo(dice) > 0)
		{
			return 0;
		}
		else if (maxID.ToString().Equals(dice) || maxID.CompareTo(dice) < 0)
		{
			return maxIndex;
		}
		else if (minIndex == maxIndex)
		{
			return 0;
		}

		// This performs a binary search of the data based on its ID
		for (int i = 0; i < DICE_COMBINATION_ARRAY_SIZE; i++)
		{

			// When the search is narrowed to two options it picks the correct index
			if (maxIndex - minIndex <= 1)
			{
				if (maxID.ToString().Equals(dice))
				{
					return maxIndex;
				}
				else
				{
					return minIndex;
				}
			}

			// This updates the middle index in between the min and max indexes
			currentIndex = (maxIndex + minIndex) / 2;
			currentID = diceCombinations[currentIndex];

			// This decides whether to search the upper half or lower half of the array depending on the currentID value and the value to search for
			if (currentID.ToString().Equals(dice))
			{
				return currentIndex;
			}
			else if (currentID.CompareTo(dice) < 0)
			{
				minID = currentID;
				minIndex = currentIndex;
			}
			else
			{
				maxID = currentID;
				maxIndex = currentIndex;
			}
		}

		// This theoretically shouldn't be reached because a line should always be found, but if this executes then something was wrong with this method or array
		throw new System.Exception("There was a problem with the GetDiceCombinationIndex function in Path.");
	}

	/// <summary>
	/// This adds the averagePointValue for the given diceStart to remember for a column
	/// </summary>
	/// <param name="diceStart">The dice combination of 5 dice characters in numericalphabetical order 123456G</param>
	/// <param name="averagePointValue">The resulting average point value of the given dice to be rolled in a column</param>
	public static void AddDiceCombination(string diceStart, float averagePointValue)
	{

		// This is the index to add the float value and set the calculated bool to be set to true
		int index = GetDiceCombinationIndex(diceStart);
		diceRollCombinationsAlreadyCalculated[index] = true;
		diceRollCombinationsAveragePointValues[index] = averagePointValue;
	}

	/// <summary>
	/// This initializes the dice combinations array with all unique dice combinations in numericalphabetical order 123456G
	/// </summary>
	/// <returns>The ordered array of dice combinations</returns>
	static string[] InitializeRollCombinations()
	{
		string[] arr = new string[462];

		// This counts the index to add the next element in the array
		int count = 0;

		// This loops through each dice combination not counting repeats
		for (int i = 0; i < 16807; i++)
		{
			// This creates the next possible dice combination that is cycled through based on i. From 11111 to 66666
			int[] thing = new int[5];
			for (int j = 0; j < 5; j++)
			{
				thing[5 - j - 1] = 1 + ((i / (int)Mathf.Pow(7, j)) % 7);
			}

			// The dice combination is only added if the order of the dice is ascending, which results in no repeats of dice combinations
			if (thing[0] <= thing[1] && thing[1] <= thing[2] && thing[2] <= thing[3] && thing[3] <= thing[4])
			{

				// This converts the int[] of dice numbers to a string
				string stringThing = "";

				// This either adds the number or G as 7 to create a unique string of 5 dice characters
				for (int j = 0; j < 5; j++)
				{
					if (thing[j] == 7)
					{
						stringThing += "G";
					}
					else
					{
						stringThing += thing[j];
					}
				}

				// This adds the vertex to the next open spot in the array
				arr[count] = stringThing;

				// The count is incremented to the next open spot in the array
				count++;
			}
		}

		// The arr of unique 5 character strings is returned
		return arr;
	}

	/// <summary>
	/// This initializes the bool[] of whether dice roll combinations have already been calculated or not to all false.
	/// </summary>
	public static void InitializeDiceRollCombinations()
	{
		diceRollCombinationsAlreadyCalculated = new bool[DICE_COMBINATION_ARRAY_SIZE];
		diceRollCombinationsAveragePointValues = new float[DICE_COMBINATION_ARRAY_SIZE];
	}

	/// <summary>
	/// This gets the average point value given the dice combination. If it has not yet been calculated then -1 is returned.
	/// </summary>
	/// <param name="diceStart">The dice combination of 5 dice characters in numericalphabetical order 123456G</param>
	/// <returns>The average point value of the diceStart or -1 if it has not yet been calculated</returns>
	public static float GetFloatFromStartingCombination(string diceStart)
	{

		// This is the index to add the float value and set the calculated bool to be set to true
		int index = GetDiceCombinationIndex(diceStart);

		// If the dice combination has not been calculated yet, then -1 is returned
		if (!diceRollCombinationsAlreadyCalculated[index])
		{
			throw new System.Exception("A: The dice roll combination has not been calculated yet.");
		}

		// The float value in the averagePointValues list is returned at the calculated index
		return diceRollCombinationsAveragePointValues[index];
	}

	/// <summary>
	/// This initilizes the path chances lists based on the path probabilities file with the string PATH_DATA_FILE_NAME as a name
	/// </summary>
	static void InitializePathChances()
	{
		diceRollOptions = PathProbabilitiesIO.GetEntireIDArray(PATH_DATA_FILE_NAME);
		diceRollProbabilities = PathProbabilitiesIO.GetEntireFloatValueArray(PATH_DATA_FILE_NAME);
	}

	/// <summary>
	/// This calculates the probability of having one hand and rolling another.
	/// </summary>
	/// <param name="currentHand">This is a 5 character string consisting of 1, 2, 3, 4, 5, 6, or G to represent the die numbers or G to represent a die that has not been rolled.</param>
	/// <param name="roll">The ending hand that the dice will end in</param>
	/// <returns>The probability of starting with currentHand and ending in roll</returns>
	static float CalculateRollChance(string currentHand, string roll)
	{

		// This eliminates the common characters in both the currentHand and roll
		for (int i = 0; i < currentHand.Length; i++)
		{

			// This linearly takes a single character from current hand to compare with roll
			string commonCharacter = currentHand.Substring(i, 1);

			// This deletes the character in both strings if they are the same and not G
			if (roll.Contains(commonCharacter) && "123456".Contains(commonCharacter))
			{

				// This deletes the common character from roll
				int commonCharacterIndex = roll.IndexOf(commonCharacter);
				string tempString = roll.Substring(0, commonCharacterIndex);
				if (roll.IndexOf(commonCharacter) != roll.Length - 1)
				{
					tempString += roll.Substring(commonCharacterIndex + 1);
				}
				roll = tempString;

				// This deletes the common character from currentHand
				commonCharacterIndex = currentHand.IndexOf(commonCharacter);
				tempString = currentHand.Substring(0, commonCharacterIndex);
				if (currentHand.IndexOf(commonCharacter) != currentHand.Length - 1)
				{
					tempString += currentHand.Substring(commonCharacterIndex + 1);
				}
				currentHand = tempString;

				// This decrements the index in the loop because a character has been deleted
				i--;
			}
		}

		// If there is a number in currentHand then that means it is impossible to get roll given the currentHand
		for (int i = 0; i < currentHand.Length; i++)
		{
			if (!currentHand.Substring(i, 1).Equals("G"))
			{
				return 0;
			}
		}

		// The roll chance starts at 1
		float chance = 1;

		// There are 6^(number of dice) possible combinations of rolling the dice not counting repeats
		chance /= Mathf.Pow(6, currentHand.Length);

		// This counts the number of each occurance of a die number in roll
		string numbersCount = "";

		// This loops through each possible die number from 1 to 6
		for (int i = 1; i <= 6; i++)
		{

			// This is the number of i dice in roll
			int individualNumbers = 0;

			// The loops through each character in roll and checks to see if it matches i and if so increments individualNumbers
			for (int j = 0; j < roll.Length; j++)
			{
				if (roll.Substring(j, 1).Equals(i.ToString()))
				{
					individualNumbers++;
				}
			}

			// If individualNumbers is not 0 it is concatenated to the numbersCount string
			if (individualNumbers != 0)
			{
				numbersCount += individualNumbers.ToString();
			}
		}

		// These are the combinations of possible dice combinations where it sees if it is an anagram of each case, which tells us that the combinations are equivalent and then it multiplies the chance by the corresponding chanceMultipliers which tells us how many repeats of dice combinations in each case
		if (Anagram.IsAnagram(numbersCount, ""))
		{
			chance *= chanceMultipliers[0];
		}
		else if (Anagram.IsAnagram(numbersCount, "1"))
		{
			chance *= chanceMultipliers[1];
		}
		else if (Anagram.IsAnagram(numbersCount, "11"))
		{
			chance *= chanceMultipliers[2];
		}
		else if (Anagram.IsAnagram(numbersCount, "2"))
		{
			chance *= chanceMultipliers[3];
		}
		else if (Anagram.IsAnagram(numbersCount, "111"))
		{
			chance *= chanceMultipliers[4];
		}
		else if (Anagram.IsAnagram(numbersCount, "12"))
		{
			chance *= chanceMultipliers[5];
		}
		else if (Anagram.IsAnagram(numbersCount, "3"))
		{
			chance *= chanceMultipliers[6];
		}
		else if (Anagram.IsAnagram(numbersCount, "1111"))
		{
			chance *= chanceMultipliers[7];
		}
		else if (Anagram.IsAnagram(numbersCount, "112"))
		{
			chance *= chanceMultipliers[8];
		}
		else if (Anagram.IsAnagram(numbersCount, "22"))
		{
			chance *= chanceMultipliers[9];
		}
		else if (Anagram.IsAnagram(numbersCount, "13"))
		{
			chance *= chanceMultipliers[10];
		}
		else if (Anagram.IsAnagram(numbersCount, "4"))
		{
			chance *= chanceMultipliers[11];
		}
		else if (Anagram.IsAnagram(numbersCount, "11111"))
		{
			chance *= chanceMultipliers[12];
		}
		else if (Anagram.IsAnagram(numbersCount, "1112"))
		{
			chance *= chanceMultipliers[13];
		}
		else if (Anagram.IsAnagram(numbersCount, "122"))
		{
			chance *= chanceMultipliers[14];
		}
		else if (Anagram.IsAnagram(numbersCount, "113"))
		{
			chance *= chanceMultipliers[15];
		}
		else if (Anagram.IsAnagram(numbersCount, "14"))
		{
			chance *= chanceMultipliers[16];
		}
		else if (Anagram.IsAnagram(numbersCount, "23"))
		{
			chance *= chanceMultipliers[17];
		}
		else if (Anagram.IsAnagram(numbersCount, "5"))
		{
			chance *= chanceMultipliers[18];
		}

		// After this algorithm the chance is returned
		return chance;
	}

	/// <summary>
	/// This returns the probability of starting with the current hand and rolling roll. The currentHand and roll characters need to be in numericalphabetical order: 123456G
	/// </summary>
	/// <param name="currentHand">A string of 5 numbers 1-6 or G which represents a die that will be rolled</param>
	/// <param name="roll">The ending hand after the dice have been rolled</param>
	/// <returns>The probability of rolling roll by starting with currentHand</returns>
	public static float ChanceOfRollingHand(string currentHand, string roll)
	{

		// This handles improper input
		if (currentHand.Length != 5)
		{
			throw new System.Exception("A: The current hand does not have 5 characters: " + currentHand + ". It needs to be 5 characters.");
		}
		else if (roll.Length != 5)
		{
			throw new System.Exception("B: The roll does not have 5 characters: " + roll + ". It needs to be 5 characters.");
		}
		for (int i = 0; i < 5; i++)
		{
			if (currentHand.Length > i && !"123456G".Contains(currentHand.Substring(i, 1)))
			{
				throw new System.Exception("C: The current hand has characters other than 1, 2, 3, 4, 5, 6, or G: " + currentHand + ". It needs to only have these characters");
			}
			if (roll.Length > i && !"123456G".Contains(roll.Substring(i, 1)))
			{
				throw new System.Exception("D: The roll has characters other than 1, 2, 3, 4, 5, 6, or G: " + roll + ". It needs to only have these characters");
			}
		}

		// This initializes the path rolling chance data if it has not already been initilized
		if (diceRollOptions == null)
		{
			InitializePathChances();
		}

		// These save the original starting and ending dice hands to be used later
		string originalCurrentHand = String.Copy(currentHand);
		string originalRoll = String.Copy(roll);

		// This eliminates the common characters in both the currentHand and roll
		for (int i = 0; i < currentHand.Length; i++)
		{

			// This linearly takes a single character from current hand to compare with roll
			string commonCharacter = currentHand.Substring(i, 1);

			// This deletes the character in both strings if they are the same and not G
			if (roll.Contains(commonCharacter) && "123456".Contains(commonCharacter))
			{

				// This deletes the common character from roll
				int commonCharacterIndex = roll.IndexOf(commonCharacter);
				string tempString = roll.Substring(0, commonCharacterIndex);
				if (roll.IndexOf(commonCharacter) != roll.Length - 1)
				{
					tempString += roll.Substring(commonCharacterIndex + 1);
				}
				roll = tempString;

				// This deletes the common character from currentHand
				commonCharacterIndex = currentHand.IndexOf(commonCharacter);
				tempString = currentHand.Substring(0, commonCharacterIndex);
				if (currentHand.IndexOf(commonCharacter) != currentHand.Length - 1)
				{
					tempString += currentHand.Substring(commonCharacterIndex + 1);
				}
				currentHand = tempString;

				// This decrements the index in the loop because a character has been deleted
				i--;
			}
		}

		// If there is a number in currentHand then that means it is impossible to get roll given the currentHand
		for (int i = 0; i < currentHand.Length; i++)
		{
			if (!currentHand.Substring(i, 1).Equals("G"))
			{
				return 0;
			}
		}

		// This is the probability of starting with currentHand and rolling roll
		float probability;

		// The probability is retrieved from the path probability list, but if it is not properly formatted then an exception is thrown
		try
		{
			probability = diceRollProbabilities[FindLineIndex(originalCurrentHand + originalRoll)];
		}
		catch (Exception e)
		{
			throw new System.Exception("E: The given currentHand or roll are not in numbericalphabetical order. They are currentHand: " + originalCurrentHand + " and roll: " + originalRoll + ". They should be in the order 123456G.");
		}

		// The probability is returned
		return probability;
	}

	/// <summary>
	/// This uses binary search to find the index of the ID to be overwritten or inserted. The given array must be sorted by ID. The array can't be null or have a length of 0.
	/// </summary>
	/// <param name="ID">The ID to be used to find the elements place in the list</param>
	/// <param name="dataTable">The list to search through</param>
	/// <returns>The index where the element should be inserted before OR after based on the ID</returns>
	static int FindLineIndex(string ID)
	{

		// These are the min and max indexes of possible ID indexes
		int minIndex = 0;
		int maxIndex = diceRollOptions.Length - 1;

		// These are the ID values at the min and max indexes
		string minID = diceRollOptions[minIndex];
		string maxID = diceRollOptions[maxIndex];

		// This is the index and ID value that split the searching in half using a binary search
		int currentIndex;
		string currentID;

		// These handle if the ID is at the beginning or end of the array or if the array is length 1
		if (minID.Equals(ID) || minID.CompareTo(ID) > 0)
		{
			return 0;
		}
		else if (maxID.ToString().Equals(ID) || maxID.CompareTo(ID) < 0)
		{
			return maxIndex;
		}
		else if (minIndex == maxIndex)
		{
			return 0;
		}

		// This performs a binary search of the data based on its ID
		for (int i = 0; i < diceRollOptions.Length; i++)
		{

			// When the search is narrowed to two options it picks the correct index
			if (maxIndex - minIndex <= 1)
			{
				if (maxID.ToString().Equals(ID))
				{
					return maxIndex;
				}
				else
				{
					return minIndex;
				}
			}

			// This updates the middle index in between the min and max indexes
			currentIndex = (maxIndex + minIndex) / 2;
			currentID = diceRollOptions[currentIndex];

			// This decides whether to search the upper half or lower half of the array depending on the currentID value and the value to search for
			if (currentID.ToString().Equals(ID))
			{
				return currentIndex;
			}
			else if (currentID.CompareTo(ID) < 0)
			{
				minID = currentID;
				minIndex = currentIndex;
			}
			else
			{
				maxID = currentID;
				maxIndex = currentIndex;
			}
		}

		// This theoretically shouldn't be reached because a line should always be found, but if this executes then something was wrong with this method or array
		throw new System.Exception("A: There was a problem with the FindLineIndex method in Path.");
	}
}
