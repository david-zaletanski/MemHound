using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;

namespace MemHound.Memory
{
    public class ExternalMemManager
    {
        public const long ProcessUpperMemoryBound64 = 8796093022207;
        public Process ExternalProcess { get; private set; }

        public ExternalMemManager(Process p)
        {
            ExternalProcess = p;
        }

        #region Read Memory

        public Int16 ReadInt16(IntPtr address)
        {
            byte[] buffer = new byte[2];
            int bytesread;

            if (!Kernel32.ReadProcessMemory(ExternalProcess.Handle, address, buffer, buffer.Length, out bytesread))
            {
                Core.Output("Read process memory unsuccessful.\nLast Error #: '" + System.Runtime.InteropServices.Marshal.GetLastWin32Error() + "'", System.Drawing.Color.Red);
            }
            return BitConverter.ToInt16(buffer, 0);
        }

        public Int32 ReadInt32(IntPtr address)
        {
            byte[] buffer = new byte[4];
            int bytesread;

            if(!Kernel32.ReadProcessMemory(ExternalProcess.Handle, address, buffer, buffer.Length, out bytesread))
            {
                Core.Output("Read process memory unsuccessful.\nLast Error #: '" + System.Runtime.InteropServices.Marshal.GetLastWin32Error() + "'", System.Drawing.Color.Red);
            }
            return BitConverter.ToInt32(buffer, 0);
        }

        public UInt32 ReadUInt32(IntPtr address)
        {
            byte[] buffer = new byte[4];
            int bytesread;

            if (!Kernel32.ReadProcessMemory(ExternalProcess.Handle, address, buffer, buffer.Length, out bytesread))
            {
                Core.Output("Read process memory unsuccessful.\nLast Error #: '" + System.Runtime.InteropServices.Marshal.GetLastWin32Error() + "'", System.Drawing.Color.Red);
            }
            return BitConverter.ToUInt32(buffer, 0);
        }

        public Int64 ReadInt64(IntPtr address)
        {
            byte[] buffer = new byte[8];
            int bytesread;

            if(!Kernel32.ReadProcessMemory(ExternalProcess.Handle, address, buffer, buffer.Length, out bytesread))
            {
                Core.Output("Read process memory unsuccessful.\nLast Error #: '" + System.Runtime.InteropServices.Marshal.GetLastWin32Error() + "'", System.Drawing.Color.Red);
            }
            return BitConverter.ToInt64(buffer, 0);
        }

        public UInt64 ReadUInt64(IntPtr address)
        {
            byte[] buffer = new byte[8];
            int bytesread;

            if (!Kernel32.ReadProcessMemory(ExternalProcess.Handle, address, buffer, buffer.Length, out bytesread))
            {
                Core.Output("Read process memory unsuccessful.\nLast Error #: '" + System.Runtime.InteropServices.Marshal.GetLastWin32Error() + "'", System.Drawing.Color.Red);
            }
            return BitConverter.ToUInt64(buffer, 0);
        }

        public float ReadFloat(IntPtr address)
        {
            byte[] buffer = new byte[4];
            int bytesread;

            if (!Kernel32.ReadProcessMemory(ExternalProcess.Handle, address, buffer, buffer.Length, out bytesread))
            {
                Core.Output("Read process memory unsuccessful.\nLast Error #: '" + System.Runtime.InteropServices.Marshal.GetLastWin32Error() + "'", System.Drawing.Color.Red);
            }

            return BitConverter.ToSingle(buffer, 0);
        }

        public Double ReadDouble(IntPtr address)
        {
            byte[] buffer = new byte[8];
            int bytesread;

            if(!Kernel32.ReadProcessMemory(ExternalProcess.Handle, address, buffer, buffer.Length, out bytesread))
            {
                Core.Output("Read process memory unsuccessful.\nLast Error #: '" + System.Runtime.InteropServices.Marshal.GetLastWin32Error() + "'", System.Drawing.Color.Red);
            }
            return BitConverter.ToDouble(buffer, 0);
        }

        public byte[] ReadBytes(IntPtr address, int size)
        {
            byte[] buffer = new byte[size];
            int bytesread;

            if (!Kernel32.ReadProcessMemory(ExternalProcess.Handle, address, buffer, buffer.Length, out bytesread))
            {
                Core.Output("Read process memory unsuccessful.\nLast Error #: '" + System.Runtime.InteropServices.Marshal.GetLastWin32Error() + "'", System.Drawing.Color.Red);
            }
            return buffer;
        }

        public byte[] ReadBytes(IntPtr address, long size)
        {
            byte[] buffer = new byte[size];
            int bytesread;

            if (!Kernel32.ReadProcessMemory(ExternalProcess.Handle, address, buffer, buffer.Length, out bytesread))
            {
                Core.Output("Read process memory unsuccessful.\nLast Error #: '" + System.Runtime.InteropServices.Marshal.GetLastWin32Error() + "'", System.Drawing.Color.Red);
            }
            if (bytesread < size)
            {
                Core.Output("WARNING: ReadBytes did not read expected number of bytes.\nAt address "+address+" read "+bytesread+"/"+size, System.Drawing.Color.Orange);
            }
            return buffer;
        }

        #endregion

        #region Scan Memory

        public List<IntPtr> FindOccurancesInt32(IntPtr startAddress, IntPtr endAddress, Int32 value)
        {
            List<IntPtr> Results = new List<IntPtr>();
            long READ_CHUNK_SIZE = 67108864; // 64 MB
            long TotalReadSize = endAddress.ToInt64() - startAddress.ToInt64();
            long ValueSize=4; // Size (in bytes) of value to be read.
            byte[] buffer;

            // Case A: All bytes can be read in one chunk.
            if (TotalReadSize < READ_CHUNK_SIZE)
            {
                buffer = ReadBytes(startAddress, TotalReadSize);
                for (int j = 0; j < buffer.Length - ValueSize; j++)
                {
                    if (BitConverter.ToInt32(buffer, j).Equals(value))
                        Results.Add(new IntPtr(startAddress.ToInt64() + j));
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
                   buffer = ReadBytes(readAddress, READ_CHUNK_SIZE);
                   for (long j = 0; j < buffer.Length - ValueSize; j++)
                   {
                       if (BitConverter.ToInt32(buffer, (int)j).Equals(value))
                           Results.Add(new IntPtr(readAddress.ToInt64() + j));
                   }
                }
                else
                {
                    readAddress = new IntPtr(startAddress.ToInt64() + i * READ_CHUNK_SIZE - ValueSize);
                    buffer = ReadBytes(new IntPtr(readAddress.ToInt64()), READ_CHUNK_SIZE + ValueSize);
                    for (long j = 0; j < buffer.Length - ValueSize; j++)
                    {
                        if (BitConverter.ToInt32(buffer, (int)j).Equals(value))
                            Results.Add(new IntPtr(startAddress.ToInt64()+j));
                    }
                }

            }
            IntPtr finalAddress = new IntPtr(startAddress.ToInt64() + NumberOfScans * READ_CHUNK_SIZE - ValueSize);
            buffer = ReadBytes(finalAddress, RemainingBytes + ValueSize);
            for (long j = 0; j < buffer.Length - ValueSize; j++)
            {
                if (BitConverter.ToInt32(buffer, (int)j).Equals(value))
                    Results.Add(new IntPtr(finalAddress.ToInt64() + j));
            }

            return Results;
        }

        public List<IntPtr> FindOccurancesUInt32(IntPtr startAddress, IntPtr endAddress, UInt32 value)
        {
            List<IntPtr> Results = new List<IntPtr>();
            long READ_CHUNK_SIZE = 67108864; // 64 MB
            long TotalReadSize = endAddress.ToInt64() - startAddress.ToInt64();
            long ValueSize = 4; // Size (in bytes) of value to be read.
            byte[] buffer;

            // Case A: All bytes can be read in one chunk.
            if (TotalReadSize < READ_CHUNK_SIZE)
            {
                buffer = ReadBytes(startAddress, TotalReadSize);
                for (int j = 0; j < buffer.Length - ValueSize; j++)
                {
                    if (BitConverter.ToUInt32(buffer, j).Equals(value))
                        Results.Add(new IntPtr(startAddress.ToInt64() + j));
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
                    buffer = ReadBytes(readAddress, READ_CHUNK_SIZE);
                    for (long j = 0; j < buffer.Length - ValueSize; j++)
                    {
                        if (BitConverter.ToUInt32(buffer, (int)j).Equals(value))
                            Results.Add(new IntPtr(readAddress.ToInt64() + j));
                    }
                }
                else
                {
                    readAddress = new IntPtr(startAddress.ToInt64() + i * READ_CHUNK_SIZE - ValueSize);
                    buffer = ReadBytes(new IntPtr(readAddress.ToInt64()), READ_CHUNK_SIZE + ValueSize);
                    for (long j = 0; j < buffer.Length - ValueSize; j++)
                    {
                        if (BitConverter.ToUInt32(buffer, (int)j).Equals(value))
                            Results.Add(new IntPtr(startAddress.ToInt64() + j));
                    }
                }

            }
            IntPtr finalAddress = new IntPtr(startAddress.ToInt64() + NumberOfScans * READ_CHUNK_SIZE - ValueSize);
            buffer = ReadBytes(finalAddress, RemainingBytes + ValueSize);
            for (long j = 0; j < buffer.Length - ValueSize; j++)
            {
                if (BitConverter.ToUInt32(buffer, (int)j).Equals(value))
                    Results.Add(new IntPtr(finalAddress.ToInt64() + j));
            }

            return Results;
        }

        public List<IntPtr> FindOccurancesInt64(IntPtr startAddress, IntPtr endAddress, Int64 value)
        {
            List<IntPtr> Results = new List<IntPtr>();
            long READ_CHUNK_SIZE = 67108864; // 64 MB
            long TotalReadSize = endAddress.ToInt64() - startAddress.ToInt64();
            long ValueSize = 8; // Size (in bytes) of value to be read.
            byte[] buffer;

            // Case A: All bytes can be read in one chunk.
            if (TotalReadSize < READ_CHUNK_SIZE)
            {
                buffer = ReadBytes(startAddress, TotalReadSize);
                for (int j = 0; j < buffer.Length - ValueSize; j++)
                {
                    if (BitConverter.ToInt64(buffer, j).Equals(value))
                        Results.Add(new IntPtr(startAddress.ToInt64() + j));
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
                    buffer = ReadBytes(readAddress, READ_CHUNK_SIZE);
                    for (long j = 0; j < buffer.Length - ValueSize; j++)
                    {
                        if (BitConverter.ToInt64(buffer, (int)j).Equals(value))
                            Results.Add(new IntPtr(readAddress.ToInt64() + j));
                    }
                }
                else
                {
                    readAddress = new IntPtr(startAddress.ToInt64() + i * READ_CHUNK_SIZE - ValueSize);
                    buffer = ReadBytes(new IntPtr(readAddress.ToInt64()), READ_CHUNK_SIZE + ValueSize);
                    for (long j = 0; j < buffer.Length - ValueSize; j++)
                    {
                        if (BitConverter.ToInt64(buffer, (int)j).Equals(value))
                            Results.Add(new IntPtr(startAddress.ToInt64() + j));
                    }
                }

            }
            IntPtr finalAddress = new IntPtr(startAddress.ToInt64() + NumberOfScans * READ_CHUNK_SIZE - ValueSize);
            buffer = ReadBytes(finalAddress, RemainingBytes + ValueSize);
            for (long j = 0; j < buffer.Length - ValueSize; j++)
            {
                if (BitConverter.ToInt64(buffer, (int)j).Equals(value))
                    Results.Add(new IntPtr(finalAddress.ToInt64() + j));
            }

            return Results;
        }

        public List<IntPtr> FindOccurancesUInt64(IntPtr startAddress, IntPtr endAddress, UInt64 value)
        {
            List<IntPtr> Results = new List<IntPtr>();
            long READ_CHUNK_SIZE = 67108864; // 64 MB
            long TotalReadSize = endAddress.ToInt64() - startAddress.ToInt64();
            long ValueSize = 8; // Size (in bytes) of value to be read.
            byte[] buffer;

            // Case A: All bytes can be read in one chunk.
            if (TotalReadSize < READ_CHUNK_SIZE)
            {
                buffer = ReadBytes(startAddress, TotalReadSize);
                for (int j = 0; j < buffer.Length - ValueSize; j++)
                {
                    if (BitConverter.ToUInt64(buffer, j).Equals(value))
                        Results.Add(new IntPtr(startAddress.ToInt64() + j));
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
                    buffer = ReadBytes(readAddress, READ_CHUNK_SIZE);
                    for (long j = 0; j < buffer.Length - ValueSize; j++)
                    {
                        if (BitConverter.ToUInt64(buffer, (int)j).Equals(value))
                            Results.Add(new IntPtr(readAddress.ToInt64() + j));
                    }
                }
                else
                {
                    readAddress = new IntPtr(startAddress.ToInt64() + i * READ_CHUNK_SIZE - ValueSize);
                    buffer = ReadBytes(new IntPtr(readAddress.ToInt64()), READ_CHUNK_SIZE + ValueSize);
                    for (long j = 0; j < buffer.Length - ValueSize; j++)
                    {
                        if (BitConverter.ToUInt64(buffer, (int)j).Equals(value))
                            Results.Add(new IntPtr(startAddress.ToInt64() + j));
                    }
                }

            }
            IntPtr finalAddress = new IntPtr(startAddress.ToInt64() + NumberOfScans * READ_CHUNK_SIZE - ValueSize);
            buffer = ReadBytes(finalAddress, RemainingBytes + ValueSize);
            for (long j = 0; j < buffer.Length - ValueSize; j++)
            {
                if (BitConverter.ToUInt64(buffer, (int)j).Equals(value))
                    Results.Add(new IntPtr(finalAddress.ToInt64() + j));
            }

            return Results;
        }

        public List<IntPtr> FindOccurancesFloat(IntPtr startAddress, IntPtr endAddress, float value)
        {
            List<IntPtr> Results = new List<IntPtr>();
            long READ_CHUNK_SIZE = 67108864; // 64 MB
            long TotalReadSize = endAddress.ToInt64() - startAddress.ToInt64();
            long ValueSize = 4; // Size (in bytes) of value to be read.
            byte[] buffer;

            // Case A: All bytes can be read in one chunk.
            if (TotalReadSize < READ_CHUNK_SIZE)
            {
                buffer = ReadBytes(startAddress, TotalReadSize);
                for (int j = 0; j < buffer.Length - ValueSize; j++)
                {
                    if (BitConverter.ToSingle(buffer, j).Equals(value))
                        Results.Add(new IntPtr(startAddress.ToInt64() + j));
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
                    buffer = ReadBytes(readAddress, READ_CHUNK_SIZE);
                    for (long j = 0; j < buffer.Length - ValueSize; j++)
                    {
                        if (BitConverter.ToSingle(buffer, (int)j).Equals(value))
                            Results.Add(new IntPtr(readAddress.ToInt64() + j));
                    }
                }
                else
                {
                    readAddress = new IntPtr(startAddress.ToInt64() + i * READ_CHUNK_SIZE - ValueSize);
                    buffer = ReadBytes(new IntPtr(readAddress.ToInt64()), READ_CHUNK_SIZE + ValueSize);
                    for (long j = 0; j < buffer.Length - ValueSize; j++)
                    {
                        if (BitConverter.ToSingle(buffer, (int)j).Equals(value))
                            Results.Add(new IntPtr(startAddress.ToInt64() + j));
                    }
                }

            }
            IntPtr finalAddress = new IntPtr(startAddress.ToInt64() + NumberOfScans * READ_CHUNK_SIZE - ValueSize);
            buffer = ReadBytes(finalAddress, RemainingBytes + ValueSize);
            for (long j = 0; j < buffer.Length - ValueSize; j++)
            {
                if (BitConverter.ToSingle(buffer, (int)j).Equals(value))
                    Results.Add(new IntPtr(finalAddress.ToInt64() + j));
            }

            return Results;
        }

        public List<IntPtr> FindOccurancesDouble(IntPtr startAddress, IntPtr endAddress, double value)
        {
            List<IntPtr> Results = new List<IntPtr>();
            long READ_CHUNK_SIZE = 67108864; // 64 MB
            long TotalReadSize = endAddress.ToInt64() - startAddress.ToInt64();
            long ValueSize = 8; // Size (in bytes) of value to be read.
            byte[] buffer;

            // Case A: All bytes can be read in one chunk.
            if (TotalReadSize < READ_CHUNK_SIZE)
            {
                buffer = ReadBytes(startAddress, TotalReadSize);
                for (int j = 0; j < buffer.Length - ValueSize; j++)
                {
                    if (BitConverter.ToDouble(buffer, j).Equals(value))
                        Results.Add(new IntPtr(startAddress.ToInt64() + j));
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
                    buffer = ReadBytes(readAddress, READ_CHUNK_SIZE);
                    for (long j = 0; j < buffer.Length - ValueSize; j++)
                    {
                        if (BitConverter.ToDouble(buffer, (int)j).Equals(value))
                            Results.Add(new IntPtr(readAddress.ToInt64() + j));
                    }
                }
                else
                {
                    readAddress = new IntPtr(startAddress.ToInt64() + i * READ_CHUNK_SIZE - ValueSize);
                    buffer = ReadBytes(new IntPtr(readAddress.ToInt64()), READ_CHUNK_SIZE + ValueSize);
                    for (long j = 0; j < buffer.Length - ValueSize; j++)
                    {
                        if (BitConverter.ToDouble(buffer, (int)j).Equals(value))
                            Results.Add(new IntPtr(startAddress.ToInt64() + j));
                    }
                }

            }
            IntPtr finalAddress = new IntPtr(startAddress.ToInt64() + NumberOfScans * READ_CHUNK_SIZE - ValueSize);
            buffer = ReadBytes(finalAddress, RemainingBytes + ValueSize);
            for (long j = 0; j < buffer.Length - ValueSize; j++)
            {
                if (BitConverter.ToDouble(buffer, (int)j).Equals(value))
                    Results.Add(new IntPtr(finalAddress.ToInt64() + j));
            }

            return Results;
        }

        #endregion

        #region Write Memory

        public bool WriteInt32(IntPtr address, Int32 value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            UIntPtr result = UIntPtr.Zero;

            bool worked = Kernel32.WriteProcessMemory(ExternalProcess.Handle, address, buffer, (uint)buffer.Length, out result);

            if (result.ToUInt32() != buffer.Length)
            {
                Core.Output("Warning: WriteProcessMemory did not write as many bytes as expected (" + result + "/" + buffer.Length + ")", System.Drawing.Color.Orange);
            }

            return worked;
        }

        public bool WriteInt64(IntPtr address, Int64 value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            UIntPtr result = UIntPtr.Zero;

            bool worked = Kernel32.WriteProcessMemory(ExternalProcess.Handle, address, buffer, (uint)buffer.Length, out result);

            if (result.ToUInt32() != buffer.Length)
            {
                Core.Output("Warning: WriteProcessMemory did not write as many bytes as expected (" + result + "/" + buffer.Length + ")", System.Drawing.Color.Orange);
            }

            return worked;
        }

        public bool WriteFloat(IntPtr address, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            UIntPtr result = UIntPtr.Zero;

            bool worked = Kernel32.WriteProcessMemory(ExternalProcess.Handle, address, buffer, (uint)buffer.Length, out result);

            if (result.ToUInt32() != buffer.Length)
            {
                Core.Output("Warning: WriteProcessMemory did not write as many bytes as expected (" + result + "/" + buffer.Length + ")", System.Drawing.Color.Orange);
            }

            return worked;
        }

        public bool WriteDouble(IntPtr address, double value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            UIntPtr result = UIntPtr.Zero;

            bool worked = Kernel32.WriteProcessMemory(ExternalProcess.Handle, address, buffer, (uint)buffer.Length, out result);

            if (result.ToUInt32() != buffer.Length)
            {
                Core.Output("Warning: WriteProcessMemory did not write as many bytes as expected (" + result + "/" + buffer.Length + ")", System.Drawing.Color.Orange);
            }

            return worked;
        }

        #endregion
    }
}
