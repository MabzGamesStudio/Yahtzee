using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class YahtzeeDataIO
{

	/// <summary>
	/// This is the name of the most recently accessed yahtzee data file
	/// </summary>
	static string recentFileName;

	/// <summary>
	/// This is the data within the most recently accessed yahtzee data file
	/// </summary>
	static string[] recentYahtzeeData;

	/// <summary>
	/// This adds the given value to the index ID in the fileName file. If the file does not exist then a new file is created. If a value is overwritten then the debugger will tell you it has been overwritten and it tells you the old line.
	/// </summary>
	/// <param name="ID">The ID to index the value</param>
	/// <param name="value">The value to be stored at the index</param>
	/// <param name="fileName">The fileName in the Data folder where the dictionary is being updated</param>
	public static void AddToDictionary(string ID, string value, string fileName)
	{

		// The file path for the data to be edited
		string filePath = "Assets/Data/" + fileName + ".txt";

		string dataToAdd = ID + " " + value;

		// This creates the file if the file does not already exist
		if (!System.IO.File.Exists(filePath))
		{
			Debug.Log("A new file " + fileName + ".txt has been added to the data folder");
			System.IO.File.Create(filePath);
		}

		// The whole data set of the file path
		string[] yahtzeeData = System.IO.File.ReadAllLines(filePath);

		// If the text file is empty then it only adds the given ID and value
		if (yahtzeeData.Length == 0)
		{
			System.IO.File.WriteAllText(filePath, ID + " " + value);
			return;
		}

		// This is the index in the array where the line will be edited
		int indexToInsert = FindLineIndex(ID, yahtzeeData);

		// If the ID less than the ID in the first line, then it will be inserted in line 0 before the data
		if (int.Parse(ID) < int.Parse(GetElementID(yahtzeeData[0])))
		{
			yahtzeeData[0] = dataToAdd + "\n" + yahtzeeData[0];
		}

		// If the element already exists, then it will be overwritten and display a message in the console
		else if (ID.Equals(GetElementID(yahtzeeData[indexToInsert])))
		{
			string oldData = yahtzeeData[indexToInsert];
			yahtzeeData[indexToInsert] = dataToAdd;
			Debug.Log("In the file Assets/Data/" + fileName + ".txt on line " + (indexToInsert + 1) + " has been updated from:\n" + oldData + "\n to:\n" + dataToAdd);
		}

		// In any other scenario the data will be concatinated to the end of the line
		else
		{
			yahtzeeData[indexToInsert] += "\n" + dataToAdd;
		}

		// This overwrites the entire yahtzee data with the new added data
		System.IO.File.WriteAllLines(filePath, yahtzeeData);
	}

	/// <summary>
	/// This adds the given value to the index ID in the fileName file. If the file does not exist then a new file is created. If a value is overwritten then the debugger will tell you it has been overwritten and it tells you the old line.
	/// </summary>
	/// <param name="IDs">The ordered list of IDs to index the value</param>
	/// <param name="values">The ordered list of values to be stored at the index</param>
	/// <param name="fileName">The fileName in the Data folder where the dictionary is being updated</param>
	public static void AddToDictionary(List<string> IDs, List<string> values, string fileName)
	{

		// The file path for the data to be edited
		string filePath = "Assets/Data/" + fileName + ".txt";

		// This creates the file if the file does not already exist
		if (!System.IO.File.Exists(filePath))
		{
			Debug.Log("A new file " + fileName + ".txt has been added to the data folder");
			System.IO.File.Create(filePath);
		}

		// The whole data set of the file path
		List<string> yahtzeeData = new List<string>(System.IO.File.ReadAllLines(filePath));

		// If the text file is empty then it only adds the given IDs and values
		if (yahtzeeData.Count == 0)
		{

			// This loops through each element in IDs and adds the IDs and values in order
			string allData = "";
			for (int i = 0; i < IDs.Count; i++)
			{
				allData += IDs[i] + " " + values[i] + "\n";

			}
			System.IO.File.WriteAllText(filePath, allData);
			return;
		}

		// This loops through each element in the list of values and IDs and adds
		for (int i = 0; i < IDs.Count; i++)
		{

			// This is the line of data to be added
			string dataToAdd = IDs[i] + " " + values[i];

			// This is the index in the array where the line will be edited
			int indexToInsert = FindLineIndex(IDs[i], yahtzeeData.ToArray());

			// If the ID less than the ID in the first line, then it will be inserted in line 0 before the data
			if (int.Parse(IDs[i]) < int.Parse(GetElementID(yahtzeeData[0])))
			{
				yahtzeeData.Insert(0, dataToAdd);
			}

			// If the element already exists, then it will be overwritten and display a message in the console
			else if (IDs[i].Equals(GetElementID(yahtzeeData[indexToInsert])))
			{
				string oldData = yahtzeeData[indexToInsert];
				yahtzeeData[indexToInsert] = dataToAdd;
				Debug.Log("In the file Assets/Data/" + fileName + ".txt on line " + (indexToInsert + 1) + " has been updated from:\n" + oldData + "\n to:\n" + dataToAdd);
			}

			// In any other scenario the data will be concatinated to the end of the line
			else
			{
				yahtzeeData.Insert(indexToInsert + 1, dataToAdd);
			}
		}

		// This overwrites the entire yahtzee data with the new added data
		System.IO.File.WriteAllLines(filePath, yahtzeeData);
	}

	/// <summary>
	/// This adds the given value to the index ID in the fileName file. If the file does not exist then a new file is created. If a value is overwritten then the debugger will tell you it has been overwritten and it tells you the old line.
	/// </summary>
	/// <param name="ID">The ID to index the value</param>
	/// <param name="value">The value to be stored at the index</param>
	/// <param name="fileName">The fileName in the Data folder where the dictionary is being updated</param>
	public static string GetFromDictionary(string ID, string fileName)
	{

		// The file path for the data to be edited
		string filePath = "Assets/Data/" + fileName + ".txt";

		// If the file does not exist, then it throws this error
		if (!System.IO.File.Exists(filePath))
		{
			throw new System.Exception("A: The file: " + filePath + " does not exist.");
		}

		// The whole data set of the file path
		string[] yahtzeeData;

		// If the file has just been accessed, then the yahtzee data is equal to an already initialized yahtzee data
		if (recentFileName != null && recentFileName.Equals(fileName))
		{
			yahtzeeData = recentYahtzeeData;
		}

		// If the yahtzee data has not been recently initialiezd, then the recentYahtzeeData is initialized to this yahtzeeData and the recentFileName is set to the fileName of this accessed file
		else
		{
			yahtzeeData = System.IO.File.ReadAllLines(filePath);
			recentYahtzeeData = yahtzeeData;
			recentFileName = fileName;
		}


		// If the text file is empty, then it throws this error
		if (yahtzeeData.Length == 0)
		{
			throw new System.Exception("B: The file is empty, could not get data.");
		}

		// This is the index in the array where the line will be edited
		int indexToInsert = FindLineIndex(ID, yahtzeeData);

		// If the ID is not found in the file then an exception is thrown
		if (!GetElementID(yahtzeeData[indexToInsert]).Equals(ID))
		{
			throw new System.Exception("C: The data with the ID " + ID + " does not exist in the file " + fileName);
		}

		// The data at the line with the ID is returned
		return GetElementValue(yahtzeeData[indexToInsert]);
	}

	/// <summary>
	/// This returns the string ID of the given GameState
	/// </summary>
	/// <param name="gameState">Game state to find ID</param>
	/// <returns>ID of the given GameState</returns>
	public static string ConvertGameStateToID(GameState gameState)
	{

		// This loops through the boxesFilledIn array and converts the bool[] to a binary string
		string binaryID = "";
		foreach (bool boxFilledIn in gameState.GetBoxesFilledIn())
		{
			if (boxFilledIn)
			{
				binaryID += "1";
			}
			else
			{
				binaryID += "0";
			}
		}

		// This concatinates the binary value of the bonusAvailableVariable
		if (gameState.GetBonusAvailable())
		{
			binaryID += "1";
		}
		else
		{
			binaryID += "0";
		}

		// This is the binary representation of the top total of length 7
		string topTotalBinary = "";

		// This concatinates the binary topTotal 
		int integerTopTotal = gameState.GetTopTotal();

		// This adds digits to the topTotalBinary
		while (integerTopTotal != 0)
		{
			topTotalBinary = (integerTopTotal % 2) + topTotalBinary;
			integerTopTotal /= 2;
		}

		// If the topTotalBinary is less than 7 digits, then 0s are concatinated to the front to keep all topTotal numbers 7 digits
		topTotalBinary = "000000".Substring(0, 6 - topTotalBinary.Length) + topTotalBinary;

		// The topTotalBinary is concatinated to the binary ID
		binaryID += topTotalBinary;

		// This converts the binary string to a base 10 int
		int integerID = 0;
		int multiplier = 1;
		for (int i = binaryID.Length - 1; i >= 0; i--)
		{
			integerID += multiplier * int.Parse(binaryID.Substring(i, 1));
			multiplier *= 2;
		}

		// This returns the int ID as a string
		return integerID.ToString();
	}

	/// <summary>
	/// This takes the given line of data and returns the ID in the dateLine
	/// </summary>
	/// <param name="dataLine">The line to find the ID</param>
	/// <returns>The ID in the given line</returns>
	static string GetElementID(string dataLine)
	{
		if (dataLine.IndexOf(" ") == -1)
		{
			throw new System.Exception("Line is not properly formatted: " + dataLine + "\nIt should be number + space + float");
		}

		try
		{
			int.Parse(dataLine.Substring(0, dataLine.IndexOf(" ")));
		}
		catch
		{
			throw new System.Exception("Line is not properly formatted: " + dataLine + "\nIt should be number + space + float");
		}


		return dataLine.Substring(0, dataLine.IndexOf(" "));
	}

	/// <summary>
	/// This takes the given line of data and returns the value in the dateLine
	/// </summary>
	/// <param name="dataLine">The line to find the value</param>
	/// <returns>The value in the given line</returns>
	static string GetElementValue(string dataLine)
	{
		if (dataLine.IndexOf(" ") == -1)
		{
			throw new System.Exception("Line is not properly formatted: " + dataLine + "\nIt should be number + space + float");
		}

		try
		{
			int.Parse(dataLine.Substring(0, dataLine.IndexOf(" ")));
		}
		catch
		{
			throw new System.Exception("Line is not properly formatted: " + dataLine + "\nIt should be number + space + float");
		}


		return dataLine.Substring(dataLine.IndexOf(" ") + 1);
	}

	/// <summary>
	/// This uses binary search to find the index of the ID to be overwritten or inserted. The given array must be sorted by ID. The array can't be null or have a length of 0.
	/// </summary>
	/// <param name="ID">The ID to be used to find the elements place in the list</param>
	/// <param name="dataTable">The list to search through</param>
	/// <returns>The index where the element should be inserted before OR after based on the ID</returns>
	static int FindLineIndex(string ID, string[] dataTable)
	{

		// These are the min and max indexes of possible ID indexes
		int minIndex = 0;
		int maxIndex = dataTable.Length - 1;

		// These are the ID values at the min and max indexes
		int minID = int.Parse(GetElementID(dataTable[minIndex]));
		int maxID = int.Parse(GetElementID(dataTable[maxIndex]));

		// This is the index and ID value that split the searching in half using a binary search
		int currentIndex;
		int currentID;

		// These handle if the ID is at the beginning or end of the array or if the array is length 1
		if (minID.ToString().Equals(ID) || minID > int.Parse(ID))
		{
			return 0;
		}
		else if (maxID.ToString().Equals(ID) || maxID < int.Parse(ID))
		{
			return maxIndex;
		}
		else if (minIndex == maxIndex)
		{
			return 0;
		}

		// This performs a binary search of the data based on its ID
		for (int i = 0; i < dataTable.Length; i++)
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
			currentID = int.Parse(GetElementID(dataTable[currentIndex]));

			// This decides whether to search the upper half or lower half of the array depending on the currentID value and the value to search for
			if (currentID.ToString().Equals(ID))
			{
				return currentIndex;
			}
			else if (currentID < int.Parse(ID))
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
		throw new System.Exception("There was a problem with the FindLineIndex method in YahtzeeDataIO.");
	}
}
