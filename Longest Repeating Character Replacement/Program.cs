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

    // Run time  - O(26*n)
    // space - O(n)
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

    // Time - O(n)
    // Space - O(n)
    public int CharacterReplacement_Improved(string s, int k)
    {
      int res = 0;
      int l = 0; int r = 0;
      int maxFrequency = 0;
      int length = s.Length;
      int[] freq = new int[26];
      // r < length for the array out of bound
      // length - l > res, length - l if it is greater than current max res then only there is a chance to have a new updated res
      while (r < length && length - l > res)
      {
        int maxSubstringLength = r - l + 1;
        char c = s[r];
        // update the frequency
        freq[c - 'A']++;
        // we are maintaining global max freqency
        maxFrequency = Math.Max(maxFrequency, freq[c - 'A']);
        // (r - l + 1) is the current max length of the substring
        while (maxSubstringLength - maxFrequency > k)
        {
          char prevCharAtLeft = s[l];
          freq[prevCharAtLeft - 'A']--;
          l++;
          // udpate the maxSubstringLength because we have moved left position 
          maxSubstringLength = r - l + 1;
        }
        res = Math.Max(res, maxSubstringLength);
        r++;
      }

      return res;
    }
  }
}
