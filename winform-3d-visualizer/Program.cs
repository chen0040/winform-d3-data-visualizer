using System;
using System.Linq;
using System.Windows.Forms;
using CefSharp;

namespace PredictiveModels.Tools.Charting
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            var settings = new CefSettings();
            settings.BrowserSubprocessPath = @"x86\CefSharp.BrowserSubprocess.exe";

            Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmCorrelogramChart());
        }
    }
}