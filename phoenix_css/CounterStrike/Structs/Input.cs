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
    internal struct Input
    {
        [FieldOffset(0)]
        public uint m_pVftable;

        [FieldOffset(4)]
        public bool m_bTrackIRAvailable;

        [FieldOffset(5)]
        public bool m_bMouseInitialized;

        [FieldOffset(6)]
        public bool m_bMouseActive;

        [FieldOffset(7)]
        public bool m_bJoystickAdvancedInit;

        [FieldOffset(0x34)]
        public uint m_pKeys;

        [FieldOffset(0x9C)]
        public bool m_bCameraInterceptingMouse;

        [FieldOffset(0x9D)]
        public bool m_bCameraInThirdPerson;

        [FieldOffset(0x9E)]
        public bool m_bCameraMovingWithMouse;

        [FieldOffset(0xA0)]
        public Vector3D m_vecCameraOffset;

        [FieldOffset(0xAC)]
        public bool m_bCameraDistanceMove;

        [FieldOffset(0xB0)]
        public int m_nCameraOldX;

        [FieldOffset(0xB4)]
        public int m_nCameraOldY;

        [FieldOffset(0xB8)]
        public int m_nCameraX;

        [FieldOffset(0xBC)]
        public int m_nCameraY;

        [FieldOffset(0xC0)]
        public bool m_bCameraIsOrthographic;

        [FieldOffset(0xC4)]
        public Vector3D m_vecPreviousViewAngles;

        [FieldOffset(0xD0)]
        public Vector3D m_vecPreviousViewAnglesTilt;

        [FieldOffset(0xDC)]
        public float m_flLastForwardMove;

        [FieldOffset(0xE0)]
        public int m_nClearInputState;

        [FieldOffset(0xEC)]
        public uint m_pCommands;

        [FieldOffset(0xF0)]
        public uint m_pVerifiedCommands;
    }
}
