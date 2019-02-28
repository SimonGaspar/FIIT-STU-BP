using Bakalárska_práca.Manager;
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


        public Form1()
        {
            InitializeComponent();

            fileManager = new FileManager(this);
            displayManager = new DisplayManager(this,fileManager);
            menuManager = new MenuManager(this, displayManager);
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
            menuManager.OnlyOneCheck(sender, e);
        }
    }
}
