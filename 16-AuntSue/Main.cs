using _16_AuntSue;

var input = File.ReadAllText("input.txt");
var aunt = CrimeScene.GetAuntId(input, false);
Console.WriteLine("Part 1: " + aunt);

aunt = CrimeScene.GetAuntId(input, true);
Console.WriteLine("Part 2: " + aunt);
