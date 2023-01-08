using _13_KnightsoftheDinnerTable;

var text = File.ReadAllText("input.txt");
var maxHappiness = Dinner.GetMaxHappiness(text);
Console.WriteLine("Part 1: " + maxHappiness);
