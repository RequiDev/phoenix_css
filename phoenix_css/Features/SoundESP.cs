using Phoenix.CommandSystem;
using Phoenix.CounterStrike;
using Phoenix.CounterStrike.Enums;
using Phoenix.CounterStrike.Models;
using Phoenix.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Features
{
    internal class SoundESP
    {
        private static long lastTick = 0;
        public static void Run()
        {
			if (Phoenix.Overlay == null) return;
            var pointCrosshair = new Vector2D(Phoenix.Overlay.Width / 2, Phoenix.Overlay.Height / 2);
            if (CommandHandler.GetParameter("soundesp", "fov").Value.ToFloat() < 0.1f) return;
            if (!CommandHandler.GetParameter("soundesp", "active").Value.ToBool()) return;
            if (Phoenix.EntityList == null) return;
            if (Phoenix.EntityList.Players == null || Phoenix.EntityList.Players.Count < 1) return;
            var pLocal = Phoenix.EntityList.GetLocalPlayer();
            if (pLocal == null) return;
            var closestPlayer = GetClosestPlayer();
            if (closestPlayer.Key == null) return;
            if (lastTick < DateTime.Now.Ticks)
            {
                Console.Beep();
                lastTick = DateTime.Now.AddMilliseconds(closestPlayer.Value).Ticks;
            }
        }

        private static KeyValuePair<BasePlayer, float> GetClosestPlayer()
        {
            var fov = CommandHandler.GetParameter("soundesp", "fov").Value.ToFloat();
            var pointCrosshair = new Vector2D(Phoenix.Overlay.Width / 2, Phoenix.Overlay.Height / 2);

            BasePlayer result = null;
            var localPlayer = Phoenix.EntityList.GetLocalPlayer();
            var radius = fov * Phoenix.Overlay.Width / localPlayer.GetFieldOfView();
            float maxDistance = float.MaxValue;
            foreach (var player in Phoenix.EntityList.Players)
            {
				if (player == localPlayer) continue;
				if (player.GetTeam() == localPlayer.GetTeam()) continue;
				//if (player.IsDormant()) continue;
				var health = player.GetHealth();
				if (health <= 1) continue;
				if (player.GetLifeState() != LifeState.Alive) continue;
				var bone = player.GetBonePos(14);

                var w2sBone = BaseClient.WorldToScreen(bone);

                var distance = Utils.DistanceToPoint(pointCrosshair, w2sBone);

                if (distance < maxDistance && Utils.IsPointInRadius(w2sBone, pointCrosshair, radius))
                {
                    maxDistance = distance;
                    result = player;
                }
            }
            return new KeyValuePair<BasePlayer, float>(result, maxDistance);
        }
    }
}
