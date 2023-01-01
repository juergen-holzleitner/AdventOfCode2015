using FluentAssertions;

namespace _09_SingleNight
{
  public class PermutatinosTest
  {
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    public void Can_get_all_permutations(int number)
    {
      int expectedElements = GetFactorial(number);

      var permutations = Permutation.GetAllOf(number).Select(p => new List<int>(p)).ToList();

      permutations.Should().HaveCount(expectedElements);
      foreach (var p in permutations)
        p.Count.Should().Be(number);
      for (int n = 0; n < permutations.Count; ++n)
        for (int m = n + 1; m < permutations.Count; ++m)
        {
          permutations[m].Should().NotEqual(permutations[n]);
        }
    }

    private static int GetFactorial(int number)
    {
      int factorial = 1;
      for (int n = 2; n <= number; ++n)
        factorial *= n;
      return factorial;
    }

    [Fact]
    public void Can_get_in_time()
    {
      int number = 10;
      int expectedElements = GetFactorial(number);

      var numElements = Permutation.GetAllOf(number).Count();

      numElements.Should().Be(expectedElements);
    }

    [Fact]
    public void Can_parse_line()
    {
      var text = "London to Dublin = 464\r\n";
      var route = Permutation.ParseLine(text);

      route.Route.Start.Should().Be("London");
      route.Route.End.Should().Be("Dublin");
      route.Distance.Should().Be(464);
    }

    [Fact]
    public void Can_parse_intput()
    {
      var text = "London to Dublin = 464\r\nLondon to Belfast = 518\r\nDublin to Belfast = 141";

      var input = Permutation.ParseInput(text);

      input.Cities.Should().BeEquivalentTo(new[] {"London", "Dublin", "Belfast"});
      input.Distances.Should().HaveCount(6);
    }

    [Fact]
    public void Can_get_distance()
    {
      var text = "London to Dublin = 464\r\nLondon to Belfast = 518\r\nDublin to Belfast = 141";
      var input = Permutation.ParseInput(text);

      var distance = Permutation.GetRouteDistance(input, new List<int> { 0, 1, 2});

      distance.Should().Be(605);
    }

    [Fact]
    public void Can_get_shortest_distance()
    {
      var text = "London to Dublin = 464\r\nLondon to Belfast = 518\r\nDublin to Belfast = 141";
      var distance = Permutation.GetShortestDistance(text);
      
      distance.Should().Be(605);
    }

    [Fact]
    public void Can_get_longest_distance()
    {
      var text = "London to Dublin = 464\r\nLondon to Belfast = 518\r\nDublin to Belfast = 141";
      var distance = Permutation.GetLongestDistance(text);

      distance.Should().Be(982);
    }
  }
}