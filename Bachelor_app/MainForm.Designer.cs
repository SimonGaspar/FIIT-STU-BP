using System;
using System.Linq;
using Bachelor_app.Enumerate;
using Bachelor_app.Extension;
using Bakalárska_práca.Enumerate;
using Bakalárska_práca.StereoVision;
using Bakalárska_práca.StructureFromMotion;

namespace Bakalárska_práca
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton16 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton18 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton19 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton17 = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.LeftViewBox = new Emgu.CV.UI.ImageBox();
            this.RightViewBox = new Emgu.CV.UI.ImageBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.ListViewer0 = new System.Windows.Forms.ListView();
            this.ImageList0 = new System.Windows.Forms.ImageList(this.components);
            this.ListViewer1 = new System.Windows.Forms.ListView();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ListViewer2 = new System.Windows.Forms.ListView();
            this.ImageList2 = new System.Windows.Forms.ImageList(this.components);
            this.ListViewer3 = new System.Windows.Forms.ListView();
            this.ImageList3 = new System.Windows.Forms.ImageList(this.components);
            this.ListViewer4 = new System.Windows.Forms.ListView();
            this.ImageList4 = new System.Windows.Forms.ImageList(this.components);
            this.ListViewer5 = new System.Windows.Forms.ListView();
            this.ImageList5 = new System.Windows.Forms.ImageList(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox3 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox4 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel10 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox8 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton14 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton15 = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox5 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox6 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel9 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox7 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton13 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip6 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel11 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripComboBox9 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel12 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox10 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel14 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox12 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel13 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox11 = new System.Windows.Forms.ToolStripComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LeftViewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightViewBox)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.toolStrip5.SuspendLayout();
            this.toolStrip6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip6, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.330688F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.330688F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.330022F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.01033F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.330688F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.330688F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0062F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.330688F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1264, 681);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.toolStripComboBox1,
            this.toolStripButton4,
            this.toolStripSeparator7,
            this.toolStripButton16,
            this.toolStripButton18,
            this.toolStripButton19,
            this.toolStripSeparator12,
            this.toolStripButton11,
            this.toolStripButton17});
            this.toolStrip1.Location = new System.Drawing.Point(0, 22);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(1264, 22);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(80, 22);
            this.toolStripLabel1.Text = "Stereo vision";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(61, 22);
            this.toolStripLabel2.Text = "Algorithm";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 22);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::Bachelor_app.Properties.Resources.Settings_Dave_Gandy;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(24, 20);
            this.toolStripButton4.Text = "toolStripButton4";
            this.toolStripButton4.ToolTipText = "Settings";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripButton16
            // 
            this.toolStripButton16.Image = global::Bachelor_app.Properties.Resources.CameraCalibration_Freepik;
            this.toolStripButton16.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton16.Name = "toolStripButton16";
            this.toolStripButton16.Size = new System.Drawing.Size(89, 19);
            this.toolStripButton16.Text = "Calibration";
            this.toolStripButton16.Click += new System.EventHandler(this.toolStripButton16_Click);
            // 
            // toolStripButton18
            // 
            this.toolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton18.Image = global::Bachelor_app.Properties.Resources.Download_Freepik;
            this.toolStripButton18.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton18.Name = "toolStripButton18";
            this.toolStripButton18.Size = new System.Drawing.Size(24, 19);
            this.toolStripButton18.Text = "Download camera settings";
            // 
            // toolStripButton19
            // 
            this.toolStripButton19.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton19.Image = global::Bachelor_app.Properties.Resources.Upload_Freepik;
            this.toolStripButton19.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton19.Name = "toolStripButton19";
            this.toolStripButton19.Size = new System.Drawing.Size(24, 19);
            this.toolStripButton19.Text = "Load camera settings";
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.Image = global::Bachelor_app.Properties.Resources.PlayButton_Roundicons;
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(55, 19);
            this.toolStripButton11.Text = "Run ";
            this.toolStripButton11.Click += new System.EventHandler(this.toolStripButton11_Click);
            // 
            // toolStripButton17
            // 
            this.toolStripButton17.Image = global::Bachelor_app.Properties.Resources.Stop_Freepik;
            this.toolStripButton17.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton17.Name = "toolStripButton17";
            this.toolStripButton17.Size = new System.Drawing.Size(55, 19);
            this.toolStripButton17.Text = "Stop";
            this.toolStripButton17.Click += new System.EventHandler(this.toolStripButton17_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0F));
            this.tableLayoutPanel2.Controls.Add(this.LeftViewBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.RightViewBox, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 69);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1258, 334);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // LeftViewBox
            // 
            this.LeftViewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftViewBox.Location = new System.Drawing.Point(2, 2);
            this.LeftViewBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LeftViewBox.Name = "LeftViewBox";
            this.LeftViewBox.Size = new System.Drawing.Size(625, 330);
            this.LeftViewBox.TabIndex = 2;
            this.LeftViewBox.TabStop = false;
            // 
            // RightViewBox
            // 
            this.RightViewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightViewBox.Location = new System.Drawing.Point(631, 2);
            this.RightViewBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RightViewBox.Name = "RightViewBox";
            this.RightViewBox.Size = new System.Drawing.Size(625, 330);
            this.RightViewBox.TabIndex = 2;
            this.RightViewBox.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Controls.Add(this.statusStrip1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 654);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1264, 27);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(421, 27);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(64, 22);
            this.toolStripStatusLabel1.Text = "Processing";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 21);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 7;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 420F));
            this.tableLayoutPanel4.Controls.Add(this.ListViewer0, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.ListViewer1, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.ListViewer2, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.ListViewer3, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.ListViewer4, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.ListViewer5, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.richTextBox1, 6, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 450);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1264, 204);
            this.tableLayoutPanel4.TabIndex = 4;
            this.tableLayoutPanel4.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel4_Paint);
            // 
            // ListViewer0
            // 
            this.ListViewer0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewer0.LargeImageList = this.ImageList0;
            this.ListViewer0.Location = new System.Drawing.Point(0, 0);
            this.ListViewer0.Margin = new System.Windows.Forms.Padding(0);
            this.ListViewer0.Name = "ListViewer0";
            this.ListViewer0.ShowItemToolTips = true;
            this.ListViewer0.Size = new System.Drawing.Size(841, 204);
            this.ListViewer0.SmallImageList = this.ImageList0;
            this.ListViewer0.TabIndex = 1;
            this.ListViewer0.UseCompatibleStateImageBehavior = false;
            this.ListViewer0.Visible = false;
            this.ListViewer0.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewer_ItemSelectionChanged);
            // 
            // ImageList0
            // 
            this.ImageList0.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ImageList0.ImageSize = new System.Drawing.Size(128, 72);
            this.ImageList0.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ListViewer1
            // 
            this.ListViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewer1.LargeImageList = this.ImageList1;
            this.ListViewer1.Location = new System.Drawing.Point(841, 0);
            this.ListViewer1.Margin = new System.Windows.Forms.Padding(0);
            this.ListViewer1.Name = "ListViewer1";
            this.ListViewer1.ShowItemToolTips = true;
            this.ListViewer1.Size = new System.Drawing.Size(841, 204);
            this.ListViewer1.SmallImageList = this.ImageList1;
            this.ListViewer1.TabIndex = 2;
            this.ListViewer1.UseCompatibleStateImageBehavior = false;
            this.ListViewer1.Visible = false;
            this.ListViewer1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewer_ItemSelectionChanged);
            // 
            // ImageList1
            // 
            this.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ImageList1.ImageSize = new System.Drawing.Size(128, 72);
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ListViewer2
            // 
            this.ListViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewer2.LargeImageList = this.ImageList2;
            this.ListViewer2.Location = new System.Drawing.Point(1682, 0);
            this.ListViewer2.Margin = new System.Windows.Forms.Padding(0);
            this.ListViewer2.Name = "ListViewer2";
            this.ListViewer2.ShowItemToolTips = true;
            this.ListViewer2.Size = new System.Drawing.Size(841, 204);
            this.ListViewer2.SmallImageList = this.ImageList2;
            this.ListViewer2.TabIndex = 1;
            this.ListViewer2.UseCompatibleStateImageBehavior = false;
            this.ListViewer2.Visible = false;
            this.ListViewer2.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewer_ItemSelectionChanged);
            // 
            // ImageList2
            // 
            this.ImageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ImageList2.ImageSize = new System.Drawing.Size(128, 72);
            this.ImageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ListViewer3
            // 
            this.ListViewer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewer3.LargeImageList = this.ImageList3;
            this.ListViewer3.Location = new System.Drawing.Point(2523, 0);
            this.ListViewer3.Margin = new System.Windows.Forms.Padding(0);
            this.ListViewer3.Name = "ListViewer3";
            this.ListViewer3.ShowItemToolTips = true;
            this.ListViewer3.Size = new System.Drawing.Size(841, 204);
            this.ListViewer3.SmallImageList = this.ImageList3;
            this.ListViewer3.TabIndex = 2;
            this.ListViewer3.UseCompatibleStateImageBehavior = false;
            this.ListViewer3.Visible = false;
            this.ListViewer3.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewer_ItemSelectionChanged);
            // 
            // ImageList3
            // 
            this.ImageList3.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ImageList3.ImageSize = new System.Drawing.Size(128, 72);
            this.ImageList3.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ListViewer4
            // 
            this.ListViewer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewer4.LargeImageList = this.ImageList4;
            this.ListViewer4.Location = new System.Drawing.Point(3364, 0);
            this.ListViewer4.Margin = new System.Windows.Forms.Padding(0);
            this.ListViewer4.Name = "ListViewer4";
            this.ListViewer4.ShowItemToolTips = true;
            this.ListViewer4.Size = new System.Drawing.Size(841, 204);
            this.ListViewer4.SmallImageList = this.ImageList4;
            this.ListViewer4.TabIndex = 1;
            this.ListViewer4.UseCompatibleStateImageBehavior = false;
            this.ListViewer4.Visible = false;
            this.ListViewer4.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewer_ItemSelectionChanged);
            // 
            // ImageList4
            // 
            this.ImageList4.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ImageList4.ImageSize = new System.Drawing.Size(128, 72);
            this.ImageList4.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ListViewer5
            // 
            this.ListViewer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewer5.LargeImageList = this.ImageList5;
            this.ListViewer5.Location = new System.Drawing.Point(4205, 0);
            this.ListViewer5.Margin = new System.Windows.Forms.Padding(0);
            this.ListViewer5.Name = "ListViewer5";
            this.ListViewer5.ShowItemToolTips = true;
            this.ListViewer5.Size = new System.Drawing.Size(841, 204);
            this.ListViewer5.SmallImageList = this.ImageList5;
            this.ListViewer5.TabIndex = 2;
            this.ListViewer5.UseCompatibleStateImageBehavior = false;
            this.ListViewer5.Visible = false;
            this.ListViewer5.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewer_ItemSelectionChanged);
            // 
            // ImageList5
            // 
            this.ImageList5.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ImageList5.ImageSize = new System.Drawing.Size(128, 72);
            this.ImageList5.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(5046, 0);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(420, 204);
            this.richTextBox1.TabIndex = 20;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox_TextChanged);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.toolStripSeparator3,
            this.toolStripLabel4,
            this.toolStripComboBox2,
            this.toolStripButton1,
            this.toolStripSeparator4,
            this.toolStripLabel5,
            this.toolStripComboBox3,
            this.toolStripButton2,
            this.toolStripSeparator5,
            this.toolStripLabel6,
            this.toolStripComboBox4,
            this.toolStripButton3,
            this.toolStripSeparator9,
            this.toolStripLabel10,
            this.toolStripComboBox8,
            this.toolStripButton14,
            this.toolStripSeparator6,
            this.toolStripButton10,
            this.toolStripButton12,
            this.toolStripButton15});
            this.toolStrip2.Location = new System.Drawing.Point(0, 44);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip2.Size = new System.Drawing.Size(1264, 22);
            this.toolStrip2.TabIndex = 5;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel3.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripLabel3.Size = new System.Drawing.Size(135, 22);
            this.toolStripLabel3.Text = "Structure from Motion";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(93, 22);
            this.toolStripLabel4.Text = "Feature detector";
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox2.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(121, 22);
            this.toolStripComboBox2.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox2_SelectedIndexChanged);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Bachelor_app.Properties.Resources.Settings_Dave_Gandy;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 20);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "Settings";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(110, 22);
            this.toolStripLabel5.Text = "Keypoint descriptor";
            // 
            // toolStripComboBox3
            // 
            this.toolStripComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox3.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripComboBox3.Name = "toolStripComboBox3";
            this.toolStripComboBox3.Size = new System.Drawing.Size(121, 22);
            this.toolStripComboBox3.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox3_SelectedIndexChanged);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Bachelor_app.Properties.Resources.Settings_Dave_Gandy;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(24, 20);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "Settings";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(108, 22);
            this.toolStripLabel6.Text = "Descriptor matcher";
            // 
            // toolStripComboBox4
            // 
            this.toolStripComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox4.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripComboBox4.Name = "toolStripComboBox4";
            this.toolStripComboBox4.Size = new System.Drawing.Size(121, 22);
            this.toolStripComboBox4.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox4_SelectedIndexChanged);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::Bachelor_app.Properties.Resources.Settings_Dave_Gandy;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(24, 20);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.ToolTipText = "Settings";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripLabel10
            // 
            this.toolStripLabel10.Name = "toolStripLabel10";
            this.toolStripLabel10.Size = new System.Drawing.Size(58, 19);
            this.toolStripLabel10.Text = "Matching";
            // 
            // toolStripComboBox8
            // 
            this.toolStripComboBox8.Name = "toolStripComboBox8";
            this.toolStripComboBox8.Size = new System.Drawing.Size(92, 22);
            this.toolStripComboBox8.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox8_SelectedIndexChanged);
            // 
            // toolStripButton14
            // 
            this.toolStripButton14.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripButton14.CheckOnClick = true;
            this.toolStripButton14.Image = global::Bachelor_app.Properties.Resources.Parallel_Freepik;
            this.toolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton14.Name = "toolStripButton14";
            this.toolStripButton14.Size = new System.Drawing.Size(69, 19);
            this.toolStripButton14.Text = "Parallel";
            this.toolStripButton14.Click += new System.EventHandler(this.toolStripButton14_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.Image = global::Bachelor_app.Properties.Resources.PlayButton_Roundicons;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(52, 19);
            this.toolStripButton10.Text = "Run";
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.Image = global::Bachelor_app.Properties.Resources.Repeat_Freepik;
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(73, 24);
            this.toolStripButton12.Text = "Resume";
            this.toolStripButton12.Click += new System.EventHandler(this.toolStripButton12_Click);
            // 
            // toolStripButton15
            // 
            this.toolStripButton15.Image = global::Bachelor_app.Properties.Resources.Stop_Freepik;
            this.toolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton15.Name = "toolStripButton15";
            this.toolStripButton15.Size = new System.Drawing.Size(55, 24);
            this.toolStripButton15.Text = "Stop";
            this.toolStripButton15.Click += new System.EventHandler(this.toolStripButton15_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.toolStrip3, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.toolStrip4, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 406);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1264, 22);
            this.tableLayoutPanel6.TabIndex = 6;
            this.tableLayoutPanel6.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel6_Paint);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip3.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel7,
            this.toolStripComboBox5});
            this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip3.Size = new System.Drawing.Size(632, 22);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel7.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(77, 22);
            this.toolStripLabel7.Text = "Left window";
            // 
            // toolStripComboBox5
            // 
            this.toolStripComboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox5.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripComboBox5.Name = "toolStripComboBox5";
            this.toolStripComboBox5.Size = new System.Drawing.Size(121, 22);
            this.toolStripComboBox5.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox5_SelectedIndexChanged);
            // 
            // toolStrip4
            // 
            this.toolStrip4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip4.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip4.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel8,
            this.toolStripComboBox6});
            this.toolStrip4.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip4.Location = new System.Drawing.Point(632, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip4.Size = new System.Drawing.Size(632, 22);
            this.toolStrip4.TabIndex = 1;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel8.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(84, 22);
            this.toolStripLabel8.Text = "Right window";
            // 
            // toolStripComboBox6
            // 
            this.toolStripComboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox6.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripComboBox6.Name = "toolStripComboBox6";
            this.toolStripComboBox6.Size = new System.Drawing.Size(121, 22);
            this.toolStripComboBox6.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox6_SelectedIndexChanged);
            // 
            // toolStrip5
            // 
            this.toolStrip5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip5.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip5.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel9,
            this.toolStripComboBox7,
            this.toolStripSeparator8,
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripButton8,
            this.toolStripButton9,
            this.toolStripSeparator2,
            this.toolStripButton13});
            this.toolStrip5.Location = new System.Drawing.Point(0, 428);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip5.Size = new System.Drawing.Size(1264, 22);
            this.toolStrip5.TabIndex = 7;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // toolStripLabel9
            // 
            this.toolStripLabel9.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel9.Name = "toolStripLabel9";
            this.toolStripLabel9.Size = new System.Drawing.Size(76, 22);
            this.toolStripLabel9.Text = "Items in view";
            // 
            // toolStripComboBox7
            // 
            this.toolStripComboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox7.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripComboBox7.Name = "toolStripComboBox7";
            this.toolStripComboBox7.Size = new System.Drawing.Size(121, 22);
            this.toolStripComboBox7.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox7_SelectedIndexChanged);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::Bachelor_app.Properties.Resources.AddImage_Swifticon;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(24, 20);
            this.toolStripButton5.Text = "toolStripButton5";
            this.toolStripButton5.ToolTipText = "Add image";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = global::Bachelor_app.Properties.Resources.RemoveImage_Srip;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(24, 20);
            this.toolStripButton6.Text = "toolStripButton6";
            this.toolStripButton6.ToolTipText = "Remove image";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = global::Bachelor_app.Properties.Resources.LeftArrow_Lyolya;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(24, 20);
            this.toolStripButton7.Text = "toolStripButton7";
            this.toolStripButton7.ToolTipText = "Switch left";
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = global::Bachelor_app.Properties.Resources.RightArrow_Lyolya;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(24, 20);
            this.toolStripButton8.Text = "toolStripButton8";
            this.toolStripButton8.ToolTipText = "Switch right";
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = global::Bachelor_app.Properties.Resources.Delete_Alfredo_Hernandez;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(24, 20);
            this.toolStripButton9.Text = "toolStripButton9";
            this.toolStripButton9.ToolTipText = "Clear";
            this.toolStripButton9.Click += new System.EventHandler(this.toolStripButton9_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripButton13
            // 
            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton13.Image = global::Bachelor_app.Properties.Resources.ErasingFile_Freepik;
            this.toolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton13.Name = "toolStripButton13";
            this.toolStripButton13.Size = new System.Drawing.Size(24, 19);
            this.toolStripButton13.Text = "toolStripButton13";
            this.toolStripButton13.ToolTipText = "Clear console";
            this.toolStripButton13.Click += new System.EventHandler(this.toolStripButton13_Click);
            // 
            // toolStrip6
            // 
            this.toolStrip6.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip6.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel11,
            this.toolStripSeparator10,
            this.toolStripTextBox1,
            this.toolStripComboBox9,
            this.toolStripLabel12,
            this.toolStripComboBox10,
            this.toolStripSeparator11,
            this.toolStripLabel14,
            this.toolStripComboBox12,
            this.toolStripSeparator13,
            this.toolStripLabel13,
            this.toolStripComboBox11});
            this.toolStrip6.Location = new System.Drawing.Point(0, 0);
            this.toolStrip6.Name = "toolStrip6";
            this.toolStrip6.Size = new System.Drawing.Size(1264, 22);
            this.toolStrip6.TabIndex = 8;
            this.toolStrip6.Text = "toolStrip6";
            // 
            // toolStripLabel11
            // 
            this.toolStripLabel11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel11.Name = "toolStripLabel11";
            this.toolStripLabel11.Size = new System.Drawing.Size(39, 19);
            this.toolStripLabel11.Text = "Menu";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(76, 22);
            this.toolStripTextBox1.Text = "Left camera";
            // 
            // toolStripComboBox9
            // 
            this.toolStripComboBox9.Name = "toolStripComboBox9";
            this.toolStripComboBox9.Size = new System.Drawing.Size(92, 22);
            this.toolStripComboBox9.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox9_SelectedIndexChanged);
            // 
            // toolStripLabel12
            // 
            this.toolStripLabel12.Name = "toolStripLabel12";
            this.toolStripLabel12.Size = new System.Drawing.Size(77, 19);
            this.toolStripLabel12.Text = "Right camera";
            // 
            // toolStripComboBox10
            // 
            this.toolStripComboBox10.Name = "toolStripComboBox10";
            this.toolStripComboBox10.Size = new System.Drawing.Size(92, 22);
            this.toolStripComboBox10.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox10_SelectedIndexChanged);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripLabel14
            // 
            this.toolStripLabel14.Name = "toolStripLabel14";
            this.toolStripLabel14.Size = new System.Drawing.Size(63, 19);
            this.toolStripLabel14.Text = "Resolution";
            // 
            // toolStripComboBox12
            // 
            this.toolStripComboBox12.Name = "toolStripComboBox12";
            this.toolStripComboBox12.Size = new System.Drawing.Size(92, 22);
            this.toolStripComboBox12.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox12_SelectedIndexChanged);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 22);
            // 
            // toolStripLabel13
            // 
            this.toolStripLabel13.Name = "toolStripLabel13";
            this.toolStripLabel13.Size = new System.Drawing.Size(35, 19);
            this.toolStripLabel13.Text = "Input";
            // 
            // toolStripComboBox11
            // 
            this.toolStripComboBox11.Name = "toolStripComboBox11";
            this.toolStripComboBox11.Size = new System.Drawing.Size(92, 22);
            this.toolStripComboBox11.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox11_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(1280, 713);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LeftViewBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightViewBox)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            this.toolStrip6.ResumeLayout(false);
            this.toolStrip6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion



        private void InitializeStringForComponents()
        {
            this.toolStripComboBox5.Items.AddRange(Enum.GetValues(typeof(EDisplayItem)).Cast<EDisplayItem>().Select(x => x.Display()).ToArray());
            //this.toolStripComboBox5.SelectedItem = EDisplayItem.ListViewer.Display();

            this.toolStripComboBox6.Items.AddRange(Enum.GetValues(typeof(EDisplayItem)).Cast<EDisplayItem>().Select(x => x.Display()).ToArray());
            //this.toolStripComboBox6.SelectedItem = EDisplayItem.ListViewer.Display();

            this.toolStripComboBox7.Items.AddRange(Enum.GetValues(typeof(EListViewGroup)).Cast<EListViewGroup>().Select(x => x.Display()).ToArray());
            //this.toolStripComboBox7.SelectedItem = EListViewGroup.BasicStack.Display();

            this.toolStripComboBox1.Items.AddRange(Enum.GetValues(typeof(EStereoCorrespondenceAlgorithm)).Cast<EStereoCorrespondenceAlgorithm>().Select(x => x.Display()).ToArray());
            //this.toolStripComboBox1.SelectedItem = EStereoCorrespondenceAlgorithm.StereoBM.Display();

            this.toolStripComboBox2.Items.AddRange(Enum.GetValues(typeof(EFeaturesDetector)).Cast<EFeaturesDetector>().Select(x => x.Display()).ToArray());
            //this.toolStripComboBox2.SelectedItem = EFeaturesDetector.ORB.Display();

            this.toolStripComboBox3.Items.AddRange(Enum.GetValues(typeof(EFeaturesDescriptor)).Cast<EFeaturesDescriptor>().Select(x => x.Display()).ToArray());
            //this.toolStripComboBox3.SelectedItem = EFeaturesDescriptor.ORB.Display();

            this.toolStripComboBox4.Items.AddRange(Enum.GetValues(typeof(EFeaturesMatcher)).Cast<EFeaturesMatcher>().Select(x => x.Display()).ToArray());
            //this.toolStripComboBox4.SelectedItem = EFeaturesMatcher.BruteForce.Display();

            this.toolStripComboBox8.Items.AddRange(Enum.GetValues(typeof(EMatchingType)).Cast<EMatchingType>().Select(x => x.Display()).ToArray());
            //this.toolStripComboBox8.SelectedItem = EEMatchingType.TwoPrevious.Display();

            this.toolStripComboBox11.Items.AddRange(Enum.GetValues(typeof(EInput)).Cast<EInput>().Select(x => x.Display()).ToArray());
            //this.toolStripComboBox11.SelectedItem = EInput.ListView.Display();

            this.toolStripComboBox10.Items.AddRange(cameraManager.ListCamerasData.Select(x => x.Value).ToArray());
            //this.toolStripComboBox10.SelectedItem = EInput.ListView.Display();

            this.toolStripComboBox9.Items.AddRange(cameraManager.ListCamerasData.Select(x => x.Value).ToArray());
            //this.toolStripComboBox9.SelectedItem = EInput.ListView.Display();

            this.toolStripComboBox12.Items.AddRange(Enum.GetValues(typeof(ECameraResolution)).Cast<ECameraResolution>().Select(x => x.Display()).ToArray());
            //this.toolStripComboBox12.SelectedItem = ECameraResolution.FullHD.Display();


            this.renderWindowControl1 = new Kitware.VTK.RenderWindowControl();
            this.renderWindowControl2 = new Kitware.VTK.RenderWindowControl();

            this.tableLayoutPanel2.Controls.Add(this.renderWindowControl1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.renderWindowControl2, 3, 0);

            ListViews.Add(ListViewer0);
            ListViews.Add(ListViewer1);
            ListViews.Add(ListViewer2);
            ListViews.Add(ListViewer3);
            ListViews.Add(ListViewer4);
            ListViews.Add(ListViewer5);

            ImageList.Add(ImageList0);
            ImageList.Add(ImageList1);
            ImageList.Add(ImageList2);
            ImageList.Add(ImageList3);
            ImageList.Add(ImageList4);
            ImageList.Add(ImageList5);
            #region ListViewer
            //// 
            //// ListViewer0
            //// 
            //this.ListViewer0.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.ListViewer0.LargeImageList = this.ImageList0;
            //this.ListViewer0.Location = new System.Drawing.Point(0, 0);
            //this.ListViewer0.Margin = new System.Windows.Forms.Padding(0);
            //this.ListViewer0.Name = "ListViewer0";
            //this.ListViewer0.ShowItemToolTips = true;
            //this.ListViewer0.Size = new System.Drawing.Size(841, 204);
            //this.ListViewer0.SmallImageList = this.ImageList0;
            //this.ListViewer0.TabIndex = 1;
            //this.ListViewer0.UseCompatibleStateImageBehavior = false;
            //this.ListViewer0.Visible = false;
            //this.ListViewer0.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewer_ItemSelectionChanged);
            //// 
            //// ImageList0
            //// 
            //this.ImageList0.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            //this.ImageList0.ImageSize = new System.Drawing.Size(128, 72);
            //this.ImageList0.TransparentColor = System.Drawing.Color.Transparent;
            //// 
            //// ListViewer1
            //// 
            //this.ListViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.ListViewer1.LargeImageList = this.ImageList1;
            //this.ListViewer1.Location = new System.Drawing.Point(841, 0);
            //this.ListViewer1.Margin = new System.Windows.Forms.Padding(0);
            //this.ListViewer1.Name = "ListViewer1";
            //this.ListViewer1.ShowItemToolTips = true;
            //this.ListViewer1.Size = new System.Drawing.Size(841, 204);
            //this.ListViewer1.SmallImageList = this.ImageList1;
            //this.ListViewer1.TabIndex = 2;
            //this.ListViewer1.UseCompatibleStateImageBehavior = false;
            //this.ListViewer1.Visible = false;
            //this.ListViewer1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewer_ItemSelectionChanged);
            //// 
            //// ImageList1
            //// 
            //this.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            //this.ImageList1.ImageSize = new System.Drawing.Size(128, 72);
            //this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            //// 
            //// ListViewer2
            //// 
            //this.ListViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.ListViewer2.LargeImageList = this.ImageList2;
            //this.ListViewer2.Location = new System.Drawing.Point(1682, 0);
            //this.ListViewer2.Margin = new System.Windows.Forms.Padding(0);
            //this.ListViewer2.Name = "ListViewer2";
            //this.ListViewer2.ShowItemToolTips = true;
            //this.ListViewer2.Size = new System.Drawing.Size(841, 204);
            //this.ListViewer2.SmallImageList = this.ImageList2;
            //this.ListViewer2.TabIndex = 1;
            //this.ListViewer2.UseCompatibleStateImageBehavior = false;
            //this.ListViewer2.Visible = false;
            //this.ListViewer2.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewer_ItemSelectionChanged);
            //// 
            //// ImageList2
            //// 
            //this.ImageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            //this.ImageList2.ImageSize = new System.Drawing.Size(128, 72);
            //this.ImageList2.TransparentColor = System.Drawing.Color.Transparent;
            //// 
            //// ListViewer3
            //// 
            //this.ListViewer3.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.ListViewer3.LargeImageList = this.ImageList3;
            //this.ListViewer3.Location = new System.Drawing.Point(1803, 0);
            //this.ListViewer3.Margin = new System.Windows.Forms.Padding(0);
            //this.ListViewer3.Name = "ListViewer3";
            //this.ListViewer3.ShowItemToolTips = true;
            //this.ListViewer3.Size = new System.Drawing.Size(841, 204);
            //this.ListViewer3.SmallImageList = this.ImageList3;
            //this.ListViewer3.TabIndex = 2;
            //this.ListViewer3.UseCompatibleStateImageBehavior = false;
            //this.ListViewer3.Visible = false;
            //this.ListViewer3.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewer_ItemSelectionChanged);
            //// 
            //// ImageList3
            //// 
            //this.ImageList3.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            //this.ImageList3.ImageSize = new System.Drawing.Size(128, 72);
            //this.ImageList3.TransparentColor = System.Drawing.Color.Transparent;
            //// 
            //// ListViewer4
            //// 
            //this.ListViewer4.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.ListViewer4.LargeImageList = this.ImageList4;
            //this.ListViewer4.Location = new System.Drawing.Point(1924, 0);
            //this.ListViewer4.Margin = new System.Windows.Forms.Padding(0);
            //this.ListViewer4.Name = "ListViewer4";
            //this.ListViewer4.ShowItemToolTips = true;
            //this.ListViewer4.Size = new System.Drawing.Size(841, 204);
            //this.ListViewer4.SmallImageList = this.ImageList4;
            //this.ListViewer4.TabIndex = 1;
            //this.ListViewer4.UseCompatibleStateImageBehavior = false;
            //this.ListViewer4.Visible = false;
            //this.ListViewer4.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewer_ItemSelectionChanged);
            //// 
            //// ImageList4
            //// 
            //this.ImageList4.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            //this.ImageList4.ImageSize = new System.Drawing.Size(128, 72);
            //this.ImageList4.TransparentColor = System.Drawing.Color.Transparent;
            //// 
            //// ListViewer5
            //// 
            //this.ListViewer5.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.ListViewer5.LargeImageList = this.ImageList5;
            //this.ListViewer5.Location = new System.Drawing.Point(2045, 0);
            //this.ListViewer5.Margin = new System.Windows.Forms.Padding(0);
            //this.ListViewer5.Name = "ListViewer5";
            //this.ListViewer5.ShowItemToolTips = true;
            //this.ListViewer5.Size = new System.Drawing.Size(841, 204);
            //this.ListViewer5.SmallImageList = this.ImageList5;
            //this.ListViewer5.TabIndex = 2;
            //this.ListViewer5.UseCompatibleStateImageBehavior = false;
            //this.ListViewer5.Visible = false;
            //this.ListViewer5.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewer_ItemSelectionChanged);
            //// 
            //// ImageList5
            //// 
            //this.ImageList5.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            //this.ImageList5.ImageSize = new System.Drawing.Size(128, 72);
            //this.ImageList5.TransparentColor = System.Drawing.Color.Transparent;
            #endregion


        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        public System.Windows.Forms.ListView ListViewer0;
        public System.Windows.Forms.ListView ListViewer1;
        public System.Windows.Forms.ListView ListViewer2;
        public System.Windows.Forms.ListView ListViewer3;
        public System.Windows.Forms.ListView ListViewer4;
        public System.Windows.Forms.ListView ListViewer5;
        public Emgu.CV.UI.ImageBox LeftViewBox;
        public Emgu.CV.UI.ImageBox RightViewBox;
        public System.Windows.Forms.ImageList ImageList0;
        public System.Windows.Forms.ImageList ImageList1;
        public System.Windows.Forms.ImageList ImageList2;
        public System.Windows.Forms.ImageList ImageList3;
        public System.Windows.Forms.ImageList ImageList4;
        public System.Windows.Forms.ImageList ImageList5;
        public Kitware.VTK.RenderWindowControl renderWindowControl1;
        public Kitware.VTK.RenderWindowControl renderWindowControl2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox3;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox4;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox5;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel8;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox6;
        private System.Windows.Forms.ToolStrip toolStrip5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel9;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStripButton toolStripButton11;
        private System.Windows.Forms.ToolStripButton toolStripButton12;
        private System.Windows.Forms.ToolStripButton toolStripButton13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripLabel toolStripLabel10;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox8;
        public System.Windows.Forms.ToolStripButton toolStripButton14;
        private System.Windows.Forms.ToolStrip toolStrip6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel11;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox9;
        private System.Windows.Forms.ToolStripLabel toolStripLabel12;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripLabel toolStripLabel13;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox11;
        private System.Windows.Forms.ToolStripButton toolStripButton15;
        private System.Windows.Forms.ToolStripButton toolStripButton16;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton toolStripButton17;
        private System.Windows.Forms.ToolStripButton toolStripButton18;
        private System.Windows.Forms.ToolStripButton toolStripButton19;
        private System.Windows.Forms.ToolStripLabel toolStripLabel14;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
    }
}

