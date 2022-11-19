using _01_NotQuiteLisp;

var input = File.ReadAllText("input.txt");

var floorEvaluator = new FloorEvaluator();
floorEvaluator.ProcessInput(input);

Console.WriteLine($"Part1: final floor: {floorEvaluator.GetFloor()}");
Console.WriteLine($"Part2: first basement position: {floorEvaluator.GetFirstBasementPosition()}");
