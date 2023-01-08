using FluentAssertions;

namespace _13_KnightsoftheDinnerTable
{
  public class DinnerTest
  {
    [Fact]
    public void Can_get_permutations()
    {
      int number = 5;
      int expectedElements = 5 * 4 * 3 * 2 * 1;

      var numElements = Dinner.GetAllPermutationsOf(number).Count();

      numElements.Should().Be(expectedElements);
    }
  }
}