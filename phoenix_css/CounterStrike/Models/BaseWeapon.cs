using Phoenix.CounterStrike.Enums;
using Phoenix.Extensions;
using System;

namespace Phoenix.CounterStrike.Models
{
    internal class BaseWeapon : BaseEntity
    {
        public BaseWeapon(IntPtr address) : base(address) { }

        public BasePlayer GetOwner()
        {
            int index = BitConverter.ToInt32(readData, Phoenix.NetVars["m_hOwner"].ToInt32());
            index &= 0xFFF;
            return Phoenix.EntityList.GetPlayerByIndex(index);
        }

        public int GetState()
        {
            return BitConverter.ToInt16(readData, Phoenix.NetVars["m_iState"].ToInt32());
        }

		internal int GetAmmo()
		{
			return BitConverter.ToInt32(readData, Phoenix.NetVars["m_iClip1"].ToInt32());
		}
	}
}
