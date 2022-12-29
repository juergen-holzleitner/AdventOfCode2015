using System.Dynamic;
using System.Text.RegularExpressions;

namespace _07_SomeAssemblyRequired
{
  record Instruction(string Target, Operand Operand);

  record Operand();

  record FactorOperand(Factor Factor) : Operand;
  record NotOperand(Factor Factor) : Operand;
  public enum Operation { And, Or, LShift, RShift };
  record BinaryOperand(Factor LeftFactor, Operation Operation, Factor RightFactor) : Operand;

  record Factor();
  record VariableFactor(string Name) : Factor;
  record NumberFactor(ushort Number) : Factor;
  record CachedOperand(Operand Operand)
  {
    public ushort? Value { get; set; }
  }

  internal partial class Wire
  {
    private readonly Dictionary<string, CachedOperand> wires = new();

    public Wire(string instructionsText)
    {
      GenerateInstructions(instructionsText);
    }

    private void GenerateInstructions(string instructionsText)
    {
      var instructions = Parse(instructionsText);
      foreach (var instruction in instructions)
        wires.Add(instruction.Target, new CachedOperand(instruction.Operand));
    }

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
      var regEx = RegExFactor();
      var matchFactor = regEx.Match(operand);
      if (matchFactor.Success)
      {
        var factor = ParseFactor(matchFactor.Groups["factor"].Value);
        return new FactorOperand(factor);
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

    [GeneratedRegex("^(?<operand>.*?) -> (?<target>[a-z]+)$")]
    private static partial Regex RegExInstruction();
    [GeneratedRegex("^NOT (?<factor>\\d+|[a-z]+)$")]
    private static partial Regex RegExNot();
    [GeneratedRegex("^(?<factorLeft>\\d+|[a-z]+) (?<operation>[A-Z]+) (?<factorRight>\\d+|[a-z]+)$")]
    private static partial Regex RegExBinaryInstruction();
    [GeneratedRegex("^(?<factor>\\d+|[a-z]+)$")]
    private static partial Regex RegExFactor();

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

    internal ushort GetWireValue(string wire)
    {
      if (wires.TryGetValue(wire, out var operand))
      {
        if (!operand.Value.HasValue)
          operand.Value = GetOperandValue(operand.Operand);

        return operand.Value.Value;
      }

      throw new ApplicationException();
    }

    private ushort GetOperandValue(Operand operand)
    {
      if (operand is FactorOperand factorOperand)
      {
        return GetFactorValue(factorOperand.Factor);
      }

      if (operand is NotOperand notOperand)
      {
        return (ushort)~GetFactorValue(notOperand.Factor);
      }

      if (operand is BinaryOperand binaryOperand)
      {
        var leftFactor = GetFactorValue(binaryOperand.LeftFactor);
        var rightFactor = GetFactorValue(binaryOperand.RightFactor);
        return BinaryOperation(leftFactor, binaryOperand.Operation, rightFactor);
      }

      throw new ApplicationException();
    }

    private ushort BinaryOperation(ushort leftFactor, Operation operation, ushort rightFactor)
    {
      return operation switch
      {
        Operation.And => (ushort)(leftFactor & rightFactor),
        Operation.Or => (ushort)(leftFactor | rightFactor),
        Operation.LShift => (ushort)(leftFactor << rightFactor),
        Operation.RShift => (ushort)(leftFactor >> rightFactor),
        _ => throw new ApplicationException()
      };
    }

    private ushort GetFactorValue(Factor factor)
    {
      if (factor is NumberFactor numberFactor)
        return numberFactor.Number;
      if (factor is VariableFactor variableFactor)
        return GetWireValue(variableFactor.Name);

      throw new ApplicationException();
    }

    internal static ushort GetFinalWireValue(string text, string wireName)
    {
      var wire = new Wire(text);
      var wireValue = wire.GetWireValue(wireName);
      return wireValue;
    }
  }
}