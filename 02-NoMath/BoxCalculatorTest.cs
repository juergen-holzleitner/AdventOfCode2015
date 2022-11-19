using FluentAssertions;

namespace _02_NoMath
{
  public class BoxCalculatorTest
  {
    [Theory]
    [InlineData(2, 3, 4, 58)]
    [InlineData(1, 1, 10, 43)]
    public void Can_calculate_box(int l, int w, int h, int expectedResult)
    {
      var sut = new BoxCalculator();

      var required = sut.CalculateRequired(l, w, h);

      required.Should().Be(expectedResult);
    }
  }
}