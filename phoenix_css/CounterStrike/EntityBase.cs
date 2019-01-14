using Phoenix.CounterStrike.Models;
using Phoenix.MemorySystem;
using System;
using System.Collections.Generic;

namespace Phoenix.CounterStrike
{
    internal class EntityBase
    {
        private static ProcessMemory Memory => Phoenix.Memory;
        private static IntPtr entityAddress;

        public static void Update()
        {
            if (Phoenix.EntityList == null)
                Phoenix.EntityList = new EntityList();
            if (entityAddress == IntPtr.Zero)
                entityAddress = SignatureManager.GetEntityList();

            var players = new List<BasePlayer>();
			var entities = new List<BaseEntity>();
			var entityList = Memory.ReadByteArray(entityAddress, 4096 * 0x10); //lol
            for (int i = 0; i < 4096/*BaseClient.GlobalVars.maxClients*/; i++)
            {
                var entity = (IntPtr)BitConverter.ToInt32(entityList, i * 0x10);

                if (entity == IntPtr.Zero) continue;
                var player = new BasePlayer(entity);
				if (player.GetClientClass().GetClassName() == "CCSPlayer")
				{
					players.Add(player);
				}
				else
				{
					var ent = new BaseEntity(entity);
					if (ent.Index < 1) ent = new BaseEntity(entity - 8);
					if (ent.Index < 1) continue;
					entities.Add(ent);
				}

			}

            Phoenix.EntityList.Players = players;
			Phoenix.EntityList.Entities = entities;
		}
    }
}
