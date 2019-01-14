using Phoenix.CounterStrike.Models;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.CounterStrike
{
    class EntityList
    {
        public List<BasePlayer> Players;
        public List<BaseEntity> Entities;

        public BasePlayer GetPlayerByIndex(int index)
        {
            return Players == null ? null : Players.FirstOrDefault(player => player.Index == index);
        }

        public BaseEntity GetEntityByIndex(int index)
        {
            return Entities == null ? null : Entities.FirstOrDefault(ent => ent.Index == index);
        }

        public BasePlayer GetLocalPlayer()
        {
            return Players == null ? null : Players.FirstOrDefault(player => player.Index == (EngineClient.LocalPlayerIndex + 1));
        }
    }
}
