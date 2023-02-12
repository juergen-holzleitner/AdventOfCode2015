using System.Security.Cryptography;

namespace _24_Balance
{
  internal class Balance
  {
    internal static long GetTargetWeight(long[] elements, int numGroups)
    {
      return elements.Sum() / numGroups;
    }

    internal static List<long> Parse(string input)
    {
      var result = new List<long>();
      foreach (var line in input.Split('\n'))
      if (!string.IsNullOrWhiteSpace(line))
      {
          result.Add(long.Parse(line));
      }
      return result;
    }

    internal static IEnumerable<List<long>> EnumerateGroupsOf(string input, int groupSize, long groupWeight)
    {
      var elements = Parse(input).ToArray();

      return EnumerateGroupsOf(elements, groupSize, 0, new(), groupWeight);
    }

    private static IEnumerable<List<long>> EnumerateGroupsOf(long[] elements, int groupSize, int n, List<long> group, long groupWeight)
    {
      if (n < elements.Length)
      {
        group.Add(elements[n]);
        if (group.Sum() <= groupWeight)
        {
          if (group.Count == groupSize)
          {
            if (group.Sum() == groupWeight)
              yield return new List<long>(group);
          }
          else
          {
            foreach (var g in EnumerateGroupsOf(elements, groupSize, n + 1, group, groupWeight))
              yield return g;
          }
        }
        group.Remove(elements[n]);

        foreach (var g in EnumerateGroupsOf(elements, groupSize, n + 1, group, groupWeight))
          yield return g;
      }
    }

    internal static IEnumerable<List<long>> EnumerateShortestGroupOf(string input, int numGroups)
    {
      var elements = Parse(input).ToArray();
      long groupWeight = GetTargetWeight(elements, numGroups);

      for (int groupSize = 1; groupSize <= elements.Length; ++groupSize)
      {
        var groups = EnumerateGroupsOf(elements, groupSize, 0, new(), groupWeight).ToList();
        if (groups.Any())
          return groups;
      }

      throw new ApplicationException();
    }

    internal static long GetSmallestQE(string input, int numGroups)
    {
      var groups = EnumerateShortestGroupOf(input, numGroups);
      return (from g in groups let qe = g.Aggregate(1, (long a, long b) => a * b) orderby qe select qe).First();
    }
  }
}