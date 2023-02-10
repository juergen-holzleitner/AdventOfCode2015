using FluentAssertions;

namespace _23_TuringLock
{
  public class UnitTest1
  {
    [Fact]
    public void Initial_register_values_are_zero()
    {
      var sut = new Cpu(0);

      var regA = sut.GetRegister(Register.A);
      var regB = sut.GetRegister(Register.B);
      var pos = sut.Position;

      regA.Should().Be(0);
      regB.Should().Be(0);
      pos.Should().Be(0);
    }

    [Fact]
    public void Can_parse_hlf_instruction()
    {
      var code = "hlf a";

      var instruction = Cpu.ParseInstruction(code);

      instruction.Should().Be(new Instruction(InstructionType.hlf, Register.A));
    }

    [Fact]
    public void Can_parse_hlf_instruction_for_register_b()
    {
      var code = "hlf b";

      var instruction = Cpu.ParseInstruction(code);

      instruction.Should().Be(new Instruction(InstructionType.hlf, Register.B));
    }

    [Fact]
    public void Can_parse_tpl_instruction()
    {
      var code = "tpl b";

      var instruction = Cpu.ParseInstruction(code);

      instruction.Should().Be(new Instruction(InstructionType.tpl, Register.B));
    }

    [Fact]
    public void Can_parse_inc_instruction()
    {
      var code = "inc a";

      var instruction = Cpu.ParseInstruction(code);

      instruction.Should().Be(new Instruction(InstructionType.inc, Register.A));
    }

    [Fact]
    public void Can_parse_jmp_instruction()
    {
      var code = "jmp -25";

      var instruction = Cpu.ParseInstruction(code);

      instruction.Should().Be(new Instruction(InstructionType.jmp, Register.A, -25));
    }

    [Fact]
    public void Can_parse_jie_instruction()
    {
      var code = "jie a, 10";

      var instruction = Cpu.ParseInstruction(code);

      instruction.Should().Be(new Instruction(InstructionType.jie, Register.A, 10));
    }

    [Fact]
    public void Can_parse_jio_instruction()
    {
      var code = "jio b, -1";

      var instruction = Cpu.ParseInstruction(code);

      instruction.Should().Be(new Instruction(InstructionType.jio, Register.B, -1));
    }

    [Fact]
    public void Can_read_all_instruction()
    {
      var code = "inc a\r\njio a, +2\r\ntpl a\r\ninc a";
      var instructions = Cpu.ParseInput(code);
      instructions.Should().BeEquivalentTo(new[] {
        new Instruction(InstructionType.inc, Register.A),
        new Instruction(InstructionType.jio, Register.A, 2),
        new Instruction(InstructionType.tpl, Register.A),
        new Instruction(InstructionType.inc, Register.A),
      });
    }

    [Fact]
    public void Can_parse_sample_input()
    {
      var code = File.ReadAllText("input.txt");
      var instructions = Cpu.ParseInput(code);
      instructions.Should().HaveCount(48);
    }

    [Fact]
    public void Can_process_inc()
    {
      var sut = new Cpu(0);
      
      sut.ProcessInstruction(new Instruction(InstructionType.inc, Register.A));

      sut.GetRegister(Register.A).Should().Be(1);
      sut.Position.Should().Be(1);
    }

    [Fact]
    public void Can_process_tpl()
    {
      var sut = new Cpu(0);

      sut.ProcessInstruction(new Instruction(InstructionType.inc, Register.B));
      sut.ProcessInstruction(new Instruction(InstructionType.tpl, Register.B));

      sut.GetRegister(Register.B).Should().Be(3);
      sut.Position.Should().Be(2);
    }

    [Fact]
    public void Can_process_hlf()
    {
      var sut = new Cpu(0);

      sut.ProcessInstruction(new Instruction(InstructionType.inc, Register.B));
      sut.ProcessInstruction(new Instruction(InstructionType.tpl, Register.B));
      sut.ProcessInstruction(new Instruction(InstructionType.hlf, Register.B));

      sut.GetRegister(Register.B).Should().Be(1);
      sut.Position.Should().Be(3);
    }

    [Fact]
    public void Can_process_jmp()
    {
      var sut = new Cpu(0);

      sut.ProcessInstruction(new Instruction(InstructionType.jmp, Register.A, 100));

      sut.Position.Should().Be(100);
    }

    [Fact]
    public void Can_process_jie()
    {
      var sut = new Cpu(0);

      sut.ProcessInstruction(new Instruction(InstructionType.jie, Register.A, -10));

      sut.Position.Should().Be(-10);
    }

    [Fact]
    public void Can_process_jie_not_satisfied()
    {
      var sut = new Cpu(0);

      sut.ProcessInstruction(new Instruction(InstructionType.inc, Register.A));
      sut.ProcessInstruction(new Instruction(InstructionType.jie, Register.A, -10));

      sut.Position.Should().Be(2);
    }

    [Fact]
    public void Can_process_jio()
    {
      var sut = new Cpu(0);

      sut.ProcessInstruction(new Instruction(InstructionType.jio, Register.B, -10));

      sut.Position.Should().Be(1);
    }

    [Fact]
    public void Can_process_jio_not_satisfied()
    {
      var sut = new Cpu(0);

      sut.ProcessInstruction(new Instruction(InstructionType.inc, Register.A));
      sut.ProcessInstruction(new Instruction(InstructionType.jio, Register.A, -10));

      sut.Position.Should().Be(-9);
    }

    [Fact]
    public void Can_process_sample_input()
    {
      var input = "inc a\r\njio a, +2\r\ntpl a\r\ninc a";
      var sut = new Cpu(0);
      
      sut.Process(input);

      sut.GetRegister(Register.A).Should().Be(2);
    }

    [Fact]
    public void Can_process_sample_input_part2()
    {
      var input = "inc a\r\njio a, +2\r\ntpl a\r\ninc a";
      var sut = new Cpu(1);

      sut.Process(input);

      sut.GetRegister(Register.A).Should().Be(7);
    }

  }
}