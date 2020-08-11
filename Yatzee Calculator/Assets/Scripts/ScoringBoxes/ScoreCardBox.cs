using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class ScoreCardBox : MonoBehaviour
{

	/// <summary>
	/// This tells whether a box has text that is grayed out
	/// </summary>
	protected bool isGrayedOut;

	/// <summary>
	/// This tells if a box is selected
	/// </summary>
	protected bool isSelected;

	/// <summary>
	/// This tells if a box is filled in
	/// </summary>
	protected bool boxFilledIn;

	/// <summary>
	/// This tells the score of the box (0 until filled in unless special box)
	/// </summary>
	protected int score;

	/// <summary>
	/// The grayed out and normal colors for when the button is being hovered over
	/// </summary>
	public Color grayedOutColor;
	public Color normalColor;

	/// <summary>
	/// These are the numbers of the held dice
	/// </summary>
	protected int[] diceNumbers;

	/// <summary>
	/// These are the scripts for the dice
	/// </summary>
	public Die[] dice;

	/// <summary>
	/// The text mesh pro component
	/// </summary>
	protected TextMeshProUGUI textMeshPro;

	/// <summary>
	/// The box collider 2D component
	/// </summary>
	protected BoxCollider2D boxCollider;

	/// <summary>
	/// The sprite renderer for the box selection
	/// </summary>
	protected SpriteRenderer boxSelectionSpriteRenderer;

	/// <summary>
	/// The column script which the scoring boxes are in
	/// </summary>
	public ScoringColumn scoringColumn;

	/// <summary>
	/// This tells if it has been after the first roll in a turn
	/// </summary>
	bool afterFirstRoll;

	/// <summary>
	/// This tells wether the yahtzee box has been filled out yet and so a yahtzee joker would be available
	/// </summary>
	protected bool jokerAvailable;

	/// <summary>
	/// This tells if the column this box is in is playable
	/// </summary>
	protected bool columnPlayable;

	/// <summary>
	/// This returns the score in the box
	/// </summary>
	/// <returns></returns>
	public int GetScore()
	{
		return score;
	}

	/// <summary>
	/// This initializes components, sets the visual settings for when the box starts, and sets private variables
	/// </summary>
	protected void Initialize()
	{
		textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
		boxSelectionSpriteRenderer = transform.Find("BoxSelection").GetComponent<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();
		scoringColumn = GetComponentInParent<ScoringColumn>();
		SetIfBoxSelcted(false);
		SetIfTextGrayedOut(true);
		boxFilledIn = false;
		afterFirstRoll = false;
		jokerAvailable = false;
		score = 0;
	}

	/// <summary>
	/// This hides the text of the box
	/// </summary>
	public void HideText()
	{
		textMeshPro.enabled = false;
	}

	/// <summary>
	/// This shows the text of the box
	/// </summary>
	public void ShowText()
	{
		textMeshPro.enabled = true;
	}

	/// <summary>
	/// This checks if the mouse has been clicked and reacts appropriately
	/// </summary>
	protected void CheckForMouseClick()
	{

		// This occurs when the mouse is clicked
		if (Input.GetMouseButtonDown(0))
		{

			// This occurs when the mouse is clicked on the box
			if (boxCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
			{

				// If the box has already selected it then it picks this box's category as the move
				if (isSelected)
				{
					if (!boxFilledIn && afterFirstRoll && ShouldBoxBeFilledIn())
					{
						CategorySelected();
					}
				}

				// If the box is not selected and clicked the it becomes selected
				else
				{
					SetIfBoxSelcted(true);
				}
			}

			// This sets the box to unselected when the mouse clicks away from the box
			else
			{
				SetIfBoxSelcted(false);
			}
		}
	}

	/// <summary>
	/// This checks to see if the enter key has been pressed and reacts appropriately
	/// </summary>
	protected void CheckForEnterKey()
	{
		if (afterFirstRoll && !boxFilledIn && isSelected && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && ShouldBoxBeFilledIn())
		{
			CategorySelected();
		}
	}

	/// <summary>
	/// This updates and fills in the category and starts a new turn
	/// </summary>
	protected virtual void CategorySelected()
	{
		if (!JokerForcedAway(this) && columnPlayable)
		{
			score = GetPoints();
			boxFilledIn = true;
			SetIfTextGrayedOut(false);
			SetIfBoxSelcted(false);
			scoringColumn.CategorySelected();
		}

	}

	/// <summary>
	/// This updates the information in the box based on the dice
	/// </summary>
	protected void UpdateDiceNumbers()
	{
		diceNumbers = new int[5];
		for (int i = 0; i < 5; i++)
		{
			diceNumbers[i] = dice[i].number;
		}
	}

	/// <summary>
	/// This updates the number in the box to match the rest of the scorecard and held dice
	/// </summary>
	protected virtual void UpdateInformation()
	{

		// This tells the box to hide text if there is a joker in play and this box is not available
		if (afterFirstRoll && JokerForcedAway(this) && !boxFilledIn && !(this is ScoringBoxBonus) && !(this is ScoringBoxTopTotal) && !(this is ScoringBoxBottomTotal) && !(this is ScoringBoxGrandTotal) && !(this is ScoringBoxYahtzeeBonus))
		{
			HideText();
		}

		// This updates the dice numbers and sets the text to the points based on the dice and scorecard
		if (!boxFilledIn)
		{
			UpdateDiceNumbers();
			textMeshPro.SetText(GetPoints().ToString());
		}

		// This tells the scorecard column to not show boxes if there is a joker in play
		if (jokerAvailable && afterFirstRoll && YahtzeeScoring.Yahtzee(dice[0].number, dice[1].number, dice[2].number, dice[3].number, dice[4].number) == 50)
		{
			scoringColumn.SetForcedJoker(true);
		}
		else
		{
			scoringColumn.SetForcedJoker(false);
		}
	}

	/// <summary>
	/// This returns the points for the category based on the current dice
	/// </summary>
	/// <returns>The points for the category based on the current dice</returns>
	public abstract int GetPoints();

	/// <summary>
	/// This tells whether a box should be able to be filled in by the user
	/// </summary>
	/// <returns>Whether this box should be able to be filled in by the user</returns>
	protected abstract bool ShouldBoxBeFilledIn();

	/// <summary>
	/// This updates the text color based on the given boolean
	/// </summary>
	/// <param name="grayedOut">This tells whether the text should be grayed out or not</param>
	public void SetIfTextGrayedOut(bool grayedOut)
	{
		isGrayedOut = grayedOut;
		if (isGrayedOut)
		{
			textMeshPro.color = grayedOutColor;
		}
		else
		{
			textMeshPro.color = normalColor;
		}
	}

	/// <summary>
	/// This sets whether the box is selected and updates the visuals
	/// </summary>
	/// <param name="boxSelected">Whether the box is to be selected</param>
	public void SetIfBoxSelcted(bool boxSelected)
	{
		isSelected = boxSelected;
		boxSelectionSpriteRenderer.enabled = isSelected;
	}

	/// <summary>
	/// This sets whether the box should be filled in and updates it visually
	/// </summary>
	/// <param name="boxFilledIn">Whether the box should be filled in</param>
	public void SetIfBoxFilledIn(bool boxFilledIn)
	{
		this.boxFilledIn = boxFilledIn;
	}

	/// <summary>
	/// This tells whether the box is filled in
	/// </summary>
	/// <returns>Whether the box is filled in</returns>
	public bool IsBoxFilledIn()
	{
		return boxFilledIn;
	}

	/// <summary>
	/// This tells whether the first roll in the turn has been done
	/// </summary>
	/// <param name="afterFirstRoll">Whether the first roll in the turn has been done</param>
	public void SetFirstRollDone(bool afterFirstRoll)
	{
		this.afterFirstRoll = afterFirstRoll;
	}

	/// <summary>
	/// This tells the box that the joker is available to play in the game
	/// </summary>
	public void JokerNowAvailable()
	{
		jokerAvailable = true;
	}

	/// <summary>
	/// This tells if a joker is forcing another category to be played 
	/// </summary>
	/// <param name="scoreCardBox">The ScoreCardBox object of the category to be decided if it cannot be played</param>
	/// <returns></returns>
	protected bool JokerForcedAway(ScoreCardBox scoreCardBox)
	{

		// If the joker is available and the dice rolled is a yahtzee
		if (jokerAvailable && YahtzeeScoring.Yahtzee(dice[0].number, dice[1].number, dice[2].number, dice[3].number, dice[4].number) == 50)
		{

			// If there is a joker that has a number in the top section that is not filled in then it says that only the category of that number can be filled in
			switch (dice[0].number)
			{
				case 1:
					if (!scoringColumn.aces.IsBoxFilledIn())
					{
						return !(scoreCardBox is ScoringBoxAces);
					}
					break;
				case 2:
					if (!scoringColumn.twos.IsBoxFilledIn())
					{
						return !(scoreCardBox is ScoringBoxTwos);
					}
					break;
				case 3:
					if (!scoringColumn.threes.IsBoxFilledIn())
					{
						return !(scoreCardBox is ScoringBoxThrees);
					}
					break;
				case 4:
					if (!scoringColumn.fours.IsBoxFilledIn())
					{
						return !(scoreCardBox is ScoringBoxFours);
					}
					break;
				case 5:
					if (!scoringColumn.fives.IsBoxFilledIn())
					{
						return !(scoreCardBox is ScoringBoxFives);
					}
					break;
				case 6:
					if (!scoringColumn.sixes.IsBoxFilledIn())
					{
						return !(scoreCardBox is ScoringBoxSixes);
					}
					break;
			}

			// If there is an open section in the bottom then the only open spots are in the bottom section
			if (!scoringColumn.threeOfAKind.IsBoxFilledIn() || !scoringColumn.fourOfAKind.IsBoxFilledIn() || !scoringColumn.fullHouse.IsBoxFilledIn() || !scoringColumn.smallStraight.IsBoxFilledIn() || !scoringColumn.largeStraight.IsBoxFilledIn() || !scoringColumn.chance.IsBoxFilledIn())
			{
				return !(scoreCardBox is ScoringBoxThreeOfAKind || scoreCardBox is ScoringBoxFourOfAKind || scoreCardBox is ScoringBoxFullHouse || scoreCardBox is ScoringBoxSmallStraight || scoreCardBox is ScoringBoxLargeStraight || scoreCardBox is ScoringBoxChance);
			}

			// If the only option left is to put the joker in the top section for 0 then any spot is available
			return false;

		}

		// If it is not a joker then no spot is forced
		return false;
	}

	/// <summary>
	/// This tells this box whether the column it is in is playable or not
	/// </summary>
	/// <param name="isColumnPlayable">Whether the column is playable</param>
	public void SetIfColumnPlayable(bool isColumnPlayable)
	{
		columnPlayable = isColumnPlayable;
	}

}
