using System.Collections.Generic;
using UnityEngine;

public class GameStateValueGenerator : MonoBehaviour
{

	/// <summary>
	/// This is used to time how long it takes to generate the GameState values
	/// </summary>
	float timer;

	static bool[,] filledInBoxesCombinations = GetFilledInBoxesCombinations();

	/// <summary>
	/// This is the name of the file that the GameState values are recorded in
	/// </summary>
	static string yahtzeeFileName = "YahtzeeTableNormalFloat";

	/// <summary>
	/// This is the iteration that the GameState value generation starts at
	/// </summary>
	static int start = 1;

	/// <summary>
	/// This is the iteration that the GameState value generation ends at. This should not exceed 2^13 - 1, which is 8191.
	/// </summary>
	static int end = 1;

	/// <summary>
	/// When the game starts it starts generating GameStateValues.
	/// </summary>
	private void Start()
	{

		// This starts the timer to measure the time it takes to generate GameState values
		timer = Time.realtimeSinceStartup;

		// This calculates the GameState values and adds them to the yahtzeeFileName data table
		GenerateGameStateValues(start, end);

		// This calculates the GameState values for the base cases and adds them to the yahtzeeFileName data table
		//GenerateBaseCaseGameStates();

		// This checks the GameState values in the yahtzeeFileName data table to see if they exists
		//CheckGameStateCases(start, end);

		// This outputs how long it took to complete the above function
		Debug.Log(Time.realtimeSinceStartup - timer);
	}

	/// <summary>
	/// This checks all iterations from iterationStart to iterationStop to see if each GameState has been calculated and exists in the yahtzee data table
	/// </summary>
	/// <param name="iterationStart">The index to start the checking for GameState values</param>
	/// <param name="iterationStop">The index to stop the checking for GameState values</param>
	static void CheckGameStateCases(int iterationStart, int iterationStop)
	{

		// This generation GameState values from iteration index iteration to iterationStop
		for (int i = iterationStart; i < iterationStop + 1; i++)
		{

			// This is the unique bool[] generated from i
			bool[] filledInBoxes = GenerateFilledInArray(i);

			// This generates all of the GameState values with all 64 top section totals and both yahtzee bonus true and false
			for (int j = 0; j < 64; j++)
			{

				// This is the GameState with filledInBoxes, no yahtzeeBonus, and j top total
				GameState gameState1 = new GameState(filledInBoxes, false, j);

				// This is the ID of the gameState1 GameState
				string gameStateID1 = YahtzeeDataIO.ConvertGameStateToID(gameState1);

				// The GameState value of this GameState is only checked if this GameState is possible
				if (GameState.GameStatePossible(gameState1))
				{
					// This checks to see if the GameState exists in the list and outputs a statement if it does not exist
					try
					{
						YahtzeeDataIO.GetFromDictionary(gameStateID1, yahtzeeFileName);
					}
					catch (System.Exception e)
					{
						Debug.Log("The GameState in iteration " + i + ", top total " + j + ", and no bonus was not found, ID: " + gameStateID1 + ".");
					}
				}

				// This is the GameState with filledInBoxes, a yahtzeeBonus, and j top total
				GameState gameState2 = new GameState(filledInBoxes, true, j);

				// This is the ID of the gameState2 GameState
				string gameStateID2 = YahtzeeDataIO.ConvertGameStateToID(gameState2);

				// The GameState value of this GameState is only checked if this GameState is possible
				if (GameState.GameStatePossible(gameState2))
				{
					// This checks to see if the GameState exists in the list and outputs a statement if it does not exist
					try
					{
						YahtzeeDataIO.GetFromDictionary(gameStateID2, yahtzeeFileName);
					}
					catch (System.Exception e)
					{
						Debug.Log("The GameState in iteration " + i + ", top total " + j + ", and a bonus was not found, ID: " + gameStateID2 + ".");
					}
				}
			}
		}

		// This displays which iterations have been checked
		Debug.Log("The GameState checks are complete.");
	}

	/// <summary>
	/// This generates the base cases in the GameStates which have an average point value of 0
	/// </summary>
	static void GenerateBaseCaseGameStates()
	{

		// This is the base case bool[] of all boxes filled in as true
		bool[] filledInBoxes = new bool[13];
		for (int i = 0; i < 13; i++)
		{
			filledInBoxes[i] = true;
		}

		// This generates all of the GameState values with all 64 top section totals and both yahtzee bonus true and false
		for (int j = 0; j < 64; j++)
		{

			// This is the GameState with filledInBoxes, no yahtzeeBonus, and j top total
			GameState gameState1 = new GameState(filledInBoxes, false, j);

			// This is the ID of the gameState1 GameState
			string gameStateID1 = YahtzeeDataIO.ConvertGameStateToID(gameState1);

			// This is the averagePointValue of gameState1
			float gameStateAveragePointValue1 = 0;

			// This adds the calculated averagePointValue of gameState1 to the yahtzee data
			YahtzeeDataIO.AddToDictionary(gameStateID1, gameStateAveragePointValue1.ToString(), yahtzeeFileName);

			// This is the GameState with filledInBoxes, a yahtzeeBonus, and j top total
			GameState gameState2 = new GameState(filledInBoxes, true, j);

			// This is the ID of the gameState2 GameState
			string gameStateID2 = YahtzeeDataIO.ConvertGameStateToID(gameState2);

			// This is the averagePointValue of gameState2
			float gameStateAveragePointValue2 = 0;

			// This adds the calculated averagePointValue of gameState2 to the yahtzee data
			YahtzeeDataIO.AddToDictionary(gameStateID2, gameStateAveragePointValue2.ToString(), yahtzeeFileName);
		}

		// This displays that the base cases were generated
		Debug.Log("The base cases have been generated");
	}

	/// <summary>
	/// This generates GameState Values starting at iteration iteration and ending at iteration iterationStop.
	/// </summary>
	/// <param name="iteration">The starting index of iteration</param>
	/// <param name="iterationStop">The ending index of iteration</param>
	static void GenerateGameStateValues(int iteration, int iterationStop)
	{

		// This generation GameState values from iteration index iteration to iterationStop
		for (int i = iteration; i < iterationStop + 1; i++)
		{

			// This is the unique bool[] generated from i
			bool[] filledInBoxes = GenerateFilledInArray(i);

			List<string> IDList = new List<string>();
			List<string> valuesList = new List<string>();

			// This generates all of the GameState values with all 64 top section totals and both yahtzee bonus true and false
			for (int j = 0; j < 64; j++)
			{

				// This is the GameState with filledInBoxes, no yahtzeeBonus, and j top total
				GameState gameState1 = new GameState(filledInBoxes, false, j);

				// The GameState value of this GameState is only generated if this GameState is possible
				if (GameState.GameStatePossible(gameState1))
				{

					// This is the ID of the gameState1 GameState
					string gameStateID1 = YahtzeeDataIO.ConvertGameStateToID(gameState1);

					// This is the YahtzeeDirectedGraph based on gameState1 GameState
					YahtzeeDirectedGraph graph1 = YahtzeeDirectedGraph.CompleteRollGraph(gameState1);

					// This is the averagePointValue of gameState1
					float gameStateAveragePointValue1 = graph1.GetStart().GetData().GetAveragePointValue();

					// This adds the ID and value to their lists
					IDList.Add(gameStateID1);
					valuesList.Add(gameStateAveragePointValue1.ToString());
				}

				// This is the GameState with filledInBoxes, a yahtzeeBonus, and j top total
				GameState gameState2 = new GameState(filledInBoxes, true, j);

				// The GameState value of this GameState is only generated if this GameState is possible
				if (GameState.GameStatePossible(gameState2))
				{

					// This is the ID of the gameState2 GameState
					string gameStateID2 = YahtzeeDataIO.ConvertGameStateToID(gameState2);

					// This is the YahtzeeDirectedGraph based on gameState2 GameState
					YahtzeeDirectedGraph graph2 = YahtzeeDirectedGraph.CompleteRollGraph(gameState2);

					// This is the averagePointValue of gameState2
					float gameStateAveragePointValue2 = graph2.GetStart().GetData().GetAveragePointValue();

					// This adds the ID and value to their lists
					IDList.Add(gameStateID2);
					valuesList.Add(gameStateAveragePointValue2.ToString());
				}
			}

			// The lists IDs and values are added to the yahtzee data
			YahtzeeDataIO.AddToDictionary(IDList, valuesList, yahtzeeFileName);
		}

		// This displays which iterations have been created
		Debug.Log("From " + start + " to " + end + " (inclusive).");
	}

	/// <summary>
	/// This gets the bool[] from the filledInBoxesCombinations bool[,] that corresponds to the iterationNumber row
	/// </summary>
	/// <param name="iterationNumber">The number that uniques generates a bool[] in a specific order</param>
	/// <returns>The array of filled in boxes based on the unique iterationNumber identifier and specific order</returns>
	static bool[] GenerateFilledInArray(int iterationNumber)
	{

		// This is the filledInBoxes array
		bool[] filledInBoxes = new bool[13];

		// This takes the iterationNumber row from filledInBoxesCombinations and writes it to the filledInBoxes
		for (int i = 0; i < 13; i++)
		{
			filledInBoxes[i] = filledInBoxesCombinations[iterationNumber, i];
		}

		// The filledInBoxes array is returned
		return filledInBoxes;
	}

	/// <summary>
	/// This generates a bool[,] where each row is a unique combination of filledInBoxes with a row order from the most true values to the least true values
	/// </summary>
	/// <returns>The ordered bool[,] of filledInBoxes</returns>
	static bool[,] GetFilledInBoxesCombinations()
	{

		// This is the filledInBoxes 2D array of the 8192 combinations of 13 filledInBoxes bool elements
		filledInBoxesCombinations = new bool[8192, 13];

		// This sets each row of the filledInBoxesCombinations with a decreasing binary number starting at 1111111111111
		for (int i = 0; i < 8192; i++)
		{
			if (i / 1 % 2 == 0)
			{
				filledInBoxesCombinations[i, 12] = true;
			}
			if (i / 2 % 2 == 0)
			{
				filledInBoxesCombinations[i, 11] = true;
			}
			if (i / 4 % 2 == 0)
			{
				filledInBoxesCombinations[i, 10] = true;
			}
			if (i / 8 % 2 == 0)
			{
				filledInBoxesCombinations[i, 9] = true;
			}
			if (i / 16 % 2 == 0)
			{
				filledInBoxesCombinations[i, 8] = true;
			}
			if (i / 32 % 2 == 0)
			{
				filledInBoxesCombinations[i, 7] = true;
			}
			if (i / 64 % 2 == 0)
			{
				filledInBoxesCombinations[i, 6] = true;
			}
			if (i / 128 % 2 == 0)
			{
				filledInBoxesCombinations[i, 5] = true;
			}
			if (i / 256 % 2 == 0)
			{
				filledInBoxesCombinations[i, 4] = true;
			}
			if (i / 512 % 2 == 0)
			{
				filledInBoxesCombinations[i, 3] = true;
			}
			if (i / 1024 % 2 == 0)
			{
				filledInBoxesCombinations[i, 2] = true;
			}
			if (i / 2048 % 2 == 0)
			{
				filledInBoxesCombinations[i, 1] = true;
			}
			if (i / 4096 % 2 == 0)
			{
				filledInBoxesCombinations[i, 0] = true;
			}
		}

		// This uses insertion sort since the generated list is hopefully close the the desired ordered list
		for (int i = 0; i < 8192; i++)
		{

			// This tells if the current element still needs to be moved list
			bool elementGreater = true;

			// This is the index of the iterator
			int j = i;

			// This moves the element at index j left until it is ordered
			while (j > 0 && elementGreater)
			{

				// If the element to the left of element j is greater than this element, then swap the elements
				if (GreaterThan(j - 1, j))
				{
					//TODO: this never executess
					Swap(j - 1, j);
				}

				// Otherwise the elements in the i subset are ordered
				else
				{
					elementGreater = false;
				}

				// The j iterator is decremented to move the iterator left to compare to the next left element
				j--;
			}
		}

		// After the array is sorted in to the specific order it is returned
		return filledInBoxesCombinations;
	}

	/// <summary>
	/// This tells whether the binary number at index1 in filledInBoxesCombinations is "greater" than at index2, where greater means more digits of 0, and if they have the same number of 0s, then the greater value is greater
	/// </summary>
	/// <param name="index1">The index of the binary number at index1 filledInBoxesCombinations</param>
	/// <param name="index2">The index of the binary number at index2 filledInBoxesCombinations</param>
	/// <returns></returns>
	static bool GreaterThan(int index1, int index2)
	{

		// This counts the number of 0s in the binary number at index index1
		int counter1 = 0;

		// This goes through each digit and increments count1 if the digit is a 0
		for (int i = 0; i < 13; i++)
		{
			if (!filledInBoxesCombinations[index1, i])
			{
				counter1++;
			}
		}

		// This counts the number of 0s in the binary number at index index1
		int counter2 = 0;

		// This goes through each digit and increments count2 if the digit is a 0
		for (int i = 0; i < 13; i++)
		{
			if (!filledInBoxesCombinations[index2, i])
			{
				counter2++;
			}
		}

		// If the first number has less zeros then it is returned as a less number
		if (counter1 < counter2)
		{
			return false;
		}

		// If the second number has less zeros then it is returned as a greater number
		else if (counter2 < counter1)
		{
			return true;
		}

		// This false if the first number is less, or returns true if the first number is greater. This loop goes through each digit and compares them to decide if one number is greater than another
		for (int i = 0; i < 13; i++)
		{

			// If the same digit on both numbers are different, then one number is greater than another
			if (filledInBoxesCombinations[index1, i] != filledInBoxesCombinations[index2, i])
			{

				// If the first number is 1, then the other number is 0, so the first number is less than the other number
				if (filledInBoxesCombinations[index1, i])
				{
					return false;
				}

				// Otherwise the first number is greater so true is returned
				else
				{
					return true;
				}
			}
		}

		// If the numbers are the same then the first number is not greater
		return false;
	}

	/// <summary>
	/// This switches the values of filledInBoxesCombinations at indexes index1 and index2
	/// </summary>
	/// <param name="index1">The first index in filledInBoxesCombinations to be swapped</param>
	/// <param name="index2">The second index in filledInBoxesCombinations to be swapped</param>
	static void Swap(int index1, int index2)
	{

		// This temporarily saves the data in one binary digit
		bool temp;

		// This swapps the value at each digit in filledInBoxesCombinations at the swapping indexes
		for (int i = 0; i < 13; i++)
		{

			// The data in one digit is saved, overwritten by the second number's data, then the other digit is overwritten with the temp data
			temp = filledInBoxesCombinations[index1, i];
			filledInBoxesCombinations[index1, i] = filledInBoxesCombinations[index2, i];
			filledInBoxesCombinations[index2, i] = temp;
		}
	}

}
