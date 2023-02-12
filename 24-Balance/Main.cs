using _24_Balance;

var input = File.ReadAllText("input.txt");
var qe = Balance.GetSmallestQE(input);
Console.WriteLine("Part 1: " + qe);
