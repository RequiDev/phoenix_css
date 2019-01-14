using Phoenix.CounterStrike;
using Phoenix.CounterStrike.Enums;
using Phoenix.MemorySystem;
using Phoenix.Overlay;
using System;
using System.Collections.Generic;

namespace Phoenix
{
	internal class Phoenix
	{
		public static string GameName { get { return "Counter-Strike Source"; } }
		public static string ProcessName { get { return "hl2"; } }
		public static OverlayWindow Overlay { get; set; }
        public static EntityList EntityList { get; set; }
        public static ProcessMemory Memory { get; set; }
        public static PatternScan Client { get; set; }
        public static PatternScan Engine { get; set; }
        public static Dictionary<string, IntPtr> NetVars { get; set; }
    }
}
