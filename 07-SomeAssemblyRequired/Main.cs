using _07_SomeAssemblyRequired;

var text = File.ReadAllText("input.txt");
var wireValue = Wire.GetFinalWireValue(text, "a");
Console.WriteLine("Part 1: " + wireValue);

wireValue = Wire.GetFinalWireValueWithRestart(text, "a", "b");
Console.WriteLine("Part 2: " + wireValue);
