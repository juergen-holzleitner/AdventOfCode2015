using System.Text.RegularExpressions;

namespace _14_ReindeerOlympics
{
  internal record Reindeer(string Name, int Speed, int Time, int Rest)
  {
    internal long GetDistanceAfter(long seconds)
    {
      var cycleLength = Time + Rest;
      var fullCycles = seconds / cycleLength;

      var secondsRemaining = Math.Min(seconds % cycleLength, Time);
      return Speed * secondsRemaining + fullCycles * Speed * Time;
    }
  }

  internal partial class ReindeerProcessor
  {
    internal static long GetMaxDistance(string text, long seconds)
    {
      var reindeers = ParseInput(text);
      return reindeers.Select(r => r.GetDistanceAfter(seconds)).Max();
    }

    internal static int GetMaxPoints(string text, long seconds)
    {
      var reindeers = ParseInput(text);

      var points = new Dictionary<string, int>();
      foreach (var r in reindeers)
        points.Add(r.Name, 0);

      for (int n = 1; n <= seconds; ++n)
      {
        var distances = from r in reindeers
                        let val = new { r.Name, Distance = r.GetDistanceAfter(n) }
                        orderby val.Distance descending
                        select val;

        var maxDistance = distances.First().Distance;

        var leadingReindeers = from r in distances where r.Distance == maxDistance select r;
        foreach (var r in leadingReindeers)
          ++points[r.Name];
      }

      return points.Values.Max();
    }

    internal static IEnumerable<Reindeer> ParseInput(string text)
    {
      foreach (var line in text.Split('\n'))
        if (!string.IsNullOrWhiteSpace(line))
        {
          yield return ParseLine(line);
        }
    }

    internal static Reindeer ParseLine(string line)
    {
      var regex = RegExReindeer();
      var match = regex.Match(line);
      if (match.Success)
      {
        var name = match.Groups["name"].Value;
        var speed = int.Parse(match.Groups["speed"].Value);
        var time = int.Parse(match.Groups["time"].Value);
        var rest = int.Parse(match.Groups["rest"].Value);
        return new Reindeer(name, speed, time, rest);
      }

      throw new ArgumentException(line, nameof(line));
    }

    [GeneratedRegex("(?<name>\\w+) can fly (?<speed>\\d+) km/s for (?<time>\\d+) seconds, but then must rest for (?<rest>\\d+) seconds.")]
    private static partial Regex RegExReindeer();
  }
}