namespace _03_PerfectlySphericalHousesinaVacuum
{
  public enum Direction { Left, Right, Up, Down };

  record struct Pos(int X, int Y)
  {
    internal Pos Move(Direction direction)
    {
      return direction switch
      {
        Direction.Left => new Pos(X - 1, Y),
        Direction.Right => new Pos(X + 1, Y),
        Direction.Up => new Pos(X, Y - 1),
        Direction.Down => new Pos(X, Y + 1),
        _ => throw new ApplicationException(),
      };
    }
  }

  internal class Santa
  {
    private readonly Pos[] pos;
    private readonly HashSet<Pos> visitedPositions = new();
    private int currentSanta;

    public Santa(int numSantas)
    {
      pos = new Pos[numSantas];
      currentSanta = 0;
      visitedPositions.Add(pos[currentSanta]);
    }

    internal static Direction ParseDirection(char dir)
    {
      return dir switch
      {
        '<' => Direction.Left,
        '>' => Direction.Right,
        '^' => Direction.Up,
        'v' => Direction.Down,
        _ => throw new ApplicationException()
      };
    }

    internal static IEnumerable<Direction> ParseInput(string text)
    {
      var inputs = new List<Direction>();
      foreach (var ch in text.Trim())
      {
        inputs.Add(ParseDirection(ch));
      }
      return inputs;
    }

    internal int GetNumberOfVisitedHouses()
    {
      return visitedPositions.Count;
    }

    internal void MoveInput(string input)
    {
      foreach (var direction in ParseInput(input))
      {
        pos[currentSanta] = pos[currentSanta].Move(direction);
        visitedPositions.Add(pos[currentSanta]);
        ++currentSanta;
        if (currentSanta >= pos.Length)
          currentSanta = 0;
      }
    }

    internal static int GetNumVisitedHouses(string input)
    {
      var santa = new Santa(1);
      santa.MoveInput(input);
      return santa.GetNumberOfVisitedHouses();
    }

    internal static int GetNumVisitedHousesWithTowSantas(string input)
    {
      var santa = new Santa(2);
      santa.MoveInput(input);
      return santa.GetNumberOfVisitedHouses();
    }
  }
}