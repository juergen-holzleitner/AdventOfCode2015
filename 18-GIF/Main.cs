using _18_GIF;

var text = File.ReadAllText("input.txt");
var num = GIF.GetActiveAfter(text, 100, false);
Console.WriteLine("Part 1: " + num);

num = GIF.GetActiveAfter(text, 100, true);
Console.WriteLine("Part 2: " + num);
