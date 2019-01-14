using System;
using System.Runtime.InteropServices;

namespace Phoenix.CounterStrike.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct GlowObject
    {
        [FieldOffset(0x00)]
        public IntPtr Entity;
        [FieldOffset(0x04)]
        public float R;
        [FieldOffset(0x08)]
        public float G;
        [FieldOffset(0x0C)]
        public float B;
        [FieldOffset(0x10)]
        public float A;

        [FieldOffset(0x14)]
        public int junk01;
        [FieldOffset(0x18)]
        public int junk02;
        [FieldOffset(0x1C)]
        public int junk03;
        [FieldOffset(0x20)]
        public int junk04;

        [FieldOffset(0x24)]
        public bool m_bRenderWhenOccluded;
        [FieldOffset(0x25)]
        public bool m_bRenderWhenUnoccluded;
        [FieldOffset(0x26)]
        public bool m_bFullBloom;

        [FieldOffset(0x2A)]
        public int junk05;
        [FieldOffset(0x2E)]
        public int junk06;
        [FieldOffset(0x32)]
        public int junk07;
        [FieldOffset(0x36)]
        public short junk08;
    }
}
