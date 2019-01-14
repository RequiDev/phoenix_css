using Phoenix.Structs;
using System;
using System.Runtime.InteropServices;

namespace Phoenix.CounterStrike.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct Ray
    {
        [FieldOffset(0x00)]
        Vector3D m_Start;
        [FieldOffset(0x0C)]
        Vector3D m_Delta;
        [FieldOffset(0x18)]
        Vector3D m_StartOffset;
        [FieldOffset(0x24)]
        Vector3D m_Extents;
        [FieldOffset(0x30)]
        IntPtr m_pWorldAxisTransform;
        [FieldOffset(0x34)]
        byte m_IsRay;
        [FieldOffset(0x35)]
        byte m_IsSwept;

        public void Init(Vector3D start, Vector3D end)
        {
            m_Delta = end - start;

            m_IsSwept = (byte)((m_Delta.LengthSqr() != 0) ? 1 : 0);

            m_Extents = new Vector3D();

            m_IsRay = 1;

            m_StartOffset = start;
        }
    }
}
