using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_NotQuiteLisp
{
  internal class FloorEvaluator
  {
    private int _currentFloor = 0;

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
      }
    }
  }
}
