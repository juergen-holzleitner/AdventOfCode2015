using System.Text.RegularExpressions;

namespace _07_SomeAssemblyRequired
{
  record Instruction(string Target, Operand Operand);

  record Operand();

  record NumberOperand(ushort Number) : Operand;
  record NotOperand(Factor Factor) : Operand;
  public enum Operation { And, Or, LShift, RShift };
  record BinaryOperand(Factor LeftFactor, Operation Operation, Factor RightFactor) : Operand;

  record Factor();
  record VariableFactor(string Name) : Factor;
  record NumberFactor(ushort Number) : Factor;

  internal partial class Wire
  {
    internal static Instruction ParseInstruction(string text)
    {
      var regex = RegExInstruction();
      var match = regex.Match(text);
      if (match.Success)
      {
        var target = match.Groups["target"].Value;
        var operand = ParseOperand(match.Groups["operand"].Value);
        return new Instruction(target, operand);
      }

      throw new ApplicationException();
    }

    private static Operand ParseOperand(string operand)
    {
      if (ushort.TryParse(operand, out var numberValue))
      {
        return new NumberOperand(numberValue);
      }

      var regexNot = RegExNot();
      var matchNot = regexNot.Match(operand);
      if (matchNot.Success)
      {
        var factor = ParseFactor(matchNot.Groups["factor"].Value);
        return new NotOperand(factor);
      }

      var regexBinary = RegExBinaryInstruction();
      var matchBinary = regexBinary.Match(operand);
      if (matchBinary.Success)
      {
        var leftFactor = ParseFactor(matchBinary.Groups["factorLeft"].Value);
        var rightFactor = ParseFactor(matchBinary.Groups["factorRight"].Value);
        var operation = ParseOperation(matchBinary.Groups["operation"].Value);
        return new BinaryOperand(leftFactor, operation, rightFactor);
      }

      throw new ApplicationException();
    }

    private static Operation ParseOperation(string operation)
    {
      return operation switch
      {
        "AND" => Operation.And,
        "OR" => Operation.Or,
        "LSHIFT" => Operation.LShift,
        "RSHIFT" => Operation.RShift,
        _ => throw new ApplicationException()
      };
    }

    private static Factor ParseFactor(string factor)
    {
      if (ushort.TryParse(factor, out var number))
        return new NumberFactor(number);

      return new VariableFactor(factor);
    }

    [GeneratedRegex("(?<operand>.*?) -> (?<target>[a-z]+)")]
    private static partial Regex RegExInstruction();
    [GeneratedRegex("NOT (?<factor>\\d+|[a-z]+)")]
    private static partial Regex RegExNot();
    [GeneratedRegex("(?<factorLeft>\\d+|[a-z]+) (?<operation>[A-Z]+) (?<factorRight>\\d+|[a-z]+)")]
    private static partial Regex RegExBinaryInstruction();

    internal static List<Instruction> Parse(string text)
    {
      var instructions = new List<Instruction>();
      foreach (var line in text.Split('\n'))
        if (!string.IsNullOrWhiteSpace(line))
        {
          var instruction = ParseInstruction(line.TrimEnd());
          instructions.Add(instruction);
        }
      return instructions;
    }
  }
}