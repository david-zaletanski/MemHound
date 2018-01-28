using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemHound.Memory
{
    class SmartMemoryScanner
    {
        ExternalMemManager MemoryManager;

        public SmartMemoryScanner(ExternalMemManager MM)
        {
            this.MemoryManager = MM;
        }

        public List<IntPtr> FindOccurrencesInt32(Int32 value)
        {
            List<IntPtr> results = new List<IntPtr>();

            List<Tuple<IntPtr, IntPtr>> PossibleLocations = GetPossibleLocations();

            // Scan the possible areas for a result.
            foreach(Tuple<IntPtr,IntPtr> pl in PossibleLocations) {
                results.AddRange(MemoryManager.FindOccurancesInt32(pl.Item1,pl.Item2,value));
            }

            return results;
        }

        public List<IntPtr> FindOccurrencesUInt32(UInt32 value)
        {
            List<IntPtr> results = new List<IntPtr>();

            List<Tuple<IntPtr, IntPtr>> PossibleLocations = GetPossibleLocations();

            // Scan the possible areas for a result.
            foreach (Tuple<IntPtr, IntPtr> pl in PossibleLocations)
            {
                results.AddRange(MemoryManager.FindOccurancesUInt32(pl.Item1, pl.Item2, value));
            }

            return results;
        }

        public List<IntPtr> FindOccurrencesInt64(Int64 value)
        {
            List<IntPtr> results = new List<IntPtr>();

            List<Tuple<IntPtr, IntPtr>> PossibleLocations = GetPossibleLocations();

            // Scan the possible areas for a result.
            foreach (Tuple<IntPtr, IntPtr> pl in PossibleLocations)
            {
                results.AddRange(MemoryManager.FindOccurancesInt64(pl.Item1, pl.Item2, value));
            }

            return results;
        }

        public List<IntPtr> FindOccurrencesUInt64(UInt64 value)
        {
            List<IntPtr> results = new List<IntPtr>();

            List<Tuple<IntPtr, IntPtr>> PossibleLocations = GetPossibleLocations();

            // Scan the possible areas for a result.
            foreach (Tuple<IntPtr, IntPtr> pl in PossibleLocations)
            {
                results.AddRange(MemoryManager.FindOccurancesUInt64(pl.Item1, pl.Item2, value));
            }

            return results;
        }

        public List<IntPtr> FindOccurrencesFloat(float value)
        {
            List<IntPtr> results = new List<IntPtr>();

            List<Tuple<IntPtr, IntPtr>> PossibleLocations = GetPossibleLocations();

            // Scan the possible areas for a result.
            foreach (Tuple<IntPtr, IntPtr> pl in PossibleLocations)
            {
                results.AddRange(MemoryManager.FindOccurancesFloat(pl.Item1, pl.Item2, value));
            }

            return results;
        }

        public List<IntPtr> FindOccurrencesDouble(double value)
        {
            List<IntPtr> results = new List<IntPtr>();

            List<Tuple<IntPtr, IntPtr>> PossibleLocations = GetPossibleLocations();

            // Scan the possible areas for a result.
            foreach (Tuple<IntPtr, IntPtr> pl in PossibleLocations)
            {
                results.AddRange(MemoryManager.FindOccurancesDouble(pl.Item1, pl.Item2, value));
            }

            return results;
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
                int result = Kernel32.VirtualQueryEx(MemoryManager.ExternalProcess.Handle, new IntPtr(Address), out m,
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
