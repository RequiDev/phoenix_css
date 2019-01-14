using System.Runtime.InteropServices;

namespace Phoenix.CounterStrike.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct VerifiedUserCmd
    {
        public UserCmd m_Command;
        public uint m_Crc;
    }
}
