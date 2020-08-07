using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringBoxYahtzee : ScoreCardBox
{

	public Die[] dice;
	int[] diceNumbers;

	// Start is called before the first frame update
	void Start()
	{
		Initialize();
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
			textMeshPro.SetText(YahtzeeScoring.Yahtzee(diceNumbers[0], diceNumbers[1], diceNumbers[2], diceNumbers[3], diceNumbers[4]).ToString());
		}
	}

	protected override bool ShouldBoxBeFilledIn()
	{
		return true;
	}

	public override int GetPoints()
	{
		return YahtzeeScoring.Yahtzee(diceNumbers[0], diceNumbers[1], diceNumbers[2], diceNumbers[3], diceNumbers[4]);
	}

	// Update is called once per frame
	void Update()
	{
		UpdateInformation();
		CheckForEnterKey();
		CheckForMouseClick();
	}
}
