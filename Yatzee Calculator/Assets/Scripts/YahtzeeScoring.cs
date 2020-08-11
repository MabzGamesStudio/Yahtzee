using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YahtzeeScoring
{

	/// <summary>
	/// This returns the points awarded for aces given a hand of 5 dice
	/// </summary>
	/// <param name="die1">The first die</param>
	/// <param name="die2">The second die</param>
	/// <param name="die3">The third die</param>
	/// <param name="die4">The fourth die</param>
	/// <param name="die5">The fifth die</param>
	/// <returns>The points awarded for the aces category</returns>
	public static int Aces(int die1, int die2, int die3, int die4, int die5)
	{
		int points = 0;
		if (die1 == 1)
		{
			points++;
		}
		if (die2 == 1)
		{
			points++;
		}
		if (die3 == 1)
		{
			points++;
		}
		if (die4 == 1)
		{
			points++;
		}
		if (die5 == 1)
		{
			points++;
		}
		return points;
	}

	/// <summary>
	/// This returns the points awarded for twos given a hand of 5 dice
	/// </summary>
	/// <param name="die1">The first die</param>
	/// <param name="die2">The second die</param>
	/// <param name="die3">The third die</param>
	/// <param name="die4">The fourth die</param>
	/// <param name="die5">The fifth die</param>
	/// <returns>The points awarded for the twos category</returns>
	public static int Twos(int die1, int die2, int die3, int die4, int die5)
	{
		int points = 0;
		if (die1 == 2)
		{
			points += 2;
		}
		if (die2 == 2)
		{
			points += 2;
		}
		if (die3 == 2)
		{
			points += 2;
		}
		if (die4 == 2)
		{
			points += 2;
		}
		if (die5 == 2)
		{
			points += 2;
		}
		return points;
	}

	/// <summary>
	/// This returns the points awarded for threes given a hand of 5 dice
	/// </summary>
	/// <param name="die1">The first die</param>
	/// <param name="die2">The second die</param>
	/// <param name="die3">The third die</param>
	/// <param name="die4">The fourth die</param>
	/// <param name="die5">The fifth die</param>
	/// <returns>The points awarded for the threes category</returns>
	public static int Threes(int die1, int die2, int die3, int die4, int die5)
	{
		int points = 0;
		if (die1 == 3)
		{
			points += 3;
		}
		if (die2 == 3)
		{
			points += 3;
		}
		if (die3 == 3)
		{
			points += 3;
		}
		if (die4 == 3)
		{
			points += 3;
		}
		if (die5 == 3)
		{
			points += 3;
		}
		return points;
	}

	/// <summary>
	/// This returns the points awarded for fours given a hand of 5 dice
	/// </summary>
	/// <param name="die1">The first die</param>
	/// <param name="die2">The second die</param>
	/// <param name="die3">The third die</param>
	/// <param name="die4">The fourth die</param>
	/// <param name="die5">The fifth die</param>
	/// <returns>The points awarded for the fours category</returns>
	public static int Fours(int die1, int die2, int die3, int die4, int die5)
	{
		int points = 0;
		if (die1 == 4)
		{
			points += 4;
		}
		if (die2 == 4)
		{
			points += 4;
		}
		if (die3 == 4)
		{
			points += 4;
		}
		if (die4 == 4)
		{
			points += 4;
		}
		if (die5 == 4)
		{
			points += 4;
		}
		return points;
	}

	/// <summary>
	/// This returns the points awarded for fives given a hand of 5 dice
	/// </summary>
	/// <param name="die1">The first die</param>
	/// <param name="die2">The second die</param>
	/// <param name="die3">The third die</param>
	/// <param name="die4">The fourth die</param>
	/// <param name="die5">The fifth die</param>
	/// <returns>The points awarded for the fives category</returns>
	public static int Fives(int die1, int die2, int die3, int die4, int die5)
	{
		int points = 0;
		if (die1 == 5)
		{
			points += 5;
		}
		if (die2 == 5)
		{
			points += 5;
		}
		if (die3 == 5)
		{
			points += 5;
		}
		if (die4 == 5)
		{
			points += 5;
		}
		if (die5 == 5)
		{
			points += 5;
		}
		return points;
	}

	/// <summary>
	/// This returns the points awarded for sixes given a hand of 5 dice
	/// </summary>
	/// <param name="die1">The first die</param>
	/// <param name="die2">The second die</param>
	/// <param name="die3">The third die</param>
	/// <param name="die4">The fourth die</param>
	/// <param name="die5">The fifth die</param>
	/// <returns>The points awarded for the sixes category</returns>
	public static int Sixes(int die1, int die2, int die3, int die4, int die5)
	{
		int points = 0;
		if (die1 == 6)
		{
			points += 6;
		}
		if (die2 == 6)
		{
			points += 6;
		}
		if (die3 == 6)
		{
			points += 6;
		}
		if (die4 == 6)
		{
			points += 6;
		}
		if (die5 == 6)
		{
			points += 6;
		}
		return points;
	}

	/// <summary>
	/// This returns the bonus score depending on the given points in the top section
	/// </summary>
	/// <param name="acesPoints">The points scored from the aces section</param>
	/// <param name="twosPoints">The points scored from the twos section</param>
	/// <param name="threesPoints">The points scored from the threes section</param>
	/// <param name="foursPoints">The points scored from the fours section</param>
	/// <param name="fivesPoints">The points scored from the fives section</param>
	/// <param name="sixesPoints">The points scored from the sixes section</param>
	/// <returns></returns>
	public static int Bonus(int acesPoints, int twosPoints, int threesPoints, int foursPoints, int fivesPoints, int sixesPoints)
	{
		if (acesPoints + twosPoints + threesPoints + foursPoints + fivesPoints + sixesPoints >= 63)
		{
			return 35;
		}
		return 0;
	}

	/// <summary>
	/// This returns the points awarded for three of a kind given a hand of 5 dice
	/// </summary>
	/// <param name="die1">The first die</param>
	/// <param name="die2">The second die</param>
	/// <param name="die3">The third die</param>
	/// <param name="die4">The fourth die</param>
	/// <param name="die5">The fifth die</param>
	/// <returns>The points awarded for the three of a kind category</returns>
	public static int ThreeOfAKind(int die1, int die2, int die3, int die4, int die5)
	{

		// An array to hold the count of each number
		int[] dieNumbers = new int[6];

		// Counting each number for each die based on its number category
		dieNumbers[die1 - 1]++;
		dieNumbers[die2 - 1]++;
		dieNumbers[die3 - 1]++;
		dieNumbers[die4 - 1]++;
		dieNumbers[die5 - 1]++;

		// Seeing if any number category is greater than or equal to 3
		for (int i = 0; i < 6; i++)
		{
			if (dieNumbers[i] >= 3)
			{
				return die1 + die2 + die3 + die4 + die5;
			}
		}
		return 0;
	}

	/// <summary>
	/// This returns the points awarded for four of a kind given a hand of 5 dice
	/// </summary>
	/// <param name="die1">The first die</param>
	/// <param name="die2">The second die</param>
	/// <param name="die3">The third die</param>
	/// <param name="die4">The fourth die</param>
	/// <param name="die5">The fifth die</param>
	/// <returns>The points awarded for the four of a kind category</returns>
	public static int FourOfAKind(int die1, int die2, int die3, int die4, int die5)
	{

		// An array to hold the count of each number
		int[] dieNumbers = new int[6];

		// Counting each number for each die based on its number category
		dieNumbers[die1 - 1]++;
		dieNumbers[die2 - 1]++;
		dieNumbers[die3 - 1]++;
		dieNumbers[die4 - 1]++;
		dieNumbers[die5 - 1]++;

		// Seeing if any number category is greater than or equal to 4
		for (int i = 0; i < 6; i++)
		{
			if (dieNumbers[i] >= 4)
			{
				return die1 + die2 + die3 + die4 + die5;
			}
		}
		return 0;
	}

	/// <summary>
	/// This returns the points awarded for full house given a hand of 5 dice
	/// </summary>
	/// <param name="die1">The first die</param>
	/// <param name="die2">The second die</param>
	/// <param name="die3">The third die</param>
	/// <param name="die4">The fourth die</param>
	/// <param name="die5">The fifth die</param>
	/// <returns>The points awarded for the full house category</returns>
	public static int FullHouse(int die1, int die2, int die3, int die4, int die5)
	{

		// An array to hold the count of each number
		int[] dieNumbers = new int[6];

		// Bools to tell if the hand has a two of a kind and a three of a kind
		bool threeOfAKind = false;
		bool twoOfAKind = false;

		// Counting each number for each die based on its number category
		dieNumbers[die1 - 1]++;
		dieNumbers[die2 - 1]++;
		dieNumbers[die3 - 1]++;
		dieNumbers[die4 - 1]++;
		dieNumbers[die5 - 1]++;

		// Seeing there is a three of a kind and a two of a kind
		for (int i = 0; i < 6; i++)
		{
			if (dieNumbers[i] == 2)
			{
				twoOfAKind = true;
			}
			else if (dieNumbers[i] == 3)
			{
				threeOfAKind = true;
			}
		}

		// This returns the points depending on the hand
		if (twoOfAKind && threeOfAKind)
		{
			return 25;
		}
		return 0;
	}

	/// <summary>
	/// This returns the points awarded for small straight given a hand of 5 dice
	/// </summary>
	/// <param name="die1">The first die</param>
	/// <param name="die2">The second die</param>
	/// <param name="die3">The third die</param>
	/// <param name="die4">The fourth die</param>
	/// <param name="die5">The fifth die</param>
	/// <returns>The points awarded for the small straight category</returns>
	public static int SmallStraight(int die1, int die2, int die3, int die4, int die5)
	{
		int[] diceArray = new int[] { die1, die2, die3, die4, die5 };
		if (ArrayContainsValue(diceArray, 3) && ArrayContainsValue(diceArray, 4) && ((ArrayContainsValue(diceArray, 1) && ArrayContainsValue(diceArray, 2)) || (ArrayContainsValue(diceArray, 2) && ArrayContainsValue(diceArray, 5)) || (ArrayContainsValue(diceArray, 5) && ArrayContainsValue(diceArray, 6))))
		{
			return 30;
		}
		return 0;
	}

	/// <summary>
	/// This returns the points awarded for large straight given a hand of 5 dice
	/// </summary>
	/// <param name="die1">The first die</param>
	/// <param name="die2">The second die</param>
	/// <param name="die3">The third die</param>
	/// <param name="die4">The fourth die</param>
	/// <param name="die5">The fifth die</param>
	/// <returns>The points awarded for the large straight category</returns>
	public static int LargeStraight(int die1, int die2, int die3, int die4, int die5)
	{
		int[] diceArray = new int[] { die1, die2, die3, die4, die5 };
		if (ArrayContainsValue(diceArray, 2) && ArrayContainsValue(diceArray, 3) && ArrayContainsValue(diceArray, 4) && ArrayContainsValue(diceArray, 5) && (ArrayContainsValue(diceArray, 1) || ArrayContainsValue(diceArray, 6)))
		{
			return 40;
		}
		return 0;
	}

	/// <summary>
	/// This returns whether a given int array contains at least one instance of the given value
	/// </summary>
	/// <param name="array">The array to be searched</param>
	/// <param name="value">The value to be searched for</param>
	/// <returns>Whether a given int array contains at least one instance of the given value</returns>
	static bool ArrayContainsValue(int[] array, int value)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == value)
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// This returns the points awarded for yahtzee given a hand of 5 dice
	/// </summary>
	/// <param name="die1">The first die</param>
	/// <param name="die2">The second die</param>
	/// <param name="die3">The third die</param>
	/// <param name="die4">The fourth die</param>
	/// <param name="die5">The fifth die</param>
	/// <returns>The points awarded for the yahzee category</returns>
	public static int Yahtzee(int die1, int die2, int die3, int die4, int die5)
	{

		// An array to hold the count of each number
		int[] dieNumbers = new int[6];

		// Counting each number for each die based on its number category
		dieNumbers[die1 - 1]++;
		dieNumbers[die2 - 1]++;
		dieNumbers[die3 - 1]++;
		dieNumbers[die4 - 1]++;
		dieNumbers[die5 - 1]++;

		// Seeing if any number category is equal to 5
		for (int i = 0; i < 6; i++)
		{
			if (dieNumbers[i] == 5)
			{
				return 50;
			}
		}
		return 0;
	}

	/// <summary>
	/// This returns the points awarded for yahtzee bonus given a hand of 5 dice and the current yahtzee points
	/// </summary>
	/// <param name="die1">The first die</param>
	/// <param name="die2">The second die</param>
	/// <param name="die3">The third die</param>
	/// <param name="die4">The fourth die</param>
	/// <param name="die5">The fifth die</param>
	/// <param name="currentYahtzeePoints">The current points in the yahtzee category</param>
	/// <returns>The points awarded for the yahzee bonus category</returns>
	public static int YahtzeeBonus(int die1, int die2, int die3, int die4, int die5, int currentYahtzeeBonus, int currentYahtzee)
	{

		// There is no Yahtzee Bonus unless one Yahtzee is already filled in with 50 points
		if (currentYahtzee != 50)
		{
			return 0;
		}

		// An array to hold the count of each number
		int[] dieNumbers = new int[6];

		// Counting each number for each die based on its number category
		dieNumbers[die1 - 1]++;
		dieNumbers[die2 - 1]++;
		dieNumbers[die3 - 1]++;
		dieNumbers[die4 - 1]++;
		dieNumbers[die5 - 1]++;

		// Seeing if any number category is equal to 5
		for (int i = 0; i < 6; i++)
		{
			if (dieNumbers[i] == 5)
			{
				return currentYahtzeeBonus += 100;
			}
		}
		return currentYahtzeeBonus;
	}

	/// <summary>
	/// This returns the points awarded for chance given a hand of 5 dice
	/// </summary>
	/// <param name="die1">The first die</param>
	/// <param name="die2">The second die</param>
	/// <param name="die3">The third die</param>
	/// <param name="die4">The fourth die</param>
	/// <param name="die5">The fifth die</param>
	/// <returns>The points awarded for the chance category</returns>
	public static int Chance(int die1, int die2, int die3, int die4, int die5)
	{
		return die1 + die2 + die3 + die4 + die5;
	}

	/// <summary>
	/// This returns the total of the top section of the scorecard
	/// </summary>
	/// <param name="acesPoints">The points in the aces category</param>
	/// <param name="twosPoints">The points in the twos category</param>
	/// <param name="threesPoints">The points in the threes category</param>
	/// <param name="foursPoints">The points in the fours category</param>
	/// <param name="fivesPoints">The points in the fives category</param>
	/// <param name="sixesPoints">The points in the sixes category</param>
	/// <param name="bonusPoints">The points in the bonus category</param>
	/// <returns>The total of the top section of the scorecard</returns>
	public static int TopTotal(int acesPoints, int twosPoints, int threesPoints, int foursPoints, int fivesPoints, int sixesPoints, int bonusPoints)
	{
		return acesPoints + twosPoints + threesPoints + foursPoints + fivesPoints + sixesPoints + bonusPoints;
	}

	/// <summary>
	/// This returns the total of the bottom section of the scorecard
	/// </summary>
	/// <param name="threeOfAKindPoints">The points in the three of a kind category</param>
	/// <param name="fourOfAKindPoints">The points in the four of a kind category</param>
	/// <param name="fullHousePoints">The points in the full house category</param>
	/// <param name="smallStraightPoints">The points in the small straight category</param>
	/// <param name="largeStraightPoints">The points in the large straight category</param>
	/// <param name="yahtzeePoints">The points in the yahtzee category</param>
	/// /// <param name="yahtzeeBonusPoints">The points in the yahtzee bonus category</param>
	/// <param name="chancePoints">The points in the chance category</param>
	/// <returns>The total of the bottom section of the scorecard</returns>
	public static int BottomTotal(int threeOfAKindPoints, int fourOfAKindPoints, int fullHousePoints, int smallStraightPoints, int largeStraightPoints, int yahtzeePoints, int yahtzeeBonusPoints, int chancePoints)
	{
		return threeOfAKindPoints + fourOfAKindPoints + fullHousePoints + smallStraightPoints + largeStraightPoints + yahtzeePoints + yahtzeeBonusPoints + chancePoints;
	}

	/// <summary>
	/// This returns the grand total of the scorecard
	/// </summary>
	/// <param name="topTotal">The top section total points</param>
	/// <param name="bottomTotal">The bottom section total points</param>
	/// <returns>The grand total of the scorecard</returns>
	public static int GrandTotal(int topTotal, int bottomTotal)
	{
		return topTotal + bottomTotal;
	}

	/// <summary>
	/// This returns the grand total of the scorecard
	/// </summary>
	/// <param name="acesPoints">The points in the aces category</param>
	/// <param name="twosPoints">The points in the twos category</param>
	/// <param name="threesPoints">The points in the threes category</param>
	/// <param name="foursPoints">The points in the fours category</param>
	/// <param name="fivesPoints">The points in the fives category</param>
	/// <param name="sixesPoints">The points in the sixes category</param>
	/// <param name="bonusPoints">The points in the bonus category</param>
	/// <param name="threeOfAKindPoints">The points in the three of a kind category</param>
	/// <param name="fourOfAKindPoints">The points in the four of a kind category</param>
	/// <param name="fullHousePoints">The points in the full house category</param>
	/// <param name="smallStraightPoints">The points in the small straight category</param>
	/// <param name="largeStraightPoints">The points in the large straight category</param>
	/// <param name="yahtzeePoints">The points in the yahtzee category</param>
	/// <param name="chancePoints">The points in the chance category</param>
	/// <returns>The grand total of the scorecard</returns>
	public static int GrandTotal(int acesPoints, int twosPoints, int threesPoints, int foursPoints, int fivesPoints, int sixesPoints, int bonusPoints, int threeOfAKindPoints, int fourOfAKindPoints, int fullHousePoints, int smallStraightPoints, int largeStraightPoints, int yahtzeePoints, int chancePoints)
	{
		return acesPoints + twosPoints + threesPoints + foursPoints + fivesPoints + sixesPoints + bonusPoints + threeOfAKindPoints + fourOfAKindPoints + fullHousePoints + smallStraightPoints + largeStraightPoints + yahtzeePoints + chancePoints;
	}
}
