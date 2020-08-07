using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringBoxTopTotal : ScoreCardBox
{

	public ScoringBoxAces aces;
	public ScoringBoxTwos twos;
	public ScoringBoxThrees threes;
	public ScoringBoxFours fours;
	public ScoringBoxFives fives;
	public ScoringBoxSixes sixes;
	public ScoringBoxBonus bonus;

	// Start is called before the first frame update
	void Start()
	{
		Initialize();
	}


	protected override void UpdateInformation()
	{
		if (!boxFilledIn)
		{
			if (bonus.IsBoxFilledIn())
			{
				score = GetPoints();
				boxFilledIn = true;
				SetIfTextGrayedOut(false);
				SetIfBoxSelcted(false);
			}

			textMeshPro.SetText(YahtzeeScoring.TopTotal(aces.GetScore(), twos.GetScore(), threes.GetScore(), fours.GetScore(), fives.GetScore(), sixes.GetScore(), bonus.GetScore()).ToString());
		}
	}

	protected override bool ShouldBoxBeFilledIn()
	{
		return false;
	}

	public override int GetPoints()
	{
		return YahtzeeScoring.TopTotal(aces.GetScore(), twos.GetScore(), threes.GetScore(), fours.GetScore(), fives.GetScore(), sixes.GetScore(), bonus.GetScore());
	}

	// Update is called once per frame
	void Update()
	{
		UpdateInformation();
		CheckForEnterKey();
		CheckForMouseClick();
	}
}
