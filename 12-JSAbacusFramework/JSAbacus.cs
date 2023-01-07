using System.Text.RegularExpressions;

namespace _12_JSAbacusFramework
{
  internal partial class JSAbacus
  {
    internal static List<int> GetAllNumbers(string str, bool discardRed)
    {
      int n = 0;
      if (str[n] == '{')
        return ParseObject(str, ref n, discardRed);
      else if (str[n] == '[')
        return ParseArray(str, ref n, discardRed);

      throw new ApplicationException();
    }

    private static List<int> ParseObject(string str, ref int n, bool discardRed)
    {
      if (str[n] != '{')
        throw new ApplicationException();
      ++n;

      bool returnEmpty = false;
      var numbers = new List<int>();

      while (n < str.Length)
      {
        if (discardRed && IsRed(str, ref n))
        {
          returnEmpty = true;
        }
        else if (char.IsDigit(str[n]))
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
        else if (str[n] == '{')
        {
          var objectNumbers = ParseObject(str, ref n, discardRed);
          numbers.AddRange(objectNumbers);
        }
        else if (str[n] == '}')
        {
          ++n;

          if (returnEmpty)
            return new();
          return numbers;
        }
        else if (str[n] == '[')
        {
          var arrayNumbers = ParseArray(str, ref n, discardRed);
          numbers.AddRange(arrayNumbers);
        }
        else
        {
          ++n;
        }
      }

      throw new ApplicationException();
    }

    private static List<int> ParseArray(string str, ref int n, bool discardRed)
    {
      if (str[n] != '[')
        throw new ApplicationException();
      ++n;

      var numbers = new List<int>();

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
        else if (str[n] == '{')
        {
          var objectNumbers = ParseObject(str, ref n, discardRed);
          numbers.AddRange(objectNumbers);
        }
        else if (str[n] == ']')
        {
          ++n;
          return numbers;
        }
        else if (str[n] == '[')
        {
          var arrayNumbers = ParseArray(str, ref n, discardRed);
          numbers.AddRange(arrayNumbers);
        }
        else
        {
          ++n;
        }
      }

      throw new ApplicationException();
    }

    private static bool IsRed(string str, ref int n)
    {
      const string valueRed = "red";

      if (str[n..].StartsWith(valueRed))
      {
        n += valueRed.Length;
        return true;
      }

      return false;
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
      return GetAllNumbers(str, false).Sum();
    }

    internal static int GetSumOfAllNumbersWithoutRed(string str)
    {
      return GetAllNumbers(str, true).Sum();
    }
  }
}