using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceToRoll : MonoBehaviour
{

	/// <summary>
	/// This is the number of dice in the holder
	/// </summary>
	int diceHeld;

	/// <summary>
	/// These are the die scripts for each of the 5 die
	/// </summary>
	public Die[] diceToRoll;

	/// <summary>
	/// These tell wether a die is sliding back to its spot
	/// </summary>
	bool[] dieSlidingBack;

	/// <summary>
	/// This tells wether a die is in the holder
	/// </summary>
	bool[] diceInHolder;

	/// <summary>
	/// This is the time it takes for the dice in the holder to move into position
	/// </summary>
	public float moveDieTime;

	/// <summary>
	/// This is a list of the dice indexes in the holder
	/// </summary>
	List<int> diceIndexes;

	/// <summary>
	/// This tells whether the held die has left the holder or not
	/// </summary>
	bool dieLeftHolder;

	/// <summary>
	/// This is the box collider for this holder
	/// </summary>
	BoxCollider2D boxCollider;

	/// <summary>
	/// These are the positions the dice should move to depending on how many dice are in the holder
	/// </summary>
	public Vector2 oneDie;
	public Vector2[] twoDice;
	public Vector2[] threeDice;
	public Vector2[] fourDice;
	public Vector2[] fiveDice;

	/// <summary>
	/// This tells whether the roll holder is able to hold dice
	/// </summary>
	bool canHoldDice;

	/// <summary>
	/// This is the index of the die that is sliding back to the roll holder
	/// </summary>
	int dieSlidingBackIndex;

	/// <summary>
	/// When the holder is created it initializes variables
	/// </summary>
	void Start()
	{
		Initialize();
	}

	/// <summary>
	/// This initializes private variables and the box collider component
	/// </summary>
	void Initialize()
	{
		diceHeld = 5;
		diceInHolder = new bool[5] { true, true, true, true, true };
		dieSlidingBack = new bool[5];
		boxCollider = GetComponent<BoxCollider2D>();
		diceIndexes = new List<int>() { 0, 1, 2, 3, 4 };
		dieLeftHolder = false;
		canHoldDice = true;
	}

	/// <summary>
	/// Every frame it checks any changes to be made to the dice in the holder
	/// </summary>
	void Update()
	{
		CheckDieMovedOutOfHolder();
		CheckDieMovedIntoHolder();
		CheckDieNewHolderPositions();
	}

	/// <summary>
	/// This sets wether the die roll holder is able to hold dice
	/// </summary>
	/// <param name="enabled">This tells whether the roll holder is able to hold dice</param>
	public void SetHolderEnabled(bool enabled)
	{
		canHoldDice = enabled;
	}

	/// <summary>
	/// This checks to see whether a held die should be moved out of the holder
	/// </summary>
	void CheckDieMovedOutOfHolder()
	{
		for (int i = 0; i < 5; i++)
		{
			if (diceInHolder[i] && !dieSlidingBack[i] && !boxCollider.bounds.Intersects(diceToRoll[i].GetDieBoxCollider().bounds))
			{
				dieLeftHolder = true;
				MoveDieOutOfHolder(diceToRoll[i]);
			}
		}
	}

	/// <summary>
	/// This sets the diceHeld variable to 0 and sets all diceInHolder bools to false
	/// </summary>
	public void EmptyHolder()
	{
		diceHeld = 0;
		for (int i = 0; i < 5; i++)
		{
			diceInHolder[i] = false;
		}
	}

	/// <summary>
	/// This checks to see whether a held die should be moved into the holder
	/// </summary>
	void CheckDieMovedIntoHolder()
	{
		for (int i = 0; i < 5; i++)
		{
			if (canHoldDice && !diceInHolder[i] && !dieSlidingBack[i] && !diceToRoll[i].SlidingToNewPosition() && boxCollider.bounds.Intersects(diceToRoll[i].GetDieBoxCollider().bounds))
			{
				dieLeftHolder = false;
				MoveDieIntoHolder(diceToRoll[i]);
			}
		}
	}

	/// <summary>
	/// This updates whether the dice are sliding to a new position
	/// </summary>
	void UpdateDieSlidingBack()
	{
		for (int i = 0; i < diceToRoll.Length; i++)
		{
			dieSlidingBack[i] = diceToRoll[i].SlidingToNewPosition();
		}
	}

	/// <summary>
	/// This checks to see if the held die should move the positions of the dice already in the holder
	/// </summary>
	void CheckDieNewHolderPositions()
	{

		// This tells wether any of the dice are sliding into a new position
		bool anyDiceSliding = false;

		// This updates whether the dice are sliding to a new position
		UpdateDieSlidingBack();

		for (int i = 0; i < 5; i++)
		{
			if (dieSlidingBack[i])
			{
				anyDiceSliding = true;
			}
		}

		// If the dice positions need to be updated and the dice are not already moving then it moves the dice to new positions
		if (!ListInfoEqual(GetOrderedDice(), diceIndexes) && !anyDiceSliding)
		{
			OrderDice();
			MoveDiceInHolder();
		}
	}

	/// <summary>
	/// This returns the index of the given die in the holder (-1 if it is not in the holder)
	/// </summary>
	/// <param name="die">The die script which index will be returned</param>
	/// <returns></returns>
	public int GetHolderIndex(Die die)
	{
		for (int i = 0; i < 5; i++)
		{
			if (die == diceToRoll[i])
			{
				return diceIndexes.IndexOf(i);
			}

		}
		return -1;
	}

	/// <summary>
	/// This tells whether the given int lists have the same values
	/// </summary>
	/// <param name="list1">The fist list</param>
	/// <param name="list2">The second list</param>
	/// <returns>This returns whether the given int lists have the same values</returns>
	bool ListInfoEqual(List<int> list1, List<int> list2)
	{
		if (list1.Count != list2.Count)
		{
			return false;
		}
		for (int i = 0; i < list1.Count; i++)
		{
			if (list1[i] != list2[i])
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>
	/// This returns the box collider of the holder
	/// </summary>
	/// <returns>This returns the box collider of the holder</returns>
	public BoxCollider2D GetBoxCollider()
	{
		return boxCollider;
	}

	/// <summary>
	/// This moves the die of the given die script into the holder
	/// </summary>
	/// <param name="die">The die script of the die to be put in the holder</param>
	public void MoveDieIntoHolder(Die die)
	{
		for (int i = 0; i < 5; i++)
		{
			if (die == diceToRoll[i])
			{
				if (!diceInHolder[i])
				{

					// Private variables are updated when a die has entered the holder
					diceHeld++;
					diceIndexes.Add(i);
				}
				dieSlidingBackIndex = i;
				diceInHolder[i] = true;
				break;
			}
		}

		// All the dice in the holder are ordered and physically moved to the spots they need to go
		OrderDice();
		MoveDiceInHolder();
	}

	/// <summary>
	/// This moves all dice of the given die script into the holder
	/// </summary>
	/// <param name="die">The die script list of the die to be put in the holder</param>
	public void MoveAllDiceIntoHolder(Die[] dice)
	{

		// This resets the dice indexes list
		diceIndexes = new List<int>();

		// This sets the dice held to the number of dice
		diceHeld = dice.Length;

		// This sets variables telling the dice that they are in the holder
		for (int i = 0; i < dice.Length; i++)
		{

			// This sorts the dice indexes based on the given dice order
			for (int j = 0; j < dice.Length; j++)
			{
				if (diceToRoll[j] == dice[i])
				{
					diceIndexes.Add(j);
				}
			}

			// This sets the dice variables to sliding and in the holder
			diceInHolder[i] = true;
			dieSlidingBack[i] = true;
		}

		// All the dice in the holder are ordered and physically moved to the spots they need to go
		MoveDiceInHolder();
	}

	/// <summary>
	/// This sets the information that tells that a die has started sliding
	/// </summary>
	/// <param name="die">The die script of the die that is sliding</param>
	public void DieSlideStart(Die die)
	{
		for (int i = 0; i < 5; i++)
		{
			if (die == diceToRoll[i])
			{
				dieSlidingBack[i] = true;
			}
		}
	}

	/// <summary>
	/// This sets the information that tells that a die is done sliding
	/// </summary>
	/// <param name="die">The die script of the die that is finished sliding</param>
	public void DieSlideDone(Die die)
	{
		for (int i = 0; i < 5; i++)
		{
			if (die == diceToRoll[i])
			{
				dieSlidingBack[i] = false;
			}
		}
	}

	/// <summary>
	/// This moves the die of the given die script out of the holder
	/// </summary>
	/// <param name="die">The die script of the die to be put in the holder</param>
	void MoveDieOutOfHolder(Die die)
	{
		for (int i = 0; i < 5; i++)
		{
			if (die == diceToRoll[i])
			{
				if (diceInHolder[i])
				{

					// Private variables are updated when a die has entered the holder
					diceHeld--;
					diceIndexes.Remove(i);
				}
				dieSlidingBackIndex = i;
				diceInHolder[i] = false;
				break;
			}
		}

		// All the dice in the holder are ordered and physically moved to the spots they need to go
		OrderDice();
		MoveDiceInHolder();
	}

	/// <summary>
	/// This orders the dice based on horizontal positions and whether the held die has left the holder
	/// </summary>
	void OrderDice()
	{

		// This is the index positions of the dice
		int[] dieIndexes = new int[] { 0, 1, 2, 3, 4 };

		// These are the horizontal positions of the dice
		float[] dieXPositions = new float[5];
		for (int i = 0; i < dieXPositions.Length; i++)
		{
			dieXPositions[i] = diceToRoll[i].DieXPosition();
		}

		// This uses bubble sort to sort the dice by horizontal position and in doing so the index positions
		for (int i = 0; i < dieXPositions.Length; i++)
		{
			for (int j = 0; j < dieXPositions.Length - 1 - i; j++)
			{
				if (dieXPositions[j] > dieXPositions[j + 1])
				{
					float temp = dieXPositions[j];
					dieXPositions[j] = dieXPositions[j + 1];
					dieXPositions[j + 1] = temp;

					int temp2 = dieIndexes[j];
					dieIndexes[j] = dieIndexes[j + 1];
					dieIndexes[j + 1] = temp2;
				}
			}
		}

		// This sorts the diceIndexes in order of horizontal positions if the dice are in the holder
		diceIndexes = new List<int>();
		for (int i = 0; i < dieIndexes.Length; i++)
		{
			if (diceInHolder[dieIndexes[i]])
			{
				this.diceIndexes.Add(dieIndexes[i]);
			}
		}

		// If the held die has left the holder and is sliding back it returns to its original position
		if (diceToRoll[dieSlidingBackIndex].GetDieLastRollIndex() != -1 && diceInHolder[dieSlidingBackIndex] && dieLeftHolder)
		{
			dieLeftHolder = false;
			diceIndexes.Remove(dieSlidingBackIndex);
			diceIndexes.Insert(diceToRoll[dieSlidingBackIndex].GetDieLastRollIndex(), dieSlidingBackIndex);
		}
	}

	/// <summary>
	/// This returns the order of the dice that they should be in based on their horizontal positions
	/// </summary>
	/// <returns>A list of the die indexes in the order they should be in horizontally</returns>
	List<int> GetOrderedDice()
	{

		// This is the index positions of the dice
		List<int> dieIndexes = new List<int> { 0, 1, 2, 3, 4 };

		// These are the horizontal positions of the dice
		float[] dieXPositions = new float[5];
		for (int i = 0; i < dieXPositions.Length; i++)
		{
			dieXPositions[i] = diceToRoll[i].DieXPosition();
		}

		// This uses bubble sort to sort the dice by horizontal position and in doing so the index positions
		for (int i = 0; i < dieXPositions.Length; i++)
		{
			for (int j = 0; j < dieXPositions.Length - 1 - i; j++)
			{
				if (dieXPositions[j] > dieXPositions[j + 1])
				{
					float temp = dieXPositions[j];
					dieXPositions[j] = dieXPositions[j + 1];
					dieXPositions[j + 1] = temp;

					int temp2 = dieIndexes[j];
					dieIndexes[j] = dieIndexes[j + 1];
					dieIndexes[j + 1] = temp2;

				}
			}
		}

		// This removes the horizontal positions and indexes of dice not in the holder
		for (int i = 0; i < dieIndexes.Count; i++)
		{
			if (!diceInHolder[dieIndexes[i]])
			{
				dieIndexes.Remove(dieIndexes[i]);
				i--;
			}
		}

		return dieIndexes;
	}

	/// <summary>
	/// This moves the dice in the holder to the positions they should go to
	/// </summary>
	void MoveDiceInHolder()
	{
		Vector2 transformVector2 = new Vector2(transform.localPosition.x, transform.localPosition.y);
		switch (diceHeld)
		{

			case 1:
				if (!diceToRoll[diceIndexes[0]].BeingDragged())
				{
					diceToRoll[diceIndexes[0]].MoveDie(oneDie + transformVector2, moveDieTime);
				}
				break;
			case 2:
				if (!diceToRoll[diceIndexes[0]].BeingDragged())
				{
					diceToRoll[diceIndexes[0]].MoveDie(twoDice[0] + transformVector2, moveDieTime);
				}
				if (!diceToRoll[diceIndexes[1]].BeingDragged())
				{
					diceToRoll[diceIndexes[1]].MoveDie(twoDice[1] + transformVector2, moveDieTime);
				}
				break;
			case 3:
				if (!diceToRoll[diceIndexes[0]].BeingDragged())
				{
					diceToRoll[diceIndexes[0]].MoveDie(threeDice[0] + transformVector2, moveDieTime);
				}
				if (!diceToRoll[diceIndexes[1]].BeingDragged())
				{
					diceToRoll[diceIndexes[1]].MoveDie(threeDice[1] + transformVector2, moveDieTime);
				}
				if (!diceToRoll[diceIndexes[2]].BeingDragged())
				{
					diceToRoll[diceIndexes[2]].MoveDie(threeDice[2] + transformVector2, moveDieTime);
				}
				break;
			case 4:
				if (!diceToRoll[diceIndexes[0]].BeingDragged())
				{
					diceToRoll[diceIndexes[0]].MoveDie(fourDice[0] + transformVector2, moveDieTime);
				}
				if (!diceToRoll[diceIndexes[1]].BeingDragged())
				{
					diceToRoll[diceIndexes[1]].MoveDie(fourDice[1] + transformVector2, moveDieTime);
				}
				if (!diceToRoll[diceIndexes[2]].BeingDragged())
				{
					diceToRoll[diceIndexes[2]].MoveDie(fourDice[2] + transformVector2, moveDieTime);
				}
				if (!diceToRoll[diceIndexes[3]].BeingDragged())
				{
					diceToRoll[diceIndexes[3]].MoveDie(fourDice[3] + transformVector2, moveDieTime);
				}
				break;
			case 5:
				if (!diceToRoll[diceIndexes[0]].BeingDragged())
				{
					diceToRoll[diceIndexes[0]].MoveDie(fiveDice[0] + transformVector2, moveDieTime);
				}
				if (!diceToRoll[diceIndexes[1]].BeingDragged())
				{
					diceToRoll[diceIndexes[1]].MoveDie(fiveDice[1] + transformVector2, moveDieTime);
				}
				if (!diceToRoll[diceIndexes[2]].BeingDragged())
				{
					diceToRoll[diceIndexes[2]].MoveDie(fiveDice[2] + transformVector2, moveDieTime);
				}
				if (!diceToRoll[diceIndexes[3]].BeingDragged())
				{
					diceToRoll[diceIndexes[3]].MoveDie(fiveDice[3] + transformVector2, moveDieTime);
				}
				if (!diceToRoll[diceIndexes[4]].BeingDragged())
				{
					diceToRoll[diceIndexes[4]].MoveDie(fiveDice[4] + transformVector2, moveDieTime);
				}
				break;
		}
	}

	/// <summary>
	/// This returns the list of Die objects that are held in the roll holder
	/// </summary>
	/// <returns>A list of Die objects in the roll holder</returns>
	public List<Die> DiceToBeRolled()
	{
		List<Die> dice = new List<Die>();
		for (int i = 0; i < 5; i++)
		{
			if (diceInHolder[i])
			{
				dice.Add(diceToRoll[i]);
			}
		}
		return dice;
	}

}