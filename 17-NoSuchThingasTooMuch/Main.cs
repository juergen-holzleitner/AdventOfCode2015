using _17_NoSuchThingasTooMuch;

var input = File.ReadAllText("input.txt");
var num = Container.GetCountThatFit(input, 150);
Console.WriteLine("Part 1: " + num);
