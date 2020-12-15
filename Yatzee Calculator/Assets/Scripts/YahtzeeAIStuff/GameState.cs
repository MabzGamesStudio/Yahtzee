using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{

	/* Order in boxesFilledIn bool array:
	 * 0.  Ones
	 * 1.  Twos
	 * 2.  Threes
	 * 3.  Fours
	 * 4.  Fives
	 * 5.  Sixes
	 * 6.  3 of a kind
	 * 7.  4 of a kind
	 * 8.  Full house
	 * 9.  Small straight
	 * 10. Large straight
	 * 11. Yahtzee
	 * 12. Chance
	 */

	/// <summary>
	/// This is the bool array that tells which of the 13 boxes have been filled in: 0. ones, 1. twos, 2. threes, 3. fours, 4. fives, 5. sixes, 6. 3 of a kind, 7. 4 of a kind, 8. full house, 9. small straight, 10. large straight, 11. yahtzee, and 12. chance (in this specific order)
	/// </summary>
	bool[] boxesFilledIn;

	/// <summary>
	/// This tells wether a joker bonus is possible (only false when yahtzee box is filled in with 0)
	/// </summary>
	bool bonusAvailable;

	/// <summary>
	/// This is the sum of the top section to see if 35 point bonus is possible (63 counts for any sum above 63)
	/// </summary>
	int topTotal;

	/// <summary>
	/// This initialzes a GameState with the given infomration
	/// </summary>
	/// <param name="boxesFilledIn">Boxes in the scorecard filled in</param>
	/// <param name="bonusAvailable">Whether a joker bonus is possible</param>
	/// <param name="topTotal">The sum of the top section</param>
	public GameState(bool[] boxesFilledIn, bool bonusAvailable, int topTotal)
	{
		if (boxesFilledIn.Length != 13)
		{
			throw new System.Exception("GameState was initialized with invalid bool[] of " + boxesFilledIn.Length + ", instead need length 13.");
		}
		if (topTotal < 0 || topTotal > 63)
		{
			throw new System.Exception("GameState was initialized with invalid topTotal of " + topTotal + ", instead need number between 0 and 63.");
		}
		this.boxesFilledIn = boxesFilledIn;
		this.bonusAvailable = bonusAvailable;
		this.topTotal = topTotal;
	}

	/// <summary>
	/// Sets boxesFilledIn
	/// </summary>
	/// <param name="boxesFilledIn">Updated boxesFilledIn</param>
	public void SetBoxesFilledIn(bool[] boxesFilledIn)
	{
		if (boxesFilledIn.Length != 13)
		{
			throw new System.Exception("SetBoxesFilledIn was updated with invalid bool[] of " + boxesFilledIn.Length + ", instead need length 13.");
		}
		this.boxesFilledIn = boxesFilledIn;
	}

	/// <summary>
	/// Sets bonus available bool
	/// </summary>
	/// <param name="bonusAvailable">Updated bonusAvailable</param>
	public void SetBonusAvailable(bool bonusAvailable)
	{
		this.bonusAvailable = bonusAvailable;
	}

	/// <summary>
	/// Sets topTotal
	/// </summary>
	/// <param name="topTotal">Updated topTotal</param>
	public void SetTopTotal(int topTotal)
	{
		if (topTotal < 0 || topTotal > 63)
		{
			throw new System.Exception("SetTopTotal was updated with invalid topTotal of " + topTotal + ", instead need number between 0 and 63.");
		}
		this.topTotal = topTotal;
	}

	/// <summary>
	/// Gets boxesFilledIn
	/// </summary>
	/// <returns>boxesFilledIn</returns>
	public bool[] GetBoxesFilledIn()
	{
		return boxesFilledIn;
	}

	/// <summary>
	/// Gets bonusAvailable
	/// </summary>
	/// <returns>bonusAvailable</returns>
	public bool GetBonusAvailable()
	{
		return bonusAvailable;
	}

	/// <summary>
	/// Gets topTotal
	/// </summary>
	/// <returns>topTotal</returns>
	public int GetTopTotal()
	{
		return topTotal;
	}

	/// <summary>
	/// This determines whether the given GameState is possible while playing Yahtzee by the rules
	/// </summary>
	/// <param name="gameState">The GameState to be assessed as possible</param>
	/// <returns>Whether the given GameState is possible</returns>
	public static bool GameStatePossible(GameState gameState)
	{

		// This throws an exception if the given GameState is null
		if (gameState == null)
		{
			throw new System.Exception("The gameState variable is null.");
		}

		// A yahtzee bonus is always possible when the yahtzee box has not been filled in yet
		if (!gameState.GetBonusAvailable() && !gameState.GetBoxesFilledIn()[11])
		{
			return false;
		}

		// Saving the boxesFilledIn variable
		bool[] boxes = gameState.GetBoxesFilledIn();

		// This initializes an list that will keep track of possible numbers of based on boxes
		List<int> boxesFilledInPossibleNumbers = new List<int>();

		// This adds 5 of each number if the box is filled in from ones to sixes
		for (int i = 5; i >= 0; i--)
		{
			if (boxes[i])
			{
				for (int j = 0; j < 5; j++)
				{
					boxesFilledInPossibleNumbers.Add(i + 1);
				}
			}
		}

		// This converts the list to an array
		int[] possibleNumbersArray = new int[boxesFilledInPossibleNumbers.Count];
		boxesFilledInPossibleNumbers.CopyTo(possibleNumbersArray);

		// This tells us that the GameState is not possible if the topTotal is not possible with the boxesFilledIn
		if (!SetCanSumToValue(possibleNumbersArray, gameState.GetTopTotal()))
		{
			return false;
		}

		// If both of these tests are passed then the GameState is possible
		return true;
	}

	/// <summary>
	/// This tells whether the given set has a subset that sums to a given value. The set must be in descending order and not include negative numbers.
	/// </summary>
	/// <param name="set">The set of numbers in descending order</param>
	/// <param name="value">The value to see if the set can sum to</param>
	/// <returns>Whether a subset of numbers in the given set can sum to the given value</returns>
	public static bool SetCanSumToValue(int[] set, int value)
	{

		// Trivial case where any set can sum to 0
		if (value == 0)
		{
			return true;
		}

		// Turning the given int[] into a list
		List<int> setList = new List<int>(set);

		// Returns the recursive helper method
		return SetCanSumToValueHelper(setList, value);
	}

	/// <summary>
	/// his tells whether the given set has a subset that sums to a given value. The set must be in descending order and not include negative numbers.
	/// </summary>
	/// <param name="set">The set of numbers in descending order</param>
	/// <param name="value">The value to see if the set can sum to</param>
	/// <returns>Whether a subset of numbers in the given set can sum to the given value</returns>
	static bool SetCanSumToValueHelper(List<int> set, int value)
	{

		// If the value exists in the list, then the value in the set itself can sum to the value
		if (set.Contains(value))
		{
			return true;
		}

		// If there are no numbers left in the set, then no subset can add to the value
		if (set.Count == 0)
		{
			return false;
		}

		// This is the sum of the numbers in the set
		int sum = 0;

		// This calculates the sum by adding each number from the set to the sum variable
		for (int i = 0; i < set.Count; i++)
		{
			sum += set[i];
		}

		// If the sum of the numbers in the set is less than the value, then there is no possibe way the numbers in the set can add to the value
		if (sum < value)
		{
			return false;
		}

		// This is the previous value in the list, in this case it 
		int lastNum = set[0] + 1;

		// This loops through each possible subset to see if the current set is possible
		for (int i = 0; i < set.Count; i++)
		{

			// A subset is only checked if it hasn't already been checked and the number is less than the value
			if (set[i] < lastNum && set[i] < value)
			{

				// This creates a subset with one less element
				List<int> subset = new List<int>(set);
				subset.RemoveAt(i);

				// If the subset can create the value minus the removed element, then the current set is possible and returns true
				if (SetCanSumToValueHelper(subset, value - set[i]))
				{
					return true;
				}
			}

			// This changes the previous number to the current number for the next loop iteration
			lastNum = set[i];
		}

		// If no subset is possible then if returns false
		return false;
	}

}
