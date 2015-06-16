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
        int randNum = 0;
        Random r;
        NetConfig _config;
        int repeats = 0;
        public AddExperiment(Experiment experiment, NetConfig config)
        {
            InitializeComponent();
            this.newExp = experiment;
            r = new Random();
            _config = config;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                int tmp = 0;

                var v = this.tbIndexes.Text.Split(',').Select(x => x.Trim());
                this.randNum = v.Count(x => x.Equals("r"));
                var raw = from item in v where Int32.TryParse(item, out tmp) select Int32.Parse(item);
                var indexes = raw.Distinct<int>();

                if ((raw.Count() + randNum) != v.Count() && !(this.tbIndexes.Text.Length==0))
                {
                    MessageBox.Show("Bad input: Fixed Neurons. Required integer.");
                    this.tbIndexes.Focus();
                    return;
                }

                if (indexes.Count(x => x >= this._config.NumHidden) > 0)
                {
                    MessageBox.Show(String.Format("Wrong input data: 1 or more indecies is out of bounds. Required number from 0 to {0}", this._config.NumHidden - 1));
                    this.tbIndexes.Focus();
                    return;
                }

                var resultItems = indexes.ToList<int>();
                for (int i = 0; i < randNum; i++)
                {
                    int tmpIndex = 0;
                    do
                    {
                        tmpIndex = r.Next(0, this._config.NumHidden);
                    } while (resultItems.Contains(tmpIndex));
                    resultItems.Add(tmpIndex);
                }

                if (this.tbTitle.Text.Length == 0)
                    this.tbTitle.Text = String.Format("Fixed {0} neuron(s)", resultItems.Count());

                if (!Int32.TryParse(this.tbRepeats.Text, out this.repeats))
                {
                    MessageBox.Show("Wrong input data: repeats. Required integer.");
                    this.tbRepeats.Focus();
                    return;
                }

                this.newExp.name = this.tbTitle.Text;
                this.newExp.fixedNeurons = new List<int>(resultItems);
                this.newExp.repeats = this.repeats;
                CanClose = true;
            }

            catch (FormatException)
            {
                MessageBox.Show("Bad input: Fixed Neurons Index");
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
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
