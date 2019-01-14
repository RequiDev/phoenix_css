using Phoenix.CounterStrike.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phoenix.CounterStrike.Structs
{
    internal struct RecvProp
    {
        private IntPtr m_pVarName;
        private SendPropType m_RecvType;
        private int m_Flags;
        private int m_StringBufferSize;
        private bool m_bInsideArray;
        private IntPtr m_pExtraData;
        private IntPtr m_pArrayProp;
        private IntPtr m_ArrayLengthProxy;
        private IntPtr m_ProxyFn;
        private IntPtr m_DataTableProxyFn;
        private IntPtr m_pDataTable;
        private int m_Offset;
        private int m_ElementStride;
        private int m_nElements;
        private IntPtr m_pParentArrayPropName;

        public string GetVarName()
        {
            return Phoenix.Memory.ReadString(m_pVarName);
        }

        public string GetParentPropName()
        {
            return Phoenix.Memory.ReadString(m_pParentArrayPropName);
        }

        public SendPropType GetPropType()
        {
            return m_RecvType;
        }

        public int GetFlags()
        {
            return m_Flags;
        }

        public int GetOffset()
        {
            return m_Offset;
        }

        public int GetNumElements()
        {
            return m_nElements;
        }

        public RecvTable GetDataTable()
        {
            return Phoenix.Memory.Read<RecvTable>(m_pDataTable);
        }
    }
}