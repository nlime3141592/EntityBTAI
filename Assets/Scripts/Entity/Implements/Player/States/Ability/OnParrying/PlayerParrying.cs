using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerParrying : _PlayerAbility
    {
        public PlayerParrying(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }
    }
}