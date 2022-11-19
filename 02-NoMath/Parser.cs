using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _02_NoMath
{
  internal class Parser
  {
    readonly Regex _regex = new(@"(?<l>\d+)x(?<w>\d+)x(?<h>\d+)");

    internal record struct Dimensions(int L, int W, int H);

    internal Dimensions ParseLine(string line)
    {
      var match = _regex.Match(line);
      if (match.Success)
      {
        var l = int.Parse(match.Groups["l"].Value);
        var w = int.Parse(match.Groups["w"].Value);
        var h = int.Parse(match.Groups["h"].Value);

        return new Dimensions(l, w, h);
      }
      throw new ApplicationException("invalid line: " + line);
    }
  }
}
