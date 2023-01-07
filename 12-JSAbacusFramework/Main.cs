using _12_JSAbacusFramework;

var str = File.ReadAllText("input.txt");
var sum = JSAbacus.GetSumOfAllNumbers(str);
Console.WriteLine("Part 1: " + sum);

sum = JSAbacus.GetSumOfAllNumbersWithoutRed(str);
Console.WriteLine("Part 2: " + sum);
