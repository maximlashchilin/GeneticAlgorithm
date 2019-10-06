using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticEngine
{
  public interface IFitnessFunction
  {
    int getAriaty();

    long run(long[] genom);
  }
}
