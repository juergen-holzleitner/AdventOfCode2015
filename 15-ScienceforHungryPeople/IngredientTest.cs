using FluentAssertions;

namespace _15_ScienceforHungryPeople
{
  public class IngredientTest
  {
    [Fact]
    public void Can_parse_ingredient()
    {
      var line = "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8";
      var ingredient = IngredientThing.ParseLine(line);
      ingredient.Should().Be(new Ingredient("Butterscotch", -1, -2, 6, 3, 8));
    }

    [Fact]
    public void Can_parse_all_ingredients()
    {
      var text = "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8\r\nCinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3\r\n";
      var ingredients = IngredientThing.ParseInput(text);
      ingredients.Should().HaveCount(2);
    }

    [Fact]
    public void Can_calculate_score()
    {
      var text = "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8\r\nCinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3\r\n";
      var ingredients = IngredientThing.ParseInput(text);
      var spoons = new int[2] { 44, 56 };
      var score = IngredientThing.CalculateScore(ingredients, spoons);
      score.Should().Be(62842880);
    }

    [Fact]
    public void Can_get_all_permutations()
    {
      var permutations = IngredientThing.GetAllPermutations(1, 2);
      permutations.Should().HaveCount(2);
    }

    [Fact]
    public void Can_get_all_permutations_of_sample()
    {
      var permutations = IngredientThing.GetAllPermutations(100, 2);
      permutations.Should().HaveCount(101);
      foreach (var p in permutations)
      {
        p.Sum().Should().Be(100);
      }
    }

    [Fact]
    public void Can_get_all_permutations_of_input()
    {
      var permutations = IngredientThing.GetAllPermutations(100, 4);
      permutations.Should().HaveCount(176851);
      foreach (var p in permutations)
      {
        p.Length.Should().Be(4);
        p.Sum().Should().Be(100);
      }
    }

    [Fact]
    public void Can_get_best_score()
    {
      var text = "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8\r\nCinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3\r\n";
      var score = IngredientThing.GetBestScore(text);
      score.Should().Be(62842880);
    }
  }
}