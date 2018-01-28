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
    public partial class frmWriteMemory : Form
    {
        private ExternalMemManager MM;

        public frmWriteMemory(ExternalMemManager MM)
        {
            InitializeComponent();
            this.MM = MM;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Read Button
            string sAddress = textBox1.Text;
            IntPtr ptrAddress = new IntPtr(long.Parse(sAddress));
            string type = comboBox1.Text;

            if (type == "Int32")
            {
                if (MM.WriteInt32(ptrAddress, Int32.Parse(textBox2.Text)))
                    Core.Output("Successfully wrote memory.", Color.Green);
                else
                    Core.Output("An error occured while writing memory.", Color.Red);
            }
            else if (type == "Int64")
            {
                if (MM.WriteInt64(ptrAddress, Int64.Parse(textBox2.Text)))
                    Core.Output("Successfully wrote memory.", Color.Green);
                else
                    Core.Output("An error occured while writing memory.", Color.Red);
            }
            else if (type == "Float")
            {
                if (MM.WriteFloat(ptrAddress, float.Parse(textBox2.Text)))
                    Core.Output("Successfully wrote memory.", Color.Green);
                else
                    Core.Output("An error occured while writing memory.", Color.Red);
            }
            else if (type == "Double")
            {
                if (MM.WriteDouble(ptrAddress, double.Parse(textBox2.Text)))
                    Core.Output("Successfully wrote memory.", Color.Green);
                else
                    Core.Output("An error occured while writing memory.", Color.Red);
            }
        }
    }
}
