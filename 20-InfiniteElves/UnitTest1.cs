using FluentAssertions;

namespace _20_InfiniteElves
{
  public class UnitTest1
  {
    [Fact]
    public void Can_parse_input()
    {
      var text = "1234";
      var input = House.Parse(text);
      input.Should().Be(1234);
    }

    [Theory]
    [InlineData(70, 4)]
    [InlineData(119, 6)]
    [InlineData(150, 8)]
    public void Can_get_first_house(int num, int expectedHouse)
    {
      var house = House.GetFirstWithMoreThan(num);
      house.Should().Be(expectedHouse);
    }

    [Fact]
    public void Can_get_first_house_from_input()
    {
      var text = "149";
      var house = House.GetFirstWithMoreThan(text);
      house.Should().Be(8);
    }
  }
}