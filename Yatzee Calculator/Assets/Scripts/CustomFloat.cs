using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFloat
{

	/// <summary>
	/// The digits of the float
	/// </summary>
	string number;

	/// <summary>
	/// This tells how many significant digits are represented in the float
	/// </summary>
	int significantDigits;

	/// <summary>
	/// This CustomFloat holds the value of a float with a custom significant digits
	/// </summary>
	/// <param name="number">The number to be saved</param>
	/// <param name="significantDigits">The number of significant digits for the float</param>
	public CustomFloat(string number, int significantDigits)
	{
		this.significantDigits = significantDigits;
		this.number = number;
		FormatNumber();
	}

	/// <summary>
	/// This returns the CustomFloat number in string form
	/// </summary>
	/// <returns>The float string in scientific notation</returns>
	public string GetFloat()
	{
		return number;
	}

	/// <summary>
	/// This returns the CustomFloat number in string form as a normal number
	/// </summary>
	/// <returns>The float string as a normal number</returns>
	public string GetFloatNormal()
	{

		// This is the formatted version of the number
		string normalFormat = "";

		// This tells if the number is negative
		bool negative = number.Substring(0, 1).Equals("-");

		// This is the magnitude of the number (The number after the e)
		int magnitude = int.Parse(number.Substring(number.IndexOf("e") + 1));

		// This gets the numbers in the scientific notation string (Not including after the e)
		for (int i = 0; i < number.IndexOf("e"); i++)
		{

			// This is the next character in the loop
			string nextCharacter = number.Substring(i, 1);

			// This adds the character to the new format if it is a number
			if ("1234567890".IndexOf(nextCharacter) != -1)
			{
				normalFormat += nextCharacter;
			}
		}

		// If zeros need to be added to end of the number it adds them
		if (magnitude > normalFormat.Length - 1)
		{

			// This finds the normalFormat length at the start of the loop because it is changing
			int normalFormatLength = normalFormat.Length;
			for (int i = 0; i < magnitude - normalFormatLength + 1; i++)
			{
				normalFormat += "0";
			}
		}

		// If zeros need to be added to the beginning of the number if it is < 1 they are added
		else if (magnitude < -1)
		{
			for (int i = 0; i < -magnitude - 1; i++)
			{
				normalFormat = "0" + normalFormat;
			}
			normalFormat = "0." + normalFormat;
		}

		// If only a decimal needs to be placed in the middle it does so
		else if (significantDigits > 1)
		{
			normalFormat = normalFormat.Substring(0, magnitude + 1) + "." + normalFormat.Substring(magnitude + 1);
		}

		// This appends the negative sign to the start if it is negative
		if (negative)
		{
			normalFormat = "-" + normalFormat;
		}

		if (normalFormat.Substring(normalFormat.Length - 1, 1).Equals("."))
		{
			normalFormat = normalFormat.Substring(0, normalFormat.Length - 1);
		}

		// Returns the normally formatted float as a string
		return normalFormat;
	}

	/// <summary>
	/// This adds the two given CustomFloats
	/// </summary>
	/// <param name="number1">The first number to add</param>
	/// <param name="number2">The second number to add</param>
	/// /// <param name="significantDigits">The significant digits to be used in the result</param>
	/// <returns>The sum of the two CustomFloats</returns>
	public static CustomFloat Add(CustomFloat number1, CustomFloat number2, int significantDigits)
	{

		// These are the string values of the CustomFloat numbers
		string number1Float = number1.GetFloat();
		string number2Float = number2.GetFloat();

		// These tell whether each of the numbers are negative or not
		bool number1IsNegative = number1Float.Substring(0, 1).Equals("-");
		bool number2IsNegative = number2Float.Substring(0, 1).Equals("-");

		// This counts the digits to be used for the adding
		int number1Counter = 0;
		int number2Counter = 0;

		// These show the magnitude of each of the numbers to add
		int number1Magnitude = int.Parse(number1Float.Substring(number1Float.IndexOf("e") + 1));
		int number2Magnitude = int.Parse(number2Float.Substring(number2Float.IndexOf("e") + 1));

		string sum = "0";

		// The two numbers are added if they have the same sign
		if (number1IsNegative == number2IsNegative)
		{

			// The negative signs are temporarily taken away to add the numbers
			if (number1IsNegative)
			{
				number1Float = number1Float.Substring(1);
				number2Float = number2Float.Substring(1);
			}

			// The counters are set up so they add in corresponding digit spots
			if (number1Magnitude > number2Magnitude)
			{
				number2Counter -= Mathf.Abs(number1Magnitude - number2Magnitude);
			}
			else
			{
				number1Counter -= Mathf.Abs(number1Magnitude - number2Magnitude);
			}

			// This adds the digits in order of digit magnitude for the number of significant digits
			for (int i = 0; i < significantDigits; i++)
			{

				// These are the strings that store the next character for each number
				string nextCharacter1;
				string nextCharacter2;

				// This is the sum of the digits for a magnitude
				int digitSum = 0;

				// This increases the number counter to skip over a decimal if necessary
				if (number1Counter == 1 && !number1Float.Substring(number1Counter + 1, 1).Equals("e"))
				{
					number1Counter++;
				}
				if (number2Counter == 1 && !number2Float.Substring(number2Counter + 1, 1).Equals("e"))
				{
					number2Counter++;
				}

				// This only happens if the number1 counter is equal to or greater than 0
				if (number1Counter >= 0)
				{
					// This is the next character
					nextCharacter1 = number1Float.Substring(number1Counter, 1);

					// This stops this number from counting once it has reached e and adds the character to the digit sum otherwise
					if (nextCharacter1.Equals("e"))
					{
						number1Counter = -significantDigits - 1;
					}
					else
					{
						digitSum += int.Parse(nextCharacter1);
					}
				}

				// This only happens if the number2 counter is equal to or greater than 0
				if (number2Counter >= 0)
				{

					// This is the next character
					nextCharacter2 = number2Float.Substring(number2Counter, 1);

					// This stops this number from counting once it has reached e and adds the character to the digit sum otherwise
					if (nextCharacter2.Equals("e"))
					{
						number2Counter = -significantDigits - 1;
					}
					else
					{
						digitSum += int.Parse(nextCharacter2);
					}
				}

				// If the digit sum is greater than or equal to 10 then it has to carry to the place to the left and otherwise appends the digit to the end of the string
				if (digitSum >= 10)
				{
					sum = Carry(sum) + (digitSum - 10).ToString();
				}
				else
				{
					sum += digitSum.ToString();
				}

				// The number counters increase through each loop
				number1Counter++;
				number2Counter++;
			}

			// This adds the decimal to the third place since the number either starts with 0#. or 1#.
			sum = sum.Substring(0, 2) + "." + sum.Substring(2);

			// The e and magnitude are appended to the end
			sum = sum + "e" + Mathf.Max(number1Magnitude, number2Magnitude);

			// If the two numbers are negative then the result is negative
			if (number1IsNegative)
			{
				sum = "-" + sum;
			}

			// This returns the final total of the two CustomFloats
			return new CustomFloat(sum, significantDigits);

		}

		// Adding one positive number and one negative number is the same as subtraction
		else
		{

			// This tells the order of the subtraction
			if (number1IsNegative)
			{
				return Subtract(number2, new CustomFloat(number1.GetFloat().Substring(1), number1.significantDigits), significantDigits);
			}
			else
			{
				return Subtract(number1, new CustomFloat(number2.GetFloat().Substring(1), number2.significantDigits), significantDigits);
			}
		}
	}

	/// <summary>
	/// This subtracts the first number minus the second number
	/// </summary>
	/// <param name="number1">The first number</param>
	/// <param name="number2">The number to subtract</param>
	/// <param name="significantDigits">The significant digits to be used in the result</param>
	/// <returns>The CustomFloat of the first number minus the second number</returns>
	public static CustomFloat Subtract(CustomFloat number1, CustomFloat number2, int significantDigits)
	{

		// If the numbers are the same then it returns a CustomFloat with a value of 0
		if (number1.GetFloatNormal().Equals(number2.GetFloatNormal()))
		{
			return new CustomFloat("0", significantDigits);
		}

		// These are the string values of the CustomFloat numbers
		string number1Float = number1.GetFloat();
		string number2Float = number2.GetFloat();

		// These tell whether each of the numbers are negative or not
		bool number1IsNegative = number1Float.Substring(0, 1).Equals("-");
		bool number2IsNegative = number2Float.Substring(0, 1).Equals("-");

		// This counts the digits to be used for the adding
		int number1Counter = 0;
		int number2Counter = 0;

		// These show the magnitude of each of the numbers to add
		int number1Magnitude = int.Parse(number1Float.Substring(number1Float.IndexOf("e") + 1));
		int number2Magnitude = int.Parse(number2Float.Substring(number2Float.IndexOf("e") + 1));

		string difference = "";

		// The two numbers are subtracted if they have the same sign
		if (number1IsNegative == number2IsNegative)
		{
			// The negative signs are temporarily taken away to add the numbers
			if (number1IsNegative)
			{
				number1Float = number1Float.Substring(1);
				number2Float = number2Float.Substring(1);
			}

			// The counters are set up so they add in corresponding digit spots
			if (number1Magnitude > number2Magnitude)
			{
				number2Counter -= Mathf.Abs(number1Magnitude - number2Magnitude);
			}
			else
			{
				number1Counter -= Mathf.Abs(number1Magnitude - number2Magnitude);
			}

			// This tells if the first number is greater than the second number in magnitude (not sign)
			bool firstNumberGreater;
			CustomFloat number1FloatMagnitude;
			if (number1IsNegative)
			{
				number1FloatMagnitude = new CustomFloat(number1Float.Substring(1), number1.significantDigits);
			}
			else
			{
				number1FloatMagnitude = new CustomFloat(number1Float, number1.significantDigits);
			}
			CustomFloat number2FloatMagnitude;
			if (number2IsNegative)
			{
				number2FloatMagnitude = new CustomFloat(number2Float.Substring(1), number2.significantDigits);
			}
			else
			{
				number2FloatMagnitude = new CustomFloat(number2Float, number2.significantDigits);
			}
			firstNumberGreater = IsGreaterThanOrEqual(number1FloatMagnitude, number2FloatMagnitude);

			// This subtracts the digits in order of digit magnitude for the number of significant digits
			for (int i = 0; i < significantDigits + 1; i++)
			{

				// These are the strings that store the next character for each number
				string nextCharacter1;
				string nextCharacter2;

				// This is the sum of the digits for a magnitude
				int digitDifference = 0;

				// This increases the number counter to skip over a decimal if necessary
				if (number1Counter == 1 && !number1Float.Substring(number1Counter + 1, 1).Equals("e"))
				{
					number1Counter++;
				}
				if (number2Counter == 1 && !number2Float.Substring(number2Counter + 1, 1).Equals("e"))
				{
					number2Counter++;
				}

				// This only happens if the number1 counter is equal to or greater than 0
				if (number1Counter >= 0 && number1Float.Length > number1Counter)
				{

					// This is the next character
					nextCharacter1 = number1Float.Substring(number1Counter, 1);

					// This stops this number from counting once it has reached e and adds the character to the digit sum otherwise
					if (nextCharacter1.Equals("e"))
					{
						number1Counter = -significantDigits - 1;
					}
					else
					{

						// This adds the digit if it is on top and subtracts it if it is on the bottom
						if (firstNumberGreater)
						{
							digitDifference += int.Parse(nextCharacter1);
						}
						else
						{
							digitDifference -= int.Parse(nextCharacter1);
						}

					}
				}

				// This only happens if the number2 counter is equal to or greater than 0
				if (number2Counter >= 0 && number2Float.Length > number2Counter)
				{

					// This is the next character
					nextCharacter2 = number2Float.Substring(number2Counter, 1);

					// This stops this number from counting once it has reached e and adds the character to the digit sum otherwise
					if (nextCharacter2.Equals("e"))
					{
						number2Counter = -significantDigits - 1;
					}
					else
					{
						// This adds the digit if it is on top and subtracts it if it is on the bottom
						if (!firstNumberGreater)
						{
							digitDifference += int.Parse(nextCharacter2);
						}
						else
						{
							digitDifference -= int.Parse(nextCharacter2);
						}
					}
				}

				// If the digit sum is less than 0 then it has to take a carry from the left and otherwise appends the digit to the end of the string
				if (digitDifference < 0)
				{
					difference = SubtractionCarry(difference) + (digitDifference + 10).ToString();
				}
				else
				{
					difference += digitDifference.ToString();
				}

				// The number counters increase through each loop
				number1Counter++;
				number2Counter++;
			}

			// This adds the decimal to the third place since the number either starts with 0#. or 1#.
			difference = difference.Substring(0, 1) + "." + difference.Substring(1);

			// The e and magnitude are appended to the end
			difference = difference + "e" + Mathf.Max(number1Magnitude, number2Magnitude);

			// If the two numbers are negative then the result is negative
			if (!firstNumberGreater)
			{
				difference = "-" + difference;
			}

			// This returns the final difference of the two CustomFloats
			return new CustomFloat(difference, significantDigits);

		}

		// Subtracting one negative number from another positive number is the same as adding
		else
		{
			// This tells the sign of the addition
			if (number1IsNegative)
			{
				return Add(number1, new CustomFloat("-" + number2.GetFloat(), number2.significantDigits), significantDigits);
			}
			else
			{
				return Add(number1, new CustomFloat(number2.GetFloat().Substring(1), number2.significantDigits), significantDigits);
			}
		}
	}

	/// <summary>
	/// This multiplies the first number times the second number
	/// </summary>
	/// <param name="number1">The first number to multiply</param>
	/// <param name="number2">The second number to multiply</param>
	/// <param name="significantDigits">The significant digits to be used in the result</param>
	/// <returns>The CustomFloat of the first number times the second number</returns>
	public static CustomFloat Multiply(CustomFloat number1, CustomFloat number2, int significantDigits)
	{
		// These are the string values of the CustomFloat numbers
		string number1Float = number1.GetFloat();
		string number2Float = number2.GetFloat();

		// This tells whether the first number is positive or not then removes the minus sign for the calculations
		bool number1IsNegative = number1Float.Substring(0, 1).Equals("-");
		if (number1IsNegative)
		{
			number1Float = number1Float.Substring(1);
		}

		// This tells whether the second number is positive or not then removes the minus sign for the calculations
		bool number2IsNegative = number2Float.Substring(0, 1).Equals("-");
		if (number2IsNegative)
		{
			number2Float = number2Float.Substring(1);
		}

		// This tells the number of significant digits in the first number
		int number1SignificantDigits = number1Float.IndexOf("e") - 1;
		if (number1Float.IndexOf(".") == -1)
		{
			number1SignificantDigits = 1;
		}

		// This tells the number of significant digits in the second number
		int number2SignificantDigits = number2Float.IndexOf("e") - 1;
		if (number2Float.IndexOf(".") == -1)
		{
			number2SignificantDigits = 1;
		}

		// This removes the decimal for the first number for the calculations
		if (number1Float.IndexOf(".") != -1)
		{
			number1Float = number1Float.Substring(0, 1) + number1Float.Substring(2);
		}

		// This removes the decimal for the second number for the calculations
		if (number2Float.IndexOf(".") != -1)
		{
			number2Float = number2Float.Substring(0, 1) + number2Float.Substring(2);
		}

		// These show the magnitude of each of the numbers to add
		int number1Magnitude = int.Parse(number1Float.Substring(number1Float.IndexOf("e") + 1));
		int number2Magnitude = int.Parse(number2Float.Substring(number2Float.IndexOf("e") + 1));

		// This is the product to be totalled from multiplying
		CustomFloat product = new CustomFloat("0", significantDigits + 1);

		// This loops through the diagonal when the factors are layed out in a product square
		for (int i = 0; i < significantDigits + 1; i++)
		{

			// This is the length of the boxes in the diagonal
			int diagonal = Mathf.Min(Mathf.Min(number1SignificantDigits + 1, number2SignificantDigits + 1), i + 1);

			// These are the coordinates of the box start for the diagonal
			int x = Mathf.Max(i - number1SignificantDigits, 0);
			int y = Mathf.Min(i, number2SignificantDigits);

			// This loops through the boxes in the diagonal
			for (int j = 0; j < diagonal; j++)
			{

				// The digit product starts at 1
				string digitProduct = "1";

				// This multiplies the digitProduct by the xth digit in the first number
				if (y < number1SignificantDigits)
				{
					digitProduct = number1Float.Substring(y, 1);
				}
				else
				{
					digitProduct = "0";
				}

				// This multiplies the digitProduct by the yth digit in the second number
				if (x < number2SignificantDigits)
				{
					digitProduct = (int.Parse(digitProduct) * int.Parse(number2Float.Substring(x, 1))).ToString();
				}
				else
				{
					digitProduct = "0";
				}

				// This adds the necessary zeros based on the x and y values and the number magnitudes
				for (int k = 0; k < (number1SignificantDigits - y - 1) + (number2SignificantDigits - x - 1); k++)
				{
					digitProduct += "0";
				}

				// This adds the box's product to the total so far
				product.Add(new CustomFloat(digitProduct, significantDigits + 1));

				// This moves the x and y positions to the next in the diagonal
				x++;
				y--;
			}
		}

		// This is what the magnitude of the number needs to change by
		int magnitude = 0;

		// This changes the magnitude based on the significant digits and magnitude of the original numbers
		magnitude += number1Magnitude - number1SignificantDigits + 1;
		magnitude += number2Magnitude - number2SignificantDigits + 1;

		// This appends the magnitude to the number using e and the magnitude
		product = new CustomFloat(product.GetFloatNormal() + "e" + magnitude, significantDigits);

		// This manages whether the answer is positive or negative
		if (number1IsNegative != number2IsNegative)
		{
			product = new CustomFloat("-" + product.GetFloat(), significantDigits);
		}

		// This returns the product
		return product;
	}

	/// <summary>
	/// This divides the first number divided by the second number
	/// </summary>
	/// <param name="number1">The first number to be divided</param>
	/// <param name="number2">The second number to divide by</param>
	/// <param name="significantDigits">The significant digits to be used in the result</param>
	/// <returns>The CustomFloat of the first number divided by the second number</returns>
	public static CustomFloat Divide(CustomFloat number1, CustomFloat number2, int significantDigits)
	{

		// This returns zero if the divisor is zero since it is undefined and could cause an error 
		if (number2.GetFloat().Substring(0, 1).Equals("0"))
		{
			Debug.LogError("Yoooo, you divided by zero and idk what to do wid dat");
			return new CustomFloat("0", significantDigits);
		}

		// This returns zero if the first number is zero
		else if (number1.GetFloat().Substring(0, 1).Equals("0"))
		{
			return new CustomFloat("0", significantDigits);
		}

		// These are the string values of the CustomFloat numbers
		string number1Float = number1.GetFloat();
		string number2Float = number2.GetFloat();

		// This tells whether the first number is positive or not then removes the minus sign for the calculations
		bool number1IsNegative = number1Float.Substring(0, 1).Equals("-");
		if (number1IsNegative)
		{
			number1Float = number1Float.Substring(1);
		}

		// This tells whether the second number is positive or not then removes the minus sign for the calculations
		bool number2IsNegative = number2Float.Substring(0, 1).Equals("-");
		if (number2IsNegative)
		{
			number2Float = number2Float.Substring(1);
		}

		// This tells the number of significant digits in the first number
		int number1SignificantDigits = number1Float.IndexOf("e") - 1;
		if (number1Float.IndexOf(".") == -1)
		{
			number1SignificantDigits = 1;
		}

		// This tells the number of significant digits in the second number
		int number2SignificantDigits = number2Float.IndexOf("e") - 1;
		if (number2Float.IndexOf(".") == -1)
		{
			number2SignificantDigits = 1;
		}

		// This removes the decimal for the first number for the calculations
		if (number1Float.IndexOf(".") != -1)
		{
			number1Float = number1Float.Substring(0, 1) + number1Float.Substring(2);
		}

		// This removes the decimal for the second number for the calculations
		if (number2Float.IndexOf(".") != -1)
		{
			number2Float = number2Float.Substring(0, 1) + number2Float.Substring(2);
		}

		// These show the magnitude of each of the numbers to add
		int number1Magnitude = int.Parse(number1Float.Substring(number1Float.IndexOf("e") + 1));
		int number2Magnitude = int.Parse(number2Float.Substring(number2Float.IndexOf("e") + 1));

		// This counts the number of digits used in the dividend
		int dividendDigits = 1;

		// This is the number of times that it takes until the first nonzero number digit is found when dividing
		int dividingPlaces = 0;

		// This is the number that will be divided
		CustomFloat dividend = new CustomFloat(number1Float.Substring(0, dividendDigits), dividendDigits);

		// This is the number which will be divided by
		CustomFloat divisor = new CustomFloat(number2Float.Substring(0, number2.significantDigits), number2SignificantDigits);

		// This is the quotient of the dividend divided by the divisor
		CustomFloat quotient = new CustomFloat("0", 1);

		// This tells whether the first nonzero digit has been found
		bool firstNonzeroDigitFound = false;

		// This loops through the dividing process to find the number of significant digits
		for (int i = 0; i < significantDigits; i++)
		{

			// This happens when the section of the dividend is greater than the divisor
			if (IsGreaterThanOrEqual(dividend, divisor))
			{
				firstNonzeroDigitFound = true;

				// This subtracts the divisor from the dividend to find the next digit in the quotient
				for (int j = 0; j < 10; j++)
				{

					// If the dividend is greater than or equal to the divisor then it subtracts another divisor from the dividend
					if (IsGreaterThanOrEqual(dividend, divisor))
					{

						// This subtracts the divisor from the dividend
						dividend = Subtract(dividend, divisor, dividend.significantDigits + 1);

						// This variable gets the exact significant digits for the whole number
						int newDividendSignificantDigit = dividend.GetFloat().IndexOf("e") - 1;
						newDividendSignificantDigit += int.Parse(dividend.GetFloat().Substring(dividend.GetFloat().IndexOf("e") + 1));
						newDividendSignificantDigit -= (dividend.GetFloat().IndexOf("e") - 2);
						if (dividend.significantDigits == 1)
						{
							newDividendSignificantDigit = 1;
						}

						// The new significant digit that is found is set in the dividend
						dividend.significantDigits = newDividendSignificantDigit;

					}

					// If the dividend is less than the divisor then the digit is appended to the end of the quotient and the leftovers are appended to the start of the next digit in the dividend
					else
					{


						// The next digit is appended to the end of the quotient
						quotient = new CustomFloat(quotient.GetFloatNormal() + j, i + 1);

						// This appends the leftovers to the start of the next digit
						dividendDigits++;
						if (dividendDigits > number1SignificantDigits)
						{
							dividend = new CustomFloat(dividend.GetFloatNormal() + "e1", dividend.significantDigits + 1);
						}
						else
						{
							dividend = new CustomFloat(dividend.GetFloatNormal() + number1Float.Substring(dividendDigits - 1, 1), dividend.significantDigits + 1);
						}
						break;
					}
				}
			}

			// This happens when the dividend is less than the divisor so another digit needs to be appended to calculate the next nonzero digit
			else
			{

				// If the first nonzero digit has been found then it appends a zero to the quotient
				if (firstNonzeroDigitFound)
				{
					quotient = new CustomFloat(quotient.GetFloatNormal() + "0", i + 1);
				}

				// If the first nonzero digit has not been found the it decreases i so that it finds the correct amount of significant digits and increases the dividing places since the zero digit will not be recorded in the quotient
				else
				{
					i--;
					dividingPlaces++;
				}

				// The next digit in the number1 is appended to the dividend and a zero is appended if there are no more significant digits left
				dividendDigits++;
				if (dividendDigits > number1SignificantDigits)
				{
					dividend = new CustomFloat(dividend.GetFloatNormal() + "e1", dividend.significantDigits + 1);
				}
				else
				{
					dividend = new CustomFloat(dividend.GetFloatNormal() + number1Float.Substring(dividendDigits - 1, 1), dividend.significantDigits + 1);
				}
			}
		}

		// This formula determines how the decimal place should be shifted in the quotient
		int decimalPlaceShift = number1Magnitude - number2Magnitude - 1;
		decimalPlaceShift -= (dividingPlaces - number2Float.Substring(0, number2Float.IndexOf("e")).Length);

		// This shifts the decimal places based on the above variable
		quotient = new CustomFloat(quotient.GetFloatNormal().Substring(0, 1) + "." + quotient.GetFloatNormal().Substring(1) + "e" + decimalPlaceShift, quotient.significantDigits);

		// If exactly one of the numbers is negative then the result is negative
		if (number1IsNegative != number2IsNegative)
		{
			quotient = new CustomFloat("-" + quotient.GetFloat(), significantDigits);
		}

		// This returns the final quotient CustomFloat
		return quotient;
	}

	/// <summary>
	/// This recursively takes a carry from the previous number until it is no longer necessary
	/// </summary>
	/// <param name="number">The number that will be subtracted one from</param>
	/// <returns>The given number minus one</returns>
	static string SubtractionCarry(string number)
	{
		if (number.Equals(""))
		{
			Debug.LogError("Bruv tha minus carry thing aint work");
			return null;
		}
		if (number.Substring(number.Length - 1).Equals("0"))
		{
			return SubtractionCarry(number.Substring(0, number.Length - 1)) + "9";
		}

		return number.Substring(0, number.Length - 1) + (int.Parse(number.Substring(number.Length - 1)) - 1).ToString();
	}

	/// <summary>
	/// This recusively carries a number until it is no longer necessary
	/// </summary>
	/// <param name="number">The number that will be added one to</param>
	/// <returns>The given number plus one</returns>
	static string Carry(string number)
	{
		if (number.Equals(""))
		{
			return "1";
		}
		if (number.Substring(number.Length - 1).Equals("9"))
		{
			return Carry(number.Substring(0, number.Length - 1)) + "0";
		}

		return number.Substring(0, number.Length - 1) + (int.Parse(number.Substring(number.Length - 1)) + 1).ToString();
	}

	/// <summary>
	/// This adds the given number to this CustomFloat
	/// </summary>
	/// <param name="numberToAdd">The number to be added to this CustomFloat</param>
	public void Add(CustomFloat numberToAdd)
	{
		number = Add(this, numberToAdd, significantDigits).GetFloat();
	}

	/// <summary>
	/// This subtracts the given number from this CustomFloat
	/// </summary>
	/// <param name="numberToSubtract">The number to be subtracted from this CustomFloat</param>
	public void Subtract(CustomFloat numberToSubtract)
	{
		number = Subtract(this, numberToSubtract, significantDigits).GetFloat();
	}

	/// <summary>
	/// This tells if number1 is greater than number2
	/// </summary>
	/// <param name="number1">The first number to compare</param>
	/// <param name="number2">The second number to compare</param>
	/// <returns>True iff number1 is greater than number2</returns>
	public static bool IsGreaterThanOrEqual(CustomFloat number1, CustomFloat number2)
	{

		string number1Float = number1.GetFloat();
		string number2Float = number2.GetFloat();

		// If one number is negative and the other is not, then return the positive number
		if (number1Float.Substring(0, 1).Equals("-") != number2Float.Substring(0, 1).Equals("-"))
		{
			return !number1Float.Substring(0, 1).Equals("-");
		}

		// If one number has a greater magnitude then it returns the number with the greater magnitude
		if (int.Parse(number1Float.Substring(number1Float.IndexOf("e") + 1)) != int.Parse(number2Float.Substring(number2Float.IndexOf("e") + 1)))
		{

			// This returns the number with the least magnitude if they are both negative
			if (number1Float.Substring(0, 1).Equals("-"))
			{
				return int.Parse(number1Float.Substring(number1Float.IndexOf("e") + 1)) < int.Parse(number2Float.Substring(number2Float.IndexOf("e") + 1));
			}

			// This returns the number with the least magnitude if they are both positive
			else
			{
				return int.Parse(number1Float.Substring(number1Float.IndexOf("e") + 1)) > int.Parse(number2Float.Substring(number2Float.IndexOf("e") + 1));
			}
		}

		// This loops through the two strings until it has found a difference or reaches the end of one string
		for (int i = 0; i < Mathf.Min(number1Float.IndexOf("e"), number2Float.IndexOf("e")); i++)
		{

			// This moves onto the next place if it reaches a decimal and both numbers are positive
			if (!number1Float.Substring(0, 1).Equals("-") && i == 1 && number1Float.IndexOf("e") != i + 1 && number2Float.IndexOf("e") != i + 1)
			{
				i++;
			}

			// This moves onto the next place if it reaches a decimal and both numbers are negative
			else if (number1Float.Substring(0, 1).Equals("-") && i == 2 && number1Float.IndexOf("e") != i + 1 && number2Float.IndexOf("e") != i + 1)
			{
				i++;
			}

			// If one digit doesn't equal the other digit then it determines which number is greater depending on if the numbers are positive or negative
			if (number1Float.Substring(i, 1) != number2Float.Substring(i, 1))
			{
				if (number1Float.Substring(0, 1).Equals("-"))
				{
					return int.Parse(number1Float.Substring(i, 1)) < int.Parse(number2Float.Substring(i, 1));
				}
				else
				{
					return int.Parse(number1Float.Substring(i, 1)) > int.Parse(number2Float.Substring(i, 1));
				}
			}
		}

		// If the first number has more digits then it is greater if both positive and lesser if both negative
		if (number1Float.Length > number2Float.Length)
		{
			if (number1Float.Substring(0, 1).Equals("-"))
			{
				return number1Float.Length < number2Float.Length;
			}
			else
			{
				return number1Float.Length > number2Float.Length;
			}
		}

		// If the two numbers are exactly the same it returns true
		return true;
	}

	/// <summary>
	/// This formats the string number
	/// </summary>
	void FormatNumber()
	{
		FormatSignificantDigits();
		number = FormatNumber(number, significantDigits);
	}

	/// <summary>
	/// This formats the CustomFloat number given a string
	/// </summary>
	/// <param name="number">The number that should be turned to a CustomFloat</param>
	/// <returns>The formatted string of the CustomFloat</returns>
	static string FormatNumber(string number, int significantDigits)
	{

		// This tells the length of the given string
		int stringLength = number.Length;

		// This tells if the first nonzero character has been found
		bool firstNumberFound = false;

		// This is the new string that is formatted correctly
		string formattedString = "";

		// This tells if the first decimal has been found
		bool firstDecimalFound = false;

		// This tells if the first e has been found
		bool firstEFound = false;

		// This tells if the first - has been found
		bool firstMinusFound = false;

		// This tells if the first - has been found
		bool firstMinusAfterEFound = false;

		// This tells if the first number has been found after e
		bool firstNumberAfterEFound = false;

		// This formats the number to only include numbers, one decimal, and one e and cuts out unnecessary zeros in the front
		for (int i = 0; i < stringLength; i++)
		{

			// This is the next character in the string
			string nextCharacter = number.Substring(i, 1);

			// This only happens when the next character is a number, decimal, or e
			if ("0123456789e.-".IndexOf(nextCharacter) != -1)
			{

				// This only happens when a nonzero numbner has been found or the character is not zero
				if (!nextCharacter.Equals("0") || (firstNumberFound && (firstNumberAfterEFound == firstEFound)))
				{

					// This happens when the next character is a minus sign
					if (nextCharacter.Equals("-"))
					{

						// This writes the minus sign if it is at the start or the start of the number after e
						if (!firstNumberFound && !firstMinusFound)
						{
							firstMinusFound = true;
							formattedString += "-";
						}
						else if (firstEFound && !firstNumberAfterEFound && !firstMinusAfterEFound)
						{
							firstMinusAfterEFound = true;
							formattedString += "-";
						}
					}

					// This happens when the next character is a decimal
					else if (nextCharacter.Equals("."))
					{

						// This only writes the decimal if it is the first decimal and it is before any e
						if (!firstDecimalFound && !firstEFound)
						{
							firstDecimalFound = true;
							firstNumberFound = true;
							formattedString += nextCharacter;
						}
					}

					// This happens when the next character is e
					else if (nextCharacter.Equals("e"))
					{

						// This writes the e if it is the first one found
						if (!firstEFound)
						{
							firstEFound = true;
							formattedString += nextCharacter;
						}
					}

					// This happens when the next character is a number
					else
					{
						firstNumberFound = true;
						formattedString += nextCharacter;
						if (firstEFound)
						{
							firstNumberAfterEFound = true;
						}
					}
				}
			}
		}

		// This tells whether the number is negative and temporarily takes away the negative sign to deal with the magnitude of the number
		bool negative = false;
		if (formattedString.Length > 0 && formattedString.Substring(0, 1).Equals("-"))
		{
			negative = true;
			formattedString = formattedString.Substring(1, formattedString.Length - 1);
		}

		// This defaults a blank string to 0 with correct significant digits
		if (formattedString.Equals(""))
		{
			formattedString = "0";
			if (significantDigits > 1)
			{
				formattedString += ".";
				for (int i = 0; i < significantDigits; i++)
				{
					formattedString += "0";
				}
			}
			return formattedString + "e0";
		}

		// This is the magnitude of the number and what will be displayed after the e
		int numberMagnitude = 0;

		// This happens when the number has no decimal and no e
		if (formattedString.IndexOf(".") == -1 && formattedString.IndexOf("e") == -1)
		{
			numberMagnitude = formattedString.Length - 1;
		}

		// This happens when the number starts with a decimal and has no e
		else if (formattedString.IndexOf(".") == 0 && formattedString.IndexOf("e") == -1)
		{
			// This loops through the string and finds the first nonzero number to find the number magnitude
			for (int i = 1; i < formattedString.Length; i++)
			{
				numberMagnitude--;
				if (!formattedString.Substring(i, 1).Equals("0"))
				{
					break;
				}
			}
		}

		// This happens when the number has a decimal and no e
		else if (formattedString.IndexOf(".") != -1 && formattedString.IndexOf("e") == -1)
		{
			numberMagnitude = formattedString.IndexOf(".") - 1;
		}

		// This happens when the number has no decimal and has an e
		else if (formattedString.IndexOf(".") == -1 && formattedString.IndexOf("e") != -1)
		{
			if (formattedString.IndexOf("e") == 0)
			{
				formattedString = "0" + formattedString;
			}

			// If the last character is an e, then it puts a 0 at the end to prevent errors when taking the parse
			if (formattedString.IndexOf("e") == formattedString.Length - 1)
			{
				formattedString += "0";
			}
			numberMagnitude = formattedString.IndexOf("e") - 1 + int.Parse(formattedString.Substring(formattedString.IndexOf("e") + 1));

		}

		// This happens when the number starts with a decimal and has an e
		else if (formattedString.IndexOf(".") == 0 && formattedString.IndexOf("e") != -1)
		{

			// This loops through the string and finds the first nonzero number to find the number magnitude
			for (int i = 1; i < formattedString.IndexOf("e"); i++)
			{
				numberMagnitude--;
				if (!formattedString.Substring(i, 1).Equals("0"))
				{
					break;
				}
			}

			// If the last character is an e, then it puts a 0 at the end to prevent errors when taking the parse
			if (formattedString.IndexOf("e") == formattedString.Length - 1)
			{
				formattedString += "0";
			}
			numberMagnitude += int.Parse(formattedString.Substring(formattedString.IndexOf("e") + 1));
		}

		else if (formattedString.IndexOf(".") != -1 && formattedString.IndexOf("e") != -1)
		{

			// If the last character is an e, then it puts a 0 at the end to prevent errors when taking the parse
			if (formattedString.IndexOf("e") == formattedString.Length - 1)
			{
				formattedString += "0";
			}

			numberMagnitude = formattedString.IndexOf(".") - 1 + int.Parse(formattedString.Substring(formattedString.IndexOf("e") + 1));
		}

		// This is the final format in scientific notation
		string finalFormat = "";

		// This tells if a number has been found in the new formatted string
		bool firstNumberFoundInFinalFormat = false;

		int significantDigitsCount = 0;

		// This loops through the formatted string and finds numbers to add to the first part of the scientific notation
		for (int i = 0; i < formattedString.Length && significantDigitsCount < significantDigits; i++)
		{

			// This is the next character to read
			string character = formattedString.Substring(i, 1);

			// If the character is then it is done reading
			if (character.Equals("e"))
			{
				break;
			}

			// If the next character is a nonzero number then it adds it and adds a decimal if necessary
			else if (!firstNumberFoundInFinalFormat && "123456789".IndexOf(character) != -1)
			{
				finalFormat += character;
				significantDigitsCount++;
				firstNumberFoundInFinalFormat = true;
				if (significantDigits == 1)
				{
					break;
				}
				else
				{
					finalFormat += ".";
				}
			}

			// This adds the number if the first digit has been found
			else if (firstNumberFoundInFinalFormat && "0123456789".IndexOf(character) != -1)
			{
				significantDigitsCount++;
				finalFormat += character;
			}
		}

		// If the number is blank, then it defaults to zero
		if (finalFormat.Equals(""))
		{
			if (significantDigits == 1)
			{
				finalFormat = "0";
			}
			else
			{
				finalFormat = "0.";
				significantDigitsCount++;
			}
		}

		// This adds zeros if there aren't enough significant digits
		for (int i = significantDigitsCount; i < significantDigits; i++)
		{
			finalFormat += "0";
		}

		// This adds the e and magnitude number to finalize the scientific notation
		finalFormat += "e" + numberMagnitude.ToString();

		// This makes the magnitude 0 if the number is exactly zero
		if (finalFormat.Substring(0, 1).Equals("0"))
		{
			finalFormat = finalFormat.Substring(0, finalFormat.IndexOf("e")) + "e0";
		}

		// This makes the number negative if it was said negative before fiinding the magnitude
		if (negative)
		{
			finalFormat = "-" + finalFormat;
		}

		return finalFormat;
	}

	/// <summary>
	/// This formats the significant digits to be a positive whole number
	/// </summary>
	void FormatSignificantDigits()
	{
		significantDigits = Mathf.Max(1, significantDigits);
	}

}
