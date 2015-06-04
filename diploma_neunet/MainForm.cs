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
        //int picsize = 50;
        Thread learner;
        AddExperiment addexp;
        ExperimentsWorker exps;
        GraphWorker graph;
        TimeSpan learnTime;
        Boolean running;

        public MainForm()
        {
            InitializeComponent();
            this.clbExperiments.CheckOnClick = false;
            net = new NeuNet (); //{ NumInput = 2500, NumHidden = 100, NumOutput = 10 };  //input: 50x50 pixels picture, output: 26 letters
            exps = new ExperimentsWorker();
            graph = new GraphWorker();
            learner = new Thread(new ThreadStart(this.Learn));
            running = false;
        }
        private void Learn()
        {
            this.learnTime = this.net.LearnInt(this);
        }
        private void btnLearn_Click(object sender, EventArgs e)
        {
            if (!this.running)
            {
                this.running = true;
                this.btnLearn.Text = "Stop";
                net.LearnInt(this);
                //MessageBox.Show("Ended!");
            }
            this.DoStop();

            /*
            if (this.clbExperiments.Items.Count < 1)
                return;
            learner.Start();
            for (int j = 0; j < this.clbExperiments.Items.Count; j++)
            {
                if (this.clbExperiments.CheckedItems.Count == this.clbExperiments.Items.Count)
                    break;
                if (this.clbExperiments.CheckedIndices.Contains(j))
                    continue;
                this.clbExperiments.SetItemChecked(j, true);
            }
             */
        }
        private void DoStop()
        {
            this.btnLearn.Text = "Learn";
            this.running = false;
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
            using (var t = new NeuroNet_Hard.TestFormPaint(this.net))
            {
                t.ShowDialog();
            }
        }

        internal protected void SetState(String state)
        {
            this.tbStatus.Text = state;
        }

        internal protected bool GetState()
        {
            return this.running;
        }
    }
}