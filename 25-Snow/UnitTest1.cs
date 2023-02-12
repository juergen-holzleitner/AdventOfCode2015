using FluentAssertions;

namespace _25_Snow
{
  public class UnitTest1
  {
    [Fact]
    public void Can_parse_row_and_column()
    {
      var input = "To continue, please consult the code grid in the manual.  Enter the code at row 2947, column 3029.\r\n";
      var pos = Snow.Parse(input);
      pos.Should().Be(new Pos(2947, 3029));
    }

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(2, 1, 2)]
    [InlineData(6, 1, 16)]
    [InlineData(1, 2, 3)]
    [InlineData(4, 3, 18)]
    public void Can_get_number_at_position(int row, int col, int expected)
    {
      var pos = new Pos(row, col);
      var num = Snow.GetNumberAtPos(pos);
      num.Should().Be(expected);
    }

    [Theory]
    [InlineData(1, 20151125)]
    [InlineData(2, 31916031)]
    public void Can_evaluate_mod_exp(int pos, uint expected)
    {
      var val = Snow.GetNumberAt(pos);
      val.Should().Be(expected);
    }

    [Fact]
    public void Can_get_result()
    {
      var input = "To continue, please consult the code grid in the manual.  Enter the code at row 6, column 5.\r\n";
      var num = Snow.GetNumber(input);
      num.Should().Be(1534922);
    }
  }
}