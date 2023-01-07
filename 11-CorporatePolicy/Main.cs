using _11_CorporatePolicy;

var initial = File.ReadAllText("input.txt");
var next = Password.GetNextValidPassword(initial);
Console.WriteLine("Part 1: " + next);

next = Password.GetNextValidPassword(next);
Console.WriteLine("Part 2: " + next);
