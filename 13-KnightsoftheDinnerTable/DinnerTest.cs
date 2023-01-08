using FluentAssertions;
using static System.Net.Mime.MediaTypeNames;

namespace _13_KnightsoftheDinnerTable
{
  public class DinnerTest
  {
    [Fact]
    public void Can_get_permutations()
    {
      int number = 5;
      int expectedElements = 5 * 4 * 3 * 2 * 1;

      var numElements = Dinner.GetAllPermutationsOf(number).Count();

      numElements.Should().Be(expectedElements);
    }

    [Theory]
    [InlineData("Alice would gain 54 happiness units by sitting next to Bob.", "Alice", "Bob", 54)]
    [InlineData("Alice would lose 79 happiness units by sitting next to Carol.", "Alice", "Carol", -79)]
    public void Can_parse_input(string line, string expectedPerson, string expectedNeighbor, int expectedHappiness)
    {
      var input = Dinner.ParseLine(line);
      
      input.Position.Person.Should().Be(expectedPerson);
      input.Position.Neighbor.Should().Be(expectedNeighbor);
      input.Happiness.Should().Be(expectedHappiness);
    }

    [Fact]
    public void Can_parse_all_sample_intput()
    {
      var text = "Alice would gain 54 happiness units by sitting next to Bob.\r\nAlice would lose 79 happiness units by sitting next to Carol.\r\nAlice would lose 2 happiness units by sitting next to David.\r\nBob would gain 83 happiness units by sitting next to Alice.\r\nBob would lose 7 happiness units by sitting next to Carol.\r\nBob would lose 63 happiness units by sitting next to David.\r\nCarol would lose 62 happiness units by sitting next to Alice.\r\nCarol would gain 60 happiness units by sitting next to Bob.\r\nCarol would gain 55 happiness units by sitting next to David.\r\nDavid would gain 46 happiness units by sitting next to Alice.\r\nDavid would lose 7 happiness units by sitting next to Bob.\r\nDavid would gain 41 happiness units by sitting next to Carol.\r\n";
      var input = Dinner.ParseInput(text);
      input.Table.Should().HaveCount(12);
      input.Table[new Position("Alice", "David")].Should().Be(-2);
    }

    [Fact]
    public void Can_get_persons()
    {
      var text = "Alice would gain 54 happiness units by sitting next to Bob.\r\nAlice would lose 79 happiness units by sitting next to Carol.\r\nAlice would lose 2 happiness units by sitting next to David.\r\nBob would gain 83 happiness units by sitting next to Alice.\r\nBob would lose 7 happiness units by sitting next to Carol.\r\nBob would lose 63 happiness units by sitting next to David.\r\nCarol would lose 62 happiness units by sitting next to Alice.\r\nCarol would gain 60 happiness units by sitting next to Bob.\r\nCarol would gain 55 happiness units by sitting next to David.\r\nDavid would gain 46 happiness units by sitting next to Alice.\r\nDavid would lose 7 happiness units by sitting next to Bob.\r\nDavid would gain 41 happiness units by sitting next to Carol.\r\n";
      var input = Dinner.ParseInput(text);
      var persons = Dinner.GetPersons(input);
      persons.Should().HaveCount(4);
    }

    [Fact]
    public void Can_get_person_at_position()
    {
      var text = "Alice would gain 54 happiness units by sitting next to Bob.\r\nAlice would lose 79 happiness units by sitting next to Carol.\r\nAlice would lose 2 happiness units by sitting next to David.\r\nBob would gain 83 happiness units by sitting next to Alice.\r\nBob would lose 7 happiness units by sitting next to Carol.\r\nBob would lose 63 happiness units by sitting next to David.\r\nCarol would lose 62 happiness units by sitting next to Alice.\r\nCarol would gain 60 happiness units by sitting next to Bob.\r\nCarol would gain 55 happiness units by sitting next to David.\r\nDavid would gain 46 happiness units by sitting next to Alice.\r\nDavid would lose 7 happiness units by sitting next to Bob.\r\nDavid would gain 41 happiness units by sitting next to Carol.";
      var input = Dinner.ParseInput(text);
      var persons = Dinner.GetPersons(input);
      var permutation = Dinner.GetAllPermutationsOf(persons.Count - 1).First();

      var person = Dinner.GetPersonAt(persons, permutation, 0);
      person.Should().Be("Alice");

    }

    [Fact]
    public void Can_calculate_single_happiness()
    {
      var text = "Alice would gain 54 happiness units by sitting next to Bob.\r\nAlice would lose 79 happiness units by sitting next to Carol.\r\nAlice would lose 2 happiness units by sitting next to David.\r\nBob would gain 83 happiness units by sitting next to Alice.\r\nBob would lose 7 happiness units by sitting next to Carol.\r\nBob would lose 63 happiness units by sitting next to David.\r\nCarol would lose 62 happiness units by sitting next to Alice.\r\nCarol would gain 60 happiness units by sitting next to Bob.\r\nCarol would gain 55 happiness units by sitting next to David.\r\nDavid would gain 46 happiness units by sitting next to Alice.\r\nDavid would lose 7 happiness units by sitting next to Bob.\r\nDavid would gain 41 happiness units by sitting next to Carol.";
      var input = Dinner.ParseInput(text);
      var persons = Dinner.GetPersons(input);

      var permutation = Dinner.GetAllPermutationsOf(persons.Count - 1).First();

      var happiness = Dinner.CalculateHappiness(persons, permutation, input.Table);
      happiness.Should().Be(330);
    }

    [Fact]
    public void Can_calculate_best_happiness()
    {
      var text = "Alice would gain 54 happiness units by sitting next to Bob.\r\nAlice would lose 79 happiness units by sitting next to Carol.\r\nAlice would lose 2 happiness units by sitting next to David.\r\nBob would gain 83 happiness units by sitting next to Alice.\r\nBob would lose 7 happiness units by sitting next to Carol.\r\nBob would lose 63 happiness units by sitting next to David.\r\nCarol would lose 62 happiness units by sitting next to Alice.\r\nCarol would gain 60 happiness units by sitting next to Bob.\r\nCarol would gain 55 happiness units by sitting next to David.\r\nDavid would gain 46 happiness units by sitting next to Alice.\r\nDavid would lose 7 happiness units by sitting next to Bob.\r\nDavid would gain 41 happiness units by sitting next to Carol.";
      var maxHappiness = Dinner.GetMaxHappiness(text);
      maxHappiness.Should().Be(330);
    }

  }
}