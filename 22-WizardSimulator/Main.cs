using _22_WizardSimulator;

var inputText = File.ReadAllText("input.txt");
var initialPlayerHitPoints = 50;
var initialPlayerMana = 500;

var minManaUsed = RPG.GetMinManaUsedToWin(inputText, initialPlayerHitPoints, initialPlayerMana);

Console.WriteLine("Part 1: " + minManaUsed);
