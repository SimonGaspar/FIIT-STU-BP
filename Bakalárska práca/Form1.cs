using Bakalárska_práca.Manager;
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


        public Form1()
        {
            InitializeComponent();

            fileManager = new FileManager(this);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            fileManager.AddToListView();
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
