using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerJump : PlayerAction
    {
        public PlayerJump(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }
    }
}