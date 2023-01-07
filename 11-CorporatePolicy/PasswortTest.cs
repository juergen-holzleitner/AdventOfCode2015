using FluentAssertions;

namespace _11_CorporatePolicy
{
  public class PasswortTest
  {
    [Fact]
    public void Throw_if_password_is_not_8_char()
    {
      var pwd = "abc";
      var act = () => Password.IsValid(pwd);
      act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Throw_if_password_is_not_lowercase_only()
    {
      var pwd = "abcd1234";
      var act = () => Password.IsValid(pwd);
      act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("abcdffaa")]
    [InlineData("ghjaabcc")]
    public void Can_verify_valid_password(string pwd)
    {
      bool isValid = Password.IsValid(pwd);
      isValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("hijklmmn")]
    [InlineData("abbceffg")]
    [InlineData("abbcegjk")]
    public void Can_verify_invalid_password(string pwd)
    {
      bool isValid = Password.IsValid(pwd);
      isValid.Should().BeFalse();
    }

    [Fact]
    public void Can_check_increasing_straight()
    {
      var pwd = "abbceffg";
      bool isValid = Password.IsValid(pwd);
      isValid.Should().BeFalse();
    }

    [Fact]
    public void Can_check_forbidden_chars()
    {
      var pwd = "ghiaabcc";
      bool isValid = Password.IsValid(pwd);
      isValid.Should().BeFalse();
    }

    [Fact]
    public void Can_check_pairs_of_letters()
    {
      var pwd = "ghjaabcd";
      bool isValid = Password.IsValid(pwd);
      isValid.Should().BeFalse();
    }

    [Fact]
    public void Can_get_next_password()
    {
      var next = Password.GetNextPasswords("aa").First().ToString();
      next.Should().Be("ab");
    }

    [Theory]
    [InlineData("abcdefgh", "abcdffaa")]
    [InlineData("ghijklmn", "ghjaabcc")]
    public void Can_get_next_valid_password(string initial, string expected)
    {
      var next = Password.GetNextValidPassword(initial);
      next.Should().Be(expected);
    }
  }
}