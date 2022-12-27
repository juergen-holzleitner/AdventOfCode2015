namespace _06_ProbablyaFireHazard
{
  internal class Board
  {
    Action[,] board = new Action[1000, 1000];

    public Board()
    {
    }

    internal int GetNumLightsOn()
    {
      int numLightsOn = 0;
      for (int x = 0; x < board.GetLength(0); ++x)
        for (int y = 0; y < board.GetLength(1); ++y)
        {
          if (board[x, y] == Action.On)
            ++numLightsOn;
        }
      return numLightsOn;
    }

    internal void Perform(Instruction instruction)
    {
      for (int x = instruction.TopLeft.X; x <= instruction.BottomRight.X; ++x)
        for (int y = instruction.TopLeft.Y; y <= instruction.BottomRight.Y; ++y)
        {
          switch (instruction.Action)
          {
            case Action.On:
              board[x, y] = Action.On;
              break;
            case Action.Off:
              board[x, y] = Action.Off;
              break;
            case Action.Toggle:
              board[x, y] = board[x, y] == Action.Off ? Action.On : Action.Off;
              break;
            default:
              throw new ApplicationException();
          }
        }
    }
  }
}