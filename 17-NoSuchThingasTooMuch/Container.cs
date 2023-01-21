namespace _17_NoSuchThingasTooMuch
{
  internal class Container
  {
    internal static IEnumerable<List<int>> GetAllThatFit(List<int> containers, int expectedValue)
    {
      var items = new Stack<int>();

      return GetAllThatFit(containers, expectedValue, items, 0);
    }

    private static IEnumerable<List<int>> GetAllThatFit(List<int> containers, int valueLeft, Stack<int> items, int pos)
    {
      if (pos >= containers.Count)
        yield break;

      foreach (var item in GetAllThatFit(containers, valueLeft, items, pos + 1))
        yield return item;

      var nextItem = containers[pos];
      valueLeft -= nextItem;
      if (valueLeft < 0)
        yield break;

      items.Push(nextItem);
      if (valueLeft == 0)
      {
        yield return items.ToList();
        items.Pop();
        yield break;
      }

      foreach (var item in GetAllThatFit(containers, valueLeft, items, pos + 1))
        yield return item;

      items.Pop();
    }

    internal static List<int> Parse(string input)
    {
      var items = new List<int>();
      foreach (var line in input.Split('\n'))
        if (!string.IsNullOrWhiteSpace(line))
        {
          items.Add(int.Parse(line));
        }
      return items;
    }

    internal static int GetCountThatFit(string input, int expectedValue)
    {
      var containers = Parse(input);
      var permutations = GetAllThatFit(containers, expectedValue);
      return permutations.Count();
    }

    internal static int GetCountOfMin(string input, int expectedValue)
    {
      var containers = Parse(input);
      var permutations = GetAllThatFit(containers, expectedValue);

      var items = from p in permutations group p by p.Count into groups orderby groups.Key select new { Key = groups.Key, Num = groups.Count() };
      return items.First().Num;
    }
  }
}