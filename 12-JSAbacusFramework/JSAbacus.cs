using System.Text.RegularExpressions;

namespace _12_JSAbacusFramework
{
  internal partial class JSAbacus
  {
    internal static List<int> GetAllNumbers(string str)
    {
      var numbers = new List<int>();

      var regex = RegexNumber();
      var matches = regex.Matches(str);
      foreach (Match match in matches.Cast<Match>())
      {
        numbers.Add(int.Parse(match.Value));
      }

      return numbers;
    }

    internal static int GetSumOfAllNumbers(string str)
    {
      return GetAllNumbers(str).Sum();
    }

    [GeneratedRegex("-?\\d+")]
    private static partial Regex RegexNumber();
  }
}