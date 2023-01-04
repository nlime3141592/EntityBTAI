using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerAction : TaskNodeBT<EntityPlayer>
    {
        public PlayerAction(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }
    }
}