using Phoenix.MemorySystem;
using System;

namespace Phoenix.CounterStrike
{
    internal static class NetvarManager
    {
        private static ProcessMemory Memory => Phoenix.Memory;
        private static IntPtr _clientClassesHead;
        private static IntPtr ClientClassesHead
        {
            get
			{
				if (_clientClassesHead == IntPtr.Zero)
                {
					_clientClassesHead = SignatureManager.GetClientClassesHead();
				}
				return _clientClassesHead;
            }
        }
        private static IntPtr SearchInSubSubTable(IntPtr subTable, string searchFor)
        {
            var current = Memory.Read<IntPtr>(Memory.Read<IntPtr>(subTable + 0x28));
            while (true)
            {
                string entryName = Memory.ReadString(Memory.Read<IntPtr>(current));

                if (entryName == "")
                    break;

                if (entryName.Length < 1)
                    break;

                if (entryName.Length > 3)
                {
                    var offset = Memory.Read<IntPtr>(current + 0x2C);
                    if (entryName.Equals(searchFor))
                        return offset;
                }

                var subSubTable = Memory.Read<IntPtr>(current + 0x28);

                if (subSubTable != IntPtr.Zero)
                {
                    IntPtr a = SearchInSubSubTable(current, searchFor);
                    if (a != IntPtr.Zero)
                        return a;
                }
                current += 0x3C;
            }

            return IntPtr.Zero;

        }
        private static IntPtr SearchInSubtable(IntPtr subTable, string searchFor)
        {
            var current = subTable;
            while (true)
            {
                string entryName = Memory.ReadString(Memory.Read<IntPtr>(current));

                if (entryName == "")
                    break;

                if (entryName.Length < 1)
                    break;

                if (entryName == "baseclass")
                {
                    var a = SearchInBaseClass(current, searchFor);
                    if (a != IntPtr.Zero)
                        return a;
                }

                if (entryName == "cslocaldata")
                {
                    var a = SearchInCSLocalData(current, searchFor);
                    if (a != IntPtr.Zero)
                        return a;
                }

                if (entryName == "localdata")
                {
                    var a = SearchInLocalData(current, searchFor);
                    if (a != IntPtr.Zero)
                        return a;
                }

                int subSubTable = Memory.Read<int>(current + 0x28);

                if (subSubTable > 0)
                {
                    var a = SearchInSubSubTable(current, searchFor);
                    if (a != IntPtr.Zero)
                        return a;
                }

                var offset = Memory.Read<IntPtr>(current + 0x2C);
                if (entryName == searchFor)
                    return offset;

                current += 0x3C;
            }

            return IntPtr.Zero;
        }
        private static IntPtr SearchInBaseClass(IntPtr baseClass, string searchFor)
        {
            var a = SearchInSubtable(baseClass + 0x3C, searchFor);
            if (a != IntPtr.Zero)
                return a;

            string className = Memory.ReadString(Memory.Read<IntPtr>(baseClass));

            if (className == "baseclass")
            {
                return SearchInBaseClass(Memory.Read<IntPtr>(Memory.Read<IntPtr>(baseClass + 0x28)), searchFor);
            }

            return IntPtr.Zero;
        }
        private static IntPtr SearchInCSLocalData(IntPtr csLocalData, string searchFor)
        {
            var a = SearchInSubtable(csLocalData + 0x28, searchFor);
            if (a != IntPtr.Zero)
                return a;

            string className = Memory.ReadString(Memory.Read<IntPtr>(csLocalData));

            if (className == "cslocaldata")
            {
                return SearchInBaseClass(Memory.Read<IntPtr>(Memory.Read<IntPtr>(csLocalData + 0x28)), searchFor);
            }

            return IntPtr.Zero;
        }
        private static IntPtr SearchInLocalData(IntPtr localData, string searchFor)
        {
            var a = SearchInSubtable(localData + 0x28, searchFor);

            if (a != IntPtr.Zero)
                return a;

            string className = Memory.ReadString(Memory.Read<IntPtr>(localData));

            if (className == "localdata")
            {
                return SearchInBaseClass(Memory.Read<IntPtr>(Memory.Read<IntPtr>(localData + 0x28)), searchFor);
            }

            return IntPtr.Zero;
        }
        private static IntPtr SearchInTableFor(IntPtr table, string searchFor)
        {
            var current = Memory.Read<IntPtr>(Memory.Read<IntPtr>(table + 0xC));
            while (true)
            {
                if (Memory.Read<IntPtr>(current) == IntPtr.Zero)
                    break;

                string entryName = Memory.ReadString(Memory.Read<IntPtr>(current));

                if (entryName.Length < 1)
                    break;

                if (entryName == "baseclass")
                {
                    return SearchInBaseClass(current, searchFor);
                }

                if (entryName == "cslocaldata")
                {
                    return SearchInCSLocalData(current, searchFor);
                }

                if (entryName == "localdata")
                {
                    return SearchInLocalData(current, searchFor);
                }

                var offset = Memory.Read<IntPtr>(current + 0x2C);
                if (entryName.Equals(searchFor))
                    return offset;
                current += 0x3C;

            }

            return IntPtr.Zero;
        }
        private static IntPtr GetTable(string wantedTable)
        {
            var current = ClientClassesHead;

            while (true)
            {
                string className = Memory.ReadString((Memory.Read<IntPtr>((current + 0x8))));
                string tableName = Memory.ReadString(Memory.Read<IntPtr>(Memory.Read<IntPtr>(current + 0xC) + 0xC));

                if (className.Equals(wantedTable) || tableName.Equals(wantedTable))
                    return current;

                current = Memory.Read<IntPtr>(current + 0x10);
                if (current == IntPtr.Zero)
                    break;
            }

            return IntPtr.Zero;
        }

        public static IntPtr GetOffset(string table, string entry)
        {
            var tableAddress = GetTable(table);
            var offset = SearchInTableFor(tableAddress, entry);
            return offset;
        }
    }
}
