using System.Text.RegularExpressions;

namespace _12_JSAbacusFramework
{
  internal partial class JSAbacus
  {
    internal static List<int> GetAllNumbers(string str)
    {
      var numbers = new List<int>();

      int n = 0;
      while (n < str.Length)
      {
        if (char.IsDigit(str[n]))
        {
          var number = ParseNumber(str, ref n);
          numbers.Add(number);
        }
        else if (str[n] == '-')
        {
          ++n;
          var number = ParseNumber(str, ref n);
          numbers.Add(-number);
        }
        else
        {
          ++n;
        }
      }

      return numbers;
    }

    private static int ParseNumber(string str, ref int n)
    {
      int number = 0;
      while (n < str.Length && char.IsDigit(str[n]))
      {
        number *= 10;
        number += str[n] - '0';
        ++n;
      }

      return number;
    }

    internal static int GetSumOfAllNumbers(string str)
    {
      return GetAllNumbers(str).Sum();
    }
  }
}