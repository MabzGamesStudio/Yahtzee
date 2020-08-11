using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringBoxBottomTotal : ScoreCardBox
{

	/// <summary>
	/// The top section scoring boxes in the same column
	/// </summary>
	public ScoringBoxThreeOfAKind threeOfAKind;
	public ScoringBoxFourOfAKind fourOfAKind;
	public ScoringBoxFullHouse fullHouse;
	public ScoringBoxSmallStraight smallStraight;
	public ScoringBoxLargeStraight largeStraight;
	public ScoringBoxYahtzee yahtzee;
	public ScoringBoxYahtzeeBonus yahtzeeBonus;
	public ScoringBoxChance chance;

	/// <summary>
	/// When the box is created it initializes variables
	/// </summary>
	void Start()
	{
		Initialize();
	}

	/// <summary>
	/// This checks to see whether the bottom total box should be filled in
	/// </summary>
	void CheckForFillIn()
	{

		// If the bottom section boxes are filled it fills in the box and sets the score for the box
		if (threeOfAKind.IsBoxFilledIn() && fourOfAKind.IsBoxFilledIn() && fullHouse.IsBoxFilledIn() && smallStraight.IsBoxFilledIn() && largeStraight.IsBoxFilledIn() && yahtzee.IsBoxFilledIn() && chance.IsBoxFilledIn())
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
	/// <returns>Points in the bottom total category based on the bottom section</returns>
	public override int GetPoints()
	{
		return YahtzeeScoring.BottomTotal(threeOfAKind.GetScore(), fourOfAKind.GetScore(), fullHouse.GetScore(), smallStraight.GetScore(), largeStraight.GetScore(), yahtzee.GetScore(), yahtzeeBonus.GetScore(), chance.GetScore());
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
