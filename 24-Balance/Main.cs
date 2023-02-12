using _24_Balance;

var input = File.ReadAllText("input.txt");

var qe = Balance.GetSmallestQE(input, 3);
Console.WriteLine("Part 1: " + qe);

qe = Balance.GetSmallestQE(input, 4);
Console.WriteLine("Part 2: " + qe);
