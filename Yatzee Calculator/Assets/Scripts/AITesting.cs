using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AITesting : MonoBehaviour
{

	public TextMeshProUGUI textMeshPro;

	string text;

	public string number1;
	public string number2;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		text = CustomFloat.Add(new CustomFloat(number1, 5), new CustomFloat(number2, 5), 5).GetFloatNormal();

		textMeshPro.SetText(text);
	}
}
