using FluentAssertions;

namespace _02_NoMath
{
  public class ParserTest
  {
    [Fact]
    public void Can_parse_single_line()
    {
      var sut = new Parser();

      var result = sut.ParseLine("1x2x3");

      result.Should().Be(new Parser.Dimensions(1, 2, 3));
    }
  }
}
