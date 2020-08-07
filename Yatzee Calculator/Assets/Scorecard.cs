using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorecard : MonoBehaviour
{

	public ScoringColumn[] scoringColumn;

	public RollDice rollDiceButton;

	public void DiceRolled()
	{
		for (int i = 0; i < scoringColumn.Length; i++)
		{
			scoringColumn[i].DiceRolled();
		}
	}

	public void CategorySelected()
	{
		rollDiceButton.NewTurn();
	}
}
