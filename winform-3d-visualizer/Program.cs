using System;
using System.Linq;
using System.Windows.Forms;
using winform_3d_visualizer;

namespace winform_3d_visualizer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmCorrelogramChart());
        }
    }
}