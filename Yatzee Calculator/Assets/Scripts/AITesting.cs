using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AITesting : MonoBehaviour
{

	public TextMeshProUGUI textMeshPro;

	string text;

	// Start is called before the first frame update
	void Start()
	{
		text = "Hello";

		bool[] categories = new bool[] {
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
		};

		int topTotal = 70;

		bool yahtzeeAttained = false;

		ScorecardState scorecard = new ScorecardState(categories, topTotal, yahtzeeAttained);

		text = scorecard.GetScorecardString();

		//text = YahtzeeMaximizeAlgorithm.MaximizeMove(scorecard).GetMoveString();

		// 11.67 - 7.1123 = 4.5577

		CustomFloat sub = CustomFloat.Subtract(new CustomFloat("11.67"), new CustomFloat("7.1123"));
		Debug.Log(sub.GetFloat());
	}

	// Update is called once per frame
	void Update()
	{
		textMeshPro.SetText(text);
	}
}
