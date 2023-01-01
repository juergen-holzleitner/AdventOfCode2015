using _09_SingleNight;

var text = File.ReadAllText("input.txt");
var distance = Permutation.GetShortestDistance(text);
Console.WriteLine("Part 1: " + distance);

distance = Permutation.GetLongestDistance(text);
Console.WriteLine("Part 2: " + distance);
