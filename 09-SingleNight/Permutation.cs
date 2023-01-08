using System.Text.RegularExpressions;

namespace _09_SingleNight
{
  record RouteDistance(Route Route, int Distance);

  record struct Route(string Start, string End);

  record Input(List<string> Cities, Dictionary<Route, int> Distances);

  internal partial class Permutation
  {
    internal static IEnumerable<List<int>> GetAllOf(int n)
    {
      var initial = Enumerable.Range(0, n).ToList();

      return GetPermutations(initial, 0);
    }

    internal static RouteDistance ParseLine(string line)
    {
      var regEx = RegExRoute();
      var match = regEx.Match(line);
      if (match.Success)
      {
        var from = match.Groups["from"].Value;
        var to = match.Groups["to"].Value;
        var dist = int.Parse(match.Groups["dist"].Value);
        return new RouteDistance(new Route(from, to), dist);
      }
      throw new ApplicationException();
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

    [GeneratedRegex("(?<from>\\w+) to (?<to>\\w+) = (?<dist>\\d+)")]
    private static partial Regex RegExRoute();

    internal static Input ParseInput(string text)
    {
      var cities = new HashSet<string>();
      var distances = new Dictionary<Route, int>();

      foreach (var line in text.Split('\n'))
        if (!string.IsNullOrWhiteSpace(line))
        {
          var route = ParseLine(line);
          cities.Add(route.Route.Start);
          cities.Add(route.Route.End);

          distances.Add(route.Route, route.Distance);
          distances.Add(new Route(route.Route.End, route.Route.Start), route.Distance);
        }

      return new Input(cities.ToList(), distances);
    }

    internal static int GetRouteDistance(Input input, List<int> route)
    {
      var distance = 0;

      for (int n = 1; n < route.Count; ++n)
      {
        var r = new Route(input.Cities[route[n - 1]], input.Cities[route[n]]);
        var d = input.Distances[r];
        distance += d;
      }

      return distance;
    }

    internal static int GetShortestDistance(string text)
    {
      var input = ParseInput(text);
      var permutations = GetAllOf(input.Cities.Count);

      var minDistance = permutations.Select(p => GetRouteDistance(input, p)).Min();
      return minDistance;
    }

    internal static int GetLongestDistance(string text)
    {
      var input = ParseInput(text);
      var permutations = GetAllOf(input.Cities.Count);

      var minDistance = permutations.Select(p => GetRouteDistance(input, p)).Max();
      return minDistance;
    }
  }
}