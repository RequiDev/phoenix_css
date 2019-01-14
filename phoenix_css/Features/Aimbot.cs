using Phoenix.CommandSystem;
using Phoenix.CounterStrike;
using Phoenix.CounterStrike.Enums;
using Phoenix.CounterStrike.Models;
using Phoenix.CounterStrike.Structs;
using Phoenix.MemorySystem;
using Phoenix.Structs;
using System;

namespace Phoenix.Features
{
    internal class Aimbot
    {
        private const double RAD_PI = 180 / Math.PI;
        private const double DEG_PI = Math.PI / 180;

        private static ProcessMemory Memory => Phoenix.Memory;
        public static void Run()
        {
            if (CommandHandler.GetParameter("aimbot", "fov").Value.ToFloat() < 0.1f) return;
            if (!Utils.IsKeyDown(CommandHandler.GetParameter("aimbot", "key").Value.ToInt32())) return;
            if (NativeMethods.GetForegroundWindow() != Memory.MainWindowHandle) return;
            if (Phoenix.EntityList == null) return;
            if (Phoenix.EntityList.Players == null || Phoenix.EntityList.Players.Count < 1) return;
            if (Phoenix.EntityList.Entities == null || Phoenix.EntityList.Entities.Count < 1) return;
            var pLocal = Phoenix.EntityList.GetLocalPlayer();
            if (pLocal == null) return;
			var pWeapon = pLocal.GetWeapon();
			if (pWeapon == null) return;
			if (pWeapon.GetAmmo() < 1) return;
			//Console.WriteLine(Memory.GetVirtualFunction(pWeapon.Address, 360));
			var closestPlayer = GetClosestPlayer();
            if (closestPlayer == null) return;
            var bonePos = closestPlayer.GetBonePos(CommandHandler.GetParameter("aimbot", "bone").Value.ToInt32());
            if (bonePos.IsEmpty()) return;
            var angle = CalculateAngle(pLocal.GetEyePos(), bonePos);
            if (CommandHandler.GetParameter("aimbot", "norecoil").Value.ToBool())
                angle -= pLocal.GetAimPunchAngle() * 2.0f;

            angle = SmoothAngle(angle, CommandHandler.GetParameter("aimbot", "smooth").Value.ToFloat() + 1);

            if (angle != Vector3D.Zero) EngineClient.ViewAngles = angle;
        }

        private static Vector3D SmoothAngle(Vector3D src, float smooth)
        {
			var localAngle = EngineClient.ViewAngles;
			src -= localAngle;
			src = NormalizeAndClamp(src);
			src /= smooth;
			src = NormalizeAndClamp(src);
			src += localAngle;
			src = NormalizeAndClamp(src);
			return src;
		}

        public static Vector3D NormalizeAndClamp(Vector3D angle)
        {
            if (float.IsInfinity(angle.X)) return Vector3D.Zero;
            if (float.IsInfinity(angle.Y)) return Vector3D.Zero;
            if (float.IsNaN(angle.X)) return Vector3D.Zero;
            if (float.IsNaN(angle.Y)) return Vector3D.Zero;
            Vector3D retAngle = angle;
			while (retAngle.X > 180.0f) retAngle.X -= 360.0f;
			while (retAngle.X < -180.0f) retAngle.X += 360.0f;
			while (retAngle.Y > 180.0f) retAngle.Y -= 360.0f;
            while (retAngle.Y < -180.0f) retAngle.Y += 360.0f;
            if (retAngle.X > 89.0f) retAngle.X = 89.0f;
            if (retAngle.X < -89.0f) retAngle.X = -89.0f;
            retAngle.Z = 0.0f;
            return retAngle;
        }

        private static Vector3D CalculateAngle(Vector3D src, Vector3D dst)
        {
            var delta = src - dst;
            var hypotenuse = (float)Math.Sqrt(Math.Pow(delta.X, 2) + Math.Pow(delta.Y, 2));

            var angles = new Vector3D
            {
                X = Convert.ToSingle(Math.Atan(delta.Z / hypotenuse) * RAD_PI),
                Y = Convert.ToSingle(Math.Atan(delta.Y / delta.X) * RAD_PI),
                Z = 0.0f
            };
			if (delta.X >= 0.0) { angles.Y += 180.0f; }
			return angles;
        }

        private static BasePlayer GetClosestPlayer()
        {
            var fov = CommandHandler.GetParameter("aimbot", "fov").Value.ToFloat();
            var pointCrosshair = new Vector2D(Phoenix.Overlay.Width / 2, Phoenix.Overlay.Height / 2);

            BasePlayer result = null;
            var localPlayer = Phoenix.EntityList.GetLocalPlayer();
            var radius = fov * Phoenix.Overlay.Width / localPlayer.GetFieldOfView();
            float maxDistance = float.MaxValue;
            foreach (var player in Phoenix.EntityList.Players)
            {
                if (player == localPlayer) continue;
                if (player.GetTeam() == localPlayer.GetTeam()) continue;
                if (player.IsDormant()) continue;
				var health = player.GetHealth();
                //if (health <= 1) continue;
				if (player.GetLifeState() != LifeState.Alive) continue;
                var bone = player.GetBonePos(CommandHandler.GetParameter("aimbot", "bone").Value.ToInt32());
				if (CommandHandler.GetParameter("aimbot", "visible").Value.ToBool() && !EngineClient.Map.IsVisible(localPlayer.GetEyePos(), bone)) continue;

                var w2sBone = BaseClient.WorldToScreen(bone);

                var distance = Utils.DistanceToPoint(pointCrosshair, w2sBone);

                if (distance < maxDistance && Utils.IsPointInRadius(w2sBone, pointCrosshair, radius))
                {
                    maxDistance = distance;
                    result = player;
                }
            }
            return result;
        }
    }
}
