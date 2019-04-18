namespace Bachelor_app.StereoVision.WindowsForm
{
    partial class StereoSGBMForm
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
            this.components = new System.ComponentModel.Container();
            this.BlockSize = new System.Windows.Forms.Label();
            this.Disparity = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.P1 = new System.Windows.Forms.Label();
            this.PreFilterCap = new System.Windows.Forms.Label();
            this.SpeckleWindowsSize = new System.Windows.Forms.Label();
            this.MinDisparity = new System.Windows.Forms.Label();
            this.DispMax12Diff = new System.Windows.Forms.Label();
            this.P2 = new System.Windows.Forms.Label();
            this.UniquenessRatio = new System.Windows.Forms.Label();
            this.SpeckleRange = new System.Windows.Forms.Label();
            this.ModeSGBM = new System.Windows.Forms.RadioButton();
            this.ModeHH = new System.Windows.Forms.RadioButton();
            this.Mode = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Mode.SuspendLayout();
            this.SuspendLayout();
            // 
            // BlockSize
            // 
            this.BlockSize.AutoSize = true;
            this.BlockSize.Location = new System.Drawing.Point(125, 33);
            this.BlockSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BlockSize.Name = "BlockSize";
            this.BlockSize.Size = new System.Drawing.Size(55, 13);
            this.BlockSize.TabIndex = 7;
            this.BlockSize.Text = "Block size";
            // 
            // Disparity
            // 
            this.Disparity.AutoSize = true;
            this.Disparity.Location = new System.Drawing.Point(125, 7);
            this.Disparity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Disparity.Name = "Disparity";
            this.Disparity.Size = new System.Drawing.Size(47, 13);
            this.Disparity.TabIndex = 5;
            this.Disparity.Text = "Disparity";
            // 
            // P1
            // 
            this.P1.AutoSize = true;
            this.P1.Location = new System.Drawing.Point(125, 59);
            this.P1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.P1.Name = "P1";
            this.P1.Size = new System.Drawing.Size(20, 13);
            this.P1.TabIndex = 9;
            this.P1.Text = "P1";
            // 
            // PreFilterCap
            // 
            this.PreFilterCap.AutoSize = true;
            this.PreFilterCap.Location = new System.Drawing.Point(125, 85);
            this.PreFilterCap.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PreFilterCap.Name = "PreFilterCap";
            this.PreFilterCap.Size = new System.Drawing.Size(50, 13);
            this.PreFilterCap.TabIndex = 11;
            this.PreFilterCap.Text = "Filter cap";
            // 
            // SpeckleWindowsSize
            // 
            this.SpeckleWindowsSize.AutoSize = true;
            this.SpeckleWindowsSize.Location = new System.Drawing.Point(125, 111);
            this.SpeckleWindowsSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SpeckleWindowsSize.Name = "SpeckleWindowsSize";
            this.SpeckleWindowsSize.Size = new System.Drawing.Size(111, 13);
            this.SpeckleWindowsSize.TabIndex = 13;
            this.SpeckleWindowsSize.Text = "Speckle windows size";
            // 
            // MinDisparity
            // 
            this.MinDisparity.AutoSize = true;
            this.MinDisparity.Location = new System.Drawing.Point(352, 7);
            this.MinDisparity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MinDisparity.Name = "MinDisparity";
            this.MinDisparity.Size = new System.Drawing.Size(76, 13);
            this.MinDisparity.TabIndex = 15;
            this.MinDisparity.Text = "Minus disparity";
            // 
            // DispMax12Diff
            // 
            this.DispMax12Diff.AutoSize = true;
            this.DispMax12Diff.Location = new System.Drawing.Point(352, 33);
            this.DispMax12Diff.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DispMax12Diff.Name = "DispMax12Diff";
            this.DispMax12Diff.Size = new System.Drawing.Size(97, 13);
            this.DispMax12Diff.TabIndex = 17;
            this.DispMax12Diff.Text = "Disparity difference";
            // 
            // P2
            // 
            this.P2.AutoSize = true;
            this.P2.Location = new System.Drawing.Point(352, 59);
            this.P2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.P2.Name = "P2";
            this.P2.Size = new System.Drawing.Size(20, 13);
            this.P2.TabIndex = 19;
            this.P2.Text = "P2";
            // 
            // UniquenessRatio
            // 
            this.UniquenessRatio.AutoSize = true;
            this.UniquenessRatio.Location = new System.Drawing.Point(352, 85);
            this.UniquenessRatio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UniquenessRatio.Name = "UniquenessRatio";
            this.UniquenessRatio.Size = new System.Drawing.Size(86, 13);
            this.UniquenessRatio.TabIndex = 21;
            this.UniquenessRatio.Text = "Uniqueness ratio";
            // 
            // SpeckleRange
            // 
            this.SpeckleRange.AutoSize = true;
            this.SpeckleRange.Location = new System.Drawing.Point(352, 111);
            this.SpeckleRange.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SpeckleRange.Name = "SpeckleRange";
            this.SpeckleRange.Size = new System.Drawing.Size(76, 13);
            this.SpeckleRange.TabIndex = 23;
            this.SpeckleRange.Text = "Speckle range";
            // 
            // ModeSGBM
            // 
            this.ModeSGBM.AutoSize = true;
            this.ModeSGBM.Checked = true;
            this.ModeSGBM.Location = new System.Drawing.Point(6, 19);
            this.ModeSGBM.Name = "ModeSGBM";
            this.ModeSGBM.Size = new System.Drawing.Size(56, 17);
            this.ModeSGBM.TabIndex = 24;
            this.ModeSGBM.TabStop = true;
            this.ModeSGBM.Text = "SGBM";
            this.ModeSGBM.UseVisualStyleBackColor = true;
            // 
            // ModeHH
            // 
            this.ModeHH.AutoSize = true;
            this.ModeHH.Location = new System.Drawing.Point(97, 19);
            this.ModeHH.Name = "ModeHH";
            this.ModeHH.Size = new System.Drawing.Size(41, 17);
            this.ModeHH.TabIndex = 25;
            this.ModeHH.Text = "HH";
            this.ModeHH.UseVisualStyleBackColor = true;
            // 
            // Mode
            // 
            this.Mode.Controls.Add(this.ModeHH);
            this.Mode.Controls.Add(this.ModeSGBM);
            this.Mode.Location = new System.Drawing.Point(20, 134);
            this.Mode.Name = "Mode";
            this.Mode.Size = new System.Drawing.Size(222, 46);
            this.Mode.TabIndex = 26;
            this.Mode.TabStop = false;
            this.Mode.Text = "Mode";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(20, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 27;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(20, 30);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 28;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(20, 56);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 29;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(20, 82);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 30;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(20, 108);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 31;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(247, 108);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 36;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(247, 82);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 20);
            this.textBox7.TabIndex = 35;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(247, 56);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(100, 20);
            this.textBox8.TabIndex = 34;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(247, 30);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(100, 20);
            this.textBox9.TabIndex = 33;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(247, 4);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(100, 20);
            this.textBox10.TabIndex = 32;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(272, 147);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 38;
            this.button2.Text = "Default";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(353, 147);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 37;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // StereoSGBMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 192);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Mode);
            this.Controls.Add(this.SpeckleRange);
            this.Controls.Add(this.UniquenessRatio);
            this.Controls.Add(this.P2);
            this.Controls.Add(this.DispMax12Diff);
            this.Controls.Add(this.MinDisparity);
            this.Controls.Add(this.SpeckleWindowsSize);
            this.Controls.Add(this.PreFilterCap);
            this.Controls.Add(this.P1);
            this.Controls.Add(this.BlockSize);
            this.Controls.Add(this.Disparity);
            this.Name = "StereoSGBMForm";
            this.Text = "StereoSGBMForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StereoSGBMForm_FormClosing);
            this.Mode.ResumeLayout(false);
            this.Mode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BlockSize;
        private System.Windows.Forms.Label Disparity;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label P1;
        private System.Windows.Forms.Label PreFilterCap;
        private System.Windows.Forms.Label SpeckleWindowsSize;
        private System.Windows.Forms.Label MinDisparity;
        private System.Windows.Forms.Label DispMax12Diff;
        private System.Windows.Forms.Label P2;
        private System.Windows.Forms.Label UniquenessRatio;
        private System.Windows.Forms.Label SpeckleRange;
        private System.Windows.Forms.RadioButton ModeSGBM;
        private System.Windows.Forms.RadioButton ModeHH;
        private System.Windows.Forms.GroupBox Mode;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}