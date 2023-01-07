using System.Text;

namespace _11_CorporatePolicy
{
  internal class Password
  {
    internal static bool IsValid(string pwd)
    {
      return IsValid(new StringBuilder(pwd));
    }

    internal static bool IsValid(StringBuilder pwd)
    {
      if (pwd.Length != 8)
        throw new ArgumentException("must be 8 chars");

      for (int n = 0; n < pwd.Length; ++n)
        if (!char.IsAsciiLetterLower(pwd[n]))
          throw new ArgumentException("must be lower chars only");

      if (!HasThreeIncreasingLetters(pwd))
        return false;

      if (HasInvalidChar(pwd))
        return false;

      if (!HasTwoOverlappingPairs(pwd))
        return false;

      return true;
    }

    private static bool HasTwoOverlappingPairs(StringBuilder pwd)
    {
      int numPairs = 0;
      for (int n = 1; n < pwd.Length; ++n)
      {
        if (pwd[n] == pwd[n - 1]) 
        {
          ++numPairs;
          ++n;
        }
      }

      return numPairs >= 2;
    }

    private static bool HasInvalidChar(StringBuilder pwd)
    {
      for (int n = 0; n < pwd.Length; ++n)
        if (!IsValidChar(pwd[n]))
          return true;
      return false;
    }

    private static bool IsValidChar(char ch)
    {
      return ch != 'i' && ch != 'o' && ch != 'l';
    }

    private static bool HasThreeIncreasingLetters(StringBuilder pwd)
    {
      for (int n = 2; n < pwd.Length; ++n)
      {
        if (pwd[n - 2] == pwd[n] - 2
          && pwd[n - 1] == pwd[n] - 1)
          return true;
      }

      return false;
    }

    internal static IEnumerable<StringBuilder> GetNextPasswords(string initialPassword)
    {
      return GetNextPasswords(new StringBuilder(initialPassword));
    }

    internal static IEnumerable<StringBuilder> GetNextPasswords(StringBuilder initialPassword)
    {
      var current = initialPassword;

      for (; ; )
      {
        yield return GetNextPassword(current);
      }
    }

    private static StringBuilder GetNextPassword(StringBuilder current)
    {
      for (int n = current.Length - 1; n >= 0; --n)
      {
        do
        {
          ++current[n];
        } while (!IsValidChar(current[n]));

        if (current[n] <= 'z')
          return current;

        current[n] = 'a';
      }

      throw new ApplicationException();
    }

    internal static string GetNextValidPassword(string initial)
      => GetNextValidPassword(new StringBuilder(initial)).ToString();

    internal static StringBuilder GetNextValidPassword(StringBuilder initial)
    {
      foreach (var pwd in GetNextPasswords(initial))
      {
        if (IsValid(pwd))
          return pwd;
      }

      throw new ApplicationException();
    }
  }
}