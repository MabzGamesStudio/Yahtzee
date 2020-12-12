using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexInformation
{

	/// <summary>
	/// The average point value for a vertex.
	/// </summary>
	float averagePointValue;

	/// <summary>
	/// The number of times the dice have been rolled in a turn at a vertex.
	/// </summary>
	int rollNumber;

	/// <summary>
	/// The 5 character representation of the dice in numericalphabetical order 123456G.
	/// </summary>
	string diceFormat;

	/// <summary>
	/// This creates a new VertexInformation with the given information.
	/// </summary>
	/// <param name="averagePointValue">This is the average point value of the vertex, or -1 if it is not yet initialized</param>
	/// <param name="rollNumber">This is the number of rolls that have been made at this vertex</param>
	/// <param name="diceFormat">The 5 character representation of the dice in numericalphabetical order 123456G.</param>
	public VertexInformation(float averagePointValue, int rollNumber, string diceFormat)
	{

		// This handles improper input
		if (averagePointValue < 0 && averagePointValue != -1)
		{
			throw new System.Exception("A: The given averagePointValue is out of the accepted bounds of -1, [0, max float): " + averagePointValue + ". It needs to be -1, 0, or greater than 0.");
		}
		else if (rollNumber < -1 || rollNumber > 4)
		{
			throw new System.Exception("B: The given rollNumber is not within the bounds [-1, 4]: " + rollNumber + ".");
		}
		else if (diceFormat == null)
		{
			throw new System.Exception("C: The given diceFormat is null.");
		}
		else if (diceFormat.Length != 5)
		{
			throw new System.Exception("D: The given diceFormat does not have a length of 5, but a length of " + diceFormat.Length + ".");
		}
		for (int i = 0; i < 5; i++)
		{
			if (diceFormat.Length > i && !"123456G".Contains(diceFormat.Substring(i, 1)))
			{
				throw new System.Exception("E: The given diceFormat has characters other than 1, 2, 3, 4, 5, 6, or G: " + diceFormat + ". It needs to only have these characters");
			}
		}
		this.averagePointValue = averagePointValue;
		this.rollNumber = rollNumber;
		this.diceFormat = diceFormat;
	}

	/// <summary>
	/// This gets the average point value of a vertex.
	/// </summary>
	/// <returns>The average point value of a vertex</returns>
	public float GetAveragePointValue()
	{
		return averagePointValue;
	}

	/// <summary>
	/// This gets the roll number of a vertex.
	/// </summary>
	/// <returns>The roll number of a vertex</returns>
	public int GetRollNumber()
	{
		return rollNumber;
	}

	/// <summary>
	/// This gets the diceFormat of a vertex.
	/// </summary>
	/// <returns>The dice format of a vertex</returns>
	public string GetDiceFormat()
	{
		return diceFormat;
	}

	/// <summary>
	/// This updates the average point value to the given averagePointValue. It must be either -1, 0, or greater than 0.
	/// </summary>
	/// <param name="averagePointValue">The new averagePointValue</param>
	public void SetAveragePointValue(float averagePointValue)
	{

		// This handles improper input
		if (averagePointValue < 0 && averagePointValue != -1)
		{
			throw new System.Exception("A: The given averagePointValue is out of the accepted bounds of -1, [0, max float): " + averagePointValue + ". It needs to be -1, 0, or greater than 0.");
		}
		this.averagePointValue = averagePointValue;
	}

	/// <summary>
	/// This updates the rollNumber to the given rollNumber. It must be from 0 to 4
	/// </summary>
	/// <param name="rollNumber">The new rollNumber</param>
	public void SetRollNumber(int rollNumber)
	{

		// This handles improper input
		if (rollNumber < -1 || rollNumber > 4)
		{
			throw new System.Exception("B: The given rollNumber is not within the bounds [-1, 4]: " + rollNumber + ".");
		}
		this.rollNumber = rollNumber;
	}

	/// <summary>
	/// This updates the diceFormat with the given diceFormat. It must be 5 characters in numericalphabetical order 123456G.
	/// </summary>
	/// <param name="diceFormat">The new diceFormat</param>
	public void SetDiceFormat(string diceFormat)
	{

		// This handles improper input
		if (diceFormat == null)
		{
			throw new System.Exception("C: The given diceFormat is null.");
		}
		else if (diceFormat.Length != 5)
		{
			throw new System.Exception("D: The given diceFormat does not have a length of 5, but a length of " + diceFormat.Length + ".");
		}
		for (int i = 0; i < 5; i++)
		{
			if (diceFormat.Length > i && !"123456G".Contains(diceFormat.Substring(i, 1)))
			{
				throw new System.Exception("E: The given diceFormat has characters other than 1, 2, 3, 4, 5, 6, or G: " + diceFormat + ". It needs to only have these characters");
			}
		}
		this.diceFormat = diceFormat;
	}

	/// <summary>
	/// This returns a string of the VertexInformation information.
	/// </summary>
	/// <returns>A string of the VertexInformation information</returns>
	public override string ToString()
	{
		return "Average Point Value: " + averagePointValue + "\nRoll Number: " + rollNumber + "\nDice Format: " + diceFormat;
	}

	/// <summary>
	/// This returns a VertexInformation object that has the same information as this vertex, but is a different reference.
	/// </summary>
	/// <returns>A duplicate of this VertexInformation with the same information, but a different reference</returns>
	public VertexInformation DuplicateDataInformation()
	{
		return new VertexInformation(averagePointValue, rollNumber, diceFormat);
	}
}
