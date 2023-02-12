using FluentAssertions;

namespace _24_Balance
{
  public class UnitTest1
  {
    [Fact]
    public void Can_read_input()
    {
      var input = "1\r\n2\r\n";
      
      var values = Balance.Parse(input);

      values.Should().BeEquivalentTo(new[] { 1, 2 });
    }

    [Fact]
    public void Can_get_target_weight()
    {
      var weight = Balance.GetTargetWeight(new long [] {1, 2, 3, 4, 5, 7, 8, 9, 10, 11}, 3);
      weight.Should().Be(20);
    }

    [Fact]
    public void Can_enumerate_groups_of_one()
    {
      var input = "1\r\n2\r\n";
      var groups = Balance.EnumerateGroupsOf(input, 1, 2);

      groups.Should().BeEquivalentTo(new[] 
      { 
        new List<int> { 2 } 
      });
    }

    [Fact]
    public void Can_enumerate_groups_of_two()
    {
      var input = "1\r\n2\r\n";
      var groups = Balance.EnumerateGroupsOf(input, 2, 3);

      groups.Should().BeEquivalentTo(new[]
      {
        new List<int> { 1, 2 },
      });
    }

    [Fact]
    public void Can_enumerate_shortes_group_of_weight()
    {
      var input = "1\r\n2\r\n3\r\n4\r\n5\r\n7\r\n8\r\n9\r\n10\r\n11\r\n";
      var groups = Balance.EnumerateShortestGroupOf(input, 3);
      
      groups.Should().BeEquivalentTo(new[]
      {
        new List<int> { 11, 9 },
      });
    }

    [Fact]
    public void Can_enumerate_shortes_group_of_weight_part2()
    {
      var input = "1\r\n2\r\n3\r\n4\r\n5\r\n7\r\n8\r\n9\r\n10\r\n11\r\n";
      var groups = Balance.EnumerateShortestGroupOf(input, 4);

      groups.Should().BeEquivalentTo(new[]
      {
        new List<int> { 11, 4 },
        new List<int> { 10, 5 },
        new List<int> { 8, 7 },
      });
    }

    [Fact]
    public void Can_get_smallest_QE()
    {
      var input = "1\r\n2\r\n3\r\n4\r\n5\r\n7\r\n8\r\n9\r\n10\r\n11\r\n";
      var qe = Balance.GetSmallestQE(input, 3);
      qe.Should().Be(99);
    }

    [Fact]
    public void Can_get_smallest_QE_part2()
    {
      var input = "1\r\n2\r\n3\r\n4\r\n5\r\n7\r\n8\r\n9\r\n10\r\n11\r\n";
      var qe = Balance.GetSmallestQE(input, 4);
      qe.Should().Be(44);
    }

  }
}