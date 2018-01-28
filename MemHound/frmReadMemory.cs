using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MemHound.Memory;

namespace MemHound
{
    public partial class frmReadMemory : Form
    {
        private ExternalMemManager MM;

        public frmReadMemory(ExternalMemManager MM)
        {
            InitializeComponent();
            this.MM = MM;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Read Button
            string sAddress = textBox1.Text;
            string type = comboBox1.Text;
            if (type == "Int16")
            {

            }
            else if (type == "Int32")
            {
                Int32 value = MM.ReadInt32(new IntPtr(long.Parse(sAddress)));
                textBox2.Text = value.ToString();
            }
            else if (type == "UInt32")
            {
                UInt32 value = MM.ReadUInt32(new IntPtr(long.Parse(sAddress)));
                textBox2.Text = value.ToString();
            }
            else if (type == "Int64")
            {
                Int64 value = MM.ReadInt64(new IntPtr(long.Parse(sAddress)));
                textBox2.Text = value.ToString();
            }
            else if (type == "UInt64")
            {
                UInt64 value = MM.ReadUInt64(new IntPtr(long.Parse(sAddress)));
                textBox2.Text = value.ToString();
            }
            else if (type == "Float")
            {
                float value = MM.ReadFloat(new IntPtr(long.Parse(sAddress)));
                textBox2.Text = value.ToString();
            }
            else if (type == "Double")
            {

            }
        }
    }
}
