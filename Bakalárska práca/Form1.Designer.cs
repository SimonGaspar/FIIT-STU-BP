﻿namespace Bakalárska_práca
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayLeftListViewerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayLeftLeftCameraMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayLeftRightCameraMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayLeftDepthMapMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayRight = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayRightListViewerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayRightLeftCameraMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayRightRightCameraMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayRightDepthMapMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.listOfImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftCameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightCameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.computeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.LeftViewBox = new Emgu.CV.UI.ImageBox();
            this.RightViewBox = new Emgu.CV.UI.ImageBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.Add = new System.Windows.Forms.Button();
            this.Remove = new System.Windows.Forms.Button();
            this.SwitchLeft = new System.Windows.Forms.Button();
            this.SwitchRight = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.ListViewer = new System.Windows.Forms.ListView();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LeftViewBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightViewBox)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.330666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.330666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.01F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.330666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.330666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.330666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.006F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.330666F));
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
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1685, 27);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(62, 23);
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
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(70, 23);
            this.displayToolStripMenuItem.Text = "Display";
            // 
            // DisplayLeft
            // 
            this.DisplayLeft.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DisplayLeftListViewerMenuItem,
            this.DisplayLeftLeftCameraMenuItem,
            this.DisplayLeftRightCameraMenuItem,
            this.DisplayLeftDepthMapMenuItem});
            this.DisplayLeft.Name = "DisplayLeft";
            this.DisplayLeft.Size = new System.Drawing.Size(216, 26);
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
            // DisplayRight
            // 
            this.DisplayRight.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DisplayRightListViewerMenuItem,
            this.DisplayRightLeftCameraMenuItem,
            this.DisplayRightRightCameraMenuItem,
            this.DisplayRightDepthMapMenuItem});
            this.DisplayRight.Name = "DisplayRight";
            this.DisplayRight.Size = new System.Drawing.Size(216, 26);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(213, 6);
            // 
            // listOfImagesToolStripMenuItem
            // 
            this.listOfImagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noCameraToolStripMenuItem,
            this.leftCameraToolStripMenuItem,
            this.rightCameToolStripMenuItem});
            this.listOfImagesToolStripMenuItem.Name = "listOfImagesToolStripMenuItem";
            this.listOfImagesToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.listOfImagesToolStripMenuItem.Text = "List of images";
            // 
            // noCameraToolStripMenuItem
            // 
            this.noCameraToolStripMenuItem.Checked = true;
            this.noCameraToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.noCameraToolStripMenuItem.Name = "noCameraToolStripMenuItem";
            this.noCameraToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.noCameraToolStripMenuItem.Text = "No camera";
            this.noCameraToolStripMenuItem.Click += new System.EventHandler(this.ListViewerToolStripMenuItem_Click);
            // 
            // leftCameraToolStripMenuItem
            // 
            this.leftCameraToolStripMenuItem.Name = "leftCameraToolStripMenuItem";
            this.leftCameraToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.leftCameraToolStripMenuItem.Text = "Left camera";
            this.leftCameraToolStripMenuItem.Click += new System.EventHandler(this.ListViewerToolStripMenuItem_Click);
            // 
            // rightCameToolStripMenuItem
            // 
            this.rightCameToolStripMenuItem.Name = "rightCameToolStripMenuItem";
            this.rightCameToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.rightCameToolStripMenuItem.Text = "Right camera";
            this.rightCameToolStripMenuItem.Click += new System.EventHandler(this.ListViewerToolStripMenuItem_Click);
            // 
            // computeToolStripMenuItem
            // 
            this.computeToolStripMenuItem.Name = "computeToolStripMenuItem";
            this.computeToolStripMenuItem.Size = new System.Drawing.Size(82, 23);
            this.computeToolStripMenuItem.Text = "Compute";
            // 
            // inputToolStripMenuItem
            // 
            this.inputToolStripMenuItem.Name = "inputToolStripMenuItem";
            this.inputToolStripMenuItem.Size = new System.Drawing.Size(55, 23);
            this.inputToolStripMenuItem.Text = "Input";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 23);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSplitButton1,
            this.toolStripLabel2,
            this.toolStripSeparator1,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 27);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1685, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(73, 24);
            this.toolStripLabel1.Text = "aaaaaaaa";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aToolStripMenuItem,
            this.sToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(39, 24);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(92, 26);
            this.aToolStripMenuItem.Text = "a";
            // 
            // sToolStripMenuItem
            // 
            this.sToolStripMenuItem.Name = "sToolStripMenuItem";
            this.sToolStripMenuItem.Size = new System.Drawing.Size(92, 26);
            this.sToolStripMenuItem.Text = "s";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(59, 24);
            this.toolStripLabel2.Text = "ffffffffff";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.LeftViewBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.RightViewBox, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 58);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 411F));
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
            this.RightViewBox.Size = new System.Drawing.Size(833, 407);
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
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 809);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1677, 25);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.ListViewer, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(4, 558);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 243F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1677, 243);
            this.tableLayoutPanel4.TabIndex = 4;
            this.tableLayoutPanel4.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel4_Paint);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.Add, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.Remove, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.SwitchLeft, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.SwitchRight, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.button5, 0, 4);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(1512, 2);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 5;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(162, 239);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // Add
            // 
            this.Add.AutoSize = true;
            this.Add.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Add.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Add.Location = new System.Drawing.Point(3, 2);
            this.Add.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(156, 43);
            this.Add.TabIndex = 0;
            this.Add.Text = "Add";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Remove
            // 
            this.Remove.AutoSize = true;
            this.Remove.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Remove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Remove.Location = new System.Drawing.Point(3, 49);
            this.Remove.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(156, 43);
            this.Remove.TabIndex = 1;
            this.Remove.Text = "Remove";
            this.Remove.UseVisualStyleBackColor = true;
            // 
            // SwitchLeft
            // 
            this.SwitchLeft.AutoSize = true;
            this.SwitchLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SwitchLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SwitchLeft.Location = new System.Drawing.Point(3, 96);
            this.SwitchLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SwitchLeft.Name = "SwitchLeft";
            this.SwitchLeft.Size = new System.Drawing.Size(156, 43);
            this.SwitchLeft.TabIndex = 2;
            this.SwitchLeft.Text = "Switch LEFT";
            this.SwitchLeft.UseVisualStyleBackColor = true;
            // 
            // SwitchRight
            // 
            this.SwitchRight.AutoSize = true;
            this.SwitchRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SwitchRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SwitchRight.Location = new System.Drawing.Point(3, 143);
            this.SwitchRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SwitchRight.Name = "SwitchRight";
            this.SwitchRight.Size = new System.Drawing.Size(156, 43);
            this.SwitchRight.TabIndex = 3;
            this.SwitchRight.Text = "Switch RIGHT";
            this.SwitchRight.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.AutoSize = true;
            this.button5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button5.Location = new System.Drawing.Point(3, 190);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(156, 47);
            this.button5.TabIndex = 4;
            this.button5.Text = "Clear";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // ListViewer
            // 
            this.ListViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup4.Header = "ListViewGroup";
            listViewGroup4.Name = "NoViewGroup";
            listViewGroup5.Header = "ListViewGroup";
            listViewGroup5.Name = "LeftViewGroup";
            listViewGroup6.Header = "ListViewGroup";
            listViewGroup6.Name = "RightViewGroup";
            this.ListViewer.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup4,
            listViewGroup5,
            listViewGroup6});
            this.ListViewer.LargeImageList = this.ImageList;
            this.ListViewer.Location = new System.Drawing.Point(3, 2);
            this.ListViewer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ListViewer.Name = "ListViewer";
            this.ListViewer.ShowItemToolTips = true;
            this.ListViewer.Size = new System.Drawing.Size(1503, 239);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1685, 838);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1701, 873);
            this.Name = "Form1";
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
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem computeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.Button SwitchLeft;
        private System.Windows.Forms.Button SwitchRight;
        private System.Windows.Forms.Button button5;
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
    }
}

