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
    public partial class frmPointerFinder : Form
    {
        ExternalMemManager MM;

        public frmPointerFinder(ExternalMemManager MM)
        {
            InitializeComponent();
            this.MM = MM;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Scan
            long addressA = long.Parse(textBox1.Text);
            long addressB = long.Parse(textBox2.Text);

            List<Tuple<IntPtr, long>> Results = new List<Tuple<IntPtr, long>>();

            List<Tuple<IntPtr, IntPtr>> possibleAddresses = GetPossibleLocations();
            foreach (Tuple<IntPtr, IntPtr> t in possibleAddresses)
            {
                Results.AddRange(FindOccurancesOfPointersTo(t.Item1, t.Item2, addressA, addressB));
            }

            BindingSource s = new BindingSource();
            s.DataSource = Results;
            Core.Output("Pointer finder found " + Results.Count + " results!", Color.Green);
            dataGridView1.DataSource = s;

        }

        private List<Tuple<IntPtr,long>> FindOccurancesOfPointersTo(IntPtr startAddress, IntPtr endAddress, long addressA, long addressB)
        {
            List<Tuple<IntPtr,long>> Results = new List<Tuple<IntPtr,long>>();
            long READ_CHUNK_SIZE = 67108864; // 64 MB
            long TotalReadSize = endAddress.ToInt64() - startAddress.ToInt64();
            long ValueSize = 8; // Size (in bytes) of value to be read.
            byte[] buffer;

            // Case A: All bytes can be read in one chunk.
            if (TotalReadSize < READ_CHUNK_SIZE)
            {
                buffer = MM.ReadBytes(startAddress, TotalReadSize);
                for (int j = 0; j < buffer.Length - ValueSize; j++)
                {
                    Int64 dval = BitConverter.ToInt64(buffer, (int)j);
                    if (dval >= addressA && dval <= addressB)
                        Results.Add(new Tuple<IntPtr,long>(new IntPtr(startAddress.ToInt64() + j),dval));
                }
                return Results;
            }
            // Case B: Byte reads need to be split up into chunks.
            long NumberOfScans = TotalReadSize / READ_CHUNK_SIZE;
            long RemainingBytes = TotalReadSize % READ_CHUNK_SIZE;
            for (long i = 0; i < NumberOfScans; i++)
            {
                IntPtr readAddress;
                if (i == 0)
                {
                    readAddress = startAddress;
                    buffer = MM.ReadBytes(readAddress, READ_CHUNK_SIZE);
                    for (long j = 0; j < buffer.Length - ValueSize; j++)
                    {
                        Int64 dval = BitConverter.ToInt64(buffer, (int)j);
                        if (dval >= addressA && dval <= addressB)
                            Results.Add(new Tuple<IntPtr,long>(new IntPtr(readAddress.ToInt64() + j),dval));
                    }
                }
                else
                {
                    readAddress = new IntPtr(startAddress.ToInt64() + i * READ_CHUNK_SIZE - ValueSize);
                    buffer = MM.ReadBytes(new IntPtr(readAddress.ToInt64()), READ_CHUNK_SIZE + ValueSize);
                    for (long j = 0; j < buffer.Length - ValueSize; j++)
                    {
                        Int64 dval = BitConverter.ToInt64(buffer, (int)j);
                        if (dval >= addressA && dval <= addressB)
                            Results.Add(new Tuple<IntPtr,long>(new IntPtr(readAddress.ToInt64() + j),dval));
                    }
                }

            }
            IntPtr finalAddress = new IntPtr(startAddress.ToInt64() + NumberOfScans * READ_CHUNK_SIZE - ValueSize);
            buffer = MM.ReadBytes(finalAddress, RemainingBytes + ValueSize);
            for (long j = 0; j < buffer.Length - ValueSize; j++)
            {
                Int64 dval = BitConverter.ToInt64(buffer, (int)j);
                if(dval>=addressA && dval <= addressB)
                    Results.Add(new Tuple<IntPtr,long>(new IntPtr(finalAddress.ToInt64() + j),dval));
            }

            return Results;
        }

        public List<Tuple<IntPtr, IntPtr>> GetPossibleLocations()
        {
            // Start by finding valid pages of memory and adding the range to a list.
            long MaxAddress = 0x7FFFFFFFFFF;
            long Address = 0;

            List<Tuple<IntPtr, IntPtr>> PossibleLocations = new List<Tuple<IntPtr, IntPtr>>();
            do
            {
                Memory.Kernel32.MEMORY_BASIC_INFORMATION m;
                int result = Kernel32.VirtualQueryEx(MM.ExternalProcess.Handle, new IntPtr(Address), out m,
                    (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(Memory.Kernel32.MEMORY_BASIC_INFORMATION)));

                /* MEM_COMMIT = 0x1000
                 * MEM_FREE = 0x10000
                 * MEM_RESERVE = 0x2000 */
                // Only scan committed memory.
                if (m.State == 0x1000)
                {
                    // Check memory protection to make sure PAGE_GUARD isn't enabled.
                    if ((m.Protect & (uint)Kernel32.AllocationProtect.PAGE_GUARD) == 0)
                        PossibleLocations.Add(new Tuple<IntPtr, IntPtr>(m.BaseAddress, new IntPtr(m.BaseAddress.ToInt64() + m.RegionSize.ToInt64())));
                }

                if (Address == (long)m.BaseAddress + (long)m.RegionSize)
                    break;
                Address = (long)m.BaseAddress + (long)m.RegionSize;
            } while (Address <= MaxAddress);

            return PossibleLocations;
        }
    }
}
