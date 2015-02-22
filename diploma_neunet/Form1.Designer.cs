namespace diploma_neunet
{
    partial class Form1
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
            this.btnLearn = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblStText = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.clbExperiments = new System.Windows.Forms.CheckedListBox();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLearn
            // 
            this.btnLearn.Location = new System.Drawing.Point(355, 358);
            this.btnLearn.Name = "btnLearn";
            this.btnLearn.Size = new System.Drawing.Size(85, 25);
            this.btnLearn.TabIndex = 1;
            this.btnLearn.Text = "Learn";
            this.btnLearn.UseVisualStyleBackColor = true;
            this.btnLearn.Click += new System.EventHandler(this.btnLearn_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(264, 358);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(85, 25);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblStText
            // 
            this.lblStText.AutoSize = true;
            this.lblStText.Location = new System.Drawing.Point(12, 333);
            this.lblStText.Name = "lblStText";
            this.lblStText.Size = new System.Drawing.Size(52, 17);
            this.lblStText.TabIndex = 3;
            this.lblStText.Text = "Status:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(82, 358);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 25);
            this.button1.TabIndex = 5;
            this.button1.Text = "Graph";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // clbExperiments
            // 
            this.clbExperiments.FormattingEnabled = true;
            this.clbExperiments.Location = new System.Drawing.Point(15, 12);
            this.clbExperiments.Name = "clbExperiments";
            this.clbExperiments.Size = new System.Drawing.Size(425, 310);
            this.clbExperiments.TabIndex = 6;
            // 
            // tbStatus
            // 
            this.tbStatus.Enabled = false;
            this.tbStatus.Location = new System.Drawing.Point(70, 330);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.Size = new System.Drawing.Size(370, 22);
            this.tbStatus.TabIndex = 7;
            this.tbStatus.Text = "Ready";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(173, 358);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 25);
            this.button2.TabIndex = 8;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 395);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbStatus);
            this.Controls.Add(this.clbExperiments);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblStText);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnLearn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Genetic Learn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLearn;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblStText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckedListBox clbExperiments;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.Button button2;

    }
}

