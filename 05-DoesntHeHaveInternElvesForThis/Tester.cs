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
  }
}