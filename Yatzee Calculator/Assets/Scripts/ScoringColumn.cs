using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringColumn : MonoBehaviour
{

	/// <summary>
	/// This tells the turn that this column is on (1-13)
	/// </summary>
	bool turnReady;

	/// <summary>
	/// This tells how many rolls there are left in this turn
	/// </summary>
	int rollsLeft;

	/// <summary>
	/// This tells if there is a forced joker to be played in the column
	/// </summary>
	bool forcedJoker;

	/// <summary>
	/// This tells if the column this is playable
	/// </summary>
	protected bool columnPlayable;

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

	public void JokerAvailableToColumn()
	{
		aces.JokerNowAvailable();
		twos.JokerNowAvailable();
		threes.JokerNowAvailable();
		fours.JokerNowAvailable();
		fives.JokerNowAvailable();
		sixes.JokerNowAvailable();
		bonus.JokerNowAvailable();
		threeOfAKind.JokerNowAvailable();
		fourOfAKind.JokerNowAvailable();
		fullHouse.JokerNowAvailable();
		smallStraight.JokerNowAvailable();
		largeStraight.JokerNowAvailable();
		yahtzee.JokerNowAvailable();
		yahtzeeBonus.JokerNowAvailable();
		chance.JokerNowAvailable();
		topTotal.JokerNowAvailable();
		bottomTotal.JokerNowAvailable();
		grandTotal.JokerNowAvailable();
	}

	/// <summary>
	/// This decrements the rolls left
	/// </summary>
	public void DiceRolled()
	{
		rollsLeft--;

		// This tells the boxes that the dice have been rolled and they can be written in
		aces.SetFirstRollDone(true);
		twos.SetFirstRollDone(true);
		threes.SetFirstRollDone(true);
		fours.SetFirstRollDone(true);
		fives.SetFirstRollDone(true);
		sixes.SetFirstRollDone(true);
		bonus.SetFirstRollDone(true);
		threeOfAKind.SetFirstRollDone(true);
		fourOfAKind.SetFirstRollDone(true);
		fullHouse.SetFirstRollDone(true);
		smallStraight.SetFirstRollDone(true);
		largeStraight.SetFirstRollDone(true);
		yahtzee.SetFirstRollDone(true);
		yahtzeeBonus.SetFirstRollDone(true);
		chance.SetFirstRollDone(true);
		topTotal.SetFirstRollDone(true);
		bottomTotal.SetFirstRollDone(true);
		grandTotal.SetFirstRollDone(true);
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
		yahtzeeBonus.ShowText();
		chance.ShowText();
	}

	/// <summary>
	/// This tells this column that they have made a new turn
	/// </summary>
	public void NewTurn()
	{

		// This sets the rolls to 3, increases the turn count, and tells the scorecard that a category was selected
		rollsLeft = 3;
		turnReady = true;

		// This tells the boxes that a new turn has been done and they can't be written in
		aces.SetFirstRollDone(false);
		twos.SetFirstRollDone(false);
		threes.SetFirstRollDone(false);
		fours.SetFirstRollDone(false);
		fives.SetFirstRollDone(false);
		sixes.SetFirstRollDone(false);
		threeOfAKind.SetFirstRollDone(false);
		fourOfAKind.SetFirstRollDone(false);
		fullHouse.SetFirstRollDone(false);
		smallStraight.SetFirstRollDone(false);
		largeStraight.SetFirstRollDone(false);
		yahtzee.SetFirstRollDone(false);
		chance.SetFirstRollDone(false);

		// This tells the yahtzee bonus that a new turn has been made
		yahtzeeBonus.SetNewTurn(true);
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
		rollsLeft = 3;
		forcedJoker = false;
	}

	/// <summary>
	/// This tells the column whether there is a forced joker in play
	/// </summary>
	/// <param name="isForcedJoker">Whether there is a forced joker</param>
	public void SetForcedJoker(bool isForcedJoker)
	{
		forcedJoker = isForcedJoker;
	}

	/// <summary>
	/// Every frame it hides grayed out scores if there are 3 rolls left and shows grayed out scores otherwise
	/// </summary>
	void Update()
	{
		if (!forcedJoker)
		{
			if (rollsLeft != 3 && turnReady)
			{
				ShowGrayedOutScores();
			}
			else
			{
				HideGrayedOutScores();
			}
		}
	}

	/// <summary>
	/// This tells this column if it is their turn to play
	/// </summary>
	/// <param name="isTurnReady"></param>
	public void SetIfTurnReady(bool isTurnReady)
	{
		turnReady = isTurnReady;

		// This tells each of the boxes whether it is playable
		aces.SetIfColumnPlayable(isTurnReady);
		twos.SetIfColumnPlayable(isTurnReady);
		threes.SetIfColumnPlayable(isTurnReady);
		fours.SetIfColumnPlayable(isTurnReady);
		fives.SetIfColumnPlayable(isTurnReady);
		sixes.SetIfColumnPlayable(isTurnReady);
		bonus.SetIfColumnPlayable(isTurnReady);
		threeOfAKind.SetIfColumnPlayable(isTurnReady);
		fourOfAKind.SetIfColumnPlayable(isTurnReady);
		fullHouse.SetIfColumnPlayable(isTurnReady);
		smallStraight.SetIfColumnPlayable(isTurnReady);
		largeStraight.SetIfColumnPlayable(isTurnReady);
		yahtzee.SetIfColumnPlayable(isTurnReady);
		yahtzeeBonus.SetIfColumnPlayable(isTurnReady);
		chance.SetIfColumnPlayable(isTurnReady);
		topTotal.SetIfColumnPlayable(isTurnReady);
		bottomTotal.SetIfColumnPlayable(isTurnReady);
		grandTotal.SetIfColumnPlayable(isTurnReady);
	}

	/// <summary>
	/// This tells the scorecard that a category has been selected
	/// </summary>
	public void CategorySelected()
	{
		scorecard.CategorySelected();
	}

}
