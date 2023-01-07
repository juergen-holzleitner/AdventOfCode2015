using FluentAssertions;

namespace _12_JSAbacusFramework
{
  public class JSAbacusTest
  {
    [Fact]
    public void Can_get_numbers_from_string()
    {
      var str = "[1,2,3]";
      var numbers = JSAbacus.GetAllNumbers(str, false);
      numbers.Should().BeEquivalentTo(new[] { 1, 2,3 });
    }

    [Theory]
    [InlineData("[1,2,3]", 6)]
    [InlineData("{\"a\":2,\"b\":4}", 6)]
    [InlineData("[[[3]]]", 3)]
    [InlineData("{\"a\":{\"b\":4},\"c\":-1}", 3)]
    [InlineData("{\"a\":[-1,1]}", 0)]
    [InlineData("[-1,{\"a\":1}]", 0)]
    [InlineData("[]", 0)]
    [InlineData("{}", 0)]
    public void Can_get_sum_of_numbers(string str, int expectedSum)
    {
      var sum = JSAbacus.GetSumOfAllNumbers(str);
      sum.Should().Be(expectedSum);
    }

    [Theory]
    [InlineData("[1,2,3]", 6)]
    [InlineData("[1,{\"c\":\"red\",\"b\":2},3]", 4)]
    [InlineData("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}", 0)]
    [InlineData("[1,\"red\",5]", 6)]
    public void Can_get_sum_of_numbers_without_red(string str, int expectedSum)
    {
      var sum = JSAbacus.GetSumOfAllNumbersWithoutRed(str);
      sum.Should().Be(expectedSum);
    }
  }
}