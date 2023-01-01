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

    private int GetFactorial(int number)
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
  }
}