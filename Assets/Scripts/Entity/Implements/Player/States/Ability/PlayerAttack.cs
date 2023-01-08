using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public abstract class PlayerAttack : _PlayerAbility
    {
        public LTRB range;
        public int targetCount;

        public PlayerAttack(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }
    }
}