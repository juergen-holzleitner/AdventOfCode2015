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

      instruction.Should().Be(new Instruction(expectedTarget, new NumberOperand(expectedNumber)));
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
      var text = "123 -> x\r\n456 -> y\r\nx AND y -> d\r\nx OR y -> e\r\nx LSHIFT 2 -> f\r\ny RSHIFT 2 -> g\r\nNOT x -> h\r\nNOT y -> i";
      var instructions = Wire.Parse(text);
      instructions.Should().HaveCount(8);
    }

  }
}