using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticEngine
{
  public partial class FormGeneticEngine : Form
  {
    private static FitnessFunction ff = new FitnessFunction();
    private GeneticEngine ge = new GeneticEngine(ff);

    public FormGeneticEngine()
    {
      InitializeComponent();

      ge.IndividualCount = 100;
      ge.GenerationCount = 10000;
      ge.SelectionType = SelectionType.TOURNEY;
      ge.CrossingType = CrossingType.ELEMENTWISE_RECOMBINATION;
      ge.UseMutation = true;
      ge.MutationPercent = 0.01d;
    }

    private void buttonRun_Click(object sender, EventArgs e)
    {
      long[] better = ge.run();

      textBoxResultX.Text = better[0].ToString();
      textBoxResultY.Text = better[1].ToString();
    }
  }
}
