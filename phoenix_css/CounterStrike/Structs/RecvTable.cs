using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phoenix.CounterStrike.Structs
{
    internal struct RecvTable
    {
        public IntPtr m_pProps;
        private int m_nProps;
        private IntPtr m_pDecoder;
        private IntPtr m_pNetTableName;

        public RecvProp[] GetProps()
        {
            return Phoenix.Memory.ReadArray<RecvProp>(m_pProps, m_nProps);
        }

        public int GetNumProps()
        {
            return m_nProps;
        }

        public IntPtr GetDecoder()
        {
            return m_pDecoder;
        }

        public string GetTablename()
        {
            return Phoenix.Memory.ReadString(m_pNetTableName);
        }
    }
}