using _12_JSAbacusFramework;

var str = File.ReadAllText("input.txt");
var sum = JSAbacus.GetSumOfAllNumbers(str);
Console.WriteLine("Part 1: " + sum);
