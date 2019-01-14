using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.CounterStrike.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct TraceFilter
    {
        [FieldOffset(0x00)]
        public IntPtr Skip;
    }
}
