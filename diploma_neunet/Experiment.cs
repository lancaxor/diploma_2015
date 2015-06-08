using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace diploma_neunet
{
    public class Experiment
    {
        public NetData data { get; set; }
        public string name { get; set; }
        public List<int> fixedNeurons { get; set; }
        public Experiment()
        {
            this.fixedNeurons = new List<int>();
        }
        public override string ToString()
        {
           // if (this.fixedNeurons.Count < 1) return this.name;
            //else if (this.fixedNeurons.Count == 1) return String.Format("{0} ({1}){2}", this.name, this.fixedNeurons[0], time > 0.0 ? " (" + time.ToString() + ")" : "");

            StringBuilder b = new StringBuilder(this.name);
            //b.Append(" (");
            //for (int i = 0; i < fixedNeurons.Count-1; i++)
            //    b.Append(fixedNeurons[i]+ ", ");
            //b.Append(fixedNeurons[fixedNeurons.Count - 1]);
            //b.Append(')');
            b.Append(String.Format(" ({0}s, {1} epoch, err={2}, err.change={3})", data.seconds, data.epoch, (float)data.avgErr, (float)data.errChange));
            return b.ToString();
        }
    }
}