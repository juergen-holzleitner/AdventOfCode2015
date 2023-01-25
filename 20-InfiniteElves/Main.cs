using _20_InfiniteElves;

var text = File.ReadAllText("input.txt");
var house = House.GetFirstWithMoreThan(text, false);
Console.WriteLine("Part 1: " + house);

house = House.GetFirstWithMoreThan(text, true);
Console.WriteLine("Part 2: " + house);
