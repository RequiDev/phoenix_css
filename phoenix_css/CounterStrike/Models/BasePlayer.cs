using Phoenix.CounterStrike.Enums;
using System;
using Phoenix.CounterStrike.Structs;
using Phoenix.MemorySystem;
using Phoenix.Structs;
using Phoenix.Extensions;
using System.Linq;
using System.Collections.Generic;

namespace Phoenix.CounterStrike.Models
{
    internal class BasePlayer : BaseEntity
    {
        private BaseBone[] cachedBones = null;

        public BasePlayer(IntPtr address) : base(address) { }

        public int GetHealth()
        {
            return BitConverter.ToInt32(readData, Phoenix.NetVars["m_iHealth"].ToInt32());
        }

        public Team GetTeam()
        {
            return (Team)BitConverter.ToInt32(readData, Phoenix.NetVars["m_iTeamNum"].ToInt32());
        }

		public LifeState GetLifeState()
		{
			return (LifeState)readData[Phoenix.NetVars["m_lifeState"].ToInt32()];
		}

		public PlayerFlag GetFlags()
		{
			return (PlayerFlag)BitConverter.ToInt32(readData, Phoenix.NetVars["m_fFlags"].ToInt32());
		}

		public Vector3D GetAimPunchAngle()
        {
            byte[] vecData = new byte[12];
            Buffer.BlockCopy(readData, Phoenix.NetVars["m_vecAimPunch"].ToInt32(), vecData, 0, 12);
            return ProcessMemory.BytesTo<Vector3D>(vecData);
        }
        public Vector3D GetViewPunchAngle()
        {
            byte[] vecData = new byte[12];
            Buffer.BlockCopy(readData, Phoenix.NetVars["m_vecViewPunch"].ToInt32(), vecData, 0, 12);
            return ProcessMemory.BytesTo<Vector3D>(vecData);
        }

        public Vector3D GetViewOffset()
        {
            byte[] vecData = new byte[12];
            Buffer.BlockCopy(readData, Phoenix.NetVars["m_vecViewOffset"].ToInt32(), vecData, 0, 12);
            return ProcessMemory.BytesTo<Vector3D>(vecData);
        }

        public Vector3D GetEyePos()
        {
            return GetPosition() + GetViewOffset();
        }

        public BaseWeapon GetWeapon()
        {
            var entIndex = BitConverter.ToInt32(readData, Phoenix.NetVars["m_hActiveWeapon"].ToInt32()) & 0xFFF;
            var currentWeapon = Phoenix.EntityList.GetEntityByIndex(entIndex);
            return currentWeapon != null ? new BaseWeapon(currentWeapon.Address) : null;
        }

        public BaseBone[] GetBoneMatrix()
        {
            var boneMatrix = new IntPtr(BitConverter.ToInt32(readData, Phoenix.NetVars["m_dwBoneMatrix"].ToInt32()));
            if(cachedBones == null)
                cachedBones = Memory.ReadArray<BaseBone>(boneMatrix, 128);
            return cachedBones;
        }

        public Vector3D GetBonePos(int boneId)
        {
            return GetBoneMatrix()[boneId];
        }

        public int GetFieldOfView()
        {
            var m_iFOVStart = BitConverter.ToInt32(readData, Phoenix.NetVars["m_iFOVStart"].ToInt32());
            return m_iFOVStart;
        }

		public Vector3D GetCollidableMax()
		{
			byte[] vecData = new byte[12];
			Buffer.BlockCopy(readData, Phoenix.NetVars["m_vecMaxs"].ToInt32(), vecData, 0, 12);
			return ProcessMemory.BytesTo<Vector3D>(vecData);
		}

		public Vector3D GetCollidableMin()
		{
			byte[] vecData = new byte[12];
			Buffer.BlockCopy(readData, Phoenix.NetVars["m_vecMins"].ToInt32(), vecData, 0, 12);
			return ProcessMemory.BytesTo<Vector3D>(vecData);
		}

		public bool IsDefusing()
        {
            return BitConverter.ToBoolean(readData, Phoenix.NetVars["m_bIsDefusing"].ToInt32());
        }

        public BasePlayer GetPlayerInCrosshair()
        {
            return Phoenix.EntityList.Players.FirstOrDefault(p => p.Index== BitConverter.ToInt32(readData, Phoenix.NetVars["m_iCrosshairId"].ToInt32()));
        }

        public float FlashAlpha
        {
            get
            {
                return BitConverter.ToSingle(readData, Phoenix.NetVars["m_flFlashAlpha"].ToInt32());
            }
            set
            {
                Memory.Write(address.Add(Phoenix.NetVars["m_flFlashAlpha"]), value);
            }
        }

        //public bool IsSpotted
        //{
        //    get
        //    {
        //        return BitConverter.ToBoolean(readData, Phoenix.NetVars["m_bSpotted"].ToInt32());
        //    }
        //    set
        //    {
        //        Memory.Write(address.Add(Phoenix.NetVars["m_bSpotted"]), value);
        //    }
        //}
    }
}