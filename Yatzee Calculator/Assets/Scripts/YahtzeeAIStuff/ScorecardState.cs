using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorecardState
{

	/// <summary>
	/// Whether the categies are filled in of the scorecard in order
	/// </summary>
	bool[] categories;

	/// <summary>
	/// The points currently in the top section
	/// </summary>
	int topTotal;

	/// <summary>
	/// Whether the yatzee category has been filled with 50
	/// </summary>
	bool yahtzeeAttained;

	/// <summary>
	/// The scorecard sheet represented as a string
	/// </summary>
	string scorecardString;

	/// <summary>
	/// This creates a scorecard based on given information
	/// </summary>
	/// <param name="categories">The 13 categories in order</param>
	/// <param name="topTotal">The points currently in the top section</param>
	/// <param name="yahtzeeAttained">Whether a yahtzee score of 50 has been attained</param>
	public ScorecardState(bool[] categories, int topTotal, bool yahtzeeAttained)
	{
		if (categories.Length != 13)
		{
			Debug.LogError("The category entered in the ScorecardState is not 13 (it should be)");
			return;
		}
		this.categories = new bool[13];
		for (int i = 0; i < 13; i++)
		{
			this.categories[i] = categories[i];
		}
		this.topTotal = Mathf.Min(topTotal, 63);
		this.yahtzeeAttained = yahtzeeAttained;
		scorecardString = GetScorecardStringValue();
	}

	public string GetScorecardString()
	{
		return scorecardString;
	}

	/// <summary>
	/// This converts the scorecard values to a string
	/// </summary>
	/// <returns>A 17 letter string of the scorecard values</returns>
	string GetScorecardStringValue()
	{

		// This converts the 13 categories to 1 or 0 depending if they have been filled in
		string scorecardString = "";
		for (int i = 0; i < 13; i++)
		{
			if (categories[i])
			{
				scorecardString += "1";
			}
			else
			{
				scorecardString += "0";
			}
		}

		// This converts the top total to a 3 character string
		string intString = topTotal.ToString();
		if (topTotal < 10)
		{
			intString = "0" + intString;
		}
		scorecardString += intString;


		// This converts the yahtzee attained to 1 or 0 depeinding if a yahtzee has been filled in
		if (yahtzeeAttained)
		{
			scorecardString += "1";
		}
		else
		{
			scorecardString += "0";
		}

		return scorecardString;
	}
}
