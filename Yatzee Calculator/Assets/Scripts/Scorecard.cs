using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorecard : MonoBehaviour
{

	/// <summary>
	/// This is the list of player columns in the scorecard
	/// </summary>
	public ScoringColumn[] scoringColumn;

	/// <summary>
	/// This is the roll dice button script
	/// </summary>
	public RollDice rollDiceButton;

	/// <summary>
	/// This is the number of what player's turn it is (starting at 1)
	/// </summary>
	int playerTurn;

	/// <summary>
	/// When the scorecard is created it initializes variables
	/// </summary>
	private void Start()
	{
		Initialize();
	}

	/// <summary>
	/// playerTurn starts at 1 for player 1 and it starts players 1's turn
	/// </summary>
	void Initialize()
	{
		playerTurn = 1;
		scoringColumn[0].SetIfTurnReady(true);
		scoringColumn[0].NewTurn();
	}

	/// <summary>
	/// This tells a column that the dice have been rolled when it is their turn and the dice have been rolled
	/// </summary>
	public void DiceRolled()
	{
		for (int i = 0; i < scoringColumn.Length; i++)
		{
			scoringColumn[i].DiceRolled();
		}
	}

	/// <summary>
	/// This tells the column to know that it is not its turn and to move onto the next columns turn
	/// </summary>
	public void CategorySelected()
	{
		scoringColumn[playerTurn - 1].SetIfTurnReady(false);
		playerTurn = playerTurn % scoringColumn.Length + 1;
		scoringColumn[playerTurn - 1].SetIfTurnReady(true);
		scoringColumn[playerTurn - 1].NewTurn();
		rollDiceButton.NewTurn();
	}
}
