using FluentAssertions;

namespace _14_ReindeerOlympics
{
  public class ReindeerTest
  {
    [Fact]
    public void Can_parse_reindeer()
    {
      var line = "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.";

      var reindeer = ReindeerProcessor.ParseLine(line);

      reindeer.Name.Should().Be("Comet");
      reindeer.Speed.Should().Be(14);
      reindeer.Time.Should().Be(10);
      reindeer.Rest.Should().Be(127);
    }

    [Fact]
    public void Can_parse_all_reindeers()
    {
      var text = "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.\r\nDancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.\r\n";
      var reindeers = ReindeerProcessor.ParseInput(text);
      reindeers.Should().HaveCount(2);
    }

    [Theory]
    [InlineData(1, 14)]
    [InlineData(10, 140)]
    [InlineData(11, 140)]
    [InlineData(137, 140)]
    [InlineData(138, 154)]
    [InlineData(1000, 1120)]
    public void Can_get_distance_after(int time, long expectedDistance)
    {
      var reindeer = new Reindeer("Comet", 14, 10, 127);

      var distance = reindeer.GetDistanceAfter(time);

      distance.Should().Be(expectedDistance);
    }

    [Fact]
    public void Can_solve_sample_part1()
    {
      var text = "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.\r\nDancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.\r\n";
      var maxDistance = ReindeerProcessor.GetMaxDistance(text, 1000);
      maxDistance.Should().Be(1120);
    }

    [Fact]
    public void Can_solve_sample_part2()
    {
      var text = "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.\r\nDancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.\r\n";
      var maxPoints = ReindeerProcessor.GetMaxPoints(text, 1000);
      maxPoints.Should().Be(689);
    }
  }
}