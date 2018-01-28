using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Threading.Tasks;

/*
 * Notes About Windows Memory Allocation:
 * 
 * For 32bit the virtual address space is broken up into:
 * 
 *      Low 2GB used by the process (0x00000000 through 0x7FFFFFFF)
 *      High 2GB used by the system (0x80000000 through 0xFFFFFFFF)
 * 
 * But since were using 64 bit...
 *      Low 8TB used by the process (0x000000000 through 0x7FFFFFFFFFF)
 *      High 248TB used by the system (0x80000000000 through 0xFFFFFFFFFFFFFFFF) wtf?
 * 
 */

using MemHound.Memory;

namespace MemHound
{
    public partial class frmMain : Form
    {
        ExternalMemManager MM;

        public frmMain()
        {
            InitializeComponent();
            MM = null;
        }

        public void OnExit()
        {
            
        }

        #region Output

        public void Output(string message)
        {
            Output(message, Color.Black);
        }

        private delegate void OutputDelegate(string message, Color c);
        public void Output(string message, Color c)
        {
            if (outputTextBox.InvokeRequired)
            {
                Invoke(new OutputDelegate(Output), new object[] { message, c });
            }
            else
            {
                int s = outputTextBox.TextLength;
                outputTextBox.AppendText(message+"\n");
                outputTextBox.Select(s, message.Length+1);
                outputTextBox.SelectionColor = c;
                outputTextBox.Select(outputTextBox.TextLength, 0);
                outputTextBox.ScrollToCaret();
            }
        }

        #endregion

        private void openProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open Process
            SelectProcessDialog spd = new SelectProcessDialog();
            if (spd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MM = new ExternalMemManager(spd.SelectedProcess);
                Core.Output("Successfully opened process '" + MM.ExternalProcess.ProcessName + "'", Color.Green);
                currentProcessTxtBox.Text = MM.ExternalProcess.ProcessName;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Exit
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Close Process
            if (MM != null)
            {
                MM = null;
                currentProcessTxtBox.Text = "";
                Core.Output("Process closed.", Color.Green);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Initial Scan
            if (MM == null)
            {
                Core.Output("Could not start initial scan, no process open.", Color.Red);
                return;
            }

            string val = textBox1.Text;
            string type = comboBox1.Text;

            Core.Output("Beginning inital scan...");
            if (type == "Int16")
            {

            }
            else if (type == "Int32")
            {
                Int32 value = Int32.Parse(val);
                SmartMemoryScanner sms = new SmartMemoryScanner(MM);
                List<IntPtr> results = sms.FindOccurrencesInt32(value);
                BindingSource s = new BindingSource();
                List<Tuple<IntPtr, Int32>> data = new List<Tuple<IntPtr, Int32>>();
                for (int i = 0; i < results.Count; i++)
                    data.Add(new Tuple<IntPtr, Int32>(results[i], value));
                s.DataSource = data;
                Core.Output("Found " + results.Count + " results!", Color.Green);
                dataGridView1.DataSource = s;
            }
            else if (type == "UInt32")
            {
                UInt32 value = UInt32.Parse(val);
                SmartMemoryScanner sms = new SmartMemoryScanner(MM);
                List<IntPtr> results = sms.FindOccurrencesUInt32(value);
                BindingSource s = new BindingSource();
                List<Tuple<IntPtr, UInt32>> data = new List<Tuple<IntPtr, UInt32>>();
                for (int i = 0; i < results.Count; i++)
                    data.Add(new Tuple<IntPtr, UInt32>(results[i], value));
                s.DataSource = data;
                Core.Output("Found " + results.Count + " results!", Color.Green);
                dataGridView1.DataSource = s;
            }
            else if (type == "Int64")
            {
                Int64 value = Int64.Parse(val);
                SmartMemoryScanner sms = new SmartMemoryScanner(MM);
                List<IntPtr> results = sms.FindOccurrencesInt64(value);
                BindingSource s = new BindingSource();
                List<Tuple<IntPtr, Int64>> data = new List<Tuple<IntPtr, Int64>>();
                for (int i = 0; i < results.Count; i++)
                    data.Add(new Tuple<IntPtr, Int64>(results[i], value));
                s.DataSource = data;
                Core.Output("Found " + results.Count + " results!", Color.Green);
                dataGridView1.DataSource = s;
            }
            else if (type == "UInt64")
            {
                UInt64 value = UInt64.Parse(val);
                SmartMemoryScanner sms = new SmartMemoryScanner(MM);
                List<IntPtr> results = sms.FindOccurrencesUInt64(value);
                BindingSource s = new BindingSource();
                List<Tuple<IntPtr, UInt64>> data = new List<Tuple<IntPtr, UInt64>>();
                for (int i = 0; i < results.Count; i++)
                    data.Add(new Tuple<IntPtr, UInt64>(results[i], value));
                s.DataSource = data;
                Core.Output("Found " + results.Count + " results!", Color.Green);
                dataGridView1.DataSource = s;
            }
            else if (type == "Float")
            {
                float value = float.Parse(val);
                SmartMemoryScanner sms = new SmartMemoryScanner(MM);
                List<IntPtr> results = sms.FindOccurrencesFloat(value);
                BindingSource s = new BindingSource();
                List<Tuple<IntPtr, float>> data = new List<Tuple<IntPtr, float>>();
                for (int i = 0; i < results.Count; i++)
                    data.Add(new Tuple<IntPtr, float>(results[i], value));
                s.DataSource = data;
                Core.Output("Found " + results.Count + " results!", Color.Green);
                dataGridView1.DataSource = s;
            }
            else if (type == "Double")
            {
                double value = double.Parse(val);
                SmartMemoryScanner sms = new SmartMemoryScanner(MM);
                List<IntPtr> results = sms.FindOccurrencesDouble(value);
                BindingSource s = new BindingSource();
                List<Tuple<IntPtr, double>> data = new List<Tuple<IntPtr, double>>();
                for (int i = 0; i < results.Count; i++)
                    data.Add(new Tuple<IntPtr, double>(results[i], value));
                s.DataSource = data;
                Core.Output("Found " + results.Count + " results!", Color.Green);
                dataGridView1.DataSource = s;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Update
            Core.Output("Updating results, showing only ones with new value...");
            string val = textBox1.Text;
            string type = comboBox1.Text;
            if (type == "Int16")
            {

            }
            else if (type == "Int32")
            {
                Int32 value = Int32.Parse(val);
                BindingSource s = (BindingSource)dataGridView1.DataSource;
                List<Tuple<IntPtr, Int32>> data = (List<Tuple<IntPtr, Int32>>)s.DataSource;
                List<Tuple<IntPtr, Int32>> newdata = new List<Tuple<IntPtr, Int32>>();
                foreach (Tuple<IntPtr, Int32> t in data)
                {
                    Int32 nval = MM.ReadInt32(t.Item1);
                    if (nval.Equals(value))
                        newdata.Add(new Tuple<IntPtr, Int32>(t.Item1, nval));
                }
                s.DataSource = newdata;
                dataGridView1.DataSource = s;
            }
            else if (type == "UInt32")
            {
                UInt32 value = UInt32.Parse(val);
                BindingSource s = (BindingSource)dataGridView1.DataSource;
                List<Tuple<IntPtr, UInt32>> data = (List<Tuple<IntPtr, UInt32>>)s.DataSource;
                List<Tuple<IntPtr, UInt32>> newdata = new List<Tuple<IntPtr, UInt32>>();
                foreach (Tuple<IntPtr, UInt32> t in data)
                {
                    UInt32 nval = MM.ReadUInt32(t.Item1);
                    if (nval.Equals(value))
                        newdata.Add(new Tuple<IntPtr, UInt32>(t.Item1, nval));
                }
                s.DataSource = newdata;
                dataGridView1.DataSource = s;
            }
            else if (type == "Int64")
            {
                Int64 value = Int64.Parse(val);
                BindingSource s = (BindingSource)dataGridView1.DataSource;
                List<Tuple<IntPtr, Int64>> data = (List<Tuple<IntPtr, Int64>>)s.DataSource;
                List<Tuple<IntPtr, Int64>> newdata = new List<Tuple<IntPtr, Int64>>();
                foreach (Tuple<IntPtr, Int64> t in data)
                {
                    Int64 nval = MM.ReadInt64(t.Item1);
                    if (nval.Equals(value))
                        newdata.Add(new Tuple<IntPtr, Int64>(t.Item1, nval));
                }
                s.DataSource = newdata;
                dataGridView1.DataSource = s;
            }
            else if (type == "UInt64")
            {
                UInt64 value = UInt64.Parse(val);
                BindingSource s = (BindingSource)dataGridView1.DataSource;
                List<Tuple<IntPtr, UInt64>> data = (List<Tuple<IntPtr, UInt64>>)s.DataSource;
                List<Tuple<IntPtr, UInt64>> newdata = new List<Tuple<IntPtr, UInt64>>();
                foreach (Tuple<IntPtr, UInt64> t in data)
                {
                    UInt64 nval = MM.ReadUInt64(t.Item1);
                    if (nval.Equals(value))
                        newdata.Add(new Tuple<IntPtr, UInt64>(t.Item1, nval));
                }
                s.DataSource = newdata;
                dataGridView1.DataSource = s;
            }
            else if (type == "Float")
            {
                float value = float.Parse(val);
                BindingSource s = (BindingSource)dataGridView1.DataSource;
                List<Tuple<IntPtr, float>> data = (List<Tuple<IntPtr, float>>)s.DataSource;
                List<Tuple<IntPtr, float>> newdata = new List<Tuple<IntPtr, float>>();
                foreach (Tuple<IntPtr, float> t in data)
                {
                    float nval = MM.ReadFloat(t.Item1);
                    if (nval.Equals(value))
                        newdata.Add(new Tuple<IntPtr, float>(t.Item1, nval));
                }
                s.DataSource = newdata;
                dataGridView1.DataSource = s;
            }
            else if (type == "Double")
            {
                double value = double.Parse(val);
                BindingSource s = (BindingSource)dataGridView1.DataSource;
                List<Tuple<IntPtr, double>> data = (List<Tuple<IntPtr, double>>)s.DataSource;
                List<Tuple<IntPtr, double>> newdata = new List<Tuple<IntPtr, double>>();
                foreach (Tuple<IntPtr, double> t in data)
                {
                    double nval = MM.ReadDouble(t.Item1);
                    if (nval.Equals(value))
                        newdata.Add(new Tuple<IntPtr, double>(t.Item1, nval));
                }
                s.DataSource = newdata;
                dataGridView1.DataSource = s;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Refresh
            string type = comboBox1.Text;
            Core.Output("Refreshing current values.");
            if (type == "Int16")
            {

            }
            else if (type == "Int32")
            {
                BindingSource s = (BindingSource)dataGridView1.DataSource;
                List<Tuple<IntPtr, Int32>> data = (List<Tuple<IntPtr, Int32>>)s.DataSource;
                List<Tuple<IntPtr, Int32>> newdata = new List<Tuple<IntPtr, Int32>>();
                foreach (Tuple<IntPtr, Int32> t in data)
                {
                    newdata.Add(new Tuple<IntPtr, Int32>(t.Item1, MM.ReadInt32(t.Item1)));
                }
                s.DataSource = newdata;
                dataGridView1.DataSource = s;
            }
            else if (type == "UInt32")
            {
                BindingSource s = (BindingSource)dataGridView1.DataSource;
                List<Tuple<IntPtr, UInt32>> data = (List<Tuple<IntPtr, UInt32>>)s.DataSource;
                List<Tuple<IntPtr, UInt32>> newdata = new List<Tuple<IntPtr, UInt32>>();
                foreach (Tuple<IntPtr, UInt32> t in data)
                {
                    newdata.Add(new Tuple<IntPtr, UInt32>(t.Item1, MM.ReadUInt32(t.Item1)));
                }
                s.DataSource = newdata;
                dataGridView1.DataSource = s;
            }
            else if (type == "Int64")
            {
                BindingSource s = (BindingSource)dataGridView1.DataSource;
                List<Tuple<IntPtr, Int64>> data = (List<Tuple<IntPtr, Int64>>)s.DataSource;
                List<Tuple<IntPtr, Int64>> newdata = new List<Tuple<IntPtr, Int64>>();
                foreach (Tuple<IntPtr, Int64> t in data)
                {
                    newdata.Add(new Tuple<IntPtr, Int64>(t.Item1, MM.ReadInt64(t.Item1)));
                }
                s.DataSource = newdata;
                dataGridView1.DataSource = s;
            }
            else if (type == "UInt64")
            {
                BindingSource s = (BindingSource)dataGridView1.DataSource;
                List<Tuple<IntPtr, UInt64>> data = (List<Tuple<IntPtr, UInt64>>)s.DataSource;
                List<Tuple<IntPtr, UInt64>> newdata = new List<Tuple<IntPtr, UInt64>>();
                foreach (Tuple<IntPtr, UInt64> t in data)
                {
                    newdata.Add(new Tuple<IntPtr, UInt64>(t.Item1, MM.ReadUInt64(t.Item1)));
                }
                s.DataSource = newdata;
                dataGridView1.DataSource = s;
            }
            else if (type == "Float")
            {
                BindingSource s = (BindingSource)dataGridView1.DataSource;
                List<Tuple<IntPtr, float>> data = (List<Tuple<IntPtr, float>>)s.DataSource;
                List<Tuple<IntPtr, float>> newdata = new List<Tuple<IntPtr, float>>();
                foreach (Tuple<IntPtr, float> t in data)
                {
                    newdata.Add(new Tuple<IntPtr, float>(t.Item1, MM.ReadFloat(t.Item1)));
                }
                s.DataSource = newdata;
                dataGridView1.DataSource = s;
            }
            else if (type == "Double")
            {
                BindingSource s = (BindingSource)dataGridView1.DataSource;
                List<Tuple<IntPtr, double>> data = (List<Tuple<IntPtr, double>>)s.DataSource;
                List<Tuple<IntPtr, double>> newdata = new List<Tuple<IntPtr, double>>();
                foreach (Tuple<IntPtr, double> t in data)
                {
                    newdata.Add(new Tuple<IntPtr, double>(t.Item1, MM.ReadDouble(t.Item1)));
                }
                s.DataSource = newdata;
                dataGridView1.DataSource = s;
            }
        }

        private void queryVirtualMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Query Virtual Memory
            if (MM == null)
            {
                Core.Output("Could not query virtual memory, no process open.", Color.Red);
                return;
            }
            int a, b, c;
            a = b = c = 0;
            long totmemorycommitted = 0;

            long MaxAddress = 0x7FFFFFFFFFF;
            long address = 0;
            do
            {
                Memory.Kernel32.MEMORY_BASIC_INFORMATION m;
                int result = Kernel32.VirtualQueryEx(MM.ExternalProcess.Handle, new IntPtr(address), out m,
                    (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(Memory.Kernel32.MEMORY_BASIC_INFORMATION)));
                Core.Output(m.BaseAddress + "-" + new IntPtr(m.BaseAddress.ToInt64() + m.RegionSize.ToInt64() - 1) + " : " + m.RegionSize + " bytes result=" + result);
                string state = "";
                if (m.State == 0x1000)
                {
                    totmemorycommitted += m.RegionSize.ToInt64();
                    state = "\tMEM_COMMIT";
                    a++;
                }
                else if (m.State == 0x10000)
                {
                    state="\tMEM_FREE";
                    b++;
                }
                else if (m.State == 0x2000)
                {
                    state="\tMEM_RESERVE";
                    c++;
                }
                state += "\t" + m.Protect;
                if ((m.Protect & (uint)Kernel32.AllocationProtect.PAGE_GUARD) != 0)
                    Core.Output("This memory allows no access.", Color.Red);
                Core.Output(state);
                if (address == (long)m.BaseAddress + (long)m.RegionSize)
                    break;
                address = (long)m.BaseAddress + (long)m.RegionSize;
            } while (address <= MaxAddress);
            Core.Output("MEM_COMMIT: " + a);
            Core.Output("Total memory comitted: " + totmemorycommitted);
            Core.Output("MEM_FREE: " + b);
            Core.Output("MEM_RESERVE: " + c);
        }

        private void readMemoryAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Read Memory Address
            if (MM == null)
            {
                Core.Output("Could not read memory address, no process open.", Color.Red);
                return;
            }
            frmReadMemory frm = new frmReadMemory(MM);
            frm.Show();
        }

        private void pointerFinderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Pointer Finder
            if (MM == null)
            {
                Core.Output("Could not open pointer finder, no process open.", Color.Red);
                return;
            }
            frmPointerFinder frm = new frmPointerFinder(MM);
            frm.Show();
        }

        private void baseAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Base Address
            if (MM == null)
            {
                Core.Output("Could not print base address, no process open.", Color.Red);
                return;
            }
            Core.Output("Process main module base address: " + MM.ExternalProcess.MainModule.BaseAddress);
        }

        private void editSelectedValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Edit Selected Value
            if (MM == null)
            {
                Core.Output("Could not edit selected value, no process open.", Color.Red);
                return;
            }

            frmWriteMemory frm = new frmWriteMemory(MM);
            frm.Show();
        }


    }
}
