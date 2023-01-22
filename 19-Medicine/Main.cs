using _19_Medicine;

var text = File.ReadAllText("input.txt");
var num = Medicine.GetNumDistinctReplacements(text);
Console.WriteLine("Part 1: " + num);

num = Medicine.GetNumStepsUntilFound(text);
Console.WriteLine("Part 2: " + num);
