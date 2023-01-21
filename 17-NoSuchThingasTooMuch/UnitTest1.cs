using FluentAssertions;

namespace _17_NoSuchThingasTooMuch
{
  public class UnitTest1
  {
    [Fact]
    public void Can_parse_intput()
    {
      string input = "20\r\n15\r\n10\r\n5\r\n5";
      var containers = Container.Parse(input);
      containers.Should().HaveCount(5);
    }

    [Fact]
    public void Can_get_all_that_fit_25()
    {
      string input = "20\r\n15\r\n10\r\n5\r\n5";
      var containers = Container.Parse(input);

      var permutations = Container.GetAllThatFit(containers, 25);

      permutations.Should().HaveCount(4);
    }

    [Fact]
    public void Can_get_all_count()
    {
      string input = "20\r\n15\r\n10\r\n5\r\n5";
      var num = Container.GetCountThatFit(input, 25);

      num.Should().Be(4);
    }

    [Fact]
    public void Can_get_count_of_min()
    {
      string input = "20\r\n15\r\n10\r\n5\r\n5";
      var num = Container.GetCountOfMin(input, 25);

      num.Should().Be(3);
    }
  }
}