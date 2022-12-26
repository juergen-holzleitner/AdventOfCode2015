using FluentAssertions;

namespace _04_TheIdealStockingStuffer
{
  public class MD5Test
  {
    [Fact]
    public void TestMD5()
    {
      var input = "abcdef609043";
      var md5 = CalculateMD5(input);
      md5.Should().StartWith("00000");
    }

    [Fact]
    public void Can_get_number_that_matches_part1()
    {
      var input = "abcdef";
      var number = CalculateMD5ToStartWithFiveZeros(input);
      number.Should().Be(609043);
    }

    [Fact]
    public void Can_get_number_that_matches_part1_second_sample()
    {
      var input = "pqrstuv";
      var number = CalculateMD5ToStartWithFiveZeros(input);
      number.Should().Be(1048970);
    }

    [Fact]
    public void Can_check_hash_valid()
    {
      var hash = new byte[] { 0x00, 0x00, 0x03 };
      var isValid = IsHashValid(hash);
      isValid.Should().BeTrue();
    }

    [Fact]
    public void Can_check_hash_invalid()
    {
      var hash = new byte[] { 0x00, 0xc3, 0x03 };
      var isValid = IsHashValid(hash);
      isValid.Should().BeFalse();
    }

    public static int CalculateMD5ToStartWithFiveZeros(string input)
    {

      for (int number = 1; ; ++number)
      {
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input + number.ToString());
        byte[] hashBytes = System.Security.Cryptography.MD5.HashData(inputBytes);

        var isValid = IsHashValid(hashBytes);
        if (isValid)
        {
          return number;
        }
      }
    }

    private static bool IsHashValid(byte[] hashBytes)
    {
      for (int n = 0; n < 2; ++n)
        if (hashBytes[n] != 0)
          return false;
      return (hashBytes[2] >> 4) == 0;
    }

    private static string CalculateMD5(string input)
    {
      byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
      byte[] hashBytes = System.Security.Cryptography.MD5.HashData(inputBytes);

      return Convert.ToHexString(hashBytes);
    }
  }
}