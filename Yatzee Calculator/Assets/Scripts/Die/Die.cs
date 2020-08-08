using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{

	/// <summary>
	/// This tells the number on the die
	/// </summary>
	[Header("Die Settings")]
	[Range(1, 6)]
	public int number;

	/// <summary>
	/// This tells whether the die is highlighted or not
	/// </summary>
	public bool isHighlighted;

	/// <summary>
	///This tells whether the die is grayed out
	/// </summary>
	public bool isGrayedOut;

	/// <summary>
	/// This tells whether the die is currently interactable with the user
	/// </summary>
	public bool isInteractable;

	/// <summary>
	/// This tells whether the die is using certain animations
	/// </summary>
	public bool isAnimated;

	/// <summary>
	/// This is the radius of the die where the die will not snap towards the mouse
	/// </summary>
	[Header("Mouse Dragging Die Settings")]
	public float dragRadius;

	/// <summary>
	/// This tells whether the die is currently sliding towards the mouse when snapping to it
	/// </summary>
	bool slidingTowardsMouse;

	/// <summary>
	/// This tells whether the last frame the die was selected
	/// </summary>
	bool dieSelectedLastFrame;

	/// <summary>
	/// This tells how long it takes for the die to snap to the mouse when about to be dragged
	/// </summary>
	public float dragTime;

	/// <summary>
	/// This is the timer for when the die is sliding
	/// </summary>
	float slideTimer;

	/// <summary>
	/// This tells if the die is being dragged
	/// </summary>
	bool alreadyDragging;

	/// <summary>
	/// This tells if the die was just dragged and dropped
	/// </summary>
	bool justDropped;

	/// <summary>
	/// This tells if the die is sliding to a new position
	/// </summary>
	bool slidingToNewPosition;

	/// <summary>
	/// This is how long it takes for the die to slide to the new position
	/// </summary>
	public float slideToNewPositionTime;

	/// <summary>
	/// This is the timer for when the die is sliding to a new position
	/// </summary>
	float slideToNewPositionTimer;

	/// <summary>
	/// This is the original position of the die before being dragged
	/// </summary>
	Vector2 originalPosition;

	/// <summary>
	/// This is the position the die will be moving towards after letting go of being dragged
	/// </summary>
	Vector2 movingToPosition;

	/// <summary>
	/// This is the old position of the die right before it is let go of being dragged
	/// </summary>
	Vector2 oldPosition;

	/// <summary>
	/// This is the sprite renderer for the die
	/// </summary>
	SpriteRenderer spriteRenderer;

	/// <summary>
	/// This is the sprite renderer for the die highlight
	/// </summary>
	SpriteRenderer dieHighlightSpriteRenderer;

	/// <summary>
	/// This is the list of sprites for each of the six die numbers
	/// </summary>
	[Header("Dice Sprites")]
	public Sprite[] sprites;

	/// <summary>
	/// This is the sprite that goes on top of the die when it is highlighted
	/// </summary>
	public Sprite highlightSprite;

	/// <summary>
	/// This is the box collider for the die
	/// </summary>
	BoxCollider2D boxCollider;

	/// <summary>
	/// The holder script for the dice holder
	/// </summary>
	[Header("Holders")]
	public DiceToHold diceHolder;

	/// <summary>
	/// The holder script for the dice to roll
	/// </summary>
	public DiceToRoll diceRollHolder;

	/// <summary>
	/// The index of the die in the roll holder when the game starts
	/// </summary>
	public int rollHolderIndexStart;

	/// <summary>
	/// This tells whether the die is allowed to enter the holder
	/// </summary>
	bool canEnterHolder;

	/// <summary>
	/// This is the magnitude of the shake when the die is dragged to a spot where it can not be placed
	/// </summary>
	[Header("Invalid New Spot Shake Settings")]
	public float shakeMagnitude;

	/// <summary>
	/// This is the damping value for when the die is shaked after being dragged to a spot where it can not be placed
	/// </summary>
	public float dampingValue;

	/// <summary>
	/// This is the number of sinusoidal cycles the die shakes after being dragged to a spot where it can not be placed
	/// </summary>
	public int cycles;

	/// <summary>
	/// This tells whether the die is shaking whil being dragged to a spot where it can not be placed
	/// </summary>
	bool isShaking;

	/// <summary>
	/// This tells if the die is shaking when being randomly rolled
	/// </summary>
	bool isShakingRoll;

	/// <summary>
	/// This is the time the die shakes when being randomly rolled
	/// </summary>
	float shakeTime;

	/// <summary>
	/// This is the magnitude the die shakes when randomly being rolled
	/// </summary>
	[Header("Roll Shake Settings")]
	public float rollShakeMagnitude;

	/// <summary>
	/// This is the damping factor when the die shakes when randomly being rolled
	/// </summary>
	public float rollDampingValue;

	/// <summary>
	/// This is the number of sinusoidal cycles the die shakes after being rolled
	/// </summary>
	public int rollCycles;

	/// <summary>
	/// This is the timer for when the die is being shaked
	/// </summary>
	float shakeTimer;

	/// <summary>
	/// This tells whether the die was placed in a valid spot
	/// </summary>
	bool validSpot;

	/// <summary>
	/// This is the time is takes the die to roll after being rolled
	/// </summary>
	public float rollShakeTime;

	/// <summary>
	/// This tells whether the die is currently in the die holder section
	/// </summary>
	bool dieInHolder;

	/// <summary>
	/// This tells whether the die is currently in the roll die section
	/// </summary>
	bool dieInRollHolder;

	/// <summary>
	/// This is the index of the holder this die was last in
	/// </summary>
	int indexInHolder;

	/// <summary>
	/// This is the index of the holder this die was last in
	/// </summary>
	int indexInRollHolder;

	/// <summary>
	/// When the die is created it initializes variables and visually updates the die
	/// </summary>
	void Start()
	{
		Initialize();
		UpdateDie();
	}

	/// <summary>
	/// This is called every frame to update animations and interactivity with the die
	/// </summary>
	void Update()
	{
		RollKeyPressed();
		MouseClicked();
		NumberKeyPressed();
		MouseDragging();
		UpdateSlideTowardsMouse();
		UpdateSlideToNewPosition();
		UpdateShakeWhenSliding();
		UpdateShakeWhenRolling();
	}

	/// <summary>
	/// This updates the die's slide towards the mouse if needed
	/// </summary>
	void UpdateSlideTowardsMouse()
	{
		if (slidingTowardsMouse)
		{
			slideTimer += Time.deltaTime;
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			UpdateSlideDie(dragTime, slideTimer, originalPosition, mousePosition, transform);
			slidingTowardsMouse = dragTime >= slideTimer;
		}
	}

	/// <summary>
	/// This updates the die's slide towards a new position if needed
	/// </summary>
	void UpdateSlideToNewPosition()
	{
		if (slidingToNewPosition)
		{
			slideToNewPositionTimer += Time.deltaTime;
			UpdateSlideDie(slideToNewPositionTime, slideToNewPositionTimer, oldPosition, movingToPosition, transform);
			slidingToNewPosition = slideToNewPositionTime >= slideToNewPositionTimer;
			isInteractable = slideToNewPositionTime < slideToNewPositionTimer;
			if (slideToNewPositionTime < slideToNewPositionTimer)
			{
				diceHolder.DieSlideDone(this);
				diceRollHolder.DieSlideDone(this);
			}
		}
	}

	/// <summary>
	/// This updates the die's shake while sliding towards a new position if needed
	/// </summary>
	void UpdateShakeWhenSliding()
	{
		if (isShaking)
		{
			shakeTimer += Time.deltaTime;
			UpdateDieShake(shakeTime, shakeTimer, shakeMagnitude, dampingValue, cycles, transform);
			isShaking = shakeTime >= shakeTimer;
		}
	}

	/// <summary>
	/// This updates the die's shake while rolling if needed
	/// </summary>
	void UpdateShakeWhenRolling()
	{
		if (isShakingRoll)
		{
			shakeTimer += Time.deltaTime;
			UpdateDieShake(rollShakeTime, shakeTimer, rollShakeMagnitude, rollDampingValue, rollCycles, transform);
			isShaking = rollShakeTime >= shakeTimer;
		}
	}

	/// <summary>
	/// This initializes private variables and gets the needed components
	/// </summary>
	void Initialize()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		boxCollider = GetComponent<BoxCollider2D>();
		dieHighlightSpriteRenderer = transform.Find("DieHighlight").GetComponent<SpriteRenderer>();
		alreadyDragging = false;
		dieSelectedLastFrame = false;
		slidingToNewPosition = false;
		isShaking = false;
		dieInHolder = false;
		dieInRollHolder = true;
		originalPosition = transform.localPosition;
		movingToPosition = transform.localPosition;
		indexInHolder = -1;
		indexInRollHolder = rollHolderIndexStart;
	}

	/// <summary>
	/// This visually updates the die's appearance changing its sprite, whether it is highlighted or if is is grayed out
	/// </summary>
	public void UpdateDie()
	{
		spriteRenderer.sprite = sprites[number - 1];
		dieHighlightSpriteRenderer.enabled = isHighlighted;
		if (isHighlighted)
		{
			spriteRenderer.sortingOrder = 3;
			dieHighlightSpriteRenderer.sortingOrder = 4;
		}
		else
		{
			spriteRenderer.sortingOrder = 1;
			dieHighlightSpriteRenderer.sortingOrder = 2;
		}
		if (isGrayedOut)
		{
			spriteRenderer.color = new Color(1, 1, 1, .5f);
			dieHighlightSpriteRenderer.color = new Color(1, 1, 1, .5f);
		}
		else
		{
			spriteRenderer.color = new Color(1, 1, 1, 1);
			dieHighlightSpriteRenderer.color = new Color(1, 1, 1, 1);
		}
	}

	/// <summary>
	/// This returns whether the die is in the roll holder
	/// </summary>
	/// <returns>This returns whether the die is in the roll holder</returns>
	public bool DieInRollHolder()
	{
		return dieInRollHolder;
	}

	/// <summary>
	/// This returns whether the die is in the hold holder
	/// </summary>
	/// <returns>This returns whether the die is in the hold holder</returns>
	public bool DieInHolder()
	{
		return dieInHolder;
	}

	/// <summary>
	/// This rolls the die to a random number and animates the shake if animatable is true
	/// </summary>
	public void RollDie()
	{
		number = Random.Range(1, 7);
		if (isAnimated)
		{
			isShakingRoll = true;
			shakeTimer = 0;
			shakeTime = rollShakeTime;
		}
		UpdateDie();
	}

	/// <summary>
	/// This sets the die number the given integer [1, 6] and updates it
	/// </summary>
	/// <param name="number">The number of the die [1, 6]</param>
	public void SetDie(int number)
	{
		this.number = IntRange(number, 1, 6);
		UpdateDie();
	}

	/// <summary>
	/// This sets the given int number in between the given max and min range
	/// </summary>
	/// <param name="number">The given int value</param>
	/// <param name="min">The minimum the int can be</param>
	/// <param name="max">The maximum the int can be</param>
	/// <returns>The number if it is in the range and the min/max if the int is out of the range</returns>
	int IntRange(int number, int min, int max)
	{
		number = Mathf.Max(number, min);
		return Mathf.Min(number, max);
	}

	/// <summary>
	/// This sets the given float number in between the given max and min range
	/// </summary>
	/// <param name="number">The given float value</param>
	/// <param name="min">The minimum the float can be</param>
	/// <param name="max">The maximum the float can be</param>
	/// <returns>The number if it is in the range and the min/max if the float is out of the range</returns>
	float FloatRange(float number, float min, float max)
	{
		number = Mathf.Max(number, min);
		return Mathf.Min(number, max);
	}

	/// <summary>
	/// This increments the die or resets the die back to 1 if it has reached the max of 6 and upates it
	/// </summary>
	public void IncrementDie()
	{
		number = number % 6 + 1;
		UpdateDie();
	}

	/// <summary>
	/// This decrements the die or resets the die back to 6 if it has reached the min of 1 and upates it
	/// </summary>
	public void DecrementDie()
	{
		number = number % 6 - 1;
		UpdateDie();
	}

	/// <summary>
	/// This changes the die sprites
	/// </summary>
	/// <param name="sprites">The list of dies sprites (There should be 6 sprites, one for each die number)</param>
	public void SetDieSprites(Sprite[] sprites)
	{
		this.sprites = sprites;
		UpdateDie();
	}

	/// <summary>
	/// This sets the die to be highlighted or not
	/// </summary>
	/// <param name="isHighlighted">Tells whether the die should be highlighted</param>
	public void SetDieHighlight(bool isHighlighted)
	{
		this.isHighlighted = isHighlighted;
		UpdateDie();
	}

	/// <summary>
	/// This sets the die to be grayed out or not
	/// </summary>
	/// <param name="isGrayedOut">Tells whether the die should be grayed out</param>
	public void SetDieGrayOut(bool isGrayedOut)
	{
		this.isGrayedOut = isGrayedOut;
		UpdateDie();
	}

	/// <summary>
	/// This sets whether the die is interactive with the user
	/// </summary>
	/// <param name="isInteractable">Tells whether the die should be able to be interacted with the user</param>
	public void SetDieInteractability(bool isInteractable)
	{
		this.isInteractable = isInteractable;
		UpdateDie();
	}

	/// <summary>
	/// This rolls the die if the 'R' key is pressed and it is highlighted
	/// </summary>
	void RollKeyPressed()
	{
		if (Input.GetKeyDown(KeyCode.R) && isHighlighted)
		{
			RollDie();
		}
	}

	/// <summary>
	/// This sets the die if to the given number pressed and it is highlighted
	/// </summary>
	void NumberKeyPressed()
	{
		if (isHighlighted)
		{
			for (int i = 0; i < 6; i++)
			{
				string key = "[1][2][3][4][5][6]";
				string key2 = "123456";
				if (Input.GetKeyDown(key.Substring(i * 3, 3)))
				{
					SetDie(int.Parse(key.Substring(i * 3 + 1, 1)));
				}
				if (Input.GetKeyDown(key2.Substring(i, 1)))
				{
					SetDie(int.Parse(key2.Substring(i, 1)));
				}
			}

		}
	}

	/// <summary>
	/// This updates activities with the mouse
	/// </summary>
	void MouseClicked()
	{

		// This updates the die if the mouse is clicked
		if (Input.GetMouseButtonDown(0))
		{

			// If the die is sliding to a new position the die is not interactable with the mouse
			if (slidingToNewPosition)
			{
				isHighlighted = false;
				UpdateDie();
			}

			// If the die is clicked by the mouse and it is interactable it becomes highlighted and says that the die is selected
			if (boxCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
			{
				if (isInteractable)
				{
					if (!isHighlighted)
					{
						SetDieHighlight(true);
					}
					else
					{
						dieSelectedLastFrame = true;
					}
				}
			}

			// This sets the highlight of the die to false if the mouse clicks away from the die
			else
			{
				SetDieHighlight(false);
			}
		}

		// when the mouse click is released it increments the die if if was just selected and not being dragged
		else if (Input.GetMouseButtonUp(0))
		{
			if (dieSelectedLastFrame)
			{
				if (!alreadyDragging)
				{
					IncrementDie();
				}
				dieSelectedLastFrame = false;
			}
		}
	}

	/// <summary>
	/// This updates activities with the mouse if the die is being dragged
	/// </summary>
	void MouseDragging()
	{
		// If the mouse is clicked and highlighted and not already moving to a new position it enters this if statement
		if (Input.GetMouseButton(0) && isHighlighted && !slidingToNewPosition)
		{
			// This sets the mouse point to the world point
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			// If the mouse is dragged a certain distance away from the die then the die follow the mouse
			if (DistanceSquared(mousePosition, originalPosition) > Mathf.Pow(dragRadius, 2) || alreadyDragging)
			{
				if (!alreadyDragging)
				{
					indexInHolder = diceHolder.GetHolderIndex(this);
					indexInRollHolder = diceRollHolder.GetHolderIndex(this);
					StartSlideToMouse();
				}
				if (!slidingTowardsMouse)
				{
					transform.localPosition = new Vector3(mousePosition.x, mousePosition.y, 0);
				}
				alreadyDragging = true;
			}
		}

		// If the draggin has stopped it moves the die to its needed position
		else if (Input.GetMouseButtonUp(0) && alreadyDragging)
		{
			alreadyDragging = false;
			CheckHolders();
		}

		// If anything else happens the dragging just stops
		else
		{
			alreadyDragging = false;
		}
	}

	/// <summary>
	/// This controls the movement of the die in the holder when hovering the die in and out of the holder (jank)
	/// </summary>
	void CheckHolders()
	{

		// This moves the die if it is already in the holder
		if (canEnterHolder && boxCollider.bounds.Intersects(diceHolder.GetBoxCollider().bounds))
		{
			dieInHolder = true;
			dieInRollHolder = false;
			diceHolder.MoveDieIntoHolder(this);
		}

		// This moves the die if it is already in the roll holder
		else if (boxCollider.bounds.Intersects(diceRollHolder.GetBoxCollider().bounds))
		{
			dieInRollHolder = true;
			dieInHolder = false;
			diceRollHolder.MoveDieIntoHolder(this);
		}

		// This moves the die back to its original position and shakes
		else
		{

			// If the die needs to return to a holder then it updates the holder
			if (dieInHolder)
			{
				diceHolder.DieSlideStart(this);
				diceHolder.MoveDieIntoHolder(this);
			}
			else if (dieInRollHolder)
			{
				diceRollHolder.DieSlideStart(this);
				diceRollHolder.MoveDieIntoHolder(this);
			}

			// This starts the animation for the die movement
			MoveDie(originalPosition, slideToNewPositionTime);
			isShaking = true;
			shakeTimer = 0;
			shakeTime = slideToNewPositionTime;
		}
	}

	/// <summary>
	/// This moves the die out of the roll holder by setting dieInRollHolder to false
	/// </summary>
	public void MoveDieOutOfRollHolder()
	{
		dieInRollHolder = false;
	}

	/// <summary>
	/// This gives whether the die was just dropped
	/// </summary>
	/// <returns>This returns whether the die was just dropped</returns>
	public bool DieDropped()
	{
		return justDropped;
	}

	/// <summary>
	/// This sets whether the die was just dropped
	/// </summary>
	/// <param name="dieDropped">This tells whether the die was just dropped</param>
	public void SetDieDropped(bool dieDropped)
	{
		this.justDropped = dieDropped;
	}

	/// <summary>
	/// This finds the distance squared between two 2D points
	/// </summary>
	/// <param name="point1">One point</param>
	/// <param name="point2">The other point</param>
	/// <returns>The distance squared between the two points</returns>
	float DistanceSquared(Vector2 point1, Vector2 point2)
	{
		return Mathf.Pow((point1.x - point2.x), 2) + Mathf.Pow((point1.y - point2.y), 2);
	}

	/// <summary>
	/// This starts the slide animation of the die towards the mouse when being dragged
	/// </summary>
	void StartSlideToMouse()
	{
		slideTimer = 0;
		originalPosition = movingToPosition;
		slidingTowardsMouse = true;
	}

	/// <summary>
	/// This starts the slide animation of the die to the new spot
	/// </summary>
	void StartSlideToNewSpot()
	{
		isInteractable = false;
		slideToNewPositionTimer = 0;
		oldPosition = transform.localPosition;
		slidingToNewPosition = true;
		isShaking = !validSpot;
		shakeTimer = 0;
		shakeTime = slideToNewPositionTime;
	}

	/// <summary>
	/// Move an object from one point to another with two even sections of easing with constant acceleration
	/// </summary>
	/// <param name="totalTime">The time length the animation takes</param>
	/// <param name="currentTime">the time since the start of the animation</param>
	/// <param name="startPosition ">The position at the start of the animation</param>
	/// <param name="endPosition ">The position at the end of the animation</param>
	/// <param name="objectTransform ">The transform component of the object to be moved</param>
	void UpdateSlideDie(float totalTime, float currentTime, Vector2 startPosition, Vector2 endPosition, Transform objectTransform)
	{

		// This sets the position to the end position if the current time is greater than or equal to the total time
		if (currentTime >= totalTime)
		{
			objectTransform.localPosition = new Vector2(endPosition.x, endPosition.y);
		}
		else
		{
			// The following are the physics variables:
			// x = ending position
			// x0 = starting position
			// v = final velocity
			// v0 = initial velocity
			// a = acceleration
			// t = time
			// v = vertical
			// h = horizontal
			// 2 is used to distinguish the second phase variables
			// The following physics equations were used to define the variables
			// x = x0 + v0t + .5a(t^2)
			// v = v0 + at

			// First half of the slide horizontal
			float x0h = startPosition.x;
			float ah = 4 * (endPosition.x - startPosition.x) / Mathf.Pow(totalTime, 2);
			float t = currentTime;

			// First half of the slide vertical
			float x0v = startPosition.y;
			float av = 4 * (endPosition.y - startPosition.y) / Mathf.Pow(totalTime, 2);

			// Second half of the slide horizontal
			float x20h = (endPosition.x + startPosition.x) / 2;
			float v20h = 4 * (endPosition.x - startPosition.x) / Mathf.Pow(totalTime, 2) * (totalTime / 2);
			float a2h = -4 * (endPosition.x - startPosition.x) / Mathf.Pow(totalTime, 2);
			float t2 = currentTime - totalTime / 2;

			// Second half of the slide vertical
			float x20v = (endPosition.y + startPosition.y) / 2;
			float v20v = 4 * (endPosition.y - startPosition.y) / Mathf.Pow(totalTime, 2) * (totalTime / 2);
			float a2v = -4 * (endPosition.y - startPosition.y) / Mathf.Pow(totalTime, 2);

			Vector2 position;
			if (currentTime <= totalTime / 2)
			{

				// This sets the position in the first phase while it is speeding up
				position.x = x0h + .5f * ah * Mathf.Pow(t, 2);
				position.y = x0v + .5f * av * Mathf.Pow(t, 2);
			}
			else
			{

				// This sets the position in the second phase while it is slowing down
				position.x = x20h + v20h * t2 + .5f * a2h * Mathf.Pow(t2, 2);
				position.y = x20v + v20v * t2 + .5f * a2v * Mathf.Pow(t2, 2);
			}
			transform.localPosition = position;
		}
	}

	/// <summary>
	/// This gives the localPosition a soldier should be placed in the array
	/// </summary>
	/// <param name="shakeTime">The total time for the object to shake</param>
	/// <param name="currentTime">The instantaneous time in the animation</param>
	/// <param name="shakeMagnitude">The amplitude of the shake</param>
	/// <param name="dampingFactor">How much the shaking damps (greater damping factor is a greater damp)</param>
	/// <param name="cycles">How many sinusoidal cycles the animation takes</param>
	/// <param name="objectTransform">The transform of the object to be shaked</param>
	void UpdateDieShake(float shakeTime, float currentTime, float shakeMagnitude, float dampingFactor, int cycles, Transform objectTransform)
	{
		// This sets the angle based on the damping, shake magnitude, and current time in the animation
		float ratio = currentTime / shakeTime;
		float angle = shakeMagnitude * Mathf.Sin(cycles * 2 * Mathf.PI * ratio) * Mathf.Exp(-dampingFactor * ratio);

		// when the animation is over this rotates the object back to an angle 0
		if (currentTime >= shakeTime)
		{
			angle = 0;
		}
		objectTransform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
	}

	/// <summary>
	/// This slides the die from its position to its new position over time
	/// </summary>
	/// <param name="position">The new position of the die</param>
	/// <param name="time">The time it takes to move the die</param>
	public void MoveDie(Vector2 position, float time)
	{
		slideToNewPositionTime = time;
		slideToNewPositionTimer = 0;
		oldPosition = transform.localPosition;
		movingToPosition = position;
		originalPosition = movingToPosition;
		slidingToNewPosition = true;
	}

	/// <summary>
	/// This returns the local x position of this die
	/// </summary>
	/// <returns>The local x position of the game object</returns>
	public float DieXPosition()
	{
		return transform.localPosition.x;
	}

	/// <summary>
	/// This returns the box collider component of the die
	/// </summary>
	/// <returns>This returns the box collider of the die</returns>
	public BoxCollider2D GetDieBoxCollider()
	{
		return boxCollider;
	}

	/// <summary>
	/// This sets the index of the die while it was in the holder
	/// </summary>
	/// <param name="index">The index of the die in the holder</param>
	public void SetDieLastIndex(int index)
	{
		indexInHolder = index;
	}

	/// <summary>
	/// This gets the index of the die while it was in the holder
	/// </summary>
	/// <returns>This returns the index of the die in the holder and otherwise -1</returns>
	public int GetDieLastIndex()
	{
		return indexInHolder;
	}

	/// <summary>
	/// This sets the index of the die while it was in the holder
	/// </summary>
	/// <param name="index">The index of the die in the holder</param>
	public void SetDieLastRollIndex(int index)
	{
		indexInRollHolder = index;
	}

	/// <summary>
	/// This gets the index of the die while it was in the holder
	/// </summary>
	/// <returns>This returns the index of the die in the holder and otherwise -1</returns>
	public int GetDieLastRollIndex()
	{
		return indexInRollHolder;
	}

	/// <summary>
	/// This returns whether the die is being dragged by the mouse
	/// </summary>
	/// <returns>This returns wether the die is being dragged by the mouse</returns>
	public bool BeingDragged()
	{
		return alreadyDragging;
	}

	/// <summary>
	/// This returns whether the die is sliding towards a new position
	/// </summary>
	/// <returns>This returns whether the die is sliding towards a new position</returns>
	public bool SlidingToNewPosition()
	{
		return slidingToNewPosition;
	}

	/// <summary>
	/// This sets whether the die is able to enter the holder
	/// </summary>
	/// <param name="canEnter">Whether the die can enter the holder</param>
	public void SetIfDieCanEnterHolder(bool canEnter)
	{
		canEnterHolder = canEnter;
	}
}