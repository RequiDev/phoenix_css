using Phoenix.CounterStrike.BSP;
using Phoenix.Extensions;
using Phoenix.MemorySystem;
using Phoenix.Structs;
using System;
using System.Threading;

namespace Phoenix.CounterStrike
{
    internal static class EngineClient
    {
        private static ProcessMemory Memory => Phoenix.Memory;
        private static IntPtr clientState;
        private static IntPtr viewAngle;
        private static IntPtr localIndex;
        private static IntPtr mapName;
        private static IntPtr gameDir;
        private static string previousMap;
        private static BspFile cachedMap;
        

        public static IntPtr ClientState
        {
            get
            {
                while (clientState == IntPtr.Zero)
                {
                    Thread.Sleep(10);
                    clientState = SignatureManager.GetClientState();
                }
                return clientState;
            }
        }

        public static int LocalPlayerIndex
        {
            get
            {
                var index = Memory.Read<int>(ClientState + 0x1AC);
                return index;
            }
        }

        public static string MapName
        {
            get
            {
                if (mapName == IntPtr.Zero)
                {
					mapName = new IntPtr(0x1B0);
                }
                var name = Memory.ReadString(ClientState.Add(mapName));
                return name;
            }
        }

        public static string GameDirectory
        {
            get
            {
                if (gameDir == IntPtr.Zero)
                {
                    gameDir = SignatureManager.GetGameDir();
                }
                return Memory.ReadString(gameDir);
            }
        }

        public static BspFile Map
        {
            get
            {
                var currentMap = MapName;
                if((previousMap != currentMap || cachedMap == null) && currentMap.EndsWith(".bsp"))
                {
                    previousMap = currentMap;
                    cachedMap = new BspFile(currentMap);
                }
                return cachedMap;
            }
        }

        public static Vector3D ViewAngles
        {
            get
            {
                if (viewAngle == IntPtr.Zero)
                    viewAngle = SignatureManager.GetViewAngle();
                return Memory.Read<Vector3D>(ClientState.Add(viewAngle));
            }
            set
            {
                if (viewAngle == IntPtr.Zero)
                    viewAngle = SignatureManager.GetViewAngle();
                Memory.Write(ClientState.Add(viewAngle), value); //4B84

			}
        }
    }
}
