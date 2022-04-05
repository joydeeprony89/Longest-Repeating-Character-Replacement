using System;

namespace Longest_Repeating_Character_Replacement
{
  class Program
  {
    static void Main(string[] args)
    {
      string s = "AABABBA";
      int k = 1;
      Program p = new Program();
      int result = p.CharacterReplacement(s, k);
      Console.WriteLine(result);
    }

    public int CharacterReplacement(string s, int k)
    {
      int maxLength = 0;
      int maxOccurance = int.MinValue;
      int left = 0 , right = 0;
      // Frequency array to store the characters occurance count.
      int[] frequency = new int[26];
      // Start left and right from the first char
      while (left < s.Length && right < s.Length)
      {
        // current window size
        int currentWindowSize = right - left + 1;
        char c = s[right];
        // Increment the frequency count.
        frequency[c - 'A'] += 1;
        // maxOccurance will always have the max occurance value
        maxOccurance = Math.Max(maxOccurance, frequency[c- 'A']);
        // ex - current window = "ABAA", size = 4 and we are allowed to replace one character, so 4 - 3(max occurance of A in this window) <= 1
        // ex - current window = "ABAAB", size = 5 and we are allowed to replace one character, so 5 - 3(max occurance of A in this window) > 1, we have to replace 2 B to make it AAAAA, but we are only allowed to replace one character
        int possibleSubsrtingLength = currentWindowSize - maxOccurance;
        if (possibleSubsrtingLength <= k)
        {
          // update our result
          maxLength = Math.Max(currentWindowSize, maxLength);
          // increment right
          right++;
        }
        else
        {
          // When one window is done, will update left to next char and reset the right as left and start with a new window and try to get the best value for maxLength.
          left++;
          right = left;
          // reset the frequency array as well for the new window.
          frequency = new int[26];
        }
      }
      return maxLength;
    }
  }
}
