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
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.Disparity = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.BlockSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 29);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(360, 56);
            this.trackBar1.TabIndex = 0;
            // 
            // Disparity
            // 
            this.Disparity.AutoSize = true;
            this.Disparity.Location = new System.Drawing.Point(12, 9);
            this.Disparity.Name = "Disparity";
            this.Disparity.Size = new System.Drawing.Size(63, 17);
            this.Disparity.TabIndex = 1;
            this.Disparity.Text = "Disparity";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(12, 91);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(357, 56);
            this.trackBar2.TabIndex = 2;
            // 
            // BlockSize
            // 
            this.BlockSize.AutoSize = true;
            this.BlockSize.Location = new System.Drawing.Point(12, 68);
            this.BlockSize.Name = "BlockSize";
            this.BlockSize.Size = new System.Drawing.Size(71, 17);
            this.BlockSize.TabIndex = 3;
            this.BlockSize.Text = "Block size";
            // 
            // StereoBM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 149);
            this.Controls.Add(this.BlockSize);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.Disparity);
            this.Controls.Add(this.trackBar1);
            this.Name = "StereoBM";
            this.Text = "StereoBM";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label Disparity;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label BlockSize;
    }
}