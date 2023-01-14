using System.Text.RegularExpressions;

namespace _16_AuntSue
{
  internal record TicketTape(Dictionary<string, int> Things);

  internal record Aunt(int Id, TicketTape Things);

  internal partial class CrimeScene
  {
    internal static TicketTape GetTicketTape()
    {
      const string thingsString = "children: 3\r\ncats: 7\r\nsamoyeds: 2\r\npomeranians: 3\r\nakitas: 0\r\nvizslas: 0\r\ngoldfish: 5\r\ntrees: 3\r\ncars: 2\r\nperfumes: 1";
      return GetTicketTape(thingsString, '\n');
    }

    private static TicketTape GetTicketTape(string input, char separator)
    {
      var things = new Dictionary<string, int>();
      foreach (var line in input.Split(separator))
      {
        var (thing, count) = ParseThing(line);
        things.Add(thing, count);
      }

      return new TicketTape(things);
    }

    internal static Aunt ParseAunt(string line)
    {
      var regEx = RegExAunt();
      var match = regEx.Match(line);
      if (match.Success)
      {
        var id = int.Parse(match.Groups["Id"].Value);
        var things = GetTicketTape(match.Groups["things"].Value, ',');
        return new Aunt(id, things);
      }
      throw new ApplicationException();
    }

    private static (string thing, int count) ParseThing(string line)
    {
      var elements = line.Split(':');
      var thing = elements[0].Trim();
      var count = int.Parse(elements[1].Trim());
      return (thing, count);
    }

    internal static int GetAuntId(string input, bool part2)
    {
      var matchingAunts = GetMatchingAunts(GetTicketTape(), input, part2);
      return matchingAunts.Single().Id;
    }

    private static IEnumerable<Aunt> GetMatchingAunts(TicketTape ticketTape, string input, bool part2)
    {
      foreach (var line in input.Split('\n'))
        if (!string.IsNullOrWhiteSpace(line))
        {
          var aunt = ParseAunt(line);
          if (IsAuntMatching(aunt, ticketTape, part2))
            yield return aunt;
        }
    }

    private static bool IsAuntMatching(Aunt aunt, TicketTape ticketTape, bool part2)
    {
      foreach (var thing in aunt.Things.Things)
      {
        if (part2 && (thing.Key == "cats" || thing.Key == "trees"))
        {
          if (ticketTape.Things[thing.Key] >= thing.Value)
            return false;
        }
        else if (part2 && (thing.Key == "pomeranians" || thing.Key == "goldfish"))
        {
          if (ticketTape.Things[thing.Key] <= thing.Value)
            return false;
        }
        else if (ticketTape.Things[thing.Key] != thing.Value)
          return false;
      }
      return true;
    }

    [GeneratedRegex("Sue (?<Id>\\d+): (?<things>.+)")]
    private static partial Regex RegExAunt();
  }
}