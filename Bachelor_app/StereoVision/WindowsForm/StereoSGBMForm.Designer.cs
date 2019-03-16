namespace Bakalárska_práca.StereoVision.WindowsForm
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
            this.BlockSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.Disparity = new System.Windows.Forms.Label();
            this.DisparityTrackBar = new System.Windows.Forms.TrackBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.P1 = new System.Windows.Forms.Label();
            this.P1TrackBar = new System.Windows.Forms.TrackBar();
            this.PreFilterCap = new System.Windows.Forms.Label();
            this.PreFilterCapTrackBar = new System.Windows.Forms.TrackBar();
            this.SpeckleWindowsSize = new System.Windows.Forms.Label();
            this.SpeckleWindowsSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.MinDisparity = new System.Windows.Forms.Label();
            this.MinDisparityTrackBar = new System.Windows.Forms.TrackBar();
            this.DispMax12Diff = new System.Windows.Forms.Label();
            this.DispMax12DiffTrackBar = new System.Windows.Forms.TrackBar();
            this.P2 = new System.Windows.Forms.Label();
            this.P2TrackBar = new System.Windows.Forms.TrackBar();
            this.UniquenessRatio = new System.Windows.Forms.Label();
            this.UniquenessRatioTrackBar = new System.Windows.Forms.TrackBar();
            this.SpeckleRange = new System.Windows.Forms.Label();
            this.SpeckleRangeTrackBar = new System.Windows.Forms.TrackBar();
            this.ModeSGBM = new System.Windows.Forms.RadioButton();
            this.ModeHH = new System.Windows.Forms.RadioButton();
            this.Mode = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.BlockSizeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisparityTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.P1TrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PreFilterCapTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpeckleWindowsSizeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinDisparityTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DispMax12DiffTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.P2TrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UniquenessRatioTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpeckleRangeTrackBar)).BeginInit();
            this.Mode.SuspendLayout();
            this.SuspendLayout();
            // 
            // BlockSize
            // 
            this.BlockSize.AutoSize = true;
            this.BlockSize.Location = new System.Drawing.Point(11, 57);
            this.BlockSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BlockSize.Name = "BlockSize";
            this.BlockSize.Size = new System.Drawing.Size(55, 13);
            this.BlockSize.TabIndex = 7;
            this.BlockSize.Text = "Block size";
            // 
            // BlockSizeTrackBar
            // 
            this.BlockSizeTrackBar.LargeChange = 1;
            this.BlockSizeTrackBar.Location = new System.Drawing.Point(11, 76);
            this.BlockSizeTrackBar.Margin = new System.Windows.Forms.Padding(2);
            this.BlockSizeTrackBar.Maximum = 25;
            this.BlockSizeTrackBar.Name = "BlockSizeTrackBar";
            this.BlockSizeTrackBar.Size = new System.Drawing.Size(268, 45);
            this.BlockSizeTrackBar.TabIndex = 6;
            this.BlockSizeTrackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChangedOdd);
            // 
            // Disparity
            // 
            this.Disparity.AutoSize = true;
            this.Disparity.Location = new System.Drawing.Point(11, 9);
            this.Disparity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Disparity.Name = "Disparity";
            this.Disparity.Size = new System.Drawing.Size(47, 13);
            this.Disparity.TabIndex = 5;
            this.Disparity.Text = "Disparity";
            // 
            // DisparityTrackBar
            // 
            this.DisparityTrackBar.LargeChange = 1;
            this.DisparityTrackBar.Location = new System.Drawing.Point(11, 26);
            this.DisparityTrackBar.Margin = new System.Windows.Forms.Padding(2);
            this.DisparityTrackBar.Maximum = 16;
            this.DisparityTrackBar.Name = "DisparityTrackBar";
            this.DisparityTrackBar.Size = new System.Drawing.Size(270, 45);
            this.DisparityTrackBar.TabIndex = 4;
            this.DisparityTrackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChangedMultiple16);
            // 
            // P1
            // 
            this.P1.AutoSize = true;
            this.P1.Location = new System.Drawing.Point(11, 108);
            this.P1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.P1.Name = "P1";
            this.P1.Size = new System.Drawing.Size(20, 13);
            this.P1.TabIndex = 9;
            this.P1.Text = "P1";
            // 
            // P1TrackBar
            // 
            this.P1TrackBar.LargeChange = 1;
            this.P1TrackBar.Location = new System.Drawing.Point(11, 127);
            this.P1TrackBar.Margin = new System.Windows.Forms.Padding(2);
            this.P1TrackBar.Maximum = 25;
            this.P1TrackBar.Name = "P1TrackBar";
            this.P1TrackBar.Size = new System.Drawing.Size(268, 45);
            this.P1TrackBar.TabIndex = 8;
            // 
            // PreFilterCap
            // 
            this.PreFilterCap.AutoSize = true;
            this.PreFilterCap.Location = new System.Drawing.Point(11, 157);
            this.PreFilterCap.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PreFilterCap.Name = "PreFilterCap";
            this.PreFilterCap.Size = new System.Drawing.Size(50, 13);
            this.PreFilterCap.TabIndex = 11;
            this.PreFilterCap.Text = "Filter cap";
            // 
            // PreFilterCapTrackBar
            // 
            this.PreFilterCapTrackBar.LargeChange = 1;
            this.PreFilterCapTrackBar.Location = new System.Drawing.Point(11, 176);
            this.PreFilterCapTrackBar.Margin = new System.Windows.Forms.Padding(2);
            this.PreFilterCapTrackBar.Maximum = 25;
            this.PreFilterCapTrackBar.Name = "PreFilterCapTrackBar";
            this.PreFilterCapTrackBar.Size = new System.Drawing.Size(268, 45);
            this.PreFilterCapTrackBar.TabIndex = 10;
            // 
            // SpeckleWindowsSize
            // 
            this.SpeckleWindowsSize.AutoSize = true;
            this.SpeckleWindowsSize.Location = new System.Drawing.Point(11, 206);
            this.SpeckleWindowsSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SpeckleWindowsSize.Name = "SpeckleWindowsSize";
            this.SpeckleWindowsSize.Size = new System.Drawing.Size(111, 13);
            this.SpeckleWindowsSize.TabIndex = 13;
            this.SpeckleWindowsSize.Text = "Speckle windows size";
            // 
            // SpeckleWindowsSizeTrackBar
            // 
            this.SpeckleWindowsSizeTrackBar.LargeChange = 1;
            this.SpeckleWindowsSizeTrackBar.Location = new System.Drawing.Point(11, 225);
            this.SpeckleWindowsSizeTrackBar.Margin = new System.Windows.Forms.Padding(2);
            this.SpeckleWindowsSizeTrackBar.Maximum = 25;
            this.SpeckleWindowsSizeTrackBar.Name = "SpeckleWindowsSizeTrackBar";
            this.SpeckleWindowsSizeTrackBar.Size = new System.Drawing.Size(268, 45);
            this.SpeckleWindowsSizeTrackBar.TabIndex = 12;
            // 
            // MinDisparity
            // 
            this.MinDisparity.AutoSize = true;
            this.MinDisparity.Location = new System.Drawing.Point(285, 6);
            this.MinDisparity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MinDisparity.Name = "MinDisparity";
            this.MinDisparity.Size = new System.Drawing.Size(76, 13);
            this.MinDisparity.TabIndex = 15;
            this.MinDisparity.Text = "Minus disparity";
            // 
            // MinDisparityTrackBar
            // 
            this.MinDisparityTrackBar.LargeChange = 1;
            this.MinDisparityTrackBar.Location = new System.Drawing.Point(285, 25);
            this.MinDisparityTrackBar.Margin = new System.Windows.Forms.Padding(2);
            this.MinDisparityTrackBar.Maximum = 25;
            this.MinDisparityTrackBar.Name = "MinDisparityTrackBar";
            this.MinDisparityTrackBar.Size = new System.Drawing.Size(268, 45);
            this.MinDisparityTrackBar.TabIndex = 14;
            // 
            // DispMax12Diff
            // 
            this.DispMax12Diff.AutoSize = true;
            this.DispMax12Diff.Location = new System.Drawing.Point(283, 55);
            this.DispMax12Diff.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DispMax12Diff.Name = "DispMax12Diff";
            this.DispMax12Diff.Size = new System.Drawing.Size(97, 13);
            this.DispMax12Diff.TabIndex = 17;
            this.DispMax12Diff.Text = "Disparity difference";
            // 
            // DispMax12DiffTrackBar
            // 
            this.DispMax12DiffTrackBar.LargeChange = 1;
            this.DispMax12DiffTrackBar.Location = new System.Drawing.Point(283, 74);
            this.DispMax12DiffTrackBar.Margin = new System.Windows.Forms.Padding(2);
            this.DispMax12DiffTrackBar.Maximum = 25;
            this.DispMax12DiffTrackBar.Name = "DispMax12DiffTrackBar";
            this.DispMax12DiffTrackBar.Size = new System.Drawing.Size(268, 45);
            this.DispMax12DiffTrackBar.TabIndex = 16;
            // 
            // P2
            // 
            this.P2.AutoSize = true;
            this.P2.Location = new System.Drawing.Point(283, 108);
            this.P2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.P2.Name = "P2";
            this.P2.Size = new System.Drawing.Size(20, 13);
            this.P2.TabIndex = 19;
            this.P2.Text = "P2";
            // 
            // P2TrackBar
            // 
            this.P2TrackBar.LargeChange = 1;
            this.P2TrackBar.Location = new System.Drawing.Point(283, 127);
            this.P2TrackBar.Margin = new System.Windows.Forms.Padding(2);
            this.P2TrackBar.Maximum = 25;
            this.P2TrackBar.Name = "P2TrackBar";
            this.P2TrackBar.Size = new System.Drawing.Size(268, 45);
            this.P2TrackBar.TabIndex = 18;
            // 
            // UniquenessRatio
            // 
            this.UniquenessRatio.AutoSize = true;
            this.UniquenessRatio.Location = new System.Drawing.Point(285, 157);
            this.UniquenessRatio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UniquenessRatio.Name = "UniquenessRatio";
            this.UniquenessRatio.Size = new System.Drawing.Size(86, 13);
            this.UniquenessRatio.TabIndex = 21;
            this.UniquenessRatio.Text = "Uniqueness ratio";
            // 
            // UniquenessRatioTrackBar
            // 
            this.UniquenessRatioTrackBar.LargeChange = 1;
            this.UniquenessRatioTrackBar.Location = new System.Drawing.Point(285, 176);
            this.UniquenessRatioTrackBar.Margin = new System.Windows.Forms.Padding(2);
            this.UniquenessRatioTrackBar.Maximum = 25;
            this.UniquenessRatioTrackBar.Name = "UniquenessRatioTrackBar";
            this.UniquenessRatioTrackBar.Size = new System.Drawing.Size(268, 45);
            this.UniquenessRatioTrackBar.TabIndex = 20;
            // 
            // SpeckleRange
            // 
            this.SpeckleRange.AutoSize = true;
            this.SpeckleRange.Location = new System.Drawing.Point(285, 204);
            this.SpeckleRange.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SpeckleRange.Name = "SpeckleRange";
            this.SpeckleRange.Size = new System.Drawing.Size(76, 13);
            this.SpeckleRange.TabIndex = 23;
            this.SpeckleRange.Text = "Speckle range";
            // 
            // SpeckleRangeTrackBar
            // 
            this.SpeckleRangeTrackBar.LargeChange = 1;
            this.SpeckleRangeTrackBar.Location = new System.Drawing.Point(285, 223);
            this.SpeckleRangeTrackBar.Margin = new System.Windows.Forms.Padding(2);
            this.SpeckleRangeTrackBar.Maximum = 25;
            this.SpeckleRangeTrackBar.Name = "SpeckleRangeTrackBar";
            this.SpeckleRangeTrackBar.Size = new System.Drawing.Size(268, 45);
            this.SpeckleRangeTrackBar.TabIndex = 22;
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
            this.Mode.Location = new System.Drawing.Point(14, 262);
            this.Mode.Name = "Mode";
            this.Mode.Size = new System.Drawing.Size(533, 46);
            this.Mode.TabIndex = 26;
            this.Mode.TabStop = false;
            this.Mode.Text = "Mode";
            // 
            // StereoSGBMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 315);
            this.Controls.Add(this.Mode);
            this.Controls.Add(this.SpeckleRange);
            this.Controls.Add(this.SpeckleRangeTrackBar);
            this.Controls.Add(this.UniquenessRatio);
            this.Controls.Add(this.UniquenessRatioTrackBar);
            this.Controls.Add(this.P2);
            this.Controls.Add(this.P2TrackBar);
            this.Controls.Add(this.DispMax12Diff);
            this.Controls.Add(this.DispMax12DiffTrackBar);
            this.Controls.Add(this.MinDisparity);
            this.Controls.Add(this.MinDisparityTrackBar);
            this.Controls.Add(this.SpeckleWindowsSize);
            this.Controls.Add(this.SpeckleWindowsSizeTrackBar);
            this.Controls.Add(this.PreFilterCap);
            this.Controls.Add(this.PreFilterCapTrackBar);
            this.Controls.Add(this.P1);
            this.Controls.Add(this.P1TrackBar);
            this.Controls.Add(this.BlockSize);
            this.Controls.Add(this.BlockSizeTrackBar);
            this.Controls.Add(this.Disparity);
            this.Controls.Add(this.DisparityTrackBar);
            this.Name = "StereoSGBMForm";
            this.Text = "StereoSGBMForm";
            ((System.ComponentModel.ISupportInitialize)(this.BlockSizeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisparityTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.P1TrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PreFilterCapTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpeckleWindowsSizeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinDisparityTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DispMax12DiffTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.P2TrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UniquenessRatioTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpeckleRangeTrackBar)).EndInit();
            this.Mode.ResumeLayout(false);
            this.Mode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BlockSize;
        private System.Windows.Forms.TrackBar BlockSizeTrackBar;
        private System.Windows.Forms.Label Disparity;
        private System.Windows.Forms.TrackBar DisparityTrackBar;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label P1;
        private System.Windows.Forms.TrackBar P1TrackBar;
        private System.Windows.Forms.Label PreFilterCap;
        private System.Windows.Forms.TrackBar PreFilterCapTrackBar;
        private System.Windows.Forms.Label SpeckleWindowsSize;
        private System.Windows.Forms.TrackBar SpeckleWindowsSizeTrackBar;
        private System.Windows.Forms.Label MinDisparity;
        private System.Windows.Forms.TrackBar MinDisparityTrackBar;
        private System.Windows.Forms.Label DispMax12Diff;
        private System.Windows.Forms.TrackBar DispMax12DiffTrackBar;
        private System.Windows.Forms.Label P2;
        private System.Windows.Forms.TrackBar P2TrackBar;
        private System.Windows.Forms.Label UniquenessRatio;
        private System.Windows.Forms.TrackBar UniquenessRatioTrackBar;
        private System.Windows.Forms.Label SpeckleRange;
        private System.Windows.Forms.TrackBar SpeckleRangeTrackBar;
        private System.Windows.Forms.RadioButton ModeSGBM;
        private System.Windows.Forms.RadioButton ModeHH;
        private System.Windows.Forms.GroupBox Mode;
    }
}