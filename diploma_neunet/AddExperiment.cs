using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace diploma_neunet
{
    public partial class AddExperiment : Form
    {
        Experiment newExp;
        bool CanClose = false;
        public AddExperiment(Experiment experiment)
        {
            InitializeComponent();
            this.newExp = experiment;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {

                var v = this.tbIndexes.Text.Split(',').Select(x => Int32.Parse(x.Trim()));
                if (this.tbTitle.Text.Length == 0)
                    this.tbTitle.Text = String.Format("Fixed {0} neuron(s)", v.Count());
                this.newExp.name = this.tbTitle.Text;
                this.newExp.fixedNeurons = new List<NeuCoord>();

                CanClose = true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Bad input: Fixed Neurons Index");
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
            //foreach (var vv in v)
            //    MessageBox.Show(vv.ToString());
        }

        private void AddExperiment_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !this.CanClose;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.CanClose = true;
            this.Close();
        }
    }
}
