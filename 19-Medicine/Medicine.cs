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
      foreach (var replacement in input.Replacements)
      {
        foreach (var str in GetAllReplacements(input.Starting, replacement))
          yield return str;
      }
    }

    internal static int GetNumDistinctReplacements(string text)
    {
      var input = ParseInput(text);
      var replacements = GetAllReplacements(input);
      return replacements.Distinct().Count();
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

    [GeneratedRegex("(?<from>\\w+) => (?<to>\\w+)")]
    private static partial Regex RegexReplacement();
  }
}