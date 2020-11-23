using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{

	/// <summary>
	/// These are the strings for the categories
	/// </summary>
	public static string ACES = "ACES";
	public static string TWOS = "TWOS";
	public static string THREES = "THREES";
	public static string FOURS = "FOURS";
	public static string FIVES = "FIVES";
	public static string SIXES = "SIXES";
	public static string THREE_OF_A_KIND = "THREE_OF_A_KIND";
	public static string FOUR_OF_A_KIND = "FOUR_OF_A_KIND";
	public static string FULL_HOUSE = "FULL_HOUSE";
	public static string SMALL_STRAIGHT = "SMALL_STRAIGHT";
	public static string LARGE_STRAIGHT = "LARGE_STRAIGHT";
	public static string YAHTZEE = "YAHTZEE";
	public static string CHANCE = "CHANCE";

	/// <summary>
	/// This is the string of the move
	/// </summary>
	string moveString;

	/// <summary>
	/// This creates a Move based on given dice to roll
	/// </summary>
	/// <param name="dieNumbers">Dice numbers to roll</param>
	public Move(int[] dieNumbers)
	{

		// This sorts the dice into numerical order
		for (int i = 0; i < dieNumbers.Length; i++)
		{
			for (int j = 0; j < dieNumbers.Length - j - 1; j++)
			{
				if (dieNumbers[i] > dieNumbers[j])
				{
					int temp = dieNumbers[i];
					dieNumbers[i] = dieNumbers[j];
					dieNumbers[j] = temp;
				}
			}
		}

		// This converts the dice integers to a string
		string numberText = "";
		for (int i = 0; i < dieNumbers.Length; i++)
		{
			numberText += dieNumbers[i].ToString();
		}
		moveString = numberText;
	}

	/// <summary>
	/// This creates a Move based on a given string that can be found in the public static Movesn strings
	/// </summary>
	/// <param name="moveString">The string value of the public static move</param>
	public Move(string moveString)
	{
		this.moveString = moveString;
	}

	/// <summary>
	/// This determines if this Move is the same a given Move
	/// </summary>
	/// <param name="move">A Move to compare this move to</param>
	/// <returns>Whether the two moves are the same</returns>
	public bool SameMove(Move move)
	{
		return moveString == move.GetMoveString();
	}

	/// <summary>
	/// This returns the string value of the Move
	/// </summary>
	/// <returns>The string value of the Move</returns>
	public string GetMoveString()
	{
		return moveString;
	}

}
