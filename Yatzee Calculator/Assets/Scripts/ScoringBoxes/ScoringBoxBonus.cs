using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringBoxBonus : ScoreCardBox
{

	public ScoringBoxAces aces;
	public ScoringBoxTwos twos;
	public ScoringBoxThrees threes;
	public ScoringBoxFours fours;
	public ScoringBoxFives fives;
	public ScoringBoxSixes sixes;

	// Start is called before the first frame update
	void Start()
	{
		Initialize();
	}

	protected override void UpdateInformation()
	{
		if (!boxFilledIn)
		{
			if (aces.IsBoxFilledIn() && twos.IsBoxFilledIn() && threes.IsBoxFilledIn() && fours.IsBoxFilledIn() && fives.IsBoxFilledIn() && sixes.IsBoxFilledIn())
			{
				score = GetPoints();
				boxFilledIn = true;
				SetIfTextGrayedOut(false);
				SetIfBoxSelcted(false);
			}
			textMeshPro.SetText(YahtzeeScoring.Bonus(aces.GetScore(), twos.GetScore(), threes.GetScore(), fours.GetScore(), fives.GetScore(), sixes.GetScore()).ToString());
		}
	}

	protected override bool ShouldBoxBeFilledIn()
	{
		return false;
	}

	public override int GetPoints()
	{
		return YahtzeeScoring.Bonus(aces.GetScore(), twos.GetScore(), threes.GetScore(), fours.GetScore(), fives.GetScore(), sixes.GetScore());
	}

	// Update is called once per frame
	void Update()
	{
		UpdateInformation();
		CheckForEnterKey();
		CheckForMouseClick();
	}
}
