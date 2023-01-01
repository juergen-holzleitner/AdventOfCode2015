using FluentAssertions;

namespace _08_Matchsticks
{
  public class LengthTest
  {
    [Theory]
    [InlineData("\"\"", 2, 0)]
    [InlineData("\"abc\"", 5, 3)]
    [InlineData("\"aaa\\\"aaa\"", 10, 7)]
    [InlineData("\"\\\"\\\"\"", 6, 2)]
    [InlineData("\"\\\\\\\\\"", 6, 2)]
    [InlineData("\"\\x27\"", 6, 1)]
    [InlineData("\"\\x27\\x2A\\xbf\"", 14, 3)]
    public void Can_get_length_of_string(string str, int expectedCodeLength, int expectedNumber)
    {
      var length = LengthCalculator.GetLength(str);
      length.Code.Should().Be(expectedCodeLength);
      length.Number.Should().Be(expectedNumber);
    }

    [Fact]
    public void Assert_if_not_starting_with_quotes()
    {
      var str = "abc\"";

      var act = () => LengthCalculator.GetLength(str);
      
      act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Assert_if_not_ending_with_quotes()
    {
      var str = "\"abc";

      var act = () => LengthCalculator.GetLength(str);

      act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Assert_if_not_at_least_two_chars()
    {
      var str = "\"";

      var act = () => LengthCalculator.GetLength(str);

      act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void String_is_trimmed()
    {
      var str = "\"\" \r";
      var length = LengthCalculator.GetLength(str);
      length.Code.Should().Be(2);
      length.Number.Should().Be(0);
    }

    [Fact]
    public void Can_calculate_result()
    {
      var text = "\"\"\r\n\"abc\"\r\n\"aaa\\\"aaa\"\r\n\"\\x27\"\r\n\r\n";
      var diff = LengthCalculator.GetDiff(text);
      diff.Should().Be(12);
    }

    [Theory]
    [InlineData("", 0, 2)]
    [InlineData("\\", 1, 4)]
    [InlineData("\\\"", 2, 6)]
    public void Can_get_length_of_encoded_string(string str, int expectedCodeLength, int expectedNumber)
    {
      var length = LengthCalculator.GetEncodedLength(str);
      length.Code.Should().Be(expectedCodeLength);
      length.Number.Should().Be(expectedNumber);
    }

    [Fact]
    public void Can_calculate_result_part2()
    {
      var text = "\"\"\r\n\"abc\"\r\n\"aaa\\\"aaa\"\r\n\"\\x27\"\r\n\r\n";
      var diff = LengthCalculator.GetEncodedDiff(text);
      diff.Should().Be(19);
    }

  }
}