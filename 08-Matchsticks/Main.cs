using _08_Matchsticks;

var text = File.ReadAllText("input.txt");
var diff = LengthCalculator.GetDiff(text);
Console.WriteLine("Part 1: " + diff);

diff = LengthCalculator.GetEncodedDiff(text);
Console.WriteLine("Part 2: " + diff);
