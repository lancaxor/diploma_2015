namespace diploma_neunet
{
    partial class GetDataForAutoLearning
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMinFixed = new System.Windows.Forms.TextBox();
            this.tbMaxPass = new System.Windows.Forms.TextBox();
            this.tbMaxAtt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tbMaxFixed = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Max number of fixed neurons:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Max number of passes:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Max number of attempts:";
            // 
            // tbMinFixed
            // 
            this.tbMinFixed.Location = new System.Drawing.Point(164, 12);
            this.tbMinFixed.Name = "tbMinFixed";
            this.tbMinFixed.Size = new System.Drawing.Size(42, 20);
            this.tbMinFixed.TabIndex = 1;
            this.tbMinFixed.Text = "0";
            // 
            // tbMaxPass
            // 
            this.tbMaxPass.Location = new System.Drawing.Point(164, 37);
            this.tbMaxPass.Name = "tbMaxPass";
            this.tbMaxPass.Size = new System.Drawing.Size(106, 20);
            this.tbMaxPass.TabIndex = 3;
            this.tbMaxPass.Text = "1";
            // 
            // tbMaxAtt
            // 
            this.tbMaxAtt.Location = new System.Drawing.Point(164, 63);
            this.tbMaxAtt.Name = "tbMaxAtt";
            this.tbMaxAtt.Size = new System.Drawing.Size(106, 20);
            this.tbMaxAtt.TabIndex = 4;
            this.tbMaxAtt.Text = "100";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(204, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 22);
            this.button1.TabIndex = 6;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(132, 89);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 22);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbMaxFixed
            // 
            this.tbMaxFixed.Location = new System.Drawing.Point(228, 12);
            this.tbMaxFixed.Name = "tbMaxFixed";
            this.tbMaxFixed.Size = new System.Drawing.Size(42, 20);
            this.tbMaxFixed.TabIndex = 2;
            this.tbMaxFixed.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(212, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "-";
            // 
            // GetDataForAutoLearning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 121);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbMaxAtt);
            this.Controls.Add(this.tbMaxPass);
            this.Controls.Add(this.tbMaxFixed);
            this.Controls.Add(this.tbMinFixed);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "GetDataForAutoLearning";
            this.Text = "GetDataForAutoLearning";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMinFixed;
        private System.Windows.Forms.TextBox tbMaxPass;
        private System.Windows.Forms.TextBox tbMaxAtt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbMaxFixed;
        private System.Windows.Forms.Label label4;
    }
}