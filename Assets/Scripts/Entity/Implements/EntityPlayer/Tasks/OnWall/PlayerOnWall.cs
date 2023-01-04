using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerOnWall : PlayerAction
    {
        public PlayerOnWall(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }
    }
}