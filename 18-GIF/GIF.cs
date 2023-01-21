namespace _18_GIF
{
  internal record struct Pos(int X, int Y)
  {
    internal IEnumerable<Pos> GetNeighbours(int size)
    {
      for (int x = X - 1; x <= X + 1; ++x)
        for (int y = Y - 1; y <= Y + 1; ++y)
          if (x != X || y != Y)
          {
            if (x >= 0 && x < size && y >= 0 && y < size)
              yield return new Pos(x, y);
          }
    }
  }

  internal record Input(HashSet<Pos> Positions, int Dimension)
  {
    public HashSet<Pos> Positions { get; set; } = Positions;
  }

  internal class GIF
  {
    readonly Input input;
    readonly bool withCornerLights;
    public GIF(string text, bool withCornerLights)
    {
      input = ParseInput(text);
      this.withCornerLights = withCornerLights;
      AddCornerLights();
    }

    private void AddCornerLights()
    {
      if (withCornerLights)
      {
        input.Positions.Add(new Pos(0, 0));
        input.Positions.Add(new Pos(input.Dimension - 1, 0));
        input.Positions.Add(new Pos(0, input.Dimension - 1));
        input.Positions.Add(new Pos(input.Dimension - 1, input.Dimension - 1));
      }
    }

    internal static Input ParseInput(string text)
    {
      var positions = new HashSet<Pos>();
      var pos = new Pos(0, 0);
      foreach (var line in text.Split('\n'))
      {
        if (!string.IsNullOrWhiteSpace(line))
        {
          pos.X = 0;
          foreach (var ch in line.Trim())
          {
            if (ch == '#')
              positions.Add(pos);
            ++pos.X;
          }
          ++pos.Y;
        }
      }
      return new Input(positions, pos.Y);
    }

    internal void ProcessGeneration()
    {
      var newPositions = new HashSet<Pos>();
      var pos = new Pos();

      for (pos.X = 0; pos.X < input.Dimension; ++pos.X)
        for (pos.Y = 0; pos.Y < input.Dimension; ++pos.Y)
        {
          var numNeighbours = GetNumNeighbours(pos);
          if (input.Positions.Contains(pos))
          {
            if (numNeighbours == 2 || numNeighbours == 3)
              newPositions.Add(pos);
          }
          else if (numNeighbours == 3)
            newPositions.Add(pos);
        }

      input.Positions = newPositions;

      AddCornerLights();
    }

    internal int GetNumNeighbours(Pos pos)
    {
      return pos.GetNeighbours(input.Dimension).Count(n => input.Positions.Contains(n));
    }

    internal int GetNumActive()
    {
      return input.Positions.Count();
    }

    internal static int GetActiveAfter(string text, int numSteps, bool withCornerLights)
    {
      var gif = new GIF(text, withCornerLights);
      for (int n = 0; n < numSteps; ++n)
        gif.ProcessGeneration();

      return gif.GetNumActive();
    }
  }
}