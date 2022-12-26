using _04_TheIdealStockingStuffer;

var input = "ckczppom";
var number = MD5Test.CalculateMD5ToStartWithFiveZeros(input);
Console.WriteLine("Part 1: " + number);

number = MD5Test.CalculateMD5ToStartWithSixZeros(input);
Console.WriteLine("Part 2: " + number);
