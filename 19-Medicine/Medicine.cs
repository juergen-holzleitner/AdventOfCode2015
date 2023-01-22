using System.Text;
using System.Text.RegularExpressions;

namespace _19_Medicine
{
  internal record Replacement(string From, string To);
  internal record Input(List<Replacement> Replacements, string Starting);

  internal partial class Medicine
  {
    internal static IEnumerable<string> GetAllReplacements(string text, Replacement replacement)
    {
      int n = 0;
      for (; ; )
      {
        n = text.IndexOf(replacement.From, n);
        if (n < 0)
          yield break;

        var sb = new StringBuilder(text);
        sb.Remove(n, replacement.From.Length);
        sb.Insert(n, replacement.To);
        yield return sb.ToString();

        ++n;
      }
    }

    internal static IEnumerable<string> GetAllReplacements(Input input)
    {
      return GetNextMolecules(input.Starting, input.Replacements);
    }

    internal static IEnumerable<string> GetNextMolecules(string current, List<Replacement> replacements)
    {
      return GetNextMolecules(new[] { current }, replacements);
    }

    internal static IEnumerable<string> GetNextMolecules(IEnumerable<string> currentElements, List<Replacement> replacements)
    {
      foreach (var current in currentElements)
        foreach (var replacement in replacements)
        {
          foreach (var str in GetAllReplacements(current, replacement))
            yield return str;
        }
    }

    internal static int GetNumDistinctReplacements(string text)
    {
      var input = ParseInput(text);
      var replacements = GetAllReplacements(input);
      return replacements.Distinct().Count();
    }

    internal static int GetNumStepsUntilFound(string text)
    {
      var input = ParseInput(text);
      var n = 0;
      IEnumerable<string> current = new[] { "e" };
      for (; ; )
      {
        if (current.Contains(input.Starting))
          return n;

        ++n;
        current = GetNextMolecules(current, input.Replacements).Where(e => e.Length <= input.Starting.Length).Distinct();
      }
    }

    internal static Input ParseInput(string text)
    {
      var lines = text.Split('\n');
      int n;
      var replacements = new List<Replacement>();
      for (n = 0; ; ++n)
      {
        var line = lines[n];
        if (string.IsNullOrWhiteSpace(line))
        {
          break;
        }

        var regEx = RegexReplacement();
        var match = regEx.Match(line);
        if (!match.Success)
        {
          throw new ApplicationException();
        }

        var from = match.Groups["from"].Value;
        var to = match.Groups["to"].Value;
        replacements.Add(new Replacement(from, to));
      }

      var starting = lines[n + 1].Trim();
      return new Input(replacements, starting);
    }

    private static int CountViaProductionRules(string str)
    {
      Func<string, int> countStr = x =>
      {
        var count = 0;
        for (var index = str.IndexOf(x); index >= 0; index = str.IndexOf(x, index + 1), ++count) { }
        return count;
      };

      var num = str.Count(char.IsUpper) - countStr("Rn") - countStr("Ar") - 2 * countStr("Y") - 1;
      return num;
    }

    [GeneratedRegex("(?<from>\\w+) => (?<to>\\w+)")]
    private static partial Regex RegexReplacement();

    internal static int CountWithProductionRules(string text)
    {
      var input = ParseInput(text);
      return CountViaProductionRules(input.Starting);
    }
  }
}