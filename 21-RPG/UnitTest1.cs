using FluentAssertions;

namespace _21_RPG
{
  public class UnitTest1
  {
    [Fact]
    public void Can_read_boss()
    {
      var text = "Hit Points: 104\r\nDamage: 8\r\nArmor: 1\r\n";
      var input = RPG.Parse(text);
      input.Should().Be(new Person(104, 8, 1));
    }

    [Fact]
    public void Can_calculate_player_damage()
    {
      var player = new Person(8, 5, 5);
      var boss = new Person(12, 7, 2);

      var playerDamage = RPG.GetPlayerDamage(player, boss);

      playerDamage.Should().Be(3);
    }

    [Fact]
    public void Can_calculate_boss_damage()
    {
      var player = new Person(8, 5, 5);
      var boss = new Person(12, 7, 2);

      var playerDamage = RPG.GetBossDamage(player, boss);

      playerDamage.Should().Be(2);
    }

    [Fact]
    public void Can_calculate_rounds_for_player()
    {
      var player = new Person(8, 5, 5);
      var boss = new Person(12, 7, 2);

      var numRounds = RPG.GetRoundsUntilPlayerDies(player, boss);

      numRounds.Should().Be(4);
    }

    [Fact]
    public void Can_calculate_rounds_for_boss()
    {
      var player = new Person(8, 5, 5);
      var boss = new Person(12, 7, 2);

      var numRounds = RPG.GetRoundsUntilBossDies(player, boss);

      numRounds.Should().Be(4);
    }

    [Fact]
    public void Can_check_if_player_wins()
    {
      var player = new Person(8, 5, 5);
      var boss = new Person(12, 7, 2);

      var isPlayerWinning = RPG.IsPlayerWinning(player, boss);

      isPlayerWinning.Should().BeTrue();
    }

    [Fact]
    public void Can_get_all_weapon_options()
    {
      var weapons = RPG.GetAllWeapons();
      weapons.Should().HaveCount(5);
    }

    [Fact]
    public void Can_get_all_Armor()
    {
      var armor = RPG.GetAllArmor();
      armor.Should().HaveCount(5);
    }

    [Fact]
    public void Can_get_all_Rings()
    {
      var rings = RPG.GetAllRings();
      rings.Should().HaveCount(6);
    }

    [Fact]
    public void Can_enumerate_all_Weapons()
    {
      var weapons = RPG.EnumerateAllWeapons();
      weapons.Should().HaveCount(5);
    }

    [Fact]
    public void Can_enumerate_all_Armor()
    {
      var armors = RPG.EnumerateAllArmor();
      armors.Should().HaveCount(6);
    }

    [Fact]
    public void Can_enumerate_all_Rings()
    {
      var rings = RPG.EnumerateAllRings();
      rings.Should().HaveCount(22);
    }

    [Fact]
    public void Can_enumerate_all_options()
    {
      var options = RPG.EnumerateAllOptions();
      options.Should().HaveCount(660);
    }

    [Fact]
    public void Can_get_min_costs_to_win()
    {
      var text = "Hit Points: 104\r\nDamage: 8\r\nArmor: 1\r\n";
      var costs = RPG.GetMinCostsToWin(text);
      costs.Should().Be(78);
    }
  }
}