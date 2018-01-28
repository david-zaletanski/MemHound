using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MemHound
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Core.MainForm = new frmMain();
            Application.Run(Core.MainForm);
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Core.OnExit();
        }
    }
}
