using Phoenix.CounterStrike.Structs;
using Phoenix.Extensions;
using Phoenix.MemorySystem;
using Phoenix.Structs;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Phoenix.CounterStrike.Models
{
    internal class BaseEntity
    {
        protected static ProcessMemory Memory => Phoenix.Memory;
        protected byte[] readData;
        protected IntPtr address;

        public BaseEntity(IntPtr address)
        {
            this.address = address;
            Update();
        }

        public BaseEntity(BaseEntity ent)
        {
            readData = ent.GetData();
        }

        internal byte[] GetData()
        {
            return readData;
        }

        public void Update()
        {
            readData = Memory.ReadByteArray(address, Phoenix.NetVars.MaxValue() + Marshal.SizeOf(typeof(Vector3D)));
        }

        public IntPtr Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

		public int Index
		{
			get
			{
				return BitConverter.ToInt32(readData, Phoenix.NetVars["m_dwIndex"].ToInt32());
			}
		}

		public bool IsDormant()
        {
            return BitConverter.ToBoolean(readData, Phoenix.NetVars["m_bDormant"].ToInt32());
        }

        public ClientClass GetClientClass()
        {
			var result = GetClientClassAddress();
			return Memory.Read<ClientClass>(result);
        }

        public IntPtr GetClientClassAddress()
        {
            var vt = new IntPtr(BitConverter.ToInt32(readData, 8));
            var fn = Memory.Read<IntPtr>(vt + 8);
            var result = Memory.Read<IntPtr>(fn + 1);
            return result;
        }

        public Vector3D GetPosition()
        {
            byte[] vecData = new byte[12];
            Buffer.BlockCopy(readData, Phoenix.NetVars["m_vecOrigin"].ToInt32(), vecData, 0, 12);
            return ProcessMemory.BytesTo<Vector3D>(vecData);
        }
    }
}
