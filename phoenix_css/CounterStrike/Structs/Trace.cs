using Phoenix.CounterStrike.BSP.Structs;
using Phoenix.Structs;
using System;
using System.Runtime.InteropServices;

namespace Phoenix.CounterStrike.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct Trace
    {
        [FieldOffset(0x00)]
        public Vector3D startpos;
        [FieldOffset(0x0C)]
        public Vector3D endpos;
        [FieldOffset(0x18)]
        public Plane plane;
        [FieldOffset(0x2C)]
        public float fraction;
        [FieldOffset(0x30)]
        public int contents;
        [FieldOffset(0x34)]
        public ushort dispFlags;
        [FieldOffset(0x36)]
        public byte allsolid;
        [FieldOffset(0x37)]
        public byte startsolid;
        [FieldOffset(0x38)]
        public float fractionleftsolid;  // time we left a solid, only valid if we started in solid
        [FieldOffset(0x3C)]
        public Surface surface;            // surface hit (impact surface)
        [FieldOffset(0x44)]
        public int hitgroup;           // 0 == generic, non-zero is specific body part
        [FieldOffset(0x48)]
        public short physicsbone;        // physics bone hit by trace in studio
        [FieldOffset(0x4A)]
        public ushort worldSurfaceIndex;  // Index of the msurface2_t, if applicable
        [FieldOffset(0x4C)]
        public IntPtr m_pEnt;
        [FieldOffset(0x50)]
        public int hitbox;                       // box hit by trace in studio
    }
}
