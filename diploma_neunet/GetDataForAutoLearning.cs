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
    public partial class GetDataForAutoLearning : Form
    {
        int minFixed, maxFixed, maxPass, maxAtt;
        bool canclose = false;
        NetConfig _config;
        public GetDataForAutoLearning(NetConfig config)
        {
            InitializeComponent();
            this._config = config;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.canclose = false;
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            if (!(Int32.TryParse(this.tbMinFixed.Text, out minFixed) && Int32.TryParse(this.tbMaxFixed.Text, out maxFixed) && Int32.TryParse(this.tbMaxAtt.Text, out maxAtt) && Int32.TryParse(this.tbMaxPass.Text, out maxPass)))
                MessageBox.Show("Bad values entered! Enter integers.");
            else if (this.minFixed > this.maxFixed)
            {
                MessageBox.Show("Min value canot be more then Max value.");
                this.tbMinFixed.Focus();
            }
            else if (this.minFixed > this._config.NumHidden)
            {
                MessageBox.Show(String.Format("Bad Min Fixed value: required integer between 0 and {0}.", this._config.NumHidden));
                this.tbMinFixed.Focus();
            }
            else if (this.maxFixed > this._config.NumHidden)
            {
                MessageBox.Show(String.Format("Bad Max Fixed value: required integer between 0 and {0}.", this._config.NumHidden));
                this.tbMaxFixed.Focus();
            }
            else
            {
                this.canclose = true;
                this.Close();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.canclose = true;
            this.Close();
        }

        public void GetData (out int MinFixed, out int MaxFixed, out int MaxPasses, out int MaxAttemptions){
            MinFixed = this.minFixed;
            MaxFixed = this.maxFixed;
            MaxPasses = this.maxPass;
            MaxAttemptions = this.maxAtt;
        }

        private void GetDataForAutoLearning_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !canclose;
        }
    }
}