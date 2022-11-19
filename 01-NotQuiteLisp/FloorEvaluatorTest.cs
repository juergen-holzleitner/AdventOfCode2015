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

    [Fact]
    public void Initial_first_basement_position_is_zero()
    {
      var sut = new FloorEvaluator();

      var firstBasementPosition = sut.GetFirstBasementPosition();

      firstBasementPosition.Should().Be(0);
    }

    [Theory]
    [InlineData(")", 1)]
    [InlineData("()())", 5)]
    public void Can_get_first_basement_position(string sequence, int expectedFirstBasementPosition)
    {
      var sut = new FloorEvaluator();

      sut.ProcessInput(sequence);

      var firstBasementPosition = sut.GetFirstBasementPosition();
      firstBasementPosition.Should().Be(expectedFirstBasementPosition);
    }
  }
}