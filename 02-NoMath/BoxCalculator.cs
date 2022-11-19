﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_NoMath
{
  internal class BoxCalculator
  {
    internal int CalculateRequired(int l, int w, int h)
    {
      var wrappingPaper = 2 * l * w + 2 * w * h + 2 * h * l;
      var slack = Math.Min(Math.Min(l * w, l * h), w * h);

      return wrappingPaper + slack;
    }
  }
}
