using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringBoxYahtzeeBonus : ScoreCardBox
{

	public Die[] dice;

	public ScoringBoxYahtzee normalYahtzee;

	public ScoringBoxGrandTotal grandTotal;

	bool newTurn;

	int[] diceNumbers;

	// Start is called before the first frame update
	void Start()
	{
		Initialize();
	}

	public void NewTurn()
	{
		newTurn = true;
	}

	protected new void Initialize()
	{
		base.Initialize();
		newTurn = false;
	}

	void UpdateDiceNumbers()
	{
		diceNumbers = new int[5];
		for (int i = 0; i < 5; i++)
		{
			diceNumbers[i] = dice[i].number;
		}
	}

	protected override void UpdateInformation()
	{
		if (!boxFilledIn)
		{
			UpdateDiceNumbers();
			if (normalYahtzee.GetScore() == 0 && normalYahtzee.IsBoxFilledIn())
			{
				score = GetPoints();
				boxFilledIn = true;
				SetIfTextGrayedOut(false);
				SetIfBoxSelcted(false);
			}
			else if (grandTotal.IsBoxFilledIn())
			{
				score = GetPoints();
				boxFilledIn = true;
				SetIfTextGrayedOut(false);
				SetIfBoxSelcted(false);
			}
			else
			{
				textMeshPro.SetText(YahtzeeScoring.YahtzeeBonus(diceNumbers[0], diceNumbers[1], diceNumbers[2], diceNumbers[3], diceNumbers[4], score, normalYahtzee.GetScore()).ToString());
			}

		}
	}

	protected override bool ShouldBoxBeFilledIn()
	{
		return false;
	}

	public override int GetPoints()
	{
		return YahtzeeScoring.YahtzeeBonus(diceNumbers[0], diceNumbers[1], diceNumbers[2], diceNumbers[3], diceNumbers[4], score, normalYahtzee.GetScore());
	}

	// Update is called once per frame
	void Update()
	{
		UpdateInformation();
		CheckForEnterKey();
		CheckForMouseClick();
	}
}
