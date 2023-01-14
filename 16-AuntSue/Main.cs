using _16_AuntSue;

var input = File.ReadAllText("input.txt");
var aunt = CrimeScene.GetAuntId(input);
Console.WriteLine("Part 1: " + aunt);
