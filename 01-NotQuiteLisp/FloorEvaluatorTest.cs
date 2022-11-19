using FluentAssertions;

namespace _01_NotQuiteLisp
{
  public class FloorEvaluatorTest
  {
    [Fact]
    public void Start_at_floor_zero()
    {
      var sut = new FloorEvaluator();

      var startingFloor = sut.GetFloor();

      startingFloor.Should().Be(0);
    }
  }
}