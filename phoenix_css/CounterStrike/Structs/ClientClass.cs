using Phoenix.CounterStrike.Enums;
using System;

namespace Phoenix.CounterStrike.Structs
{
    internal struct ClientClass
    {
        private IntPtr m_pCreateFn;
        private IntPtr m_pCreateEventFn;
        private IntPtr m_pClassName;
        private IntPtr m_pRecvTable;
        private IntPtr m_pNext;
        private ClassId m_ClassID;

        public string GetClassName()
        {
            return Phoenix.Memory.ReadString(m_pClassName);
        }

        public ClassId GetClassId()
        {
            return m_ClassID;
        }

        public RecvTable GetRecvTable()
        {
            return Phoenix.Memory.Read<RecvTable>(m_pRecvTable);
        }

        public ClientClass GetNextClass()
        {
            return Phoenix.Memory.Read<ClientClass>(m_pNext);
        }
    }
}
