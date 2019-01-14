using System;

namespace Phoenix.CounterStrike.Enums
{
    [Flags]
    internal enum PlayerFlag
    {
        OnGround = 1 << 0,
        Ducking = 1 << 1,
        WaterJump = 1 << 2,
        OnTrain = 1 << 3,
        InTrain = 1 << 4,
        Frozen = 1 << 5,
        AtControls = 1 << 6,
        Client = 1 << 7,
        Fakeclient = 1 << 8,
        InWater = 1 << 9
    }
}
