using System;
using System.Linq;
using Bachelor_app.Extension;
using Bakalárska_práca.Enumerate;

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
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("NoGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("LeftGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("RightGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayLeftListViewerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayLeftLeftCameraMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayLeftRightCameraMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayLeftDepthMapMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointCloudToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayRight = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayRightListViewerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayRightLeftCameraMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayRightRightCameraMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayRightDepthMapMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointCloudToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.listOfImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightCameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.computeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stereoVisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stereoBMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stereoSGBMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cudaStereoBMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cudaStereoConstantSpaceBPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.structureFromMotionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detectFeaturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deskriptorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matcherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.LeftViewBox = new Emgu.CV.UI.ImageBox();
            this.RightViewBox = new Emgu.CV.UI.ImageBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.ListViewer = new System.Windows.Forms.ListView();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
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
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
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
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip5, 0, 5);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1685, 838);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem,
            this.displayToolStripMenuItem,
            this.computeToolStripMenuItem,
            this.inputToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.menuStrip1.Size = new System.Drawing.Size(1685, 27);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(62, 27);
            this.homeToolStripMenuItem.Text = "Home";
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DisplayLeft,
            this.DisplayRight,
            this.toolStripSeparator2,
            this.listOfImagesToolStripMenuItem});
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(70, 27);
            this.displayToolStripMenuItem.Text = "Display";
            // 
            // DisplayLeft
            // 
            this.DisplayLeft.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DisplayLeftListViewerMenuItem,
            this.DisplayLeftLeftCameraMenuItem,
            this.DisplayLeftRightCameraMenuItem,
            this.DisplayLeftDepthMapMenuItem,
            this.pointCloudToolStripMenuItem});
            this.DisplayLeft.Name = "DisplayLeft";
            this.DisplayLeft.Size = new System.Drawing.Size(176, 26);
            this.DisplayLeft.Text = "Left";
            // 
            // DisplayLeftListViewerMenuItem
            // 
            this.DisplayLeftListViewerMenuItem.Checked = true;
            this.DisplayLeftListViewerMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DisplayLeftListViewerMenuItem.Name = "DisplayLeftListViewerMenuItem";
            this.DisplayLeftListViewerMenuItem.Size = new System.Drawing.Size(172, 26);
            this.DisplayLeftListViewerMenuItem.Text = "List viewer";
            this.DisplayLeftListViewerMenuItem.Click += new System.EventHandler(this.DisplayToolStripMenuItem_Click);
            // 
            // DisplayLeftLeftCameraMenuItem
            // 
            this.DisplayLeftLeftCameraMenuItem.Name = "DisplayLeftLeftCameraMenuItem";
            this.DisplayLeftLeftCameraMenuItem.Size = new System.Drawing.Size(172, 26);
            this.DisplayLeftLeftCameraMenuItem.Text = "Left camera";
            this.DisplayLeftLeftCameraMenuItem.Click += new System.EventHandler(this.DisplayToolStripMenuItem_Click);
            // 
            // DisplayLeftRightCameraMenuItem
            // 
            this.DisplayLeftRightCameraMenuItem.Name = "DisplayLeftRightCameraMenuItem";
            this.DisplayLeftRightCameraMenuItem.Size = new System.Drawing.Size(172, 26);
            this.DisplayLeftRightCameraMenuItem.Text = "Right camera";
            this.DisplayLeftRightCameraMenuItem.Click += new System.EventHandler(this.DisplayToolStripMenuItem_Click);
            // 
            // DisplayLeftDepthMapMenuItem
            // 
            this.DisplayLeftDepthMapMenuItem.Name = "DisplayLeftDepthMapMenuItem";
            this.DisplayLeftDepthMapMenuItem.Size = new System.Drawing.Size(172, 26);
            this.DisplayLeftDepthMapMenuItem.Text = "Depth map";
            this.DisplayLeftDepthMapMenuItem.Click += new System.EventHandler(this.DisplayToolStripMenuItem_Click);
            // 
            // pointCloudToolStripMenuItem
            // 
            this.pointCloudToolStripMenuItem.Name = "pointCloudToolStripMenuItem";
            this.pointCloudToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.pointCloudToolStripMenuItem.Text = "Point cloud";
            this.pointCloudToolStripMenuItem.Click += new System.EventHandler(this.DisplayToolStripMenuItem_Click);
            // 
            // DisplayRight
            // 
            this.DisplayRight.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DisplayRightListViewerMenuItem,
            this.DisplayRightLeftCameraMenuItem,
            this.DisplayRightRightCameraMenuItem,
            this.DisplayRightDepthMapMenuItem,
            this.pointCloudToolStripMenuItem1});
            this.DisplayRight.Name = "DisplayRight";
            this.DisplayRight.Size = new System.Drawing.Size(176, 26);
            this.DisplayRight.Text = "Right";
            // 
            // DisplayRightListViewerMenuItem
            // 
            this.DisplayRightListViewerMenuItem.Checked = true;
            this.DisplayRightListViewerMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DisplayRightListViewerMenuItem.Name = "DisplayRightListViewerMenuItem";
            this.DisplayRightListViewerMenuItem.Size = new System.Drawing.Size(172, 26);
            this.DisplayRightListViewerMenuItem.Text = "List viewer";
            this.DisplayRightListViewerMenuItem.Click += new System.EventHandler(this.DisplayToolStripMenuItem_Click);
            // 
            // DisplayRightLeftCameraMenuItem
            // 
            this.DisplayRightLeftCameraMenuItem.Name = "DisplayRightLeftCameraMenuItem";
            this.DisplayRightLeftCameraMenuItem.Size = new System.Drawing.Size(172, 26);
            this.DisplayRightLeftCameraMenuItem.Text = "Left camera";
            this.DisplayRightLeftCameraMenuItem.Click += new System.EventHandler(this.DisplayToolStripMenuItem_Click);
            // 
            // DisplayRightRightCameraMenuItem
            // 
            this.DisplayRightRightCameraMenuItem.Name = "DisplayRightRightCameraMenuItem";
            this.DisplayRightRightCameraMenuItem.Size = new System.Drawing.Size(172, 26);
            this.DisplayRightRightCameraMenuItem.Text = "Right camera";
            this.DisplayRightRightCameraMenuItem.Click += new System.EventHandler(this.DisplayToolStripMenuItem_Click);
            // 
            // DisplayRightDepthMapMenuItem
            // 
            this.DisplayRightDepthMapMenuItem.Name = "DisplayRightDepthMapMenuItem";
            this.DisplayRightDepthMapMenuItem.Size = new System.Drawing.Size(172, 26);
            this.DisplayRightDepthMapMenuItem.Text = "Depth map";
            this.DisplayRightDepthMapMenuItem.Click += new System.EventHandler(this.DisplayToolStripMenuItem_Click);
            // 
            // pointCloudToolStripMenuItem1
            // 
            this.pointCloudToolStripMenuItem1.Name = "pointCloudToolStripMenuItem1";
            this.pointCloudToolStripMenuItem1.Size = new System.Drawing.Size(172, 26);
            this.pointCloudToolStripMenuItem1.Text = "Point cloud";
            this.pointCloudToolStripMenuItem1.Click += new System.EventHandler(this.DisplayToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(173, 6);
            // 
            // listOfImagesToolStripMenuItem
            // 
            this.listOfImagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noCameraToolStripMenuItem,
            this.leftCameraToolStripMenuItem,
            this.rightCameToolStripMenuItem});
            this.listOfImagesToolStripMenuItem.Name = "listOfImagesToolStripMenuItem";
            this.listOfImagesToolStripMenuItem.Size = new System.Drawing.Size(176, 26);
            this.listOfImagesToolStripMenuItem.Text = "List of images";
            // 
            // noCameraToolStripMenuItem
            // 
            this.noCameraToolStripMenuItem.Checked = true;
            this.noCameraToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.noCameraToolStripMenuItem.Name = "noCameraToolStripMenuItem";
            this.noCameraToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.noCameraToolStripMenuItem.Text = "No camera";
            this.noCameraToolStripMenuItem.Click += new System.EventHandler(this.ListViewerToolStripMenuItem_Click);
            // 
            // leftCameraToolStripMenuItem
            // 
            this.leftCameraToolStripMenuItem.Name = "leftCameraToolStripMenuItem";
            this.leftCameraToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.leftCameraToolStripMenuItem.Text = "Left camera";
            this.leftCameraToolStripMenuItem.Click += new System.EventHandler(this.ListViewerToolStripMenuItem_Click);
            // 
            // rightCameToolStripMenuItem
            // 
            this.rightCameToolStripMenuItem.Name = "rightCameToolStripMenuItem";
            this.rightCameToolStripMenuItem.Size = new System.Drawing.Size(172, 26);
            this.rightCameToolStripMenuItem.Text = "Right camera";
            this.rightCameToolStripMenuItem.Click += new System.EventHandler(this.ListViewerToolStripMenuItem_Click);
            // 
            // computeToolStripMenuItem
            // 
            this.computeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stereoVisionToolStripMenuItem,
            this.structureFromMotionToolStripMenuItem});
            this.computeToolStripMenuItem.Name = "computeToolStripMenuItem";
            this.computeToolStripMenuItem.Size = new System.Drawing.Size(82, 27);
            this.computeToolStripMenuItem.Text = "Compute";
            // 
            // stereoVisionToolStripMenuItem
            // 
            this.stereoVisionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stereoBMToolStripMenuItem,
            this.stereoSGBMToolStripMenuItem,
            this.cudaStereoBMToolStripMenuItem,
            this.cudaStereoConstantSpaceBPToolStripMenuItem});
            this.stereoVisionToolStripMenuItem.Name = "stereoVisionToolStripMenuItem";
            this.stereoVisionToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.stereoVisionToolStripMenuItem.Text = "Stereo vision";
            // 
            // stereoBMToolStripMenuItem
            // 
            this.stereoBMToolStripMenuItem.Checked = true;
            this.stereoBMToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.stereoBMToolStripMenuItem.Name = "stereoBMToolStripMenuItem";
            this.stereoBMToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.stereoBMToolStripMenuItem.Text = "StereoBM";
            this.stereoBMToolStripMenuItem.Click += new System.EventHandler(this.stereoCorrespondenceToolStripMenuItem_Click);
            // 
            // stereoSGBMToolStripMenuItem
            // 
            this.stereoSGBMToolStripMenuItem.Name = "stereoSGBMToolStripMenuItem";
            this.stereoSGBMToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.stereoSGBMToolStripMenuItem.Text = "StereoSGBM";
            this.stereoSGBMToolStripMenuItem.Click += new System.EventHandler(this.stereoCorrespondenceToolStripMenuItem_Click);
            // 
            // cudaStereoBMToolStripMenuItem
            // 
            this.cudaStereoBMToolStripMenuItem.Name = "cudaStereoBMToolStripMenuItem";
            this.cudaStereoBMToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.cudaStereoBMToolStripMenuItem.Text = "CudaStereoBM";
            this.cudaStereoBMToolStripMenuItem.Click += new System.EventHandler(this.stereoCorrespondenceToolStripMenuItem_Click);
            // 
            // cudaStereoConstantSpaceBPToolStripMenuItem
            // 
            this.cudaStereoConstantSpaceBPToolStripMenuItem.Name = "cudaStereoConstantSpaceBPToolStripMenuItem";
            this.cudaStereoConstantSpaceBPToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.cudaStereoConstantSpaceBPToolStripMenuItem.Text = "CudaStereoConstantSpaceBP";
            this.cudaStereoConstantSpaceBPToolStripMenuItem.Click += new System.EventHandler(this.stereoCorrespondenceToolStripMenuItem_Click);
            // 
            // structureFromMotionToolStripMenuItem
            // 
            this.structureFromMotionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detectFeaturesToolStripMenuItem,
            this.deskriptorToolStripMenuItem,
            this.matcherToolStripMenuItem});
            this.structureFromMotionToolStripMenuItem.Name = "structureFromMotionToolStripMenuItem";
            this.structureFromMotionToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.structureFromMotionToolStripMenuItem.Text = "Structure from Motion";
            // 
            // detectFeaturesToolStripMenuItem
            // 
            this.detectFeaturesToolStripMenuItem.Name = "detectFeaturesToolStripMenuItem";
            this.detectFeaturesToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.detectFeaturesToolStripMenuItem.Text = "Detect features";
            // 
            // deskriptorToolStripMenuItem
            // 
            this.deskriptorToolStripMenuItem.Name = "deskriptorToolStripMenuItem";
            this.deskriptorToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.deskriptorToolStripMenuItem.Text = "Deskriptor";
            // 
            // matcherToolStripMenuItem
            // 
            this.matcherToolStripMenuItem.Name = "matcherToolStripMenuItem";
            this.matcherToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.matcherToolStripMenuItem.Text = "Matcher";
            // 
            // inputToolStripMenuItem
            // 
            this.inputToolStripMenuItem.Name = "inputToolStripMenuItem";
            this.inputToolStripMenuItem.Size = new System.Drawing.Size(55, 27);
            this.inputToolStripMenuItem.Text = "Input";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(68, 27);
            this.settingToolStripMenuItem.Text = "Setting";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 27);
            this.helpToolStripMenuItem.Text = "Help";
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
            this.toolStripSeparator7});
            this.toolStrip1.Location = new System.Drawing.Point(0, 27);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(1685, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(99, 27);
            this.toolStripLabel1.Text = "Stereo vision";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(76, 27);
            this.toolStripLabel2.Text = "Algorithm";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(160, 27);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 27);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 85);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1677, 411);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // LeftViewBox
            // 
            this.LeftViewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftViewBox.Location = new System.Drawing.Point(3, 2);
            this.LeftViewBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LeftViewBox.Name = "LeftViewBox";
            this.LeftViewBox.Size = new System.Drawing.Size(832, 407);
            this.LeftViewBox.TabIndex = 2;
            this.LeftViewBox.TabStop = false;
            // 
            // RightViewBox
            // 
            this.RightViewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightViewBox.Location = new System.Drawing.Point(841, 2);
            this.RightViewBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RightViewBox.Name = "RightViewBox";
            this.RightViewBox.Size = new System.Drawing.Size(832, 407);
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
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 805);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1685, 33);
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
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(561, 33);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(79, 28);
            this.toolStripStatusLabel1.Text = "Processing";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(133, 27);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.61367F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.38633F));
            this.tableLayoutPanel4.Controls.Add(this.ListViewer, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.richTextBox1, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 554);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 251F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1685, 251);
            this.tableLayoutPanel4.TabIndex = 4;
            this.tableLayoutPanel4.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel4_Paint);
            // 
            // ListViewer
            // 
            this.ListViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup4.Header = "NoGroup";
            listViewGroup4.Name = "NoGroup";
            listViewGroup5.Header = "LeftGroup";
            listViewGroup5.Name = "LeftGroup";
            listViewGroup6.Header = "RightGroup";
            listViewGroup6.Name = "RightGroup";
            this.ListViewer.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup4,
            listViewGroup5,
            listViewGroup6});
            this.ListViewer.LargeImageList = this.ImageList;
            this.ListViewer.Location = new System.Drawing.Point(0, 0);
            this.ListViewer.Margin = new System.Windows.Forms.Padding(0);
            this.ListViewer.Name = "ListViewer";
            this.ListViewer.ShowItemToolTips = true;
            this.ListViewer.Size = new System.Drawing.Size(1122, 251);
            this.ListViewer.SmallImageList = this.ImageList;
            this.ListViewer.TabIndex = 1;
            this.ListViewer.UseCompatibleStateImageBehavior = false;
            this.ListViewer.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewer_ItemSelectionChanged);
            // 
            // ImageList
            // 
            this.ImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.ImageList.ImageSize = new System.Drawing.Size(128, 72);
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(1122, 0);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(563, 251);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
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
            this.toolStripSeparator6});
            this.toolStrip2.Location = new System.Drawing.Point(0, 54);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip2.Size = new System.Drawing.Size(1685, 27);
            this.toolStrip2.TabIndex = 5;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel3.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripLabel3.Size = new System.Drawing.Size(168, 27);
            this.toolStripLabel3.Text = "Structure from Motion";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(118, 27);
            this.toolStripLabel4.Text = "Feature detector";
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox2.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(160, 27);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(139, 27);
            this.toolStripLabel5.Text = "Keypoint descriptor";
            // 
            // toolStripComboBox3
            // 
            this.toolStripComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox3.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripComboBox3.Name = "toolStripComboBox3";
            this.toolStripComboBox3.Size = new System.Drawing.Size(160, 27);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(136, 27);
            this.toolStripLabel6.Text = "Descriptor matcher";
            // 
            // toolStripComboBox4
            // 
            this.toolStripComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox4.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripComboBox4.Name = "toolStripComboBox4";
            this.toolStripComboBox4.Size = new System.Drawing.Size(160, 27);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
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
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 500);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1685, 27);
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
            this.toolStrip3.Size = new System.Drawing.Size(842, 27);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel7.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(96, 27);
            this.toolStripLabel7.Text = "Left window";
            // 
            // toolStripComboBox5
            // 
            this.toolStripComboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox5.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripComboBox5.Name = "toolStripComboBox5";
            this.toolStripComboBox5.Size = new System.Drawing.Size(160, 27);
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
            this.toolStrip4.Location = new System.Drawing.Point(842, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip4.Size = new System.Drawing.Size(843, 27);
            this.toolStrip4.TabIndex = 1;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel8.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(106, 27);
            this.toolStripLabel8.Text = "Right window";
            // 
            // toolStripComboBox6
            // 
            this.toolStripComboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox6.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripComboBox6.Name = "toolStripComboBox6";
            this.toolStripComboBox6.Size = new System.Drawing.Size(160, 27);
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
            this.toolStripButton9});
            this.toolStrip5.Location = new System.Drawing.Point(0, 527);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip5.Size = new System.Drawing.Size(1685, 27);
            this.toolStrip5.TabIndex = 7;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // toolStripLabel9
            // 
            this.toolStripLabel9.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripLabel9.Name = "toolStripLabel9";
            this.toolStripLabel9.Size = new System.Drawing.Size(95, 27);
            this.toolStripLabel9.Text = "Items in view";
            // 
            // toolStripComboBox7
            // 
            this.toolStripComboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox7.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripComboBox7.Name = "toolStripComboBox7";
            this.toolStripComboBox7.Size = new System.Drawing.Size(160, 27);
            this.toolStripComboBox7.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox7_SelectedIndexChanged);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton5.Text = "toolStripButton5";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton6.Text = "toolStripButton6";
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton7.Text = "toolStripButton7";
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton8.Text = "toolStripButton8";
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton9.Image")));
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Margin = new System.Windows.Forms.Padding(1);
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(24, 25);
            this.toolStripButton9.Text = "toolStripButton9";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1685, 838);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1701, 872);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem computeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        public System.Windows.Forms.ListView ListViewer;
        public Emgu.CV.UI.ImageBox LeftViewBox;
        public Emgu.CV.UI.ImageBox RightViewBox;
        public System.Windows.Forms.ImageList ImageList;
        private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisplayLeft;
        private System.Windows.Forms.ToolStripMenuItem DisplayLeftListViewerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisplayLeftLeftCameraMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisplayLeftRightCameraMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisplayLeftDepthMapMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisplayRight;
        private System.Windows.Forms.ToolStripMenuItem DisplayRightListViewerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisplayRightLeftCameraMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisplayRightRightCameraMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisplayRightDepthMapMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem listOfImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftCameraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightCameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stereoVisionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem structureFromMotionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detectFeaturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deskriptorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem matcherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stereoBMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stereoSGBMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cudaStereoBMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cudaStereoConstantSpaceBPToolStripMenuItem;
        public Kitware.VTK.RenderWindowControl renderWindowControl1;
        public Kitware.VTK.RenderWindowControl renderWindowControl2;
        private System.Windows.Forms.ToolStripMenuItem pointCloudToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointCloudToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
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
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

