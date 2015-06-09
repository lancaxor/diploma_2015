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
        static int index = 1;
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

            if (exp.data.seconds == 0.0)
                return;

            var series = this.mainChart.Series.Add(String.Format("{0}. {1}", index, exp.ToString()));
            series.Points.Add((float)exp.data.seconds);
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            series.Label = index.ToString();
            index++;
            //this.mainChart.Series.Add(series);
        }

        public void ClearChart()
        {
            this.mainChart.Series.Clear();
            index = 1;
        }

        private void GraphWorker_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
