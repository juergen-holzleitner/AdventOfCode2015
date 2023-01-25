using _20_InfiniteElves;

var text = File.ReadAllText("input.txt");
var house = House.GetFirstWithMoreThan(text);
Console.WriteLine("Part 1: " + house);
