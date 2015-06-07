using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace diploma_neunet
{
    public partial class GraphWorker : Form
    {
        public GraphWorker()
        {
            InitializeComponent();
            this.mainChart.Series.Clear();
        }

        public void AddExperiment(Experiment exp)
        {
            //var n = (new Random()).Next(0, 100);
            //exp.name = "Serie" + n.ToString();
            //exp.time = n;

            if (exp.time == 0.0)
                return;

            var series = this.mainChart.Series.Add(exp.ToString());
            series.Points.Add((float)exp.time);
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            //this.mainChart.Series.Add(series);
        }

        public void ClearChart()
        {
            this.mainChart.Series.Clear();
        }
    }
}
