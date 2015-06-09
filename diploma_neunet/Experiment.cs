using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace diploma_neunet
{
    public class Experiment
    {
        NetData Data;
        public NetData data { get { return this.Data; } set { this.Data = new NetData(value); } }
        public string name { get; set; }
        public List<int> fixedNeurons { get; set; }
        public int repeats { get; set; }
        public Experiment()
        {
            this.fixedNeurons = new List<int>();
        }
        public override string ToString()
        {
            StringBuilder b = new StringBuilder(this.name);
            b.Append(String.Format(" ({0}s, {1} epoch, err={2}, err.change={3})", data.seconds.ToString("0.000"), data.epoch, data.avgErr.ToString("0.00000"), data.errChange.ToString("0.00000")));
            return b.ToString();
        }
        public string ToExtendedString()
        {
            StringBuilder b = new StringBuilder(this.name+" fixed = [");

            if (fixedNeurons.Count > 0)
                fixedNeurons.Sort();

            for (int i = 0; i < fixedNeurons.Count - 1; i++)
                b.AppendFormat("{0}, ", fixedNeurons[i]);
            if (fixedNeurons.Count > 0)
                b.AppendFormat("{0}], ", fixedNeurons[fixedNeurons.Count - 1]);
            else b.Append("], ");

            b.AppendFormat("average time = {0}s, average epoch = {1}, average error = {2}, average error change = {3}", data.seconds, data.epoch, data.avgErr, data.errChange);
            return b.ToString();
        }
    }
}