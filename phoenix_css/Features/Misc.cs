using Phoenix.CommandSystem;
using Phoenix.CounterStrike.Enums;
using Phoenix.Extensions;
using Phoenix.MemorySystem;
using System;
using System.Linq;
using System.Threading;

namespace Phoenix.Features
{
    internal class Misc
    {
        private static ProcessMemory Memory => Phoenix.Memory;
        
        internal static void Bunnyhop()
        {
            if (!CommandHandler.GetParameter("misc", "bhop").Value.ToBool()) return;
            if (NativeMethods.GetForegroundWindow() != Memory.MainWindowHandle) return;
            if (!Utils.IsKeyDown(System.Windows.Forms.Keys.Space)) return;
            if (Phoenix.EntityList == null) return;
            if (Phoenix.EntityList.Players == null || Phoenix.EntityList.Players.Count < 1) return;
            var pLocal = Phoenix.EntityList.GetLocalPlayer();
            if (pLocal == null) return;
            if (pLocal.GetFlags().Check(PlayerFlag.OnGround))
            {
                NativeMethods.SendMessage(Memory.MainWindowHandle, 0x100, 0x20, 0x390000);
                NativeMethods.SendMessage(Memory.MainWindowHandle, 0x101, 0x20, 0x390000);
            }
        }

		internal static void NoFlash()
		{
			if (Phoenix.EntityList == null) return;
			if (Phoenix.EntityList.Players == null || Phoenix.EntityList.Players.Count < 1) return;
			var pLocal = Phoenix.EntityList.GetLocalPlayer();
			if (pLocal == null) return;
			pLocal.FlashAlpha = CommandHandler.GetParameter("misc", "noflash").Value.ToBool() ? 0 : 255;
		}
	}
}
