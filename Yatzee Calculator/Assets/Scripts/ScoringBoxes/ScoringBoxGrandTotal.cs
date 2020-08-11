using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringBoxGrandTotal : ScoreCardBox
{

	/// <summary>
	/// The top section and bottom section scoring boxes in the same column
	/// </summary>
	public ScoringBoxTopTotal topTotal;
	public ScoringBoxBottomTotal bottomTotal;

	/// <summary>
	/// When the box is created it initializes variables
	/// </summary>
	void Start()
	{
		Initialize();
	}

	/// <summary>
	/// This checks to see whether the bonus box should be filled in
	/// </summary>
	void CheckForFillIn()
	{

		// If both the top and bottom section boxes are filled it fills in the box and sets the score for the box
		if (topTotal.IsBoxFilledIn() && bottomTotal.IsBoxFilledIn())
		{
			UpdateInformation();
			score = GetPoints();
			boxFilledIn = true;
			SetIfTextGrayedOut(false);
			SetIfBoxSelcted(false);
		}
	}

	/// <summary>
	/// This box can not be filled in by the user
	/// </summary>
	/// <returns>This box can not be filled in by the user</returns>
	protected override bool ShouldBoxBeFilledIn()
	{
		return false;
	}

	/// <summary>
	/// This uses the yahtzee scoring class to tell the points in this box's category
	/// </summary>
	/// <returns>Points in the bonus category based on the top section</returns>
	public override int GetPoints()
	{
		return YahtzeeScoring.GrandTotal(topTotal.GetPoints(), bottomTotal.GetPoints());
	}

	/// <summary>
	/// Every frame this updates the information, checks to see if the box should be filled in and checks for a mouse click to enter key to be selected
	/// </summary>
	void Update()
	{
		CheckForFillIn();
		UpdateInformation();
		CheckForEnterKey();
		CheckForMouseClick();
	}
}
