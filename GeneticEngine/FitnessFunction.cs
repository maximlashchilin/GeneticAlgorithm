using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticEngine
{
  public class FitnessFunction : IFitnessFunction
  {
    private int _numberOfVariables = 2;

    public int getAriaty()
    {
      return _numberOfVariables;
    }

    public long run(long[] genom)
    {
      long x = genom[0];
      long y = genom[1];
      return -x * x - y * y;
    }
  }
}
