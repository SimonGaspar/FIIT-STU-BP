using Bakalárska_práca.Manager;
using Bakalárska_práca.StereoVision;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bakalárska_práca
{
    public partial class Form1 : Form
    {
        private FileManager fileManager;
        private DisplayManager displayManager;
        private MenuManager menuManager;
        private StereoVisionManager stereoVisionManager;


        public Form1()
        {
            InitializeComponent();

            fileManager = new FileManager(this);
            displayManager = new DisplayManager(this,fileManager);
            stereoVisionManager = new StereoVisionManager(fileManager,displayManager);
            menuManager = new MenuManager(this, displayManager,fileManager,stereoVisionManager);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            fileManager.AddToListView();
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ListViewer_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            displayManager.DisplayImageFromListView(e.Item);
        }

        private void DisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuManager.MenuSetDisplaySetting(sender, e);
        }
        
        private void ListViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuManager.MenuSetListViewerSetting(sender, e);
        }

        private void stereoCorrespondenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stereoVisionManager.SetStereoCorrespondenceAlgorithm(sender, e);
        }

        private void ComputeStereo_Click(object sender, EventArgs e)
        {
            stereoVisionManager.ComputeStereoCorrespondence();
        }

        private void ShowSetting_Click(object sender, EventArgs e)
        {
            stereoVisionManager.ShowSettingForStereoSolver();
        }
    }
}
