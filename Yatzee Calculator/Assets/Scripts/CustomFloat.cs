using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFloat
{

	/// <summary>
	/// The digits of the float where -1 represents the decimal
	/// </summary>
	string number;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="digits"></param>
	public CustomFloat(string number)
	{
		this.number = number;
	}

	/// <summary>
	/// This returns the CustomFloat number in string form
	/// </summary>
	/// <returns></returns>
	public string GetFloat()
	{
		return number;
	}

	/// <summary>
	/// This adds the two given CustomFloats
	/// </summary>
	/// <param name="number1">The first number to add</param>
	/// <param name="number2">The second number to add</param>
	/// <returns>The sum of the two CustomFloats</returns>
	public static CustomFloat Add(CustomFloat number1, CustomFloat number2)
	{

		// This saves the CustomFloat as a string
		string number1String = number1.GetFloat();
		string number2String = number2.GetFloat();

		// This finds the index of the decimal (or creates one at the end if there is none)
		int decimal1 = number1String.IndexOf(".");
		if (decimal1 == -1)
		{
			number1String += ".";
			decimal1 = number1String.Length - 1;
		}

		// This finds the index of the decimal (or creates one at the end if there is none)
		int decimal2 = number2String.IndexOf(".");
		if (decimal2 == -1)
		{
			number2String += ".";
			decimal2 = number2String.Length - 1;
		}

		// This creates zeros at the front of the number with the least amount of digits including and after the ones digit
		if (decimal1 > decimal2)
		{
			for (int i = 0; i < decimal1 - decimal2; i++)
			{
				number2String = "0" + number2String;
			}
		}
		else
		{
			for (int i = 0; i < decimal2 - decimal1; i++)
			{
				number1String = "0" + number1String;
			}
		}

		// This creates zeros at the end of the number with the least amount of digits after the decimal
		if (number1String.Length > number2String.Length)
		{
			int cycles = number1String.Length - number2String.Length;
			for (int i = 0; i < cycles; i++)
			{
				number2String += "0";
			}
		}
		else
		{
			int cycles = number2String.Length - number1String.Length;
			for (int i = 0; i < cycles; i++)
			{
				number1String += "0";
			}
		}

		// This adds the two numbers by going through each digit place and adding them and putting a carry to the next column if needed
		string total = "";
		bool carry = false;
		int digitTotal = 0;
		for (int i = 0; i < number1String.Length; i++)
		{
			if (number1String.Substring(number1String.Length - i - 1, 1).Equals("."))
			{
				total = "." + total;
			}
			else
			{
				digitTotal = int.Parse(number1String.Substring(number1String.Length - i - 1, 1)) + int.Parse(number2String.Substring(number2String.Length - i - 1, 1));

				if (carry)
				{
					digitTotal++;
				}

				if (digitTotal >= 10)
				{
					digitTotal -= 10;
					carry = true;
				}
				else
				{
					carry = false;
				}
				total = digitTotal.ToString() + total;
			}
		}
		if (carry)
		{
			total = "1" + total;
		}

		// This returns the final total of the two CustomFloats
		return new CustomFloat(total);
	}


	public static CustomFloat Subtract(CustomFloat number1, CustomFloat number2)
	{

		// This saves the CustomFloat as a string
		string number1String = number1.GetFloat();
		string number2String = number2.GetFloat();

		// This finds the index of the decimal (or creates one at the end if there is none)
		int decimal1 = number1String.IndexOf(".");
		if (decimal1 == -1)
		{
			number1String += ".";
			decimal1 = number1String.Length - 1;
		}

		// This finds the index of the decimal (or creates one at the end if there is none)
		int decimal2 = number2String.IndexOf(".");
		if (decimal2 == -1)
		{
			number2String += ".";
			decimal2 = number2String.Length - 1;
		}

		// This creates zeros at the front of the number with the least amount of digits including and after the ones digit
		if (decimal1 > decimal2)
		{
			for (int i = 0; i < decimal1 - decimal2; i++)
			{
				number2String = "0" + number2String;
			}
		}
		else
		{
			for (int i = 0; i < decimal2 - decimal1; i++)
			{
				number1String = "0" + number1String;
			}
		}

		// This creates zeros at the end of the number with the least amount of digits after the decimal
		if (number1String.Length > number2String.Length)
		{
			int cycles = number1String.Length - number2String.Length;
			for (int i = 0; i < cycles; i++)
			{
				number2String += "0";
			}
		}
		else
		{
			int cycles = number2String.Length - number1String.Length;
			for (int i = 0; i < cycles; i++)
			{
				number1String += "0";
			}
		}

		for (int i = 0; i < number1String.Length; i++)
		{
			if (number1String.Substring(number1String.Length - i - 1, 1).Equals("."))
			{

			}
			else
			{

			}
		}

		Debug.Log("Number 1: " + number1String);
		Debug.Log("Number 2: " + number2String);


		return new CustomFloat("-1");
	}
}
