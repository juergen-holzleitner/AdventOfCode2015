using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace _22_WizardSimulator
{
  internal enum Turn { Player, Boss };
  internal enum PlayerAction { Missile, Drain, Shield, Poison, Recharge };
  internal record struct Person(int HitPoints, int Damage, int Armor);
  internal record struct Magic(int Mana, int ShieldEffectTime, int PoisonEffectTime, int RechargeEffectTime);
  internal record struct State(Person Player, Person Boss, Magic Magic, Turn NextTurn, int ManaUsed) : IComparable<State>
  {
    public int CompareTo(State other)
    {
      return ManaUsed.CompareTo(other.ManaUsed);
    }

    internal bool IsVictory()
    {
      return Boss.HitPoints <= 0;
    }
  }

  internal partial class RPG
  {
    internal static State DoBossStep(State state)
    {
      if (state.NextTurn != Turn.Boss)
        throw new ApplicationException();

      if (state.IsVictory())
        return state;

      var damage = Math.Max(1, state.Boss.Damage - state.Player.Armor);
      var newPlayer = state.Player with { HitPoints = state.Player.HitPoints - damage };
      return state with { Player = newPlayer, NextTurn = Turn.Player };
    }

    internal static State DoPlayerStep(State state, PlayerAction playerAction)
    {
      if (state.NextTurn != Turn.Player)
        throw new ApplicationException();

      var newMagic = state.Magic;
      var newBoss = state.Boss;
      var newPlayer = state.Player;

      var manaCosts = GetManaCosts(playerAction);
      if (newMagic.Mana < manaCosts)
        throw new ApplicationException();
      newMagic.Mana -= manaCosts;

      if (playerAction == PlayerAction.Missile)
      {
        newBoss.HitPoints -= 4;
      }
      else if (playerAction == PlayerAction.Drain)
      {
        newBoss.HitPoints -= 2;
        newPlayer.HitPoints += 2;
      }
      else if (playerAction == PlayerAction.Shield)
      {
        if (newMagic.ShieldEffectTime > 0)
          throw new ApplicationException();

        newMagic.ShieldEffectTime = 6;
      }
      else if (playerAction == PlayerAction.Poison)
      {
        if (newMagic.PoisonEffectTime > 0)
          throw new ApplicationException();

        newMagic.PoisonEffectTime = 6;
      }
      else if (playerAction == PlayerAction.Recharge)
      {
        if (newMagic.RechargeEffectTime > 0)
          throw new ApplicationException();

        newMagic.RechargeEffectTime = 5;
      }

      return state with { Player = newPlayer, Boss = newBoss, NextTurn = Turn.Boss, Magic = newMagic, ManaUsed = state.ManaUsed + manaCosts };
    }

    internal static State DoMagic(State state)
    {
      var newMagic = state.Magic;
      var newBoss = state.Boss;
      var newPlayer = state.Player;

      if (newMagic.ShieldEffectTime > 0)
      {
        --newMagic.ShieldEffectTime;
        newPlayer.Armor = 7;
      }
      else
      {
        newPlayer.Armor = 0;
      }

      if (newMagic.PoisonEffectTime > 0)
      {
        --newMagic.PoisonEffectTime;
        newBoss.HitPoints -= 3;
      }

      if (newMagic.RechargeEffectTime > 0)
      {
        --newMagic.RechargeEffectTime;
        newMagic.Mana += 101;
      }

      return state with { Player = newPlayer, Boss = newBoss, Magic = newMagic };
    }

    private static int GetManaCosts(PlayerAction playerAction)
    {
      return playerAction switch
      {
        PlayerAction.Missile => 53,
        PlayerAction.Drain => 73,
        PlayerAction.Shield => 113,
        PlayerAction.Poison => 173,
        PlayerAction.Recharge => 229,
        _ => throw new ApplicationException(),
      };
    }

    internal static State GetInitialState(string inputText, int initialPlayerHitPoints, int initialPlayerMana)
    {
      var boss = Parse(inputText);
      var player = new Person(initialPlayerHitPoints, 0, 0);
      var magic = new Magic(initialPlayerMana, 0, 0, 0);
      return new State(player, boss, magic, Turn.Player, 0);
    }

    internal static Person Parse(string text)
    {
      var regex = RegExPerson();
      var match = regex.Match(text);
      if (!match.Success)
        throw new ArgumentException("text is invalid", nameof(text));

      var hitPoints = int.Parse(match.Groups["hit"].Value);
      var damage = int.Parse(match.Groups["damage"].Value);
      return new Person(hitPoints, damage, 0);
    }

    [GeneratedRegex("Hit Points: (?<hit>\\d+)\\r?\\nDamage: (?<damage>\\d+)")]
    private static partial Regex RegExPerson();

    internal static int GetMinManaUsedToWin(string text, int initialPlayerHitPoints, int initialPlayerMana)
    {
      var initialState = GetInitialState(text, initialPlayerHitPoints, initialPlayerMana);

      var currentStates = new PriorityQueue<State, int>();
      currentStates.Enqueue(initialState, initialState.ManaUsed);

      while (currentStates.TryDequeue(out var current, out _))
      {
        if (current.IsVictory())
          return current.ManaUsed;

        current = DoMagic(current);

        if (current.NextTurn == Turn.Boss)
        {
          var newState = DoBossStep(current);
          if (newState.Player.HitPoints > 0)
            currentStates.Enqueue(newState, newState.ManaUsed);
        }
        else
        {
          foreach (var playerAction in Enum.GetValues<PlayerAction>())
          {
            if (IsPlayerActionPossible(current.Magic, playerAction))
            {
              var newState = DoPlayerStep(current, playerAction);
              currentStates.Enqueue(newState, newState.ManaUsed);
            }
          }
        }
      }

      throw new ApplicationException();
    }

    private static bool IsPlayerActionPossible(Magic magic, PlayerAction playerAction)
    {
      if (magic.Mana < GetManaCosts(playerAction))
        return false;

      return playerAction switch
      {
        PlayerAction.Shield => magic.ShieldEffectTime <= 0,
        PlayerAction.Poison => magic.PoisonEffectTime <= 0,
        PlayerAction.Recharge => magic.RechargeEffectTime <= 0,
        _ => true
      };
    }
  }
}