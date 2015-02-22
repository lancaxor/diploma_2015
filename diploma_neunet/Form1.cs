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
    public partial class Form1 : Form
    {
        NeuNet net;
        List<Double> ltime;
        int picsize = 50;
        Thread learner;
        

        public Form1()
        {
            InitializeComponent();
            this.clbExperiments.CheckOnClick = false;
            ltime = new List<double>();
            net = new NeuNet { NumInput = 2500, NumHidden = 100, NumOutput = 26 };
        }

        private void btnLearn_Click(object sender, EventArgs e)
        {
            //net.LearnInt();
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

        private void button2_Click(object sender, EventArgs e)
        {
            ///////////cakk AddExperiment
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.clbExperiments.Items.Clear();
        }
    }
}