using FluentAssertions;

namespace _05_DoesntHeHaveInternElvesForThis
{
  public class StringTest
  {
    [Theory]
    [InlineData("aei", true)]
    [InlineData("abc", false)]
    [InlineData("xazegov", true)]
    [InlineData("aeiouaeiouaeiou", true)]
    [InlineData("aa", false)]
    public void HasThreeVovels(string testString, bool expectedResult)
    {
      var hasThreeVovels = Tester.HasThreeVovels(testString);
      hasThreeVovels.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("ab", false)]
    [InlineData("aa", true)]
    [InlineData("a", false)]
    public void HasRepeatingCharacter(string testString, bool expectedResult)
    {
      var hasRepeatingCharacter = Tester.HasRepeatingCharacter(testString);
      hasRepeatingCharacter.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("ab", true)]
    [InlineData("cd", true)]
    [InlineData("pq", true)]
    [InlineData("xy", true)]
    [InlineData("aa", false)]
    public void Has_forbidden_word(string testString, bool expectedResult)
    {
      var hasForbiddenString = Tester.HasForbiddenString(testString);
      hasForbiddenString.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("ugknbfddgicrmopn", true)]
    [InlineData("aaa", true)]
    [InlineData("jchzalrnumimnmhp", false)]
    [InlineData("haegwjzuvuyypxyu", false)]
    [InlineData("dvszwmarrgswjxmb", false)]
    public void Can_check_nice_word(string testString, bool expectedResult)
    {
      var isNiceWord = Tester.IsNiceWord(testString);
      isNiceWord.Should().Be(expectedResult);
    }

    [Fact]
    public void Can_check_num_nice_word()
    {
      var input = "ugknbfddgicrmopn\r\naaa\r\njchzalrnumimnmhp\r\nhaegwjzuvuyypxyu\r\ndvszwmarrgswjxmb\r\n";
      var numWords = Tester.GetNumNiceWords(input);
      numWords.Should().Be(2);
    }
  }
}