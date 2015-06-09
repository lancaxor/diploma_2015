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
        Boolean running;
        NetConfig config;
        GetDataForAutoLearning getData;
        Logger logger;

        bool clearChart = false;
        int AttemptsOnCurrentIndex = 3;           // we fixed some neuron and learn network some times with this fixed neuron
        int AttemptsOnCurrentCount = 3;           // we selected number of fixed neurons, but index is selected randomly. this const describe, how many times will ve select fixed index
        int MinFixedCount = 0;                   // max number of fixed neurons
        int MaxFixedCount = 10;                   // max number of fixed neurons
        //total = AttemptsOnCurrentIndex * AttemptsOnCurrentCount * MaxFixedCount times we will learn out NeuNet.

        public MainForm()
        {
            InitializeComponent();
            this.tbStatus.ScrollBars = RichTextBoxScrollBars.ForcedHorizontal;
            this.clbExperiments.CheckOnClick = false;
            config  = new NetConfig { maxEpoch = 3000, minError = 0.005, minErrorChange = 1E-9, NumInput = 784, NumHidden = 50, NumOutput = 10 };
            net = new NeuNet(config);
            exps = new ExperimentsWorker();
            graph = new GraphWorker();
            getData = new GetDataForAutoLearning(this.config);
            learner = new Thread(new ThreadStart(this.Learn));
            running = false;
            logger = new Logger();
        }
        private void Learn()
        {
            //this.learnTime = this.net.LearnInt(this);
        }
        private void btnLearn_Click(object sender, EventArgs e)
        {
            if (!this.running)
            {
                if (this.clbExperiments.Items.Count < 1)
                {
                    MessageBox.Show("No experiments in Experiment List. Press Add button for creating Experiment.");
                    return;
                }
                this.SetState("Preparing...");
                this.running = true;
                this.btnAutoLearn.Enabled = false;
                this.btnLearn.Text = "Stop";
                if (this.clearChart) this.graph.ClearChart();

                this.logger.Start();
                this.DoBatchLearning();

                MessageBox.Show("All experiments has been ended successfully. Press Graph button for chart viewing.");
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
            var dataForCurrentCount = new List<NetData>();
            NetData avgData = NetData.Empty;

            int candidate=0;
            if (this.MinFixedCount == 0)            //for 0 fixed neurons
            {
                for (int n = 0; n < AttemptsOnCurrentIndex; n++)
                {
                    this.SetState(n + 1);
                    dataForCurrentCount.Add(this.net.LearnInt(this));
                }
                foreach (var t in dataForCurrentCount)
                {
                    avgData.time += t.time;
                    avgData.epoch += t.epoch;
                    avgData.errChange += t.errChange;
                    avgData.avgErr += t.avgErr;
                }

                avgData.avgErr /= dataForCurrentCount.Count;
                avgData.epoch /= dataForCurrentCount.Count;
                avgData.errChange /= dataForCurrentCount.Count;
                avgData.seconds = avgData.time.TotalSeconds / dataForCurrentCount.Count;

                dataForCurrentCount.Clear();

                var e = new Experiment { fixedNeurons = new List<int>(0), name = "Unfixed", data = avgData };
                this.graph.AddExperiment(e);
                if (logger.isRunning)
                    logger.Log(e.ToExtendedString());

                avgData = NetData.Empty;
            }

            for (int i = MinFixedCount == 0 ? 1 : MinFixedCount; i <= MaxFixedCount; i++)         //fixed neurons
            {
                avgData = NetData.Empty;
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
                        while (exp.fixedNeurons.Count(x => x == candidate) > 0);                //check if candidate has not been fixed

                        //this.net.Fix(candidate);
                        exp.fixedNeurons.Add(candidate);
                    }

                    for (int k = 0; k < AttemptsOnCurrentIndex; k++)        //on current fixed index
                    {
                        if (!this.running)
                        {
                            this.DoStop();
                            return;
                        }

                        this.SetState(i, exp.fixedNeurons, j + 1, k + 1);
                        this.net.LearnInt(this);
                        this.net.Fix(exp.fixedNeurons);
                        dataForCurrentCount.Add(this.net.LearnInt(this));
                        this.net.Unfix();
                    }
                }

                foreach (var t in dataForCurrentCount)
                {
                    avgData.time += t.time;
                    avgData.epoch += t.epoch;
                    avgData.errChange += t.errChange;
                    avgData.avgErr += t.avgErr;
                }

                avgData.avgErr /= dataForCurrentCount.Count;
                avgData.epoch /= dataForCurrentCount.Count;
                avgData.errChange /= dataForCurrentCount.Count;
                avgData.seconds = avgData.time.TotalSeconds / dataForCurrentCount.Count;

                dataForCurrentCount.Clear();
                exp.data = avgData;
                graph.AddExperiment(exp);

                if (logger.isRunning)
                    logger.Log(exp.ToExtendedString());
            }
        }
        private void DoBatchLearning()
        {
            for (int i = 0; i < this.exps.Count; i++)
            {
                if (this.clbExperiments.GetItemChecked(i))      //user can check it manually for skipping
                    continue;

                var allData = new List<NetData>();
                NetData avgData = NetData.Empty;

                this.clbExperiments.SelectedIndex = i;

                for (int j = 0; j < this.exps[i].repeats; j++)
                {
                    this.SetState(j + 1, this.exps[i].repeats, i + 1, this.exps.Count, this.exps[i].fixedNeurons);
                    this.net.LearnInt(this);
                    this.net.Fix(this.exps[i].fixedNeurons);
                    allData.Add(this.net.LearnInt(this));
                    this.net.Unfix();
                }

                foreach (var t in allData)
                {
                    avgData.time += t.time;
                    avgData.epoch += t.epoch;
                    avgData.errChange += t.errChange;
                    avgData.avgErr += t.avgErr;
                }
                avgData.avgErr /= allData.Count;
                avgData.epoch /= allData.Count;
                avgData.errChange /= allData.Count;
                avgData.seconds = avgData.time.TotalSeconds / allData.Count;

                this.exps[i].data = avgData;
                this.clbExperiments.SetItemChecked(i, true);
                graph.AddExperiment(this.exps[i]);

                if (logger.isRunning)
                    logger.Log(exps[i].ToExtendedString());
            }
        }
        private void DoStop()
        {
            this.btnAutoLearn.Text = "AutoLearn";
            this.btnLearn.Text = "Learn";
            this.btnLearn.Enabled = true;
            this.btnAutoLearn.Enabled = true;
            this.running = false;
            if (this.logger.isRunning)
                this.logger.Stop();
            this.SetState("Ready");
        }

        private void AddExperiment_Click(object sender, EventArgs e)
        {
            int maxFx = 5;      //show in Experiments List only maxFx neurons
            var exp = new Experiment();
            var str = String.Empty;
            bool tl = false;
            if (new AddExperiment(exp, this.config).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                str = String.Format("Fixed: {0} neuron(s) (", exp.fixedNeurons.Count);
                for (int i = 0; i < exp.fixedNeurons.Count - 1; i++)
                {
                    str += String.Format("{0}, ", exp.fixedNeurons[i].ToString());
                    if (i == maxFx)
                    {
                        tl = true;
                        str += "...)";
                        break;
                    }
                }

                if (!tl)
                    str += String.Format("{0})", exp.fixedNeurons[exp.fixedNeurons.Count - 1]);
                str += String.Format("; repeats: {0}", exp.repeats);
                exps.Add(exp);

                this.clbExperiments.Items.Add(str);
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

        private void SetState(int currentAttempt, int maxAttempts, int currentExperiment, int maxExperiments, List<int> fixedNeurons)
        {
            String str = String.Format("Experiment{0}/{1}; Attempt {2}/{3}, fixed (", currentExperiment, maxExperiments, currentAttempt, maxAttempts);
            for (int i = 0; i < (fixedNeurons.Count - 1); i++)
                str += String.Format("{0}, ", fixedNeurons[i]);
            str += String.Format("{0})", fixedNeurons[fixedNeurons.Count - 1]);
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
            {
                this.DoStop();
                this.Close();
            }
        }

        private void btnAutoLearn_Click(object sender, EventArgs e)
        {
            if (!this.running)
            {
                this.SetState("Preparing...");
                this.running = true;
                this.getData.StartPosition = FormStartPosition.CenterParent;
                if (this.getData.ShowDialog() != DialogResult.OK)
                {
                    this.DoStop();
                    return;
                }
                this.getData.GetData(out this.MinFixedCount, out this.MaxFixedCount, out this.AttemptsOnCurrentCount, out this.AttemptsOnCurrentIndex);
                
                this.btnAutoLearn.Text = "Stop";
                this.btnLearn.Enabled = false;

                if (this.clearChart) this.graph.ClearChart();

                this.logger.Start();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.clearChart = this.cbClearChart.Checked;
        }
    }
}