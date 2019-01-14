using Phoenix.CommandSystem;
using Phoenix.CounterStrike;
using Phoenix.CounterStrike.Enums;
using Phoenix.MemorySystem;
using Phoenix.Overlay;
using Phoenix.Structs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Features
{
	internal class ESP
	{
		private static ProcessMemory Memory => Phoenix.Memory;
		private static OverlayWindow Overlay => Phoenix.Overlay;
		private static int Red;
		public static void Run()
		{
			if (Red == 0)
				Red = Overlay.Graphics.CreateBrush(Color.Red);

			if (!CommandHandler.GetParameter("esp", "active").Value.ToBool()) return;
			if (Phoenix.EntityList == null) return;
			if (Phoenix.EntityList.Players == null || Phoenix.EntityList.Players.Count < 1) return;
			var pLocal = Phoenix.EntityList.GetLocalPlayer();
			if (pLocal == null) return;

			foreach(var player in Phoenix.EntityList.Players)
			{
				if (player.IsDormant()) continue;
				if (player.GetTeam() == pLocal.GetTeam()) continue;
				if (player.GetLifeState() != LifeState.Alive) continue;
				var origin = player.GetPosition();
				if (origin.IsEmpty()) continue;
				var screenOrigin = BaseClient.WorldToScreen(origin);
				if (screenOrigin.IsEmpty()) continue;
				var max = player.GetCollidableMax();
				origin += new Vector3D(0, 0, max.Z);
				var top = BaseClient.WorldToScreen(origin);
				if (top.IsEmpty()) continue;
				var height = screenOrigin.Y - top.Y;
				var width = height / 3.5f;
				Overlay.Graphics.DrawCorneredBoxOutline((int)(top.X - width), (int)top.Y, (int)(width * 2), (int)height, Red);
			}
		}
	}
}
