namespace _01_NotQuiteLisp
{
  internal class FloorEvaluator
  {
    private int _currentFloor = 0;
    private int _firstBasementPosition = 0;
    private int _currentStep = 0;

    internal object GetFirstBasementPosition()
    {
      return _firstBasementPosition;
    }

    internal object GetFloor()
    {
      return _currentFloor;
    }

    internal void ProcessInput(string input)
    {
      foreach (var ch in input)
      {
        if (ch == ')')
          --_currentFloor;
        else
          ++_currentFloor;

        ++_currentStep;
        if (_firstBasementPosition == 0 && _currentFloor < 0)
          _firstBasementPosition = _currentStep;
      }

    }
  }
}
