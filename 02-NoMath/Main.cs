using _02_NoMath;

var parser = new Parser();
var boxCalculator = new BoxCalculator();

int total = 0;
int totalRibbon = 0;
var lines = File.ReadLines("input.txt");
foreach (var line in lines)
{
  var dimensions = parser.ParseLine(line);
  var required = boxCalculator.CalculateRequired(dimensions.L, dimensions.W, dimensions.H);
  total += required;

  totalRibbon += boxCalculator.CalculateRibbon(dimensions.L, dimensions.W, dimensions.H);
}

Console.WriteLine($"Part1: required wrapping paper: {total}");
Console.WriteLine($"Part2: required ribbon: {totalRibbon}");
