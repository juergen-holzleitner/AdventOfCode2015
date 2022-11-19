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

    [Theory]
    [InlineData("(", 1)]
    [InlineData(")", -1)]
    [InlineData("(())", 0)]
    [InlineData("()()", 0)]
    [InlineData("(((", 3)]
    [InlineData("(()(()(", 3)]
    [InlineData("))(((((", 3)]
    [InlineData("())", -1)]
    [InlineData("))(", -1)]
    [InlineData(")))", -3)]
    [InlineData(")())())", -3)]
    public void Can_get_correct_floor_from_sequence(string sequence, int expectedFinalFloor)
    {
      var sut = new FloorEvaluator();
      
      sut.ProcessInput(sequence);

      sut.GetFloor().Should().Be(expectedFinalFloor);
    }
  }
}