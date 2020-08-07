using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringBoxBottomTotal : ScoreCardBox
{

	public ScoringBoxThreeOfAKind threeOfAKind;
	public ScoringBoxFourOfAKind fourOfAKind;
	public ScoringBoxFullHouse fullHouse;
	public ScoringBoxSmallStraight smallStraight;
	public ScoringBoxLargeStraight largeStraight;
	public ScoringBoxYahtzee yahtzee;
	public ScoringBoxChance chance;

	// Start is called before the first frame update
	void Start()
	{
		Initialize();
	}

	protected override void UpdateInformation()
	{
		if (!boxFilledIn)
		{
			if (threeOfAKind.IsBoxFilledIn() && fourOfAKind.IsBoxFilledIn() && fullHouse.IsBoxFilledIn() && smallStraight.IsBoxFilledIn() && largeStraight.IsBoxFilledIn() && yahtzee.IsBoxFilledIn() && chance.IsBoxFilledIn())
			{
				score = GetPoints();
				boxFilledIn = true;
				SetIfTextGrayedOut(false);
				SetIfBoxSelcted(false);
			}
			textMeshPro.SetText(YahtzeeScoring.BottomTotal(threeOfAKind.GetScore(), fourOfAKind.GetScore(), fullHouse.GetScore(), smallStraight.GetScore(), largeStraight.GetScore(), yahtzee.GetScore(), chance.GetScore()).ToString());
		}
	}

	protected override bool ShouldBoxBeFilledIn()
	{
		return false;
	}

	public override int GetPoints()
	{
		return YahtzeeScoring.BottomTotal(threeOfAKind.GetScore(), fourOfAKind.GetScore(), fullHouse.GetScore(), smallStraight.GetScore(), largeStraight.GetScore(), yahtzee.GetScore(), chance.GetScore());
	}

	// Update is called once per frame
	void Update()
	{
		UpdateInformation();
		CheckForEnterKey();
		CheckForMouseClick();
	}
}
