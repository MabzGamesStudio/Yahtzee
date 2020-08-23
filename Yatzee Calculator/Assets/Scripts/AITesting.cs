using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AITesting : MonoBehaviour
{

	public TextMeshProUGUI textMeshPro;

	string text;

	[Range(1, 30)]
	public int significantDigits;
	public string number1;
	public string number2;

	// Start is called before the first frame update
	void Start()
	{
		//text = CustomFloat.Divide(new CustomFloat("30", 2), new CustomFloat("30.01", 4), 10).GetFloatNormal();
	}

	// Update is called once per frame
	void Update()
	{
		text = CustomFloat.Divide(new CustomFloat(number1, significantDigits), new CustomFloat(number2, significantDigits), significantDigits).GetFloatNormal();

		textMeshPro.SetText(text);
	}
}
