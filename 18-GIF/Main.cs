using _18_GIF;

var text = File.ReadAllText("input.txt");
var num = GIF.GetActiveAfter(text, 100);
Console.WriteLine("Part 1: " + num);
