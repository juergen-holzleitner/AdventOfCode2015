using _05_DoesntHeHaveInternElvesForThis;

var input = File.ReadAllText("input.txt");
var numWords = Tester.GetNumNiceWords(input);
Console.WriteLine("Part 1: " + numWords);

numWords = Tester.GetNumNiceWordsPart2(input);
Console.WriteLine("Part 2: " + numWords);
