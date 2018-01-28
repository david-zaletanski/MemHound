using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;

namespace MemHound
{
    public partial class SelectProcessDialog : Form
    {
        public SelectProcessDialog()
        {
            InitializeComponent();
            PopulateProcesses();
        }

        private Process[] Procs;
        public Process SelectedProcess { get; set; }

        private void PopulateProcesses()
        {
            listBox1.Items.Clear();
            Procs = Process.GetProcesses();
            for (int i = 0; i < Procs.Length; i++)
            {
                string name = Procs[i].ProcessName;
                if (Procs[i].MainWindowTitle != "")
                    name += " [" + Procs[i].MainWindowTitle + "]";
                listBox1.Items.Add(name);
            }
            listBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Select
            SelectedProcess = Procs[listBox1.SelectedIndex];

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Cancel

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Refresh
            PopulateProcesses();
        }
    }
}
