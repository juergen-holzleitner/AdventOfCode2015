using System.Text.RegularExpressions;

namespace _15_ScienceforHungryPeople
{
  internal record Ingredient(string Name, int Capacity, int Durability, int Flavor, int Texture, int Calories);

  internal partial class IngredientThing
  {
    internal static long CalculateScore(List<Ingredient> ingredients, int[] spoons)
    {
      if (ingredients.Count != spoons.Length)
        throw new ArgumentException("length differ", nameof(ingredients));

      return GetScoreCapacity(ingredients, spoons)
        * GetScoreDurability(ingredients, spoons)
        * GetScoreFlavor(ingredients, spoons)
        * GetScoreTexture(ingredients, spoons);
    }

    private static long GetScoreCapacity(List<Ingredient> ingredients, int[] spoons)
    {
      int score = 0;
      for (int n = 0; n < spoons.Length; ++n)
        score += ingredients[n].Capacity * spoons[n];

      if (score > 0)
        return score;
      return 0;
    }

    private static long GetScoreDurability(List<Ingredient> ingredients, int[] spoons)
    {
      int score = 0;
      for (int n = 0; n < spoons.Length; ++n)
        score += ingredients[n].Durability * spoons[n];

      if (score > 0)
        return score;
      return 0;
    }

    private static long GetScoreFlavor(List<Ingredient> ingredients, int[] spoons)
    {
      int score = 0;
      for (int n = 0; n < spoons.Length; ++n)
        score += ingredients[n].Flavor * spoons[n];

      if (score > 0)
        return score;
      return 0;
    }

    private static long GetScoreTexture(List<Ingredient> ingredients, int[] spoons)
    {
      int score = 0;
      for (int n = 0; n < spoons.Length; ++n)
        score += ingredients[n].Texture * spoons[n];

      if (score > 0)
        return score;
      return 0;
    }

    private static long GetScoreCalories(List<Ingredient> ingredients, int[] spoons)
    {
      int score = 0;
      for (int n = 0; n < spoons.Length; ++n)
        score += ingredients[n].Calories * spoons[n];

      if (score > 0)
        return score;
      return 0;
    }

    internal static List<Ingredient> ParseInput(string input)
    {
      var ingredients = new List<Ingredient>();
      foreach (var line in input.Split('\n'))
        if (!string.IsNullOrWhiteSpace(line))
        {
          ingredients.Add(ParseLine(line));
        }
      return ingredients;
    }

    internal static Ingredient ParseLine(string line)
    {
      var regex = RegExIngredient();
      var match = regex.Match(line);
      if (match.Success)
      {
        var name = match.Groups["name"].Value;
        var capacity = int.Parse(match.Groups["capacity"].Value);
        var durability = int.Parse(match.Groups["durability"].Value);
        var flavor = int.Parse(match.Groups["flavor"].Value);
        var texture = int.Parse(match.Groups["texture"].Value);
        var calories = int.Parse(match.Groups["calories"].Value);
        return new(name, capacity, durability, flavor, texture, calories);
      }
      throw new ApplicationException();
    }

    [GeneratedRegex("(?<name>\\w+): capacity (?<capacity>-?\\d+), durability (?<durability>-?\\d+), flavor (?<flavor>-?\\d+), texture (?<texture>-?\\d+), calories (?<calories>-?\\d+)")]
    private static partial Regex RegExIngredient();

    internal static IEnumerable<int[]> GetAllPermutations(int elements, int groups)
    {
      var current = new int[groups];
      return GetAllPermutations(elements, groups, current, 0).Select(p => (int[])p.Clone());
    }

    private static IEnumerable<int[]> GetAllPermutations(int elementsLeft, int groups, int[] current, int currentGroup)
    {
      if (currentGroup >= current.Length - 1)
      {
        current[currentGroup] = elementsLeft;
        yield return current;
      }
      else
      {
        for (int n = 0; n <= elementsLeft; ++n)
        {
          current[currentGroup] = n;
          foreach (var e in GetAllPermutations(elementsLeft - n, groups, current, currentGroup + 1))
          {
            yield return e;
          }
        }
      }
    }

    internal static long GetBestScore(string text)
    {
      var ingredients = ParseInput(text);
      return GetAllPermutations(100, ingredients.Count).Select(p => CalculateScore(ingredients, p)).Max();
    }

    internal static long GetBestScorePart2(string text)
    {
      var ingredients = ParseInput(text);
      return GetAllPermutations(100, ingredients.Count).Where(p => GetScoreCalories(ingredients, p) == 500).Select(p => CalculateScore(ingredients, p)).Max();
    }
  }
}