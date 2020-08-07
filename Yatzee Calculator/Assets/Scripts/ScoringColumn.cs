using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringColumn : MonoBehaviour
{

	/// <summary>
	/// This tells the turn that this column is on (1-13)
	/// </summary>
	int turn;

	/// <summary>
	/// This tells how many rolls there are left in this turn
	/// </summary>
	int rollsLeft;

	/// <summary>
	/// These are all of the boxes in this column
	/// </summary>
	public ScoringBoxAces aces;
	public ScoringBoxTwos twos;
	public ScoringBoxThrees threes;
	public ScoringBoxFours fours;
	public ScoringBoxFives fives;
	public ScoringBoxSixes sixes;
	public ScoringBoxBonus bonus;
	public ScoringBoxThreeOfAKind threeOfAKind;
	public ScoringBoxFourOfAKind fourOfAKind;
	public ScoringBoxFullHouse fullHouse;
	public ScoringBoxSmallStraight smallStraight;
	public ScoringBoxLargeStraight largeStraight;
	public ScoringBoxYahtzee yahtzee;
	public ScoringBoxYahtzeeBonus yahtzeeBonus;
	public ScoringBoxChance chance;
	public ScoringBoxTopTotal topTotal;
	public ScoringBoxBottomTotal bottomTotal;
	public ScoringBoxGrandTotal grandTotal;

	/// <summary>
	/// This is the scorecard that this column is in
	/// </summary>
	public Scorecard scorecard;

	/// <summary>
	/// This hides all of the grayed out text in the boxes excluding yahtzee bonus
	/// </summary>
	void HideGrayedOutScores()
	{
		if (!aces.IsBoxFilledIn())
		{
			aces.HideText();
		}
		if (!twos.IsBoxFilledIn())
		{
			twos.HideText();
		}
		if (!threes.IsBoxFilledIn())
		{
			threes.HideText();
		}
		if (!fours.IsBoxFilledIn())
		{
			fours.HideText();
		}
		if (!fives.IsBoxFilledIn())
		{
			fives.HideText();
		}
		if (!sixes.IsBoxFilledIn())
		{
			sixes.HideText();
		}
		if (!threeOfAKind.IsBoxFilledIn())
		{
			threeOfAKind.HideText();
		}
		if (!fourOfAKind.IsBoxFilledIn())
		{
			fourOfAKind.HideText();
		}
		if (!fullHouse.IsBoxFilledIn())
		{
			fullHouse.HideText();
		}
		if (!smallStraight.IsBoxFilledIn())
		{
			smallStraight.HideText();
		}
		if (!largeStraight.IsBoxFilledIn())
		{
			largeStraight.HideText();
		}
		if (!yahtzee.IsBoxFilledIn())
		{
			yahtzee.HideText();
		}
		if (!chance.IsBoxFilledIn())
		{
			chance.HideText();
		}
	}

	/// <summary>
	/// This decrements the rolls left
	/// </summary>
	public void DiceRolled()
	{
		rollsLeft--;
	}

	/// <summary>
	/// This shows all of the text in the boxes
	/// </summary>
	void ShowGrayedOutScores()
	{
		aces.ShowText();
		twos.ShowText();
		threes.ShowText();
		fours.ShowText();
		fives.ShowText();
		sixes.ShowText();
		threeOfAKind.ShowText();
		fourOfAKind.ShowText();
		fullHouse.ShowText();
		smallStraight.ShowText();
		largeStraight.ShowText();
		yahtzee.ShowText();
		chance.ShowText();
	}

	/// <summary>
	/// This tells this column that they have made a new turn
	/// </summary>
	public void NewTurn()
	{

		// This sets the rolls to 3, increases the turn count, and tells the scorecard that a category was selected
		rollsLeft = 3;
		turn++;
		scorecard.CategorySelected();
	}

	/// <summary>
	/// When the column is created it initializes variables
	/// </summary>
	void Start()
	{
		Initialize();
	}

	/// <summary>
	/// This sets the turn to 1 and sets the rolls to 3
	/// </summary>
	void Initialize()
	{
		turn = 1;
		rollsLeft = 3;
	}

	/// <summary>
	/// Every frame it hides grayed out scores if there are 3 rolls left and shows grayed out scores otherwise
	/// </summary>
	void Update()
	{
		if (rollsLeft != 3)
		{
			ShowGrayedOutScores();
		}
		else
		{
			HideGrayedOutScores();
		}
	}
}
