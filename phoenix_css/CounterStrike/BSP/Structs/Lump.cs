using Phoenix.CounterStrike.BSP.Enums;

namespace Phoenix.CounterStrike.BSP.Structs
{
    internal struct Lump
    {
        public int offset, length, version, fourCC;
        public LumpType type;
    }
}
