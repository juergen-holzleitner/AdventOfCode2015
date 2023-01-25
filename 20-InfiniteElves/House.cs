namespace _20_InfiniteElves
{
  internal class House
  {
    internal static int Parse(string text)
    {
      return int.Parse(text);
    }

    internal static int GetFirstWithMoreThan(int num, bool part2)
    {
      int factor = part2 ? 11 : 10;

      int maxNumHouses = (num + factor - 1) / factor;
      var houses = new int[maxNumHouses];
      for (int n = 0; n < houses.Length; ++n)
      {
        int elveNumber = n + 1;
        var numVisits = houses.Length;
        if (part2 && numVisits > 50 * elveNumber)
          numVisits = 50 * elveNumber;
        
        for (int visit = n; visit < numVisits; visit += elveNumber)
        {
          houses[visit] += elveNumber * factor;
        }

        if (houses[n] >= num)
          return n + 1;
      }

      throw new ApplicationException();
    }

    internal static int GetFirstWithMoreThan(string text, bool isPart2)
    {
      var num = Parse(text);
      return GetFirstWithMoreThan(num, isPart2);
    }
  }
}