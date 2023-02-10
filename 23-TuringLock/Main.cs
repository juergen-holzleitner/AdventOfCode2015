using _23_TuringLock;

var input = File.ReadAllText("input.txt");

var sut = new Cpu(0);
sut.Process(input);
var result1 = sut.GetRegister(Register.B);

sut = new Cpu(1);
sut.Process(input);
var result2 = sut.GetRegister(Register.B);

Console.WriteLine("Part 1: " + result1);
Console.WriteLine("Part 2: " + result2);
