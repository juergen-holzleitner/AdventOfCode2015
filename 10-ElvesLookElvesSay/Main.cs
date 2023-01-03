using _10_ElvesLookElvesSay;

var input = File.ReadAllText("input.txt");
const int n1 = 40;
var result = Converter.GetLengthAfter(input, n1);
Console.WriteLine("Part 1: " + result);

const int n2 = 50;
result = Converter.GetLengthAfter(input, n2);
Console.WriteLine("Part 2: " + result);
