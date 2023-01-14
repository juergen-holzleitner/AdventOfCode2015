using FluentAssertions;

namespace _16_AuntSue
{
  public class UnitTest1
  {
    [Fact]
    public void Can_get_ticket_tape()
    {
      var ticketTape = CrimeScene.GetTicketTape();
      ticketTape.Things.Should().HaveCount(10);
    }

    [Fact]
    public void Can_parse_aunt()
    {
      var line = "Sue 1: cars: 9, akitas: 3, goldfish: 0";
      var aunt = CrimeScene.ParseAunt(line);
      aunt.Id.Should().Be(1);
      aunt.Things.Things.Should().HaveCount(3);
    }

    [Fact]
    public void Can_get_aunt()
    {
      var input = "Sue 1: cars: 9, akitas: 3, goldfish: 0\r\nSue 2: akitas: 0, children: 3, samoyeds: 2\r\n";
      var aunt = CrimeScene.GetAuntId(input);
      aunt.Should().Be(2);
    }
  }
}