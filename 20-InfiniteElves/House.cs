namespace _20_InfiniteElves
{
  internal class House
  {
    internal static int Parse(string text)
    {
      return int.Parse(text);
    }

    internal static int GetFirstWithMoreThan(int num)
    {
      var houses = new int[num / 10];
      for (int n = 0; n < houses.Length; ++n)
      {
        for (int visit = n; visit < houses.Length; visit += (n + 1))
        {
          houses[visit] += (n + 1) * 10;
        }

        if (houses[n] >= num)
          return n + 1;
      }

      throw new ApplicationException();
    }

    internal static int GetFirstWithMoreThan(string text)
    {
      var num = Parse(text);
      return GetFirstWithMoreThan(num);
    }
  }
}