using FluentAssertions;

namespace _18_GIF
{
  public class UnitTest1
  {
    [Fact]
    public void Can_parse_input()
    {
      var text = ".#.#.#\r\n...##.\r\n#....#\r\n..#...\r\n#.#..#\r\n####..\r\n";
      var input = GIF.ParseInput(text);
      input.Positions.Should().HaveCount(15);
      input.Positions.Should().Contain(new Pos(1, 0));
      input.Positions.Should().Contain(new Pos(0, 5));
      input.Dimension.Should().Be(6);
    }

    [Fact]
    public void Can_get_surrounding_positions()
    {
      var pos = new Pos(0, 0);
      var neighbours = pos.GetNeighbours(6);
      neighbours.Should().HaveCount(3);
    }

    [Fact]
    public void Can_get_surrounding_positions_at_end()
    {
      var pos = new Pos(4, 5);
      var neighbours = pos.GetNeighbours(6);
      neighbours.Should().HaveCount(5);
    }

    [Fact]
    public void Can_count_neighbours()
    {
      var text = ".#.#.#\r\n...##.\r\n#....#\r\n..#...\r\n#.#..#\r\n####..\r\n";
      var sut = new GIF(text);

      var num = sut.GetNumNeighbours(new Pos(2, 0));

      num.Should().Be(3);
    }

    [Fact]
    public void Can_get_num_active()
    {
      var text = ".#.#.#\r\n...##.\r\n#....#\r\n..#...\r\n#.#..#\r\n####..\r\n";
      var sut = new GIF(text);

      var num = sut.GetNumActive();

      num.Should().Be(15);
    }

    [Fact]
    public void Can_get_next_generation()
    {
      var text = ".#.#.#\r\n...##.\r\n#....#\r\n..#...\r\n#.#..#\r\n####..\r\n";
      var sut = new GIF(text);
      sut.ProcessGeneration();

      var active = sut.GetNumActive();

      active.Should().Be(11);
    }

    [Fact]
    public void Can_get_num_after_4_steps()
    {
      var text = ".#.#.#\r\n...##.\r\n#....#\r\n..#...\r\n#.#..#\r\n####..\r\n";
      int num = GIF.GetActiveAfter(text, 4);
      num.Should().Be(4);
    }
  }
}