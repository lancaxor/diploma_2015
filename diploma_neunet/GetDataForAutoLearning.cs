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
        int maxFixed, maxPass, maxAtt;
        public GetDataForAutoLearning()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(Int32.TryParse(this.tbMaxFixed.Text, out maxFixed) && Int32.TryParse(this.tbMaxAtt.Text, out maxAtt) && Int32.TryParse(this.tbMaxPass.Text, out maxPass)))
                MessageBox.Show("Bad values entered! Enter integer.");
            else
                this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void GetData (out int MaxFixed, out int MaxPasses, out int MaxAttemptions){
            MaxFixed = this.maxFixed;
            MaxPasses = this.maxPass;
            MaxAttemptions = this.maxAtt;
        }
    }
}