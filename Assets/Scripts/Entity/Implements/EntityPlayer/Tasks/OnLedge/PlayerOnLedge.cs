using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerOnLedge : PlayerAction
    {
        public PlayerOnLedge(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }
    }
}