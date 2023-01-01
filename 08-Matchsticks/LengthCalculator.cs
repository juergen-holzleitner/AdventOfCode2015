using System.Text.RegularExpressions;

namespace _08_Matchsticks
{
  internal record Length(int Code, int Number);
  internal class LengthCalculator
  {
    internal static int GetDiff(string text)
    {
      var total = new Length(0, 0);
      foreach (var str in text.Split('\n'))
      {
        if (!string.IsNullOrWhiteSpace(str))
        {
          var l = GetLength(str);
          total = total with { Code = total.Code + l.Code, Number = total.Number + l.Number };
        }
      }

      return total.Code - total.Number;
    }

    internal static int GetEncodedDiff(string text)
    {
      var total = new Length(0, 0);
      foreach (var str in text.Split('\n'))
      {
        if (!string.IsNullOrWhiteSpace(str))
        {
          var l = GetEncodedLength(str);
          total = total with { Code = total.Code + l.Code, Number = total.Number + l.Number };
        }
      }

      return total.Number - total.Code;
    }

    internal static Length GetEncodedLength(string str)
    {
      var strInitial = str.Trim();

      var strEscaped = strInitial;

      strEscaped = strEscaped.Replace("\"", "..");
      strEscaped = strEscaped.Replace("\\", "..");

      int codeLength = strInitial.Length;
      int numberLength = strEscaped.Length + 2;

      return new Length(codeLength, numberLength);
    }

    internal static Length GetLength(string str)
    {
      var strInitial = str.Trim();

      var strEscaped = strInitial;

      if (strEscaped.Length < 2)
        throw new ArgumentException();

      if (!strEscaped.StartsWith('\"'))
        throw new ArgumentException();
      if (!strEscaped.EndsWith('\"'))
        throw new ArgumentException();

      strEscaped = strEscaped[1..^1];

      var regEx = new Regex(@"\\x[0-9a-fA-F]{2}");
      strEscaped = regEx.Replace(strEscaped, ".");

      strEscaped = strEscaped.Replace("\\\"", ".");
      strEscaped = strEscaped.Replace("\\\\", ".");


      int codeLength = strInitial.Length;
      int numberLength = strEscaped.Length;
      
      return new Length(codeLength, numberLength);
    }
  }
}