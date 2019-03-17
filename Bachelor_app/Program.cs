using System;
using System.Windows.Forms;

namespace Bakalárska_práca
{
    static class Program
    {
        //Na konci odkomentovat 
        //
        //ACTIVATEFORFULLFORM

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //new SfM();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
