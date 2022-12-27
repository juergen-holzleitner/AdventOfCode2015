using FluentAssertions;

namespace _06_ProbablyaFireHazard
{
  public class LightTest
  {
    [Theory]
    [InlineData("turn on 0,0 through 999,999", Action.On, 0, 0, 999, 999)]
    [InlineData("turn off 10,3 through 20,4", Action.Off, 10, 3, 20, 4)]
    [InlineData("toggle 10,3 through 20,4", Action.Toggle, 10, 3, 20, 4)]
    public void Can_parse_input(string text, Action expectedAction, int expectedLeft, int expectedTop, int expectedRight, int expectedBottom)
    {
      var input = Light.ParseLine(text);

      input.Action.Should().Be(expectedAction);
      input.TopLeft.Should().Be(new Pos(expectedLeft, expectedTop));
      input.BottomRight.Should().Be(new Pos(expectedRight, expectedBottom));
    }

    [Fact]
    public void Throw_if_right_smaller_left()
    {
      var text = "turn off 30,3 through 20,4";
      var act = () => Light.ParseLine(text);
      act.Should().Throw<ApplicationException>();
    }

    [Fact]
    public void Throw_if_bottom_smaller_top()
    {
      var text = "turn off 10,5 through 20,4";
      var act = () => Light.ParseLine(text);
      act.Should().Throw<ApplicationException>();
    }

    [Fact]
    public void Throw_if_invalid_action()
    {
      var text = "turn of 10,3 through 20,4";
      var act = () => Light.ParseLine(text);
      act.Should().Throw<ApplicationException>();
    }

    [Fact]
    public void Can_parse_all_input()
    {
      var text = "turn on 0,0 through 999,999\r\ntoggle 0,0 through 999,0\r\nturn off 499,499 through 500,500\r\n";
      var instructions = Light.ParseInstructions(text);
      instructions.Should().HaveCount(3);
    }

    [Fact]
    public void Board_is_initially_all_off()
    {
      var board = new Board();
      var numLightsOn = board.GetNumLightsOn();
      numLightsOn.Should().Be(0);
    }

    [Fact]
    public void Board_can_turn_lights_on()
    {
      var board = new Board();

      board.Perform(new Instruction(Action.On, new Pos(0, 0), new Pos(0, 0)));

      var numLightsOn = board.GetNumLightsOn();
      numLightsOn.Should().Be(1);
    }

    [Fact]
    public void Can_process_input()
    {
      var text = "turn on 0,0 through 999,999\r\ntoggle 0,0 through 999,0\r\nturn off 499,499 through 500,500\r\n";
      var numLightsOn = Light.GetLightsOn(text);
      numLightsOn.Should().Be(998996);
    }
  }
}