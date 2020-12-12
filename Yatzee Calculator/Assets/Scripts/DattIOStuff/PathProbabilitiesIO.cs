using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathProbabilitiesIO
{

	// This is the name of the most recently accessed path data file
	static string recentFileName;

	// This is the data of the most recently accessed path data file
	static string[] recentPathData;

	/// <summary>
	/// This adds the given IDs and values fileName file. If the file does not exist then a new file is created. If the file already exists then the file is overwritten.
	/// </summary>
	/// <param name="ID">The list of ID values to be stored for searching</param>
	/// <param name="value">The list of values to be stored at the index</param>
	/// <param name="fileName">The fileName in the Data folder where the dictionary is being updated</param>
	public static void AddToDictionary(string[] ID, string[] value, string fileName)
	{

		// This handles improper input
		if (ID == null)
		{
			throw new System.Exception("A: The given ID is null.");
		}
		else if (value == null)
		{
			throw new System.Exception("B: The given value is null.");
		}
		else if (fileName == null)
		{
			throw new System.Exception("C: The given fileName is null.");
		}
		else if (ID.Length != value.Length)
		{
			throw new System.Exception("D: The given ID and value lists have different lengths of ID: " + ID.Length + " and value: " + value.Length + ". They need to be the same length.");
		}

		// The file path for the data to be edited
		string filePath = "Assets/Data/" + fileName + ".txt";

		// This creates the file if the file does not already exist
		if (!System.IO.File.Exists(filePath))
		{
			Debug.Log("A new file " + fileName + ".txt has been added to the data folder");
			System.IO.File.Create(filePath);
		}

		// This is the data that will be added to the file
		string dataToAdd = "";

		// This formats the data to be ID[i] + space + value[i] for each element in the list and in the same order as the given list
		for (int i = 0; i < ID.Length; i++)
		{
			dataToAdd += ID[i] + " " + value[i] + "\n";
		}

		// The dataToAdd is added to the given file name
		System.IO.File.WriteAllText(filePath, dataToAdd);
	}

	/// <summary>
	/// This returns the array of all of the IDs in the given fileName file.
	/// </summary>
	/// <param name="fileName">The name of the file to retrieve the IDs</param>
	/// <returns>An array of IDs in the fileName file</returns>
	public static string[] GetEntireIDArray(string fileName)
	{
		// This handles improper input
		if (fileName == null)
		{
			throw new System.Exception("A: The given fileName is null.");
		}

		// The file path for the data to be edited
		string filePath = "Assets/Data/" + fileName + ".txt";

		// If the file does not exist, then it throws this error
		if (!System.IO.File.Exists(filePath))
		{
			throw new System.Exception("B: The file: " + filePath + " does not exist.");
		}

		// This is the entire array of data in the file
		string[] pathData;

		// If the file has just been accessed, then the yahtzee data is equal to an already initialized yahtzee data
		if (recentFileName != null && recentFileName.Equals(fileName))
		{
			pathData = recentPathData;
		}

		// If the yahtzee data has not been recently initialiezd, then the recentYahtzeeData is initialized to this yahtzeeData and the recentFileName is set to the fileName of this accessed file
		else
		{
			pathData = System.IO.File.ReadAllLines(filePath);
			recentPathData = pathData;
			recentFileName = fileName;
		}

		// If the text file is empty, then it throws this error
		if (pathData.Length == 0)
		{
			throw new System.Exception("C: The file is empty, could not get data.");
		}

		// This is the array of IDs in the entire file
		string[] IDArray = new string[pathData.Length];

		// This initializes the IDs of the entire list
		for (int i = 0; i < pathData.Length; i++)
		{
			IDArray[i] = GetElementID(pathData[i]);
		}

		// The IDArray is returned
		return IDArray;

	}

	/// <summary>
	/// THis gets the entire array of float values from the given file in sorted order.
	/// </summary>
	/// <param name="fileName">The name of the file which the data comes from</param>
	/// <returns>The array of float values in the fileName file</returns>
	public static float[] GetEntireFloatValueArray(string fileName)
	{

		// This handles improper input
		if (fileName == null)
		{
			throw new System.Exception("A: The given fileName is null.");
		}

		// The file path for the data to be edited
		string filePath = "Assets/Data/" + fileName + ".txt";

		// If the file does not exist, then it throws this error
		if (!System.IO.File.Exists(filePath))
		{
			throw new System.Exception("B: The file: " + filePath + " does not exist.");
		}

		// This is the entire array of data in the file
		string[] pathData;

		// If the file has just been accessed, then the yahtzee data is equal to an already initialized yahtzee data
		if (recentFileName != null && recentFileName.Equals(fileName))
		{
			pathData = recentPathData;
		}

		// If the yahtzee data has not been recently initialiezd, then the recentYahtzeeData is initialized to this yahtzeeData and the recentFileName is set to the fileName of this accessed file
		else
		{
			pathData = System.IO.File.ReadAllLines(filePath);
			recentPathData = pathData;
			recentFileName = fileName;
		}

		// If the text file is empty, then it throws this error
		if (pathData.Length == 0)
		{
			throw new System.Exception("C: The file is empty, could not get data.");
		}

		// This is the array of values in the entire file
		float[] valueArray = new float[pathData.Length];

		// This initializes the values of the entire list
		for (int i = 0; i < pathData.Length; i++)
		{
			try
			{
				valueArray[i] = float.Parse(GetElementValue(pathData[i]));
			}
			catch (Exception e)
			{
				throw new System.Exception("D: The content of the file " + fileName + " at line " + i + " is not properly formatted. It is " + GetElementValue(pathData[i]) + " when it should be formatted ID + space + float.");
			}
		}

		// The valueArray is returned
		return valueArray;
	}

	/// <summary>
	/// This retrieves the value from the fileName file with the given ID.
	/// </summary>
	/// <param name="ID">The ID to find the element in the path file</param>
	/// <param name="fileName">The name of the file to be found in the data folder</param>
	/// <returns>The value in the fileName fil with the given ID</returns>
	public static string GetFromDictionary(string ID, string fileName)
	{

		// This handles improper input
		if (ID == null)
		{
			throw new System.Exception("A: The given ID is null.");
		}
		else if (fileName == null)
		{
			throw new System.Exception("B: The given fileName is null.");
		}

		// The file path for the data to be edited
		string filePath = "Assets/Data/" + fileName + ".txt";

		// If the file does not exist, then it throws this error
		if (!System.IO.File.Exists(filePath))
		{
			throw new System.Exception("C: The file: " + filePath + " does not exist.");
		}

		// The whole data set of the file path
		string[] pathData;

		// If the file has just been accessed, then the yahtzee data is equal to an already initialized yahtzee data
		if (recentFileName != null && recentFileName.Equals(fileName))
		{
			pathData = recentPathData;
		}

		// If the yahtzee data has not been recently initialiezd, then the recentYahtzeeData is initialized to this yahtzeeData and the recentFileName is set to the fileName of this accessed file
		else
		{
			pathData = System.IO.File.ReadAllLines(filePath);
			recentPathData = pathData;
			recentFileName = fileName;
		}

		// If the text file is empty, then it throws this error
		if (pathData.Length == 0)
		{
			throw new System.Exception("D: The file is empty, could not get data.");
		}

		// This is the index in the array where the line will be edited
		int indexToInsert = FindLineIndex(ID, pathData);

		// If the ID is not found in the file then an exception is thrown
		if (!GetElementID(pathData[indexToInsert]).Equals(ID))
		{
			throw new System.Exception("E: The data with the ID " + ID + " does not exist in the file " + fileName);
		}

		// The data at the line with the ID is returned
		return GetElementValue(pathData[indexToInsert]);
	}

	/// <summary>
	/// This takes the given line of data and returns the ID in the dateLine
	/// </summary>
	/// <param name="dataLine">The line to find the ID</param>
	/// <returns>The ID in the given line</returns>
	static string GetElementID(string dataLine)
	{

		// Handles improper input
		if (dataLine.IndexOf(" ") == -1)
		{
			throw new System.Exception("Line is not properly formatted: " + dataLine + "\nIt should be number + space + float");
		}

		// Returns the string starting from the start and ending at the space
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
			float.Parse(dataLine.Substring(dataLine.IndexOf(" ") + 1));
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
		string minID = GetElementID(dataTable[minIndex]);
		string maxID = GetElementID(dataTable[maxIndex]);

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
			currentID = GetElementID(dataTable[currentIndex]);

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
		throw new System.Exception("There was a problem with the FindLineIndex method in YahtzeeDataIO.");
	}
}
