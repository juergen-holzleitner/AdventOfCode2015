namespace _06_ProbablyaFireHazard
{
  internal class Board
  {
    readonly int[,] board = new int[1000, 1000];

    public Board()
    {
    }

    internal int GetNumLightsOn()
    {
      int numLightsOn = 0;
      for (int x = 0; x < board.GetLength(0); ++x)
        for (int y = 0; y < board.GetLength(1); ++y)
        {
          numLightsOn += board[x, y];
        }
      return numLightsOn;
    }

    internal void Perform(Instruction instruction, bool isPartTwo)
    {
      for (int x = instruction.TopLeft.X; x <= instruction.BottomRight.X; ++x)
        for (int y = instruction.TopLeft.Y; y <= instruction.BottomRight.Y; ++y)
        {
          switch (instruction.Action)
          {
            case Action.On:
              if (isPartTwo)
                ++board[x, y];
              else
                board[x, y] = 1;
              break;
            case Action.Off:
              if (isPartTwo && board[x, y] > 0)
                --board[x, y];
              else
                board[x, y] = 0;
              break;
            case Action.Toggle:
              if (isPartTwo)
                board[x, y] += 2;
              else
                board[x, y] = board[x, y] == 0 ? 1 : 0;
              break;
            default:
              throw new ApplicationException();
          }
        }
    }
  }
}