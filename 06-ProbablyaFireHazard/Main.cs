using _06_ProbablyaFireHazard;

var text = File.ReadAllText("input.txt");
var numLightsOn = Light.GetLightsOn(text);
Console.WriteLine("Part 1: " + numLightsOn);
