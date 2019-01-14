using Phoenix.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.CounterStrike.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct UserCmd
    {
        [FieldOffset(0)]
        public uint pVft;

        [FieldOffset(0x4)]
        public int m_iCmdNumber;

        [FieldOffset(0x8)]
        public int m_iTickCount;

        [FieldOffset(0xC)]
        public Vector3D m_vecViewAngles;

        [FieldOffset(0x18)]
        public Vector3D m_vecAimDirection;

        [FieldOffset(0x24)]
        public float m_flForwardMove;

        [FieldOffset(0x28)]
        public float m_flSideMove;

        [FieldOffset(0x2C)]
        public float m_flUpMove;

        [FieldOffset(0x30)]
        public int m_iButtons;

        [FieldOffset(0x34)]
        public bool m_bImpulse;

        [FieldOffset(0x38)]
        public int m_iWeaponSelect;

        [FieldOffset(0x3C)]
        public int m_iWeaonSubtype;

        [FieldOffset(0x40)]
        public int m_iRandomSeed;

        [FieldOffset(0x44)]
        public short m_siMouseDx;

        [FieldOffset(0x46)]
        public short m_siMouseDy;

        [FieldOffset(0x48)]
        public bool m_bHasBeenPredicted;

        [FieldOffset(0x63)]
        public byte Unk_2;
    }
}
