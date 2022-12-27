namespace _05_DoesntHeHaveInternElvesForThis
{
  internal class Tester
  {
    internal static int GetNumNiceWords(string input)
    {
      int numNice = 0;
      foreach (var word in input.Split('\n'))
      {
        if (!string.IsNullOrWhiteSpace(word))
          if (IsNiceWord(word))
            ++numNice;
      }
      return numNice;
    }

    internal static bool HasDoubleRepeating(string input)
    {
      for (int n = 1; n < input.Length; ++n)
      {
        if (HasCharacterRepeating(input, input[n - 1], input[n], n + 1))
          return true;
      }
      return false;
    }

    private static bool HasCharacterRepeating(string input, char ch1, char ch2, int startPos)
    {
      for (int n = startPos + 1; n < input.Length; ++n)
        if (input[n - 1] == ch1 && input[n] == ch2)
          return true;
      return false;
    }

    internal static bool HasForbiddenString(string testString)
    {
      var forbiddenStrings = new[] { "ab", "cd", "pq", "xy" };

      foreach (var str in forbiddenStrings)
      {
        if (testString.Contains(str))
          return true;
      }

      return false;
    }

    internal static bool HasRepeatingCharacter(string testString)
    {
      for (int n = 1; n < testString.Length; ++n)
        if (testString[n - 1] == testString[n])
          return true;

      return false;
    }

    internal static bool HasThreeVovels(string testString)
    {
      int numVovels = 0;
      foreach (var ch in testString)
      {
        if (IsVovel(ch))
        {
          ++numVovels;
          if (numVovels >= 3)
            return true;
        }
      }
      return false;
    }

    internal static bool IsNiceWord(string testString)
    {
      if (HasForbiddenString(testString))
        return false;

      if (!HasThreeVovels(testString))
        return false;

      if (!HasRepeatingCharacter(testString))
        return false;

      return true;
    }

    private static bool IsVovel(char ch)
    {
      return ch switch
      {
        'a' => true,
        'e' => true,
        'i' => true,
        'o' => true,
        'u' => true,
        _ => false
      };
    }

    internal static bool HasRepeatingWithBetween(string input)
    {
      for (int n = 2; n < input.Length; ++n)
        if (input[n - 2] == input[n])
          return true;
      return false;
    }

    internal static bool IsNicePart2(string input)
    {
      return HasDoubleRepeating(input) && HasRepeatingWithBetween(input);
    }

    internal static int GetNumNiceWordsPart2(string input)
    {
      int numNice = 0;
      foreach (var word in input.Split('\n'))
      {
        if (!string.IsNullOrWhiteSpace(word))
          if (IsNicePart2(word))
            ++numNice;
      }
      return numNice;
    }
  }
}