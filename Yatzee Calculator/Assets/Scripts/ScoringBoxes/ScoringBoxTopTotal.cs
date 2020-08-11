using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringBoxTopTotal : ScoreCardBox
{

	/// <summary>
	/// The top section scoring boxes in the same column
	/// </summary>
	public ScoringBoxAces aces;
	public ScoringBoxTwos twos;
	public ScoringBoxThrees threes;
	public ScoringBoxFours fours;
	public ScoringBoxFives fives;
	public ScoringBoxSixes sixes;
	public ScoringBoxBonus bonus;

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

		// If all the top section boxes are filled it fills in the box and sets the score for the box
		if (bonus.IsBoxFilledIn())
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
	/// <returns>Points in the top total category based on the top section</returns>
	public override int GetPoints()
	{
		return YahtzeeScoring.TopTotal(aces.GetScore(), twos.GetScore(), threes.GetScore(), fours.GetScore(), fives.GetScore(), sixes.GetScore(), bonus.GetPoints());
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
