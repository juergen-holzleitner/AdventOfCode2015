using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace _13_KnightsoftheDinnerTable
{
  internal record struct LineInput(Position Position, int Happiness);

  internal record struct Position(string Person, string Neighbor);

  internal record Input(Dictionary<Position, int> Table);

  internal partial class Dinner
  {
    internal static IEnumerable<List<int>> GetAllPermutationsOf(int n)
    {
      var initial = Enumerable.Range(0, n).ToList();

      return GetPermutations(initial, 0);
    }

    internal static Input ParseInput(string text)
    {
      var table = new Dictionary<Position, int>();

      foreach (var line in text.Split('\n'))
        if (!string.IsNullOrWhiteSpace(line))
        {
          var input = ParseLine(line);
          table.Add(input.Position, input.Happiness);
        }

      return new Input(table);
    }

    internal static LineInput ParseLine(string line)
    {
      var regex = RegexLine();
      var match = regex.Match(line);
      if (match.Success)
      {
        var person = match.Groups["person"].Value;
        var neighbor = match.Groups["neighbor"].Value;
        var happiness = int.Parse(match.Groups["happiness"].Value);
        var type = match.Groups["type"].Value;
        if (type == "lose")
          happiness = -happiness;

        return new(new Position(person, neighbor), happiness);
      }

      throw new ArgumentException(line, nameof(line));
    }

    private static IEnumerable<List<int>> GetPermutations(List<int> currentList, int currentPos)
    {
      if (currentPos + 1 >= currentList.Count)
        yield return currentList;
      else
      {
        for (int n = currentPos; n < currentList.Count; ++n)
        {
          Swap(currentList, currentPos, n);
          foreach (var p in GetPermutations(currentList, currentPos + 1))
            yield return p;
          Swap(currentList, n, currentPos);
        }
      }
    }

    private static void Swap(List<int> currentList, int a, int b)
    {
      if (a == b)
        return;

      (currentList[b], currentList[a]) = (currentList[a], currentList[b]);
    }

    [GeneratedRegex("(?<person>\\w+) would (?<type>gain|lose) (?<happiness>\\d+) happiness units by sitting next to (?<neighbor>\\w+).")]
    private static partial Regex RegexLine();

    internal static List<string> GetPersons(Input input)
    {
      return input.Table.Keys.Select(k => k.Person).Distinct().ToList();
    }
    internal static int GetMaxHappiness(string text)
    {
      var input = ParseInput(text);
      var persons = GetPersons(input);

      var enumeration = GetAllPermutationsOf(persons.Count - 1);
      return enumeration.Select(e => CalculateHappiness(persons, e, input.Table)).Max();
    }

    internal static int CalculateHappiness(List<string> persons, List<int> permutation, Dictionary<Position, int> table)
    {
      int happiness = 0;
      for (int n = 0; n < persons.Count; ++n)
      {
        var person = GetPersonAt(persons, permutation, n);

        var prevN = n > 0 ? n - 1 : persons.Count - 1;
        var prevPerson = GetPersonAt(persons, permutation, prevN);

        var nextN = n < persons.Count - 1 ? n + 1 : 0;
        var nextPerson = GetPersonAt(persons, permutation, nextN);

        happiness += table[new Position(person, prevPerson)];
        happiness += table[new Position(person, nextPerson)];
      }

      return happiness;
    }

    internal static string GetPersonAt(List<string> persons, List<int> permutation, int position)
    {
      if (position == 0)
        return persons[0];

      var permPos = permutation[position - 1];
      return persons[permPos + 1];
    }
  }
}