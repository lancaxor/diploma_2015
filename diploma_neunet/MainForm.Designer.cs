namespace diploma_neunet
{
    partial class MainForm
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
            this.btnGraph = new System.Windows.Forms.Button();
            this.clbExperiments = new System.Windows.Forms.CheckedListBox();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLearn
            // 
            this.btnLearn.Location = new System.Drawing.Point(414, 358);
            this.btnLearn.Name = "btnLearn";
            this.btnLearn.Size = new System.Drawing.Size(85, 25);
            this.btnLearn.TabIndex = 1;
            this.btnLearn.Text = "Learn";
            this.btnLearn.UseVisualStyleBackColor = true;
            this.btnLearn.Click += new System.EventHandler(this.btnLearn_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(323, 358);
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
            // btnGraph
            // 
            this.btnGraph.Location = new System.Drawing.Point(141, 358);
            this.btnGraph.Name = "btnGraph";
            this.btnGraph.Size = new System.Drawing.Size(85, 25);
            this.btnGraph.TabIndex = 5;
            this.btnGraph.Text = "Graph";
            this.btnGraph.UseVisualStyleBackColor = true;
            this.btnGraph.Click += new System.EventHandler(this.btnGraph_Click);
            // 
            // clbExperiments
            // 
            this.clbExperiments.FormattingEnabled = true;
            this.clbExperiments.Location = new System.Drawing.Point(15, 12);
            this.clbExperiments.Name = "clbExperiments";
            this.clbExperiments.Size = new System.Drawing.Size(484, 310);
            this.clbExperiments.TabIndex = 6;
            // 
            // tbStatus
            // 
            this.tbStatus.Enabled = false;
            this.tbStatus.Location = new System.Drawing.Point(70, 330);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.Size = new System.Drawing.Size(429, 22);
            this.tbStatus.TabIndex = 7;
            this.tbStatus.Text = "Ready";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(232, 358);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(85, 25);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.AddExperiment_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(50, 358);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(85, 25);
            this.btnTest.TabIndex = 5;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 395);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.tbStatus);
            this.Controls.Add(this.clbExperiments);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnGraph);
            this.Controls.Add(this.lblStText);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnLearn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Genetic Learn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLearn;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblStText;
        private System.Windows.Forms.Button btnGraph;
        private System.Windows.Forms.CheckedListBox clbExperiments;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnTest;

    }
}

