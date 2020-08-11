using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringBoxYahtzeeBonus : ScoreCardBox
{

	/// <summary>
	/// The yahtzee box and grand total box in the same column
	/// </summary>
	public ScoringBoxYahtzee normalYahtzee;
	public ScoringBoxGrandTotal grandTotal;

	/// <summary>
	/// This tells when a new turn has happened so a yahtzee bonus can be added once per turn
	/// </summary>
	bool newTurn;

	/// <summary>
	/// This tells the yahtzee bonus that 
	/// </summary>
	bool justRolledFirstYahtzee;

	/// <summary>
	/// When the box is created it initializes variables
	/// </summary>
	void Start()
	{
		Initialize();
	}

	/// <summary>
	/// This tells the yahtzee bonus that a new turn has been made
	/// </summary>
	public void NewTurn()
	{
		newTurn = true;
	}

	/// <summary>
	/// This initializes the new turn variable to false and initializes regular variables
	/// </summary>
	protected new void Initialize()
	{
		base.Initialize();

		newTurn = false;
		justRolledFirstYahtzee = true;
	}

	/// <summary>
	/// This updates the information in the yahtzee bonus box
	/// </summary>
	protected override void UpdateInformation()
	{

		// Information is only updated if it is not already filled in
		if (!boxFilledIn)
		{

			// This updates the numbers based on the dice
			UpdateDiceNumbers();

			// Yahtzee bonus is 0 if the normal yahtzee is zero
			if (normalYahtzee.GetScore() == 0 && normalYahtzee.IsBoxFilledIn())
			{
				score = GetPoints();
				boxFilledIn = true;
				SetIfTextGrayedOut(false);
				SetIfBoxSelcted(false);
			}

			// If the game is over the yahtzee bonus score is done
			else if (grandTotal.IsBoxFilledIn())
			{
				score = GetPoints();
				boxFilledIn = true;
				SetIfTextGrayedOut(false);
				SetIfBoxSelcted(false);
			}

			// The box gets updated based on the dice and score if it does not get filled in
			else if (newTurn)
			{
				if (!justRolledFirstYahtzee)
				{
					score = GetPoints();

				}
				else
				{
					justRolledFirstYahtzee = false;
				}
				newTurn = false;
			}
			textMeshPro.SetText(score.ToString());
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
	/// <returns>Points in the yahtzee bonus category based on the top section</returns>
	public override int GetPoints()
	{
		return YahtzeeScoring.YahtzeeBonus(diceNumbers[0], diceNumbers[1], diceNumbers[2], diceNumbers[3], diceNumbers[4], score, normalYahtzee.GetScore());
	}

	/// <summary>
	/// Every frame this updates the information and checks for a mouse click to enter key to be selected
	/// </summary>
	void Update()
	{
		UpdateInformation();
		CheckForEnterKey();
		CheckForMouseClick();
	}

	/// <summary>
	/// This sets the newTurn variable to the given bool
	/// </summary>
	/// <param name="isNewTurn">Whether it is a new turn</param>
	public void SetNewTurn(bool isNewTurn)
	{
		newTurn = isNewTurn;
	}
}
