using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.CounterStrike.BSP.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct Surface
    {
        [FieldOffset(0x00)]
        public IntPtr name;
        [FieldOffset(0x04)]
        public short surfaceProps;
        [FieldOffset(0x06)]
        public ushort flags;
    }
}
