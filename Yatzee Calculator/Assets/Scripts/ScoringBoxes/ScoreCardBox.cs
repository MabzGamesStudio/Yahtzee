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
					if (ShouldBoxBeFilledIn())
					{
						score = GetPoints();
						boxFilledIn = true;
						SetIfTextGrayedOut(false);
						SetIfBoxSelcted(false);
						scoringColumn.NewTurn();
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
		if (isSelected && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && ShouldBoxBeFilledIn())
		{
			score = GetPoints();
			boxFilledIn = true;
			SetIfTextGrayedOut(false);
			SetIfBoxSelcted(false);
			scoringColumn.NewTurn();
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
		if (!boxFilledIn)
		{
			UpdateDiceNumbers();
			textMeshPro.SetText(GetPoints().ToString());
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
}
