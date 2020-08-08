using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollDice : MonoBehaviour
{

	/// <summary>
	/// The sprite renderer of the roll dice button
	/// </summary>
	SpriteRenderer spriteRenderer;

	/// <summary>
	/// The scripts for the 5 dice
	/// </summary>
	public Die[] dieScripts;

	/// <summary>
	/// This is the text that displays the number of rolls left in the turn
	/// </summary>
	public TextMeshProUGUI rollsLeftText;

	/// <summary>
	/// This is the number of rolls left the player has in their turn
	/// </summary>
	int rollsLeft;

	/// <summary>
	/// The dice roll holder script
	/// </summary>
	public DiceToRoll diceRollHolder;

	/// <summary>
	/// The dice holder script
	/// </summary>
	public DiceToHold diceHolder;

	/// <summary>
	/// The list of dice that are rolled once the button is clicked
	/// </summary>
	List<Die> diceToRoll;

	/// <summary>
	/// The box collider of the roll dice button
	/// </summary>
	BoxCollider2D boxCollider;

	/// <summary>
	/// The scorecard for the Yahtzee game
	/// </summary>
	public Scorecard scorecard;

	/// <summary>
	/// These are the positions for the dice to roll to depending on the number of dice in the holder
	/// </summary>
	public Vector2 roll1Position;
	public Vector2[] roll2Position;
	public Vector2[] roll3Position;
	public Vector2[] roll4Position;
	public Vector2[] roll5Position;

	/// <summary>
	/// The time it takes to move the dice from the holder to the open are when rolled
	/// </summary>
	public float rollDiceTime;

	/// <summary>
	/// When this object is created it initializes variables
	/// </summary>
	void Start()
	{
		Initialize();
	}

	/// <summary>
	/// The box collider 2D and sprite renderer components are initialized
	/// </summary>
	void Initialize()
	{
		boxCollider = GetComponent<BoxCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		diceHolder.SetHolderEnabled(false);
		rollsLeft = 3;
	}

	/// <summary>
	/// This resets the rolls left variable and text to three and makes it so that the dice can not enter the holder
	/// </summary>
	public void NewTurn()
	{
		rollsLeft = 3;
		rollsLeftText.SetText("Rolls Left: 3");
		diceHolder.SetHolderEnabled(false);
		for (int i = 0; i < dieScripts.Length; i++)
		{
			dieScripts[i].SetIfDieCanEnterHolder(false);
		}
	}

	/// <summary>
	/// Every frame this checks if the mouse was clicked or if the mouse is hovering over the button
	/// </summary>
	void Update()
	{
		MouseClicked();
		MouseHoverOverButton();
	}

	/// <summary>
	/// This checks to see if the mouse clicks the roll dice button
	/// </summary>
	void MouseClicked()
	{

		// This checks if the left mouse button was clicked
		if (Input.GetMouseButtonDown(0))
		{

			// This checks if the mouse overlaps the box collider of the button and then rolls the dice in the holder
			if (boxCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
			{
				StartRollDice();
			}
		}

	}

	/// <summary>
	/// This checks to see whether the mouse is hovering over the button
	/// </summary>
	void MouseHoverOverButton()
	{

		// If the mouse is hovering over the button then the button becomes shaded
		if (boxCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
		{
			spriteRenderer.color = new Color(.5f, .5f, .5f);
		}

		// If the mouse is not hovering over the button then the button becomes unshaded
		else
		{
			spriteRenderer.color = Color.white;
		}
	}

	/// <summary>
	/// This rolls the dice in the roll holder
	/// </summary>
	void StartRollDice()
	{
		if (AreaClear() && rollsLeft > 0)
		{

			// This tells the scorecard that the dice have been rolled
			scorecard.DiceRolled();

			// This tells the dice holder and dice that it is now able to hold dice
			diceHolder.SetHolderEnabled(true);
			for (int i = 0; i < dieScripts.Length; i++)
			{
				dieScripts[i].SetIfDieCanEnterHolder(true);
			}

			// This decreases the number of rolls left in this turn
			rollsLeft--;
			rollsLeftText.SetText("Rolls Left: " + rollsLeft);

			// The dice to roll is received from the dice roll holder script
			diceToRoll = diceRollHolder.DiceToBeRolled();

			// This rolls each die in the roll holder
			for (int i = 0; i < diceToRoll.Count; i++)
			{
				diceToRoll[i].RollDie();
				diceToRoll[i].MoveDieOutOfRollHolder();
				diceRollHolder.EmptyHolder();
			}

			// This shuffles the order of the dice
			diceToRoll = ShuffleDice(diceToRoll);

			// This moves each die to a spot in the open area
			for (int i = 0; i < diceToRoll.Count; i++)
			{
				switch (diceToRoll.Count)
				{
					case 1:
						diceToRoll[i].MoveDie(roll1Position, rollDiceTime);
						break;
					case 2:
						diceToRoll[i].MoveDie(roll2Position[i], rollDiceTime);
						break;
					case 3:
						diceToRoll[i].MoveDie(roll3Position[i], rollDiceTime);
						break;
					case 4:
						diceToRoll[i].MoveDie(roll4Position[i], rollDiceTime);
						break;
					case 5:
						diceToRoll[i].MoveDie(roll5Position[i], rollDiceTime);
						break;
				}
			}
		}
	}

	/// <summary>
	/// This tells whether there are any dice left in the middle area
	/// </summary>
	/// <returns>Returns whether there are any dice left in the middle area</returns>
	bool AreaClear()
	{
		for (int i = 0; i < 5; i++)
		{
			if (!dieScripts[i].DieInHolder() && !dieScripts[i].DieInRollHolder())
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	/// This shuffles the order of the dice
	/// </summary>
	/// <param name="diceToShuffle">The list of Die that will be shuffled</param>
	/// <returns>A shuffled list of the given Die</returns>
	List<Die> ShuffleDice(List<Die> diceToShuffle)
	{

		// This creates a new Die list
		List<Die> newDice = new List<Die>();

		// This records the size of the list array because the diceToShuffle count is changin throughout the for loop
		int diceCount = diceToShuffle.Count;

		// This repeatedly adds a random die from the old list to the new Die list and deletes the old die so no dice are repeatedly added
		for (int i = 0; i < diceCount; i++)
		{
			int randomNumber = Random.Range(0, diceToShuffle.Count);
			newDice.Add(diceToShuffle[randomNumber]);
			diceToShuffle.RemoveAt(randomNumber);
		}
		return newDice;
	}
}