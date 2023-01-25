using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace _21_RPG
{
  internal record Person(int HitPoints, int Damage, int Armor);
  internal record Item(string Name, int Cost, int Damage, int Armor);

  internal partial class RPG
  {
    internal static IEnumerable<Item> EnumerateAllArmor()
    {
      yield return new Item("nothing", 0, 0, 0);
      foreach (var item in GetAllArmor()) 
      { 
        yield return item; 
      }
    }

    internal static IEnumerable<Item> EnumerateAllOptions()
    {
      foreach (var weapon in EnumerateAllWeapons())
        foreach (var armor in EnumerateAllArmor())
          foreach (var ring in EnumerateAllRings())
          {
            yield return new Item(string.Empty, 
              weapon.Cost   + armor.Cost   + ring.Cost,
              weapon.Damage + armor.Damage + ring.Damage,
              weapon.Armor  + armor.Armor  + ring.Armor
              );
          }
    }

    internal static IEnumerable<Item> EnumerateAllRings()
    {
      yield return new Item("nothing", 0, 0, 0);

      var rings = GetAllRings();
      foreach (var ring in rings)
        yield return ring;

      for (int n0 = 0; n0 < rings.Count; ++n0)
        for (int n1 = n0 + 1; n1 < rings.Count; ++n1)
        {
          yield return new Item(
            rings[n0].Name   + rings[n1].Name, 
            rings[n0].Cost   + rings[n1].Cost, 
            rings[n0].Damage + rings[n1].Damage, 
            rings[n0].Armor  + rings[n1].Armor
            );
        }
    }

    internal static IEnumerable<Item> EnumerateAllWeapons() => GetAllWeapons();

    internal static List<Item> GetAllArmor()
    {
      return new List<Item> 
      {
        new Item("Leather",  13, 0, 1),
        new Item("Chainmail",  31, 0, 2),
        new Item("Splintmail",  53, 0, 3),
        new Item("Bandedmail",  75, 0, 4),
        new Item("Platemail", 102, 0, 5),
      };
    }

    internal static List<Item> GetAllRings()
    {
      return new List<Item>
      {
        new Item("Damage +1",  25, 1, 0),
        new Item("Damage +2",  50, 2, 0),
        new Item("Damage +3", 100, 3, 0),
        new Item("Defense +1",  20, 0, 1),
        new Item("Defense +2",  40, 0, 2),
        new Item("Defense +3",  80, 0, 3),
      };
    }

    internal static List<Item> GetAllWeapons()
    {
      return new List<Item>()
      {
        new Item("Dagger",  8, 4, 0),
        new Item("Shortsword", 10, 5, 0),
        new Item("Warhammer", 25, 6, 0),
        new Item("Longsword", 40, 7, 0),
        new Item("Greataxe", 74, 8, 0),
      };
    }

    internal static int GetBossDamage(Person player, Person boss)
    {
      return GetPlayerDamage(boss, player);
    }

    internal static int GetMinCostsToWin(string text)
    {
      var boss = Parse(text);

      var options = EnumerateAllOptions().OrderBy(o => o.Cost);
      foreach (var option in options)
      {
        var player = new Person(100, option.Damage, option.Armor);
        if (IsPlayerWinning(player, boss))
          return option.Cost;
      }

      throw new ApplicationException();
    }

    internal static int GetMaxCostsToLose(string text)
    {
      var boss = Parse(text);

      var options = EnumerateAllOptions().OrderByDescending(o => o.Cost);
      foreach (var option in options)
      {
        var player = new Person(100, option.Damage, option.Armor);
        if (!IsPlayerWinning(player, boss))
          return option.Cost;
      }

      throw new ApplicationException();
    }

    internal static int GetPlayerDamage(Person player, Person boss)
    {
      return Math.Max(1, player.Damage - boss.Armor);
    }

    internal static int GetRoundsUntilBossDies(Person player, Person boss)
    {
      return GetRoundsUntilPlayerDies(boss, player);
    }

    internal static int GetRoundsUntilPlayerDies(Person player, Person boss)
    {
      var opponentDamage = GetBossDamage(player, boss);
      var rounds = (player.HitPoints + opponentDamage - 1) / opponentDamage;
      return rounds;
    }

    internal static bool IsPlayerWinning(Person player, Person boss)
    {
      return GetRoundsUntilPlayerDies(player, boss) >= GetRoundsUntilBossDies(player, boss);
    }

    internal static Person Parse(string text)
    {
      var regex = RegExPerson();
      var match = regex.Match(text);
      if (!match.Success)
        throw new ArgumentException();

      var hitPoints = int.Parse(match.Groups["hit"].Value);
      var damage = int.Parse(match.Groups["damage"].Value);
      var armor = int.Parse(match.Groups["armor"].Value);
      return new Person(hitPoints, damage, armor);
    }

    [GeneratedRegex("Hit Points: (?<hit>\\d+)\\r?\\nDamage: (?<damage>\\d+)\\r?\\nArmor: (?<armor>\\d+)")]
    private static partial Regex RegExPerson();
  }
}