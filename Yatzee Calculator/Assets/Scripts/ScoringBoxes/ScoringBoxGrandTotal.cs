using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringBoxGrandTotal : ScoreCardBox
{

	public ScoringBoxTopTotal topTotal;
	public ScoringBoxBottomTotal bottomTotal;

	// Start is called before the first frame update
	void Start()
	{
		Initialize();
	}


	protected override void UpdateInformation()
	{
		if (!boxFilledIn)
		{
			if (topTotal.IsBoxFilledIn() && bottomTotal.IsBoxFilledIn())
			{
				score = GetPoints();
				boxFilledIn = true;
				SetIfTextGrayedOut(false);
				SetIfBoxSelcted(false);
			}
			textMeshPro.SetText(YahtzeeScoring.GrandTotal(topTotal.GetPoints(), bottomTotal.GetPoints()).ToString());
		}
	}

	protected override bool ShouldBoxBeFilledIn()
	{
		return false;
	}

	public override int GetPoints()
	{
		return YahtzeeScoring.GrandTotal(topTotal.GetScore(), bottomTotal.GetScore());
	}

	// Update is called once per frame
	void Update()
	{
		UpdateInformation();
		CheckForEnterKey();
		CheckForMouseClick();
	}
}
