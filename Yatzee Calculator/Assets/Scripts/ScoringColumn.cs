using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringColumn : MonoBehaviour
{

	int turn;
	int rollsLeft;

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

	public Scorecard scorecard;


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

	public void DiceRolled()
	{
		rollsLeft--;
	}

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

	public void NewTurn()
	{
		rollsLeft = 3;
		turn++;
		scorecard.CategorySelected();
	}

	// Start is called before the first frame update
	void Start()
	{
		turn = 1;
		rollsLeft = 3;
	}

	// Update is called once per frame
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
