using _21_RPG;

var text = File.ReadAllText("input.txt");
var costs = RPG.GetMinCostsToWin(text);
Console.WriteLine("Part 1: " + costs);

costs = RPG.GetMaxCostsToLose(text);
Console.WriteLine("Part 2: " + costs);
