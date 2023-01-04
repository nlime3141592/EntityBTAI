using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerOnFloor : PlayerAction
    {
        public PlayerOnFloor(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }
    }
}