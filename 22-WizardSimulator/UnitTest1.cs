using FluentAssertions;

namespace _22_WizardSimulator
{
  public class UnitTest1
  {
    [Fact]
    public void Can_read_boss()
    {
      var inputText = "Hit Points: 104\r\nDamage: 8\r\n";
      var boss = RPG.Parse(inputText);
      boss.Should().Be(new Person(104, 8, 0));
    }

    [Fact]
    public void Can_setup_initial_state()
    {
      var inputText = "Hit Points: 13\r\nDamage: 8\r\n";
      var initialPlayerHitPoints = 10;
      var initialPlayerMana = 250;
      var initialState = RPG.GetInitialState(inputText, initialPlayerHitPoints, initialPlayerMana);
      initialState.Player.Should().Be(new Person(10, 0, 0));
      initialState.Boss.Should().Be(new Person(13, 8, 0));
      initialState.Magic.Should().Be(new Magic(250, 0, 0, 0));
      initialState.NextTurn.Should().Be(Turn.Player);
      initialState.ManaUsed.Should().Be(0);
    }

    [Fact]
    public void Can_do_boss_step()
    {
      var inputText = "Hit Points: 13\r\nDamage: 8\r\n";
      var initialPlayerHitPoints = 10;
      var initialPlayerMana = 250;
      var initialState = RPG.GetInitialState(inputText, initialPlayerHitPoints, initialPlayerMana);
      initialState.NextTurn = Turn.Boss;

      var nextState = RPG.DoBossStep(initialState);

      nextState.Player.HitPoints.Should().Be(2);
      nextState.NextTurn.Should().Be(Turn.Player);
    }

    [Fact]
    public void Can_do_player_poison_step()
    {
      var inputText = "Hit Points: 13\r\nDamage: 8\r\n";
      var initialPlayerHitPoints = 10;
      var initialPlayerMana = 250;
      var initialState = RPG.GetInitialState(inputText, initialPlayerHitPoints, initialPlayerMana);

      var nextState = RPG.DoPlayerStep(initialState, PlayerAction.Poison);

      nextState.NextTurn.Should().Be(Turn.Boss);
      nextState.Magic.Should().Be(new Magic(77, 0, 6, 0));
      nextState.ManaUsed.Should().Be(173);
    }

    [Fact]
    public void Can_do_player_missile_step()
    {
      var inputText = "Hit Points: 13\r\nDamage: 8\r\n";
      var initialPlayerHitPoints = 10;
      var initialPlayerMana = 250;
      var initialState = RPG.GetInitialState(inputText, initialPlayerHitPoints, initialPlayerMana);

      var nextState = RPG.DoPlayerStep(initialState, PlayerAction.Missile);

      nextState.NextTurn.Should().Be(Turn.Boss);
      nextState.Magic.Should().Be(new Magic(197, 0, 0, 0));
      nextState.Boss.HitPoints.Should().Be(9);
      nextState.ManaUsed.Should().Be(53);
    }

    [Fact]
    public void Can_do_player_drain_step()
    {
      var inputText = "Hit Points: 13\r\nDamage: 8\r\n";
      var initialPlayerHitPoints = 10;
      var initialPlayerMana = 250;
      var initialState = RPG.GetInitialState(inputText, initialPlayerHitPoints, initialPlayerMana);

      var nextState = RPG.DoPlayerStep(initialState, PlayerAction.Drain);

      nextState.NextTurn.Should().Be(Turn.Boss);
      nextState.Magic.Should().Be(new Magic(177, 0, 0, 0));
      nextState.Boss.HitPoints.Should().Be(11);
      nextState.Player.HitPoints.Should().Be(12);
      nextState.ManaUsed.Should().Be(73);
    }

    [Fact]
    public void Can_do_player_shield_step()
    {
      var inputText = "Hit Points: 13\r\nDamage: 8\r\n";
      var initialPlayerHitPoints = 10;
      var initialPlayerMana = 250;
      var initialState = RPG.GetInitialState(inputText, initialPlayerHitPoints, initialPlayerMana);

      var nextState = RPG.DoPlayerStep(initialState, PlayerAction.Shield);

      nextState.NextTurn.Should().Be(Turn.Boss);
      nextState.Magic.Should().Be(new Magic(137, 6, 0, 0));
      nextState.Player.Armor.Should().Be(0);
      nextState.ManaUsed.Should().Be(113);
    }

    [Fact]
    public void Can_do_player_recharge_step()
    {
      var inputText = "Hit Points: 13\r\nDamage: 8\r\n";
      var initialPlayerHitPoints = 10;
      var initialPlayerMana = 250;
      var initialState = RPG.GetInitialState(inputText, initialPlayerHitPoints, initialPlayerMana);

      var nextState = RPG.DoPlayerStep(initialState, PlayerAction.Recharge);

      nextState.NextTurn.Should().Be(Turn.Boss);
      nextState.Magic.Should().Be(new Magic(21, 0, 0, 5));
      nextState.ManaUsed.Should().Be(229);
    }

    [Fact]
    public void Can_perform_first_sample()
    {
      var inputText = "Hit Points: 13\r\nDamage: 8\r\n";
      var initialPlayerHitPoints = 10;
      var initialPlayerMana = 250;
      var initialState = RPG.GetInitialState(inputText, initialPlayerHitPoints, initialPlayerMana);

      var nextState = RPG.DoMagic(initialState);
      nextState = RPG.DoPlayerStep(nextState, PlayerAction.Poison);
      nextState.Magic.PoisonEffectTime.Should().BeGreaterThan(0);

      nextState = RPG.DoMagic(nextState);
      nextState = RPG.DoBossStep(nextState);

      nextState.Magic.PoisonEffectTime.Should().Be(5);
      nextState.Magic.Mana.Should().Be(77);
      nextState.Player.Should().Be(new Person(2, 0, 0));
      nextState.Boss.Should().Be(new Person(10, 8, 0));

      nextState = RPG.DoMagic(nextState);
      nextState = RPG.DoPlayerStep(nextState, PlayerAction.Missile);

      nextState.Magic.PoisonEffectTime.Should().Be(4);
      nextState.Magic.Mana.Should().Be(24);
      nextState.Player.Should().Be(new Person(2, 0, 0));
      nextState.Boss.Should().Be(new Person(3, 8, 0));

      nextState = RPG.DoMagic(nextState);
      nextState = RPG.DoBossStep(nextState);

      nextState.Boss.Should().Be(new Person(0, 8, 0));
      nextState.Player.Should().Be(new Person(2, 0, 0));
      nextState.Magic.PoisonEffectTime.Should().Be(3);

      nextState.IsVictory().Should().BeTrue();
      nextState.Magic.Mana.Should().Be(24);
      
      nextState.ManaUsed.Should().Be(173 + 53);
    }

    [Fact]
    public void Can_perform_second_sample()
    {
      var inputText = "Hit Points: 14\r\nDamage: 8\r\n";
      var initialPlayerHitPoints = 10;
      var initialPlayerMana = 250;
      var initialState = RPG.GetInitialState(inputText, initialPlayerHitPoints, initialPlayerMana);

      var nextState = RPG.DoMagic(initialState);
      nextState = RPG.DoPlayerStep(nextState, PlayerAction.Recharge);

      nextState.Player.Should().Be(new Person(10, 0, 0));
      nextState.Boss.Should().Be(new Person(14, 8, 0));
      nextState.Magic.Should().Be(new Magic(21, 0, 0, 5));

      nextState = RPG.DoMagic(nextState);
      nextState = RPG.DoBossStep(nextState);

      nextState.Player.Should().Be(new Person(2, 0, 0));
      nextState.Boss.Should().Be(new Person(14, 8, 0));
      nextState.Magic.Should().Be(new Magic(122, 0, 0, 4));

      nextState = RPG.DoMagic(nextState);
      nextState = RPG.DoPlayerStep(nextState, PlayerAction.Shield);

      nextState.Player.Should().Be(new Person(2, 0, 0));
      nextState.Boss.Should().Be(new Person(14, 8, 0));
      nextState.Magic.Should().Be(new Magic(110, 6, 0, 3));

      nextState = RPG.DoMagic(nextState);
      nextState = RPG.DoBossStep(nextState);

      nextState.Player.Should().Be(new Person(1, 0, 7));
      nextState.Boss.Should().Be(new Person(14, 8, 0));
      nextState.Magic.Should().Be(new Magic(211, 5, 0, 2));

      nextState = RPG.DoMagic(nextState);
      nextState = RPG.DoPlayerStep(nextState, PlayerAction.Drain);

      nextState.Player.Should().Be(new Person(3, 0, 7));
      nextState.Boss.Should().Be(new Person(12, 8, 0));
      nextState.Magic.Should().Be(new Magic(239, 4, 0, 1));

      nextState = RPG.DoMagic(nextState);
      nextState = RPG.DoBossStep(nextState);

      nextState.Player.Should().Be(new Person(2, 0, 7));
      nextState.Boss.Should().Be(new Person(12, 8, 0));
      nextState.Magic.Should().Be(new Magic(340, 3, 0, 0));

      nextState = RPG.DoMagic(nextState);
      nextState = RPG.DoPlayerStep(nextState, PlayerAction.Poison);

      nextState.Player.Should().Be(new Person(2, 0, 7));
      nextState.Boss.Should().Be(new Person(12, 8, 0));
      nextState.Magic.Should().Be(new Magic(167, 2, 6, 0));

      nextState = RPG.DoMagic(nextState);
      nextState = RPG.DoBossStep(nextState);

      nextState.Player.Should().Be(new Person(1, 0, 7));
      nextState.Boss.Should().Be(new Person(9, 8, 0));
      nextState.Magic.Should().Be(new Magic(167, 1, 5, 0));

      nextState = RPG.DoMagic(nextState);
      nextState = RPG.DoPlayerStep(nextState, PlayerAction.Missile);

      nextState.Player.Should().Be(new Person(1, 0, 7));
      nextState.Boss.Should().Be(new Person(2, 8, 0));
      nextState.Magic.Should().Be(new Magic(114, 0, 4, 0));

      nextState = RPG.DoMagic(nextState);
      nextState = RPG.DoBossStep(nextState);

      nextState.Player.Should().Be(new Person(1, 0, 0));
      nextState.Boss.Should().Be(new Person(-1, 8, 0));
      nextState.Magic.Should().Be(new Magic(114, 0, 3, 0));

      nextState.IsVictory().Should().BeTrue();
      nextState.ManaUsed.Should().Be(641);
    }

    [Fact]
    public void Can_simulate_best_strategy_for_first_sample()
    {
      var inputText = "Hit Points: 13\r\nDamage: 8\r\n";
      var initialPlayerHitPoints = 10;
      var initialPlayerMana = 250;

      var minManaUsed = RPG.GetMinManaUsedToWin(inputText, initialPlayerHitPoints, initialPlayerMana);

      minManaUsed.Should().Be(226);
    }

    [Fact]
    public void Can_simulate_best_strategy_for_second_sample()
    {
      var inputText = "Hit Points: 14\r\nDamage: 8\r\n";
      var initialPlayerHitPoints = 10;
      var initialPlayerMana = 250;

      var minManaUsed = RPG.GetMinManaUsedToWin(inputText, initialPlayerHitPoints, initialPlayerMana);

      minManaUsed.Should().Be(641);
    }
  }
}