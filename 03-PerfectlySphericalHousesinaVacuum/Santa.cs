namespace _03_PerfectlySphericalHousesinaVacuum
{
  public enum Direction { Left, Right, Up, Down };

  record struct Pos(int X, int Y)
  {
    internal Pos Move(Direction direction)
    {
      switch (direction)
      {
        case Direction.Left:
          return new Pos(X - 1, Y);
        case Direction.Right:
          return new Pos(X + 1, Y);
        case Direction.Up:
          return new Pos(X, Y - 1);
        case Direction.Down:
          return new Pos(X, Y + 1);
        default:
          throw new ApplicationException();
      }
    }
  }

  internal class Santa
  {
    private Pos pos;
    private readonly HashSet<Pos> visitedPositions = new();

    public Santa()
    {
      visitedPositions.Add(pos);
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
        pos = pos.Move(direction);
        visitedPositions.Add(pos);
      }
    }

    internal static int GetNumVisitedHouses(string input)
    {
      var santa = new Santa();
      santa.MoveInput(input);
      return santa.GetNumberOfVisitedHouses();
    }
  }
}