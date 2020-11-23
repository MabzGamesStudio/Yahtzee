using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{



	/*This is a Pascal triangle with height 5
	 * 
	 *           1
	 *         1   1
	 *       1   2   1
	 *     1   3   3   1
	 *   1   4   6   4   1
	 * 1   5   10  10  5   1
	 */
	readonly static int[,] pascalTriangle = { { 1, 0, 0, 0, 0, 0 } , { 1, 1, 0, 0, 0, 0 }, { 1, 2, 1, 0, 0, 0 }, { 1, 3, 3, 1, 0, 0 },
		{ 1, 4, 6, 4, 1, 0 } , { 1, 5, 10, 10, 5, 1 } };



	// 1 = 1
	// 2 = 2
	// 3 = 3
	// 4 = 4
	// 5 = 5
	// 6 = 6

	// A = !1
	// B = !2
	// C = !3
	// D = !4
	// E = !5
	// F = !6

	// G = Not rolled yet
	// H = Point value vertex


	// TODO Make this work for numbers like AAAAA --> AAA12
	public static float ChanceOfRollingHand(string currentHand, string roll)
	{
		for (int i = 0; i < currentHand.Length; i++)
		{
			string commonCharacter = currentHand.Substring(i, 1);
			if (roll.Contains(commonCharacter) && "123456".Contains(commonCharacter))
			{
				int commonCharacterIndex = roll.IndexOf(commonCharacter);
				string tempString = "";
				tempString = roll.Substring(0, commonCharacterIndex);
				if (roll.IndexOf(commonCharacter) != roll.Length - 1)
				{
					tempString += roll.Substring(commonCharacterIndex + 1);
				}
				roll = tempString;

				commonCharacterIndex = currentHand.IndexOf(commonCharacter);
				tempString = currentHand.Substring(0, commonCharacterIndex);
				if (currentHand.IndexOf(commonCharacter) != currentHand.Length - 1)
				{
					tempString += currentHand.Substring(commonCharacterIndex + 1);
				}
				currentHand = tempString;

				i--;
			}
		}

		float chance = 1;

		for (int i = 0; i < roll.Length; i++)
		{
			if ("123456".Contains(roll.Substring(i, 1)))
			{
				chance /= 6;
			}
			else if ("ABCDEF".Contains(roll.Substring(i, 1)))
			{
				chance *= 5;
				chance /= 6;
			}
			else if ("G".Contains(roll.Substring(i, 1)))
			{
				chance *= 5;
				chance /= 6;
			}
			else if ("H".Contains(roll.Substring(i, 1)))
			{
				return 1f;
			}
			else
			{
				Debug.Log("uh oh. I got " + roll.Substring(i, 1));
			}
		}

		int numberCount = 0;

		for (int i = 0; i < roll.Length; i++)
		{
			if ("123456".Contains(roll.Substring(i, 1)))
			{
				numberCount++;
			}
		}

		chance *= pascalTriangle[currentHand.Length, numberCount];

		//Debug.Log("Going from " + currentHand + " to " + roll + " is " + chance);

		return chance;
	}
}
