using System.Text;

namespace _10_ElvesLookElvesSay
{
  internal class Converter
  {
    internal static int GetLengthAfter(string input, int repeats)
    {
      return GetAfter(input, repeats).Length;
    }

    internal static string GetAfter(string input, int repeats)
    {
      for (int n = 0; n < repeats; n++)
      {
        input = Get(input);
      }

      return input;
    }

    internal static string Get(string input)
    {
      var str = new StringBuilder();

      char? currentChar = null;
      int numOccurences = 0;
      foreach (var ch in input)
      {
        if (ch != currentChar)
        {
          AppendNumber(str, currentChar, numOccurences);

          currentChar = ch;
          numOccurences = 1;
        }
        else
        {
          ++numOccurences;
        }
      }

      AppendNumber(str, currentChar, numOccurences);

      return str.ToString();
    }

    private static void AppendNumber(StringBuilder str, char? currentChar, int numOccurences)
    {
      if (numOccurences > 0)
      {
        str.Append(numOccurences);
        str.Append(currentChar);
      }
    }


  }
}