using FluentAssertions;

namespace _19_Medicine
{
  public class UnitTest1
  {
    [Fact]
    public void Can_parse_intput_replacements()
    {
      var text = "H => HO\r\nH => OH\r\nO => HH\r\n\r\nHOH";
      var input = Medicine.ParseInput(text);
      input.Replacements.Should().BeEquivalentTo(new[]
      {
        new Replacement("H", "HO"),
        new Replacement("H", "OH"),
        new Replacement("O", "HH"),
      });
    }

    [Fact]
    public void Can_parse_intput_starting()
    {
      var text = "H => HO\r\nH => OH\r\nO => HH\r\n\r\nHOH\r\n";
      var input = Medicine.ParseInput(text);
      input.Starting.Should().Be("HOH");
    }

    [Fact]
    public void Can_get_all_replacements()
    {
      var starting = "HOH";
      var results = Medicine.GetAllReplacements(starting, new Replacement("H", "HO"));
      results.Should().BeEquivalentTo(new[] { "HOOH", "HOHO" });
    }

    [Fact]
    public void Can_get_all_replacements_of_input()
    {
      var text = "H => HO\r\nH => OH\r\nO => HH\r\n\r\nHOH\r\n";
      var input = Medicine.ParseInput(text);

      var results = Medicine.GetAllReplacements(input);

      results.Should().BeEquivalentTo(new[] { "HOOH", "HOHO", "OHOH", "HOOH", "HHHH" });
    }

    [Fact]
    public void Can_get_num_distinct_replacements()
    {
      var text = "H => HO\r\nH => OH\r\nO => HH\r\n\r\nHOH\r\n";
      var num = Medicine.GetNumDistinctReplacements(text);
      num.Should().Be(4);
    }

    [Fact]
    public void Can_get_num_distinct_replacements_of_other_example()
    {
      var text = "H => HO\r\nH => OH\r\nO => HH\r\n\r\nHOHOHO\r\n";
      var num = Medicine.GetNumDistinctReplacements(text);
      num.Should().Be(7);
    }

    [Fact]
    public void Can_get_next_molecules()
    {
      var text = "e => H\r\ne => O\r\nH => HO\r\nH => OH\r\nO => HH\r\n\r\nHOH";
      var input = Medicine.ParseInput(text);

      var nextMolecules = Medicine.GetNextMolecules("e", input.Replacements);
      nextMolecules.Should().BeEquivalentTo(new[] { "H", "O" });
    }

    [Fact]
    public void Can_get_next_molecules_of_set()
    {
      var text = "e => H\r\ne => O\r\nH => HO\r\nH => OH\r\nO => HH\r\n\r\nHOH";
      var input = Medicine.ParseInput(text);

      var nextMolecules = Medicine.GetNextMolecules(new[] { "e" }, input.Replacements);
      nextMolecules.Should().BeEquivalentTo(new[] { "H", "O" });
    }

    [Fact]
    public void Can_get_num_steps_until_found()
    {
      var text = "e => H\r\ne => O\r\nH => HO\r\nH => OH\r\nO => HH\r\n\r\nHOH";
      var num = Medicine.GetNumStepsUntilFound(text);
      num.Should().Be(3);
    }

    [Fact]
    public void Can_get_num_steps_until_found_second_sample()
    {
      var text = "e => H\r\ne => O\r\nH => HO\r\nH => OH\r\nO => HH\r\n\r\nHOHOHO";
      var num = Medicine.GetNumStepsUntilFound(text);
      num.Should().Be(6);
    }
  }
}