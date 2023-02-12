using System.Numerics;
using System.Text.RegularExpressions;

namespace _25_Snow
{
  record Pos(int row, int col);

  internal partial class Snow
  {
    internal static int GetNumberAtPos(Pos pos)
    {
      var sum = GetSumOfNumbers(pos.row - 1 + pos.col - 1);
      return 1 + sum + pos.col - 1;
    }

    private static int GetSumOfNumbers(int num)
    {
      return (1 + num) * num / 2;
    }

    internal static Pos Parse(string input)
    {
      var regEx = RegExInput();
      var match = regEx.Match(input);
      if (match.Success)
      {
        var row = int.Parse(match.Groups["row"].Value);
        var col = int.Parse(match.Groups["col"].Value);
        return new Pos(row, col);
      }

      throw new ApplicationException();
    }

    [GeneratedRegex("To continue, please consult the code grid in the manual.  Enter the code at row (?<row>\\d+), column (?<col>\\d+)")]
    private static partial Regex RegExInput();

    internal static uint GetNumberAt(int pos)
    {
      var mul = BigInteger.ModPow(252533, pos - 1, 33554393);
      var val = (mul * 20151125) % 33554393;
      return (uint)val;
    }

    internal static uint GetNumber(string input)
    {
      var pos = Parse(input);
      var off = GetNumberAtPos(pos);
      return GetNumberAt(off);
    }
  }
}