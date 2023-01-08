using FluentAssertions;

namespace _10_ElvesLookElvesSay
{
  public class UnitTest1
  {
    [Theory]
    [InlineData("1", "11")]
    [InlineData("11", "21")]
    [InlineData("21", "1211")]
    [InlineData("1211", "111221")]
    [InlineData("111221", "312211")]
    public void Can_convert_number_string(string number, string expected)
    {
      var result = Converter.Get(number);
      result.Should().Be(expected);
    }

    [Fact]
    public void Can_get_value_after_n()
    {
      var input = "1";
      const int n = 5;

      var result = Converter.GetAfter(input, n);

      result.Should().Be("312211");
    }

    [Fact]
    public void Can_get_length_after_n()
    {
      var input = "1";
      const int n = 5;

      var result = Converter.GetLengthAfter(input, n);

      result.Should().Be(6);
    }
  }
}