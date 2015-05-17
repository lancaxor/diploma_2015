using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace diploma_neunet
{
    public partial class MainForm : Form
    {
        NeuNet net;
        List<Double> ltime;
        int picsize = 50;
        Thread learner;
        AddExperiment addexp;
        ExperimentsWorker exps;
        GraphWorker graph;

        public MainForm()
        {
            InitializeComponent();
            this.clbExperiments.CheckOnClick = false;
            ltime = new List<double>();
            net = new NeuNet { NumInput = 2500, NumHidden = 100, NumOutput = 26 };  //input: 50x50 pixels picture, output: 26 letters
            exps = new ExperimentsWorker();
            graph = new GraphWorker();
        }

        private void btnLearn_Click(object sender, EventArgs e)
        {
            //net.LearnInt();       //◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘
            if (this.clbExperiments.Items.Count < 1)
                return;
            for (int i = 0; i < this.clbExperiments.Items.Count; i++)
            {
                if (this.clbExperiments.CheckedItems.Count == this.clbExperiments.Items.Count)
                    break;
                if (this.clbExperiments.CheckedIndices.Contains(i))
                    continue;
                this.clbExperiments.SetItemChecked(i, true);
            }
            MessageBox.Show("EndeD");
        }

        private void AddExperiment_Click(object sender, EventArgs e)
        {
            ///////////call AddExperiment
            //var v = new LearnDataGenerator();
            var exp = new Experiment();
            this.addexp = new AddExperiment(exp);
            var str = String.Empty;
            if (this.addexp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                /*foreach (var v in exp.fixedNeurons)
                    str += String.Format("fixed: {0}\n", v.ToString());
                MessageBox.Show(str);*/
                exps.Add(exp);
                this.clbExperiments.Items.Add(exp.ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.clbExperiments.Items.Clear();
            this.exps.Clear();
        }

        private void btnGraph_Click(object sender, EventArgs e)
        {
            //◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘◘
            this.graph.AddExperiment(new Experiment());
            this.graph.ShowDialog();
            
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            using (var t = new NeuroNet_Hard.MainFormWithPaint(this.net))
            {
                t.ShowDialog();
            }
        }
    }
}