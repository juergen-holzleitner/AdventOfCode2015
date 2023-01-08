using _14_ReindeerOlympics;

var text = File.ReadAllText("input.txt");
var maxDistance = ReindeerProcessor.GetMaxDistance(text, 2503);
Console.WriteLine("Part 1: " + maxDistance);
