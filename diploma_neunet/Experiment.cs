using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace diploma_neunet
{
    public class Experiment
    {
        public double time { get; set; }
        public string name { get; set; }
        public List<NeuCoord> fixedNeurons { get; set; }

        public override string ToString()
        {
            if (this.fixedNeurons.Count < 1) return this.name;
            else if (this.fixedNeurons.Count == 1) return String.Format("{0} ({1}){2}", this.name, this.fixedNeurons[0], time > 0.0 ? " (" + time.ToString() + ")" : "");

            StringBuilder b = new StringBuilder(this.name);
            b.Append(" (");
            for (int i = 0; i < fixedNeurons.Count-1; i++)
                b.Append(fixedNeurons[i].Layer + "." + fixedNeurons[i].Index + ", ");
            b.Append(fixedNeurons[fixedNeurons.Count - 1].Layer + "." + fixedNeurons[fixedNeurons.Count - 1].Index);
            b.Append(')');
            if (this.time != 0.0)
                b.Append(" (" + time.ToString() + ")");
            return b.ToString();
        }
    }
}