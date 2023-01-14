using _15_ScienceforHungryPeople;

var text = File.ReadAllText("input.txt");
var score = IngredientThing.GetBestScore(text);
Console.WriteLine("Part 1: " + score);
