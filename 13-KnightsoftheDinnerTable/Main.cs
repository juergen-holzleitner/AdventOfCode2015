using _13_KnightsoftheDinnerTable;

var text = File.ReadAllText("input.txt");
var maxHappiness = Dinner.GetMaxHappiness(text, false);
Console.WriteLine("Part 1: " + maxHappiness);

maxHappiness = Dinner.GetMaxHappiness(text, true);
Console.WriteLine("Part 2: " + maxHappiness);
