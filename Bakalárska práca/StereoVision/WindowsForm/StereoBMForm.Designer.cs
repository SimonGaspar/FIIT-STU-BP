namespace Bakalárska_práca.StereoVision.WindowsForm
{
    partial class StereoBMForm
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
            this.DisparityTrackBar = new System.Windows.Forms.TrackBar();
            this.Disparity = new System.Windows.Forms.Label();
            this.BlockSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.BlockSize = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DisparityTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlockSizeTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // DisparityTrackBar
            // 
            this.DisparityTrackBar.LargeChange = 1;
            this.DisparityTrackBar.Location = new System.Drawing.Point(9, 24);
            this.DisparityTrackBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DisparityTrackBar.Maximum = 16;
            this.DisparityTrackBar.Name = "DisparityTrackBar";
            this.DisparityTrackBar.Size = new System.Drawing.Size(270, 45);
            this.DisparityTrackBar.TabIndex = 0;
            //ACTIVATEFORFULLFORM
            //this.DisparityTrackBar.Value = _stereoBlockMatching.model.Disparity / 16;
            this.DisparityTrackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChangedMultiple16);
            // 
            // Disparity
            // 
            this.Disparity.AutoSize = true;
            this.Disparity.Location = new System.Drawing.Point(9, 7);
            this.Disparity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Disparity.Name = "Disparity";
            this.Disparity.Size = new System.Drawing.Size(47, 13);
            this.Disparity.TabIndex = 1;
            this.Disparity.Text = "Disparity";
            // 
            // BlockSizeTrackBar
            // 
            this.BlockSizeTrackBar.Location = new System.Drawing.Point(9, 74);
            this.BlockSizeTrackBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BlockSizeTrackBar.Maximum = 51;
            this.BlockSizeTrackBar.Name = "BlockSizeTrackBar";
            this.BlockSizeTrackBar.Size = new System.Drawing.Size(268, 45);
            this.BlockSizeTrackBar.TabIndex = 2;
            //ACTIVATEFORFULLFORM
            //this.BlockSizeTrackBar.Value = _stereoBlockMatching.model.BlockSize;
            this.BlockSizeTrackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // BlockSize
            // 
            this.BlockSize.AutoSize = true;
            this.BlockSize.Location = new System.Drawing.Point(9, 55);
            this.BlockSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BlockSize.Name = "BlockSize";
            this.BlockSize.Size = new System.Drawing.Size(55, 13);
            this.BlockSize.TabIndex = 3;
            this.BlockSize.Text = "Block size";
            // 
            // StereoBMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 121);
            this.Controls.Add(this.BlockSize);
            this.Controls.Add(this.BlockSizeTrackBar);
            this.Controls.Add(this.Disparity);
            this.Controls.Add(this.DisparityTrackBar);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "StereoBMForm";
            //ACTIVATEFORFULLFORM
            //this.Text = _stereoBlockMatching.GetType().ToString();
            ((System.ComponentModel.ISupportInitialize)(this.DisparityTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlockSizeTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar DisparityTrackBar;
        private System.Windows.Forms.Label Disparity;
        private System.Windows.Forms.TrackBar BlockSizeTrackBar;
        private System.Windows.Forms.Label BlockSize;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}