using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemHound
{
    class Core
    {
        public static frmMain MainForm;

        public static void Output(string message) {
            MainForm.Output(message);
        }
        public static void Output(string message, System.Drawing.Color c)
        {
            MainForm.Output(message, c);
        }

        public static void OnExit()
        {
            // Application Cleanup Procedures
            MainForm.OnExit();
        }
    }
}
