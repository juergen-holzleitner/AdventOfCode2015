namespace _09_SingleNight
{
  internal class Permutation
  {
    internal static IEnumerable<List<int>> GetAllOf(int n)
    {
      var initial = Enumerable.Range(0, n).ToList();

      return GetPermutations(initial, 0);
    }

    private static IEnumerable<List<int>> GetPermutations(List<int> currentList, int currentPos)
    {
      if (currentPos + 1 >= currentList.Count)
        yield return currentList;
      else
      {
        for (int n = currentPos; n < currentList.Count; ++n)
        {
          Swap(currentList, currentPos, n);
          foreach (var p in GetPermutations(currentList, currentPos + 1))
            yield return p;
          Swap(currentList, n, currentPos);
        }
      }
    }

    private static void Swap(List<int> currentList, int a, int b)
    {
      if (a == b)
        return;

      var tmp = currentList[a];
      currentList[a] = currentList[b];
      currentList[b] = tmp;
    }
  }
}