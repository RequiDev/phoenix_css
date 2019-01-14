using Phoenix.CommandSystem;
using Phoenix.ConsoleSystem;
using Phoenix.CounterStrike;
using Phoenix.MemorySystem;
using Phoenix.ThreadingSystem;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Phoenix.Structs;
using Phoenix.Features;
using Phoenix.Extensions;
using Phoenix.Overlay;
using System.Drawing;

namespace Phoenix
{
    internal class Program
    {
        private static ProcessMemory Memory => Phoenix.Memory;
		private static OverlayWindow Overlay => Phoenix.Overlay;

		private static void Main(string[] args)
        {
            CommandHandler.Setup();

            ThreadManager.Add(new ThreadFunction("CommandHandler", CommandHandler.Worker));
            ThreadManager.Add(new ThreadFunction("MainLoop", MainLoop));
            ThreadManager.Add(new ThreadFunction("MiscLoop", MiscLoop));
			ThreadManager.Add(new ThreadFunction("AimbotLoop", AimbotLoop));
			ThreadManager.Add(new ThreadFunction("SoundESPLoop", SoundESPLoop));
			ThreadManager.Add(new ThreadFunction("DrawingLoop", DrawingLoop));
			ThreadManager.Add(new ThreadFunction("GetMapLoop", GetMapLoop));

            AttachToGame();
        }
       
        private static void SoundESPLoop()
        {
            while(Memory.IsProcessRunning)
            {
                Thread.Sleep(10);
                SoundESP.Run();
            }
        }

		private static void DrawingLoop()
		{
			Phoenix.Overlay = new Overlay.OverlayWindow(Memory.MainWindowHandle, false);

			Overlay.Show();


			while (Memory.IsProcessRunning)
			{
				//Thread.Sleep(0);

				Overlay.Graphics.BeginScene();
				Overlay.Graphics.ClearScene();

				if (Native.GetForegroundWindow() != Overlay.ParentWindow)
				{
					Overlay.Graphics.EndScene();
					continue;
				}

				ESP.Run();

				Overlay.Graphics.EndScene();
			}
		}

		private static void GetMapLoop()
		{
			while (Memory.IsProcessRunning)
			{
				Thread.Sleep(10);
				var map = EngineClient.Map;
			}
		}

		private static void MiscLoop()
		{
			while (Memory.IsProcessRunning)
			{
                Thread.Sleep(5);
                Misc.Bunnyhop();
				Misc.NoFlash();
            }
        }

        private static void AimbotLoop()
		{
			while (Memory.IsProcessRunning)
			{
                Thread.Sleep(1);
                Aimbot.Run();
            }
        }

        private static void MainLoop()
		{
			while (Memory.IsProcessRunning)
			{
                Thread.Sleep(1);
                EntityBase.Update();
            }
        }

        private static void AttachToGame()
        {
            Process process;
            Console.WriteNotification($"\n  Looking for {Phoenix.GameName}...");
            while (Memory == null)
            {
                Thread.Sleep(100);
                try
                {
                    process = Process.GetProcessesByName(Phoenix.ProcessName).FirstOrDefault(p => p.Threads.Count > 0);
                    if (process == null || !Utils.IsModuleLoaded(process, "client.dll") || !Utils.IsModuleLoaded(process, "engine.dll")) continue;
                }
                catch
                {
                    continue;
                }

                Phoenix.Memory = new ProcessMemory(process);
                Console.WriteLine("\n  Modules:");
                
                Console.WriteSuccess("  \tclient.dll | 0x" + Memory["client.dll"].BaseAddress.ToString("X").PadLeft(8, '0') + "\t| " + Utils.ByteSizeToString(Memory["client.dll"].ModuleMemorySize));
                Console.WriteSuccess("  \tengine.dll | 0x" + Memory["engine.dll"].BaseAddress.ToString("X").PadLeft(8, '0') + "\t| " + Utils.ByteSizeToString(Memory["engine.dll"].ModuleMemorySize));

                Phoenix.Client = new PatternScan(process, "client.dll");
                Phoenix.Engine = new PatternScan(process, "engine.dll");
            }

            Console.WriteLine("\n  Offsets:");
            Console.WriteOffset("EntityBase", SignatureManager.GetEntityList());
            Console.WriteOffset("ClientClass", SignatureManager.GetClientClassesHead());
			Console.WriteOffset("ClientState", SignatureManager.GetClientState());
			Console.WriteOffset("GameDirectory", SignatureManager.GetGameDir());
            Console.WriteOffset("ViewMatrix", SignatureManager.GetWorldToViewMatrix());
            Console.WriteOffset("ViewAngle", SignatureManager.GetViewAngle(), true);

            Phoenix.NetVars = new Dictionary<string, System.IntPtr>();
            Console.WriteLine("\n  NetVars:");
            Phoenix.NetVars.Add("m_vecAimPunch", NetvarManager.GetOffset("DT_BasePlayer", "m_Local") + 0x6C);
            Phoenix.NetVars.Add("m_vecOrigin", NetvarManager.GetOffset("DT_BasePlayer", "m_vecOrigin"));
            Phoenix.NetVars.Add("m_iHealth", NetvarManager.GetOffset("DT_BasePlayer", "m_iHealth"));
            Phoenix.NetVars.Add("m_iTeamNum", NetvarManager.GetOffset("DT_BasePlayer", "m_iTeamNum"));
            Phoenix.NetVars.Add("m_vecViewOffset", NetvarManager.GetOffset("DT_BasePlayer", "m_vecViewOffset[0]"));
            Phoenix.NetVars.Add("m_dwIndex", SignatureManager.GetIndexOffset());
            Phoenix.NetVars.Add("m_dwBoneMatrix", new System.IntPtr(0x810));
            Phoenix.NetVars.Add("m_hActiveWeapon", NetvarManager.GetOffset("DT_BasePlayer", "m_hActiveWeapon"));
            Phoenix.NetVars.Add("m_hViewModel", NetvarManager.GetOffset("DT_BasePlayer", "m_hViewModel[0]"));
            Phoenix.NetVars.Add("m_hOwner", NetvarManager.GetOffset("DT_BaseCombatWeapon", "m_hOwner"));
            Phoenix.NetVars.Add("m_iState", NetvarManager.GetOffset("DT_BaseCombatWeapon", "m_iState"));
            Phoenix.NetVars.Add("m_nModelIndex", NetvarManager.GetOffset("DT_BaseCombatWeapon", "m_nModelIndex"));
            Phoenix.NetVars.Add("m_bDormant", SignatureManager.GetDormantOffset());
            Phoenix.NetVars.Add("m_flFlashAlpha", NetvarManager.GetOffset("DT_CSPlayer", "m_flFlashMaxAlpha"));
            Phoenix.NetVars.Add("m_iFOVStart", NetvarManager.GetOffset("DT_CSPlayer", "m_iFOVStart"));
            Phoenix.NetVars.Add("m_bIsDefusing", NetvarManager.GetOffset("DT_CSPlayer", "m_bIsDefusing"));
            Phoenix.NetVars.Add("m_fFlags", NetvarManager.GetOffset("DT_CSPlayer", "m_fFlags"));
            Phoenix.NetVars.Add("m_hMyWeapons", NetvarManager.GetOffset("DT_CSPlayer", "m_hMyWeapons"));
            Phoenix.NetVars.Add("m_hWeapon", NetvarManager.GetOffset("DT_BaseViewModel", "m_hWeapon"));
			Phoenix.NetVars.Add("m_lifeState", NetvarManager.GetOffset("DT_BasePlayer", "m_lifeState"));
			Phoenix.NetVars.Add("m_iClip1", NetvarManager.GetOffset("DT_BaseCombatWeapon", "m_iClip1"));
			Phoenix.NetVars.Add("m_vecMins", NetvarManager.GetOffset("DT_BaseEntity", "m_Collision") + 0x20);
			Phoenix.NetVars.Add("m_vecMaxs", NetvarManager.GetOffset("DT_BaseEntity", "m_Collision") + 0x2C);

			Phoenix.NetVars.Sort();

            foreach (var netvar in Phoenix.NetVars)
            {
                Console.WriteOffset(netvar.Key, netvar.Value, true);
            }
            Console.WriteOffset("m_numHighest", (System.IntPtr)Phoenix.NetVars.MaxValue() + Marshal.SizeOf(typeof(Vector3D)), true);

            Console.WriteNotification("\n  Found and attached to it!");
            CommandHandler.Load();
            Console.WriteCommandLine();

			ThreadManager.RunAll();
        }
    }
}
