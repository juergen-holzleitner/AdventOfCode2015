using _07_SomeAssemblyRequired;

var text = File.ReadAllText("input.txt");
var wireValue = Wire.GetFinalWireValue(text, "a");
Console.WriteLine("Part 1: " + wireValue);
