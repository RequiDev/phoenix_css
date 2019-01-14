using System;
using System.Diagnostics;
using System.Text;

namespace Phoenix.MemorySystem
{
    internal static class SignatureManager
    {
        private static ProcessMemory Memory => Phoenix.Memory;

        public static IntPtr GetViewAngle()
        {
            return Phoenix.Engine.Find("C2 ? ? D9 40 ? D9 99 ? ? ? ? D9 40 ? D9 99", 8, 8, Enums.ScanMethod.Read);
        }

        public static IntPtr GetInput()
        {
            return Phoenix.Client.Find("B9 ? ? ? ? FF 75 08 E8 ? ? ? ? 8B 06", 1, 0, Enums.ScanMethod.Read);
        }

        public static IntPtr GetCrosshairId()
        {
            return Phoenix.Client.Find("8B 81 ? ? ? ? 85 C0 75 15", 2, 0, Enums.ScanMethod.Read);
        }

        public static IntPtr GetLineInSmoke()
        {
            return Phoenix.Client.Find("83 EC 44 8B 15", 0, 0, Enums.ScanMethod.Add) - 3;
        }

        public static IntPtr GetRankRevealer()
        {
            return Phoenix.Client.Find("55 8B EC 8B 0D ? ? ? ? 68 ? ? ? ?", 0, 0, Enums.ScanMethod.Add);
        }

        public static IntPtr GetGlowObjectBase()
        {
            return Phoenix.Client.Find("E8 00 00 00 00 83 C4 04 B8 00 00 00 00 C3 CC", 9, 0, Enums.ScanMethod.Read);
        }

        public static IntPtr GetEntityList()
        {
			return Phoenix.Client.BaseAddress + 0x4D3904;

			return Phoenix.Client.Find("CC A1 ? ? ? ? C3 CC", 2, 0, Enums.ScanMethod.Read); //localplayer
		}

        public static IntPtr GetModelInfo()
        {
            return Phoenix.Engine.Find("74 29 50 A1 00 00 00 00 B9 00 00 00 00 8B 40 00", 4, 0, Enums.ScanMethod.Read);
        }

        public static IntPtr GetWorldToViewMatrix()
        {
			return Phoenix.Engine.BaseAddress + 0x5ADA6C;
			//return Phoenix.Client.Find("F3 0F 6F 05 00 00 00 00 8D 85", 4, 0xB0, Enums.ScanMethod.Read);
		}

        public static IntPtr GetClientState()
        {
			return Phoenix.Engine.BaseAddress + 0x47A630;

			return Memory.Read<IntPtr>(Phoenix.Engine.Find("C2 00 00 CC CC 8B 0D 00 00 00 00 33 C0 83 B9", 7, 0, Enums.ScanMethod.Read));
        }

        public static IntPtr GetCmdNumber()
        {
            return Phoenix.Engine.Find("56 57 8B 81 ? ? ? ? 8B B1 ? ? ? ? 40 03 C6", 4, 0, Enums.ScanMethod.Read);
        }

        public static IntPtr GetSendPacket()
        {
            return Phoenix.Engine.Find("01 8B 01 8B 40 10", 0, 0, Enums.ScanMethod.Add);
        }

        public static IntPtr GetLocalIndex()
        {
            return Phoenix.Engine.Find("8B 80 00 00 00 00 40 C3", 2, 0, Enums.ScanMethod.Read);
        }

        public static IntPtr GetGameDir()
        {
            return Phoenix.Engine.Find("68 00 00 00 00 8D 85 00 00 00 00 50 68 00 00 00 00 68", 1, 0, Enums.ScanMethod.Read);
        }

        public static IntPtr GetMapName()
        {
            return Phoenix.Engine.Find("05 00 00 00 00 C3 CC CC CC CC CC CC CC 80 3D", 1, 0, Enums.ScanMethod.Read);
        }

        public static IntPtr GetDormantOffset()
        {
            return Phoenix.Client.Find("83 79 ? FF 74 07 8A 81 ? ? ? ?", 8, 8, Enums.ScanMethod.Read);
        }

		public static IntPtr GetIndexOffset()
		{
			return new IntPtr(Memory.Read<byte>(Phoenix.Client.Find("83 79 ? FF 74 07 8A 81 ? ? ? ?", 2, 0, Enums.ScanMethod.Add)) + 8);
		}

        public static IntPtr GetSignOnState()
        {
            return Phoenix.Engine.Find("83 B9 ? ? ? ? 06 0F 94 C0 C3", 2, 0, Enums.ScanMethod.Read);
        }

        public static IntPtr GetClientClassesHead()
        {
			//0x5BDB2CB4
			return Memory.Read<IntPtr>(Phoenix.Client.Find("A1 ? ? ? ? C3 CC CC CC CC CC CC CC CC CC CC B8", 1, 0, Enums.ScanMethod.Read));
        }

        public static IntPtr GetEngineTrace()
        {
            return Phoenix.Engine.Find("50 A1 ? ? ? ? FF 50", 2, 0, Enums.ScanMethod.Read);
        }
    }
}
