using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace diploma_neunet
{
    class Experiment
    {
        public double time { get; set; }
        public string name { get; set; }
        public SeriesCollection series { get; set; }
    }
}
