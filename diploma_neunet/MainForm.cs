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
        NetConfig config;
        GetDataForAutoLearning getData;

        int AttemptsOnCurrentIndex = 3;           // we fixed some neuron and learn network some times with this fixed neuron
        int AttemptsOnCurrentCount = 3;           // we selected number of fixed neurons, but index is selected randomly. this const describe, how many times will ve select fixed index
        int MaxFixedCount = 10;                   // max number of fixed neurons
        //total = AttemptsOnCurrentIndex * AttemptsOnCurrentCount * MaxFixedCount times we will learn out NeuNet.

        public MainForm()
        {
            InitializeComponent();
            this.clbExperiments.CheckOnClick = false;
            config  = new NetConfig { maxEpoch = 3000, minError = 0.005, minErrorChange = 1E-9, NumInput = 784, NumHidden = 50, NumOutput = 10 };
            net = new NeuNet(config);
            exps = new ExperimentsWorker();
            graph = new GraphWorker();
            getData = new GetDataForAutoLearning();
            learner = new Thread(new ThreadStart(this.Learn));
            running = false;
        }
        private void Learn()
        {
            //this.learnTime = this.net.LearnInt(this);
        }
        private void btnLearn_Click(object sender, EventArgs e)
        {
            if (!this.running)
            {
                this.running = true;
                this.btnAutoLearn.Text = "Stop";
                this.DoFullLearning();
                //MessageBox.Show(String.Format("Time elapsed: {0}:{1}:{2}.{3}", time.Hours, time.Minutes, time.Seconds, time.Milliseconds));
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

        private void DoFullLearning()
        {
            Random rand = new Random();
            var timeForCurrentCount = new List<TimeSpan>();
            int candidate=0;
            double avgTime = 0.0;

            for (int n = 0; n<AttemptsOnCurrentIndex; n++)      //for 0 fixed neurons
            {
                this.SetState(n + 1);
                timeForCurrentCount.Add(this.net.LearnInt(this));
            }
            foreach (var t in timeForCurrentCount)
                avgTime+=t.TotalSeconds;
            avgTime /= timeForCurrentCount.Count;
            timeForCurrentCount.Clear();
            this.graph.AddExperiment(new Experiment { fixedNeurons = new List<int>(0), name = "Unfixed", time = avgTime });

            for (int i = 1; i <= MaxFixedCount; i++)         //fixed neurons
            {
                avgTime = 0.0;
                var exp = new Experiment();
                exp.name = String.Format("Fixed count: {0}", i);
                exp.fixedNeurons = new List<int>();

                for (int j = 0; j < AttemptsOnCurrentCount; j++)            //on current fixed count
                {
                    this.net.LearnInt(this);        //without fixing
                    exp.fixedNeurons.Clear();

                    for (int l = 0; l < i; l++)
                    {
                        do
                            candidate = rand.Next(0, this.config.NumHidden);
                        while (this.net.IsFixed(candidate));                //check if candidate has not been fixed

                        //this.net.Fix(candidate);
                        exp.fixedNeurons.Add(candidate);
                    }

                    for (int k = 0; k < AttemptsOnCurrentIndex; k++)        //on current fixed index
                    {
                        this.SetState(i, exp.fixedNeurons, j + 1, k + 1);
                        this.net.LearnInt(this);
                        this.net.Fix(exp.fixedNeurons);
                        timeForCurrentCount.Add(this.net.LearnInt(this));
                    }
                    this.net.Unfix();
                }

                foreach (var t in timeForCurrentCount)
                    avgTime += t.TotalSeconds;
                avgTime /= timeForCurrentCount.Count;
                timeForCurrentCount.Clear();
                exp.time = avgTime;
                graph.AddExperiment(exp);
            }
        }
        private void DoStop()
        {
            this.btnAutoLearn.Text = "AutoLearn";
            this.btnLearn.Text = "Learn";
            this.btnLearn.Enabled = true;
            this.btnAutoLearn.Enabled = true;
            this.running = false;
        }

        private void AddExperiment_Click(object sender, EventArgs e)
        {

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
            //this.graph.AddExperiment(new Experiment());
            this.graph.Show();
            
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            using (var t = new NeuroNet_Hard.TestFormPaint(this.net))
            {
                t.StartPosition = FormStartPosition.CenterParent;
                t.ShowDialog();
                //t.Show();
            }
        }

        internal protected void SetState(String state)
        {
            this.tbStatus.Text = state;
        }

        private void SetState(int currentCount, List<int> fixedN, int pass, int attempt)
        {
            String str = String.Format("Fixed {0}/{1} neurons (", currentCount, MaxFixedCount);
            for (int i = 0; i < (fixedN.Count-1); i++ )
                str += String.Format("{0}, ", fixedN[i]);
            str += String.Format("{0}", fixedN[fixedN.Count - 1]);
            str += String.Format("), pass {0}/{1}, attempt {2}/{3}",pass, AttemptsOnCurrentCount, attempt, AttemptsOnCurrentIndex);
            this.tbStatus.Text = str;
        }

        private void SetState(int attempt)
        {
            this.tbStatus.Text = String.Format("No fixed neurons, attempt {0}/{1}", attempt, AttemptsOnCurrentIndex);
        }

        internal protected bool GetState()
        {
            return this.running;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }

        private void btnLearn_Click_1(object sender, EventArgs e)
        {
            if (!this.running)
            {
                this.running = true;
                this.getData.StartPosition = FormStartPosition.CenterParent;
                if (this.getData.ShowDialog() != DialogResult.OK)
                {
                    this.DoStop();
                    return;
                }
                this.getData.GetData(out this.MaxFixedCount, out this.AttemptsOnCurrentCount, out this.AttemptsOnCurrentIndex);
                
                this.btnAutoLearn.Text = "Stop";
                this.btnLearn.Enabled = false;
                this.graph.ClearChart();
                this.DoFullLearning();
                MessageBox.Show("All experiments has been ended successfully. Press Graph button for chart viewing.");
            }
            this.DoStop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.clbExperiments.SelectedIndex == -1)
                return;
            this.clbExperiments.Items.RemoveAt(this.clbExperiments.SelectedIndex);
        }
    }
}