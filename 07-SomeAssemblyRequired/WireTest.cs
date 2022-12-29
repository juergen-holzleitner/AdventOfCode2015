using FluentAssertions;

namespace _07_SomeAssemblyRequired
{
  public class WireTest
  {
    [Theory]
    [InlineData("123 -> x", "x", 123)]
    [InlineData("456 -> y", "y", 456)]
    public void Can_parse_number_instruction(string text, string expectedTarget, ushort expectedNumber)
    {
      var instruction = Wire.ParseInstruction(text);

      instruction.Should().Be(new Instruction(expectedTarget, new FactorOperand(new NumberFactor(expectedNumber))));
    }

    [Fact]
    public void Can_parse_assignment()
    {
      var text = "x -> y";
      var instruction = Wire.ParseInstruction(text);
      instruction.Operand.Should().Be(new FactorOperand(new VariableFactor("x")));
    }

    [Fact]
    public void Can_parse_unary_instruction_with_variable()
    {
      var text = "NOT x -> h";
      var instruction = Wire.ParseInstruction(text);
      instruction.Operand.Should().Be(new NotOperand(new VariableFactor("x")));
    }

    [Fact]
    public void Can_parse_unary_instruction_with_number()
    {
      var text = "NOT 123 -> h";
      var instruction = Wire.ParseInstruction(text);
      instruction.Operand.Should().Be(new NotOperand(new NumberFactor(123)));
    }

    [Theory]
    [InlineData("x AND 111 -> d", Operation.And)]
    [InlineData("x OR 111 -> d", Operation.Or)]
    [InlineData("x LSHIFT 111 -> d", Operation.LShift)]
    [InlineData("x RSHIFT 111 -> d", Operation.RShift)]
    public void Can_parse_binary_instruction(string text, Operation expectedOperation)
    {
      var binaryOperand = Wire.ParseInstruction(text).Operand as BinaryOperand;

      binaryOperand.Should().NotBeNull();
      binaryOperand!.LeftFactor.Should().Be(new VariableFactor("x"));
      binaryOperand!.RightFactor.Should().Be(new NumberFactor(111));
      binaryOperand.Operation.Should().Be(expectedOperation);
    }

    [Fact]
    public void Can_parse_multiple_instructions()
    {
      var text = "123 -> x\r\n456 -> y\r\nx AND y -> d\r\nx OR y -> e\r\nx LSHIFT 2 -> f\r\ny RSHIFT 2 -> g\r\nNOT x -> h\r\nNOT y -> i\r\n";
      var instructions = Wire.Parse(text);
      instructions.Should().HaveCount(8);
    }

    [Fact]
    public void Throw_if_wire_not_available()
    {
      var sut = new Wire(string.Empty);

      var act = () => sut.GetWireValue("a");

      act.Should().Throw<ApplicationException>();
    }

    [Theory]
    [InlineData("123 -> x", "x", 123)]
    [InlineData("456 -> y", "y", 456)]
    public void Can_get_value_of_simple_wire(string text, string wire, ushort expectedValue)
    {
      var sut = new Wire(text);

      var wireValue = sut.GetWireValue(wire);

      wireValue.Should().Be(expectedValue);
    }

    [Fact]
    public void Can_get_value_after_assignment()
    {
      var text = "123 -> x\r\nx -> y\r\n";
      var sut = new Wire(text);

      var wireValue = sut.GetWireValue("y");

      wireValue.Should().Be(123);
    }

    [Fact]
    public void Can_get_value_of_unary_operation()
    {
      var text = "NOT 123 -> x";
      var wireValue = Wire.GetFinalWireValue(text, "x");
      wireValue.Should().Be(65412);
    }

    [Theory]
    [InlineData("123 AND 456 -> a", 72)]
    [InlineData("123 OR 456 -> a", 507)]
    [InlineData("123 LSHIFT 2 -> a", 492)]
    [InlineData("456 RSHIFT 2 -> a", 114)]
    public void Can_get_value_of_binary_operation(string text, ushort expectedValue)
    {
      var wireValue = Wire.GetFinalWireValue(text, "a");
      wireValue.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData("d", 72)]
    [InlineData("e", 507)]
    [InlineData("f", 492)]
    [InlineData("g", 114)]
    [InlineData("h", 65412)]
    [InlineData("i", 65079)]
    [InlineData("x", 123)]
    [InlineData("y", 456)]
    public void Can_calculate_sample_of_part1(string wire, ushort expectedValue)
    {
      var text = "123 -> x\r\n456 -> y\r\nx AND y -> d\r\nx OR y -> e\r\nx LSHIFT 2 -> f\r\ny RSHIFT 2 -> g\r\nNOT x -> h\r\nNOT y -> i";
      var wireValue = Wire.GetFinalWireValue(text, wire);
      wireValue.Should().Be(expectedValue);
    }

    [Fact]
    public void Can_calculate_part2_by_steps()
    {
      var text = """
c OR b -> a
c AND d -> b
15 -> c
1 -> d
""";
      var wire = new Wire(text);
      var valueA = wire.GetWireValue("a");
      wire.ResetWireWithNumber("b", valueA);
      valueA = wire.GetWireValue("a");
      valueA.Should().Be(15);
    }

    [Fact]
    public void Can_calculate_part2()
    {
      var text = """
c OR b -> a
c AND d -> b
15 -> c
1 -> d
""";

      var wireValue = Wire.GetFinalWireValueWithRestart(text, "a", "b");
      wireValue.Should().Be(15);
    }
  }
}