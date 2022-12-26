using _03_PerfectlySphericalHousesinaVacuum;

var input = File.ReadAllText("input.txt");
var numHouses = Santa.GetNumVisitedHouses(input);
Console.WriteLine("Part 1: " + numHouses);

numHouses = Santa.GetNumVisitedHousesWithTowSantas(input);
Console.WriteLine("Part 2: " + numHouses);
