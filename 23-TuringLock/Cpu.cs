namespace _23_TuringLock
{
  enum Register { A, B }

  enum InstructionType { hlf, tpl, inc, jmp, jie, jio }

  record Instruction(InstructionType Type, Register Register, int Offset = 0);

  internal class Cpu
  {
    private Dictionary<Register, uint> _registers = new()
    {
      { Register.A, 0 },
      { Register.B, 0 }
    };

    private int _pos = 0;

    public Cpu(uint initialA)
    {
      _registers[Register.A] = initialA;
    }

    internal static Instruction ParseInstruction(string code)
    {
      var elements = code.Split(' ');
      var instruction = GetInstructionName(elements[0]);
      var register = instruction == InstructionType.jmp ? Register.A : GetRegisterName(elements[1]);
      int offset = 0;
      if (instruction == InstructionType.jmp)
      {
        offset = GetOffset(elements[1]);
      }
      else if (instruction == InstructionType.jie || instruction == InstructionType.jio)
      {
        offset = GetOffset(elements[2]);
      }
      return new Instruction(instruction, register, offset);
    }

    private static int GetOffset(string offset)
    {
      return int.Parse(offset);
    }

    private static InstructionType GetInstructionName(string inst)
    {
      return inst switch
      {
        "hlf" => InstructionType.hlf,
        "tpl" => InstructionType.tpl,
        "inc" => InstructionType.inc,
        "jmp" => InstructionType.jmp,
        "jie" => InstructionType.jie,
        "jio" => InstructionType.jio,
        _ => throw new ApplicationException()
      };
    }

    private static Register GetRegisterName(string register)
    {
      return register[0] switch
      {
        'a' => Register.A,
        'b' => Register.B,
        _ => throw new ApplicationException()
      };
    }

    internal uint GetRegister(Register register)
    {
      return _registers[register];
    }

    internal static List<Instruction> ParseInput(string code)
    {
      var instructions = new List<Instruction>();
      foreach (var line in code.Split('\n'))
        if (!String.IsNullOrWhiteSpace(line))
        {
          instructions.Add(ParseInstruction(line));
        }
      return instructions;
    }

    internal void ProcessInstruction(Instruction instruction)
    {
      if (instruction.Type == InstructionType.inc)
      {
        ++_registers[instruction.Register];
        ++_pos;
      }
      else if (instruction.Type == InstructionType.tpl)
      {
        _registers[instruction.Register] *= 3;
        ++_pos;
      }
      else if (instruction.Type == InstructionType.hlf)
      {
        _registers[instruction.Register] /= 2;
        ++_pos;
      }
      else if (instruction.Type == InstructionType.jmp)
      {
        _pos += instruction.Offset;
      }
      else if (instruction.Type == InstructionType.jie)
      {
        if (GetRegister(instruction.Register) % 2 == 0)
        {
          _pos += instruction.Offset;
        }
        else
        {
          ++_pos;
        }
      }
      else if (instruction.Type == InstructionType.jio)
      {
        if (GetRegister(instruction.Register) == 1)
        {
          _pos += instruction.Offset;
        }
        else
        {
          ++_pos;
        }
      }
      else
      {
        throw new ApplicationException();
      }
    }

    internal void Process(string input)
    {
      var instructions = ParseInput(input);
      while (Position != instructions.Count)
        ProcessInstruction(instructions[Position]);
    }

    internal int Position => _pos;
  }
}