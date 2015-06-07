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
            this.btnAutoLearn = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblStText = new System.Windows.Forms.Label();
            this.btnGraph = new System.Windows.Forms.Button();
            this.clbExperiments = new System.Windows.Forms.CheckedListBox();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLearn = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAutoLearn
            // 
            this.btnAutoLearn.Location = new System.Drawing.Point(243, 292);
            this.btnAutoLearn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAutoLearn.Name = "btnAutoLearn";
            this.btnAutoLearn.Size = new System.Drawing.Size(64, 20);
            this.btnAutoLearn.TabIndex = 1;
            this.btnAutoLearn.Text = "AutoLearn";
            this.btnAutoLearn.UseVisualStyleBackColor = true;
            this.btnAutoLearn.Click += new System.EventHandler(this.btnLearn_Click_1);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(379, 59);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(64, 20);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblStText
            // 
            this.lblStText.AutoSize = true;
            this.lblStText.Location = new System.Drawing.Point(9, 271);
            this.lblStText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStText.Name = "lblStText";
            this.lblStText.Size = new System.Drawing.Size(40, 13);
            this.lblStText.TabIndex = 3;
            this.lblStText.Text = "Status:";
            // 
            // btnGraph
            // 
            this.btnGraph.Location = new System.Drawing.Point(175, 292);
            this.btnGraph.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGraph.Name = "btnGraph";
            this.btnGraph.Size = new System.Drawing.Size(64, 20);
            this.btnGraph.TabIndex = 5;
            this.btnGraph.Text = "Graph";
            this.btnGraph.UseVisualStyleBackColor = true;
            this.btnGraph.Click += new System.EventHandler(this.btnGraph_Click);
            // 
            // clbExperiments
            // 
            this.clbExperiments.FormattingEnabled = true;
            this.clbExperiments.Location = new System.Drawing.Point(11, 10);
            this.clbExperiments.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.clbExperiments.Name = "clbExperiments";
            this.clbExperiments.Size = new System.Drawing.Size(364, 244);
            this.clbExperiments.TabIndex = 6;
            // 
            // tbStatus
            // 
            this.tbStatus.Enabled = false;
            this.tbStatus.Location = new System.Drawing.Point(52, 268);
            this.tbStatus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.Size = new System.Drawing.Size(391, 20);
            this.tbStatus.TabIndex = 7;
            this.tbStatus.Text = "Ready";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(379, 11);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(64, 20);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.AddExperiment_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(107, 292);
            this.btnTest.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(64, 20);
            this.btnTest.TabIndex = 5;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(379, 292);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(64, 20);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLearn
            // 
            this.btnLearn.Location = new System.Drawing.Point(311, 292);
            this.btnLearn.Margin = new System.Windows.Forms.Padding(2);
            this.btnLearn.Name = "btnLearn";
            this.btnLearn.Size = new System.Drawing.Size(64, 20);
            this.btnLearn.TabIndex = 1;
            this.btnLearn.Text = "Learn";
            this.btnLearn.UseVisualStyleBackColor = true;
            this.btnLearn.Click += new System.EventHandler(this.btnLearn_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(379, 35);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(64, 20);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 327);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.tbStatus);
            this.Controls.Add(this.clbExperiments);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnGraph);
            this.Controls.Add(this.lblStText);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLearn);
            this.Controls.Add(this.btnAutoLearn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Genetic Learn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAutoLearn;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblStText;
        private System.Windows.Forms.Button btnGraph;
        private System.Windows.Forms.CheckedListBox clbExperiments;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLearn;
        private System.Windows.Forms.Button btnDelete;

    }
}

