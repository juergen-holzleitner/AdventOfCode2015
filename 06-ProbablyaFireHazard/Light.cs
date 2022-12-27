using System.Text.RegularExpressions;

namespace _06_ProbablyaFireHazard
{
  public enum Action { Off, On, Toggle }
  record struct Pos(int X, int Y);
  record Instruction(Action Action, Pos TopLeft, Pos BottomRight);

  internal partial class Light
  {
    internal static int GetLightsOn(string text)
    {
      var instructions = ParseInstructions(text);
      var board = new Board();
      foreach (var instr in instructions)
      {
        board.Perform(instr);
      }
      return board.GetNumLightsOn();
    }

    internal static IEnumerable<Instruction> ParseInstructions(string text)
    {
      var instructions = new List<Instruction>();

      foreach (var line in text.Split('\n'))
        if (!string.IsNullOrWhiteSpace(line))
        {
          var instruction = ParseLine(line.Trim());
          instructions.Add(instruction);
        }
      return instructions;
    }

    internal static Instruction ParseLine(string text)
    {
      var regex = regexInstruction();
      var match = regex.Match(text);
      if (match.Success)
      {
        var action = GetActionFromString(match.Groups["action"].Value);
        var left = int.Parse(match.Groups["left"].Value);
        var top = int.Parse(match.Groups["top"].Value);
        var right = int.Parse(match.Groups["right"].Value);
        var bottom = int.Parse(match.Groups["bottom"].Value);

        if (right < left || bottom < top)
          throw new ApplicationException();

        return new Instruction(action, new Pos(left, top), new Pos(right, bottom));
      }

      throw new ApplicationException();
    }

    private static Action GetActionFromString(string action)
    {
      return action switch
      {
        "turn on" => Action.On,
        "turn off" => Action.Off,
        "toggle" => Action.Toggle,
        _ => throw new ApplicationException()
      };
    }

    [GeneratedRegex("(?<action>turn on|turn off|toggle) (?<left>\\d+),(?<top>\\d+) through (?<right>\\d+),(?<bottom>\\d+)")]
    private static partial Regex regexInstruction();
  }
}