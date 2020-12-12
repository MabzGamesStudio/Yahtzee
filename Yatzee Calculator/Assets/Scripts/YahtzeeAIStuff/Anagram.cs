using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anagram
{

	/// <summary>
	/// This tells whether the two given texts are anagrams of each other
	/// </summary>
	/// <param name="text1">The first text to be compared</param>
	/// <param name="text2">The other text to be compared</param>
	/// <returns>Whether the two texts are anagrams of each other</returns>
	public static bool IsAnagram(string text1, string text2)
	{

		// For the texts to be anagrams they need to have the same character lengths
		if (text1.Length != text2.Length)
		{
			return false;
		}

		// This loops through each character and makes sure both texts have them, then elimates the character in text2 to prevent repeats
		for (int i = 0; i < text1.Length; i++)
		{

			// This is the character in text1 that is looped through the whole text1
			string nextCharacter = text1.Substring(i, 1);

			// This is the text2 before it is edited
			string preText2 = text2;

			// This executes when the text1 character is in text2
			if (text2.Contains(nextCharacter))
			{

				// This deletes the text1 character from text2 to prevent repeat letters messing stuff up
				text2 = preText2.Substring(0, preText2.IndexOf(nextCharacter));
				if (preText2.IndexOf(nextCharacter) != preText2.Length - 1)
				{
					text2 += preText2.Substring(preText2.IndexOf(nextCharacter) + 1);
				}
			}

			// If there is a character that exists in text1 but not text2 then it is not an anagram
			else
			{
				return false;
			}
		}

		// If each character is cycled through and is in both text1 and text2, then it is an anagram
		return true;
	}
}
