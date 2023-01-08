using FluentAssertions;

namespace _03_PerfectlySphericalHousesinaVacuum
{
  public class SantaTest
  {
    [Theory]
    [InlineData('<', Direction.Left)]
    [InlineData('>', Direction.Right)]
    [InlineData('^', Direction.Up)]
    [InlineData('v', Direction.Down)]
    public void Can_parse_directions(char input, Direction expectedDirection)
    {
      var direction = Santa.ParseDirection(input);
      direction.Should().Be(expectedDirection);
    }

    [Fact]
    public void Should_throw_on_invalid_direction()
    {
      var invalidDirection = '\r';

      var act = () => Santa.ParseDirection(invalidDirection);

      act.Should().Throw<ApplicationException>();
    }

    [Fact]
    public void Can_parse_multiple_inputs()
    {
      var text = "^>v<\r\n";
      var inputs = Santa.ParseInput(text);
      inputs.Should().HaveCount(4);
    }

    [Fact]
    public void Santa_has_initially_one_position()
    {
      var sut = new Santa(1);

      var numHouses = sut.GetNumberOfVisitedHouses();

      numHouses.Should().Be(1);
    }

    [Theory]
    [InlineData("", 1)]
    [InlineData(">", 2)]
    [InlineData("^>v<", 4)]
    [InlineData("^v^v^v^v^v", 2)]
    public void Can_count_visited_positions(string input, int expectedHouses)
    {
      var numHouses = Santa.GetNumVisitedHouses(input);
      numHouses.Should().Be(expectedHouses);
    }

    [Theory]
    [InlineData("^v", 3)]
    [InlineData("^>v<", 3)]
    [InlineData("^v^v^v^v^v ", 11)]
    public void Can_work_with_two_santas(string input, int expectedHouses)
    {
      var numHouses = Santa.GetNumVisitedHousesWithTowSantas(input);
      numHouses.Should().Be(expectedHouses);
    }
  }
}