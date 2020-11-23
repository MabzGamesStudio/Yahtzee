using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorecardDataElement
{
	// Should be 20 in length
	bool[] binaryScorecardState;

	// Should be 4 x significant digits long
	bool[] value;


	public ScorecardDataElement(string scorecardState, string value)
	{

		int intScorecardState = int.Parse(scorecardState);

		int binarySubtractor = 524288;

		binaryScorecardState = new bool[20];
		for (int i = 0; i < 20; i++)
		{
			if (intScorecardState > binarySubtractor)
			{
				intScorecardState -= binarySubtractor;
				binaryScorecardState[i] = true;
			}
			else
			{
				binaryScorecardState[i] = true;
			}
			intScorecardState /= 2;
		}

		if (value.IndexOf(".") != -1)
		{
			value = value.Substring(0, 1) + value.Substring(2, value.Length - 2);
		}

		this.value = new bool[4 * value.Length];

		for (int i = 0; i < value.Length; i++)
		{
			switch (value.Substring(i, 1))
			{
				case "0":
					this.value[4 * i] = false;
					this.value[4 * i + 1] = false;
					this.value[4 * i + 2] = false;
					this.value[4 * i + 3] = false;
					break;
				case "1":
					this.value[4 * i] = false;
					this.value[4 * i + 1] = false;
					this.value[4 * i + 2] = false;
					this.value[4 * i + 3] = true;
					break;
				case "2":
					this.value[4 * i] = false;
					this.value[4 * i + 1] = false;
					this.value[4 * i + 2] = true;
					this.value[4 * i + 3] = false;
					break;
				case "3":
					this.value[4 * i] = false;
					this.value[4 * i + 1] = false;
					this.value[4 * i + 2] = true;
					this.value[4 * i + 3] = true;
					break;
				case "4":
					this.value[4 * i] = false;
					this.value[4 * i + 1] = true;
					this.value[4 * i + 2] = false;
					this.value[4 * i + 3] = false;
					break;
				case "5":
					this.value[4 * i] = false;
					this.value[4 * i + 1] = true;
					this.value[4 * i + 2] = false;
					this.value[4 * i + 3] = true;
					break;
				case "6":
					this.value[4 * i] = false;
					this.value[4 * i + 1] = true;
					this.value[4 * i + 2] = true;
					this.value[4 * i + 3] = false;
					break;
				case "7":
					this.value[4 * i] = false;
					this.value[4 * i + 1] = true;
					this.value[4 * i + 2] = true;
					this.value[4 * i + 3] = true;
					break;
				case "8":
					this.value[4 * i] = true;
					this.value[4 * i + 1] = false;
					this.value[4 * i + 2] = false;
					this.value[4 * i + 3] = false;
					break;
				case "9":
					this.value[4 * i] = true;
					this.value[4 * i + 1] = false;
					this.value[4 * i + 2] = false;
					this.value[4 * i + 3] = true;
					break;
				case "e":
					this.value[4 * i] = true;
					this.value[4 * i + 1] = false;
					this.value[4 * i + 2] = true;
					this.value[4 * i + 3] = false;
					break;
				default:
					Debug.LogError("The given value isn't in the correct format");
					this.value = null;
					binaryScorecardState = null;
					return;
			}
		}


	}

	public ScorecardDataElement(bool[] scorecardState, bool[] value)
	{
		if (scorecardState.Length != 20)
		{
			Debug.LogError("ScorecardDataElement has been given a scorecard state with invalid size (It should be bool[20] and it isn't)");
			return;
		}
		if (value.Length % 4 != 0)
		{
			Debug.LogError("ScorecardDataElement has been given a value with invalid size (It should be bool[4 * CustomFloatLength] and it isn't)");
			return;
		}
		this.binaryScorecardState = scorecardState;
		this.value = value;
	}

	public string GetValueString()
	{

		Debug.Log(value.Length);

		string valueString = "";
		for (int i = 0; i < value.Length / 4; i++)
		{
			if (!value[i * 4] && !value[i * 4 + 1] && !value[i * 4 + 2] && !value[i * 4 + 3])
			{
				valueString += "0";
			}
			else if (!value[i * 4] && !value[i * 4 + 1] && !value[i * 4 + 2] && value[i * 4 + 3])
			{
				valueString += "1";
			}
			else if (!value[i * 4] && !value[i * 4 + 1] && value[i * 4 + 2] && !value[i * 4 + 3])
			{
				valueString += "2";
			}
			else if (!value[i * 4] && !value[i * 4 + 1] && value[i * 4 + 2] && value[i * 4 + 3])
			{
				valueString += "3";
			}
			else if (!value[i * 4] && value[i * 4 + 1] && !value[i * 4 + 2] && !value[i * 4 + 3])
			{
				valueString += "4";
			}
			else if (!value[i * 4] && value[i * 4 + 1] && !value[i * 4 + 2] && value[i * 4 + 3])
			{
				valueString += "5";
			}
			else if (!value[i * 4] && value[i * 4 + 1] && value[i * 4 + 2] && !value[i * 4 + 3])
			{
				valueString += "6";
			}
			else if (!value[i * 4] && value[i * 4 + 1] && value[i * 4 + 2] && value[i * 4 + 3])
			{
				valueString += "7";
			}
			else if (value[i * 4] && !value[i * 4 + 1] && !value[i * 4 + 2] && !value[i * 4 + 3])
			{
				valueString += "8";
			}
			else if (value[i * 4] && !value[i * 4 + 1] && !value[i * 4 + 2] && value[i * 4 + 3])
			{
				valueString += "8";
			}
			else if (value[i * 4] && !value[i * 4 + 1] && value[i * 4 + 2] && !value[i * 4 + 3])
			{
				valueString += "e";
			}
			else
			{
				valueString += "?";
			}
		}
		return valueString;
	}

	public bool[] GetValueBoolArray()
	{
		return value;
	}

}
