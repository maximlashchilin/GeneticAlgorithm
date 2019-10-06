using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticEngine
{
  public class GeneticEngine
  {
    public const SelectionType DEFAULT_SELECTION_TYPE = SelectionType.TOURNEY;
    public const CrossingType DEFAULT_CROSSING_TYPE = CrossingType.ONE_POINT_RECOMBINATION;
    public const NewGenerationFormingStratigies DEFAULT_STRATIGY = NewGenerationFormingStratigies.ELLITISM;
    public const bool DEFAULT_USE_MUTATION = true;
    public const long DEFAULT_GENERATION_COUNT = 10000L;

    private IFitnessFunction _fitnessFunction;
    private long[][] _parentPopulation;
    private long[][] _childPopulation;
    private long _currentGenerationNumber = 0;

    private Random random = new Random(DateTime.Now.Millisecond);

    public int GenomeLength { get; set; }
    public long GenerationCount { get; set; }
    public int IndividualCount { get; set; }
    public SelectionType SelectionType { get; set; }
    public CrossingType CrossingType { get; set; }
    public NewGenerationFormingStratigies FormingStratigy { get; set; }
    public bool UseMutation { get; set; }
    public double MutationPercent { get; set; }

    public GeneticEngine(IFitnessFunction fitnessFunction)
    {
      this._fitnessFunction = fitnessFunction;
      this.GenomeLength = fitnessFunction.getAriaty();
      this.GenerationCount = DEFAULT_GENERATION_COUNT;
      this.IndividualCount = 100;
      this.SelectionType = DEFAULT_SELECTION_TYPE;
      this.CrossingType = DEFAULT_CROSSING_TYPE;
      this.UseMutation = DEFAULT_USE_MUTATION;
      this.MutationPercent = 0.1d;
    }

    public long[] run()
    {
      this._parentPopulation = new long[this.IndividualCount][];
      this._childPopulation = new long[this.IndividualCount][];

      this.generateFirstGeneration();

      while (this._currentGenerationNumber < this.GenerationCount)
      {
        long[][] secondaryArray = selection();
        crossing(secondaryArray);
        if (UseMutation)
        {
          mutation();
        }

        SelectIndividualsForNextGeneration();
        _currentGenerationNumber++;
      }

      return SelectBestIndividualFromGeneration();
    }

    private void generateFirstGeneration()
    {
      for (int i = 0; i < this.IndividualCount; i++)
      {
        _parentPopulation[i] = generateGenome();
      }
    }

    private long[] generateGenome()
    {
      long[] result = new long[this.GenomeLength];
      for (int i = 0; i < GenomeLength; i++)
      {
        result[i] = (long)((2 * random.NextDouble() - 1) * long.MaxValue);
      }

      return result;
    }

    private long[][] selection()
    {
      switch (SelectionType)
      {
        case SelectionType.PROPORTIONAL:
          {
            long[] f = new long[this.IndividualCount];
            long fSum = 0;
            for (int i = 0; i < this.IndividualCount; i++)
            {
              f[i] = _fitnessFunction.run(_parentPopulation[i]);
              fSum += f[i];
            }

            double[] fDeals = new double[IndividualCount];
            double[] fN = new double[IndividualCount];
            int[] numberOfIndividual = new int[IndividualCount];

            int secondaryArraySize = 0;
            for (int i = 0; i < this.IndividualCount; i++)
            {
              fDeals[i] = f[i] / fSum;
              fN[i] = fDeals[i] * IndividualCount;
              numberOfIndividual[i] = (int) Math.Floor(fN[i]);
              double randomValue = random.NextDouble();

              if (randomValue <= fN[i] - numberOfIndividual[i])
              {
                numberOfIndividual[i]++;
              }

              secondaryArraySize += numberOfIndividual[i];
            }

            long[][] secondaryArray = new long[secondaryArraySize][];
            int k = 0;
            for (int i = 0; i < IndividualCount; i++)
            {
              for (int j = 0; j < numberOfIndividual[i]; j++)
              {
                secondaryArray[k] = _parentPopulation[i];
                k++;
              }
            }

            return secondaryArray;
          }
        case SelectionType.TOURNEY:
          {
            for (int i = 0; i < this.IndividualCount; i++)
            {
              int index1 = random.Next(IndividualCount);
              int index2 = random.Next(IndividualCount);

              //long ffTime = System.currentTimeMillis(); // time

              long fr1 = this.getFitnessFunctionResult(index1);
              long fr2 = this.getFitnessFunctionResult(index2);

              //this.timeToFF += (System.currentTimeMillis() - ffTime); // time

              this._childPopulation[i] = fr1 > fr2 ? (long[]) this._parentPopulation[index1].Clone() : (long[]) this._parentPopulation[index2].Clone();
            }
            break;
          }
        default:
          throw new NotImplementedException();
      }
    }

    private void crossing(long[][] individualsForCross)
    {
      for (int i = 0; i < individualsForCross.Length / 2; i++)
      {
        int index1 = i << 1;
        int index2 = index1 | 1;
        cross(this._childPopulation[index1], this._childPopulation[index2]);
      }
    }

    private void cross(long[] genom1, long[] genom2)
    {
      switch (this.CrossingType)
      {
        case CrossingType.ONE_POINT_RECOMBINATION:
          {
            int index = this.random.Next(this.GenomeLength);
            int outerOffset = index >> SHIFT_FOR_DIVISION;
            int innerOffset = OCTET_LENGTH - (index & MASK_FOR_MOD);
            long tmp = 0;

            if (innerOffset < 63)
            {
              long mask = 1L << (innerOffset + 1) - 1;
              long swapMask = (genom1[outerOffset] ^ genom2[outerOffset]) & mask;
              genom1[outerOffset] ^= swapMask;
              genom2[outerOffset] ^= swapMask;
              outerOffset++;
            }
            for (int i = outerOffset; i < this.SizeOfArray; i++)
            {
              tmp = genom1[i];
              genom1[i] = genom2[i];
              genom2[i] = tmp;
            }

            break;
          }
        case CrossingType.TWO_POINT_RECOMBINATION:
          {
            int index1 = this.random.Next(this.GenomeLength);
            int index2 = this.random.Next(this.GenomeLength);
            int startIndex = Math.Min(index1, index2);
            int endIndex = Math.Max(index1, index2);
            int startOuterOffset = startIndex >> SHIFT_FOR_DIVISION;
            int startInnerOffset = OCTET_LENGTH - (startIndex & MASK_FOR_MOD);
            int endOuterOffset = endIndex >> SHIFT_FOR_DIVISION;
            int endInnerOffset = OCTET_LENGTH - (endIndex & MASK_FOR_MOD);
            long tmp = 0;

            if (startInnerOffset < OCTET_LENGTH - 1)
            {
              long mask = 1L << (startInnerOffset + 1) - 1;
              long swapMask = (genom1[startOuterOffset] ^ genom2[startOuterOffset]) & mask;
              genom1[startOuterOffset] ^= swapMask;
              genom2[startOuterOffset] ^= swapMask;
              startOuterOffset++;
            }
            for (int i = startOuterOffset; i <= endOuterOffset; i++)
            {
              tmp = genom1[i];
              genom1[i] = genom2[i];
              genom2[i] = tmp;
            }
            if (endInnerOffset > 0)
            {
              long mask = 1L << endInnerOffset - 1;
              long swapMask = (genom1[endOuterOffset] ^ genom2[endOuterOffset]) & mask;
              genom1[endOuterOffset] ^= swapMask;
              genom2[endOuterOffset] ^= swapMask;
            }

            break;
          }
        case CrossingType.ELEMENTWISE_RECOMBINATION:
          {
            for (int outerOffset = 0; outerOffset < this.SizeOfArray; outerOffset++)
            {
              long mask = (long)((2 * this.random.NextDouble() - 1) * long.MaxValue);
              long swapMask = (genom1[outerOffset] ^ genom2[outerOffset]) & mask;

              genom1[outerOffset] ^= swapMask;
              genom2[outerOffset] ^= swapMask;
            }
            break;
          }
        case CrossingType.ONE_ELEMENT_EXCHANGE:
          {
            int index = this.random.Next(this.GenomeLength);
            int outerOffset = index >> SHIFT_FOR_DIVISION;
            int innerOffset = OCTET_LENGTH - (index & MASK_FOR_MOD);
            long mask = 1L << innerOffset;
            long swapMask = (genom1[outerOffset] ^ genom2[outerOffset]) & mask;

            genom1[outerOffset] ^= swapMask;
            genom2[outerOffset] ^= swapMask;
            break;
          }
        default:
          throw new NotImplementedException();
      }
    }

    private void mutation()
    {
      foreach (long[] genom in this._childPopulation)
      {
        if (random.NextDouble() <= MutationPercent)
        {
          mutate(genom);
        }
      }
    }

    private void mutate(long[] genom)
    {
      int index = this.random.Next(this.GenomeLength);
      int outerOffset = index >> SHIFT_FOR_DIVISION;
      int innerOffset = (index & MASK_FOR_MOD);
      long mask = 1L << innerOffset;
      genom[outerOffset] ^= mask;
    }

    public void SelectIndividualsForNextGeneration()
    {
      switch (FormingStratigy)
      {
        case NewGenerationFormingStratigies.ELLITISM:
          {

            break;
          }
        case NewGenerationFormingStratigies.ROULETE:
          {

            break;
          }
        default:
          {
            break;
          }
      }
    }

    public long[] SelectBestIndividualFromGeneration()
    {
      long bestFitnessFunctionResult = 0;
      long[] bestGenom = null;
      foreach (long[] genom in this._parentPopulation)
      {
        long fitnessFunctionResult = this._fitnessFunction.run(genom);
        if (bestFitnessFunctionResult <= fitnessFunctionResult)
        {
          bestGenom = genom;
          bestFitnessFunctionResult = fitnessFunctionResult;
        }
      }

      return bestGenom;
    }

    public uint ToGray(uint b)
    {
      return b ^ (b >> 1);
    }

    public uint DecodeGray(uint g)
    {
      uint b;
      for (b = 0; g > 0; g >>= 1)
      {
        b ^= g;
      }

      return b;
    }
  }
}
