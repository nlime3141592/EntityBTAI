using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerOnLedgePage : PageNodeBT<EntityPlayer>
    {
        public PlayerClimbOnLedge climbLedge;

        public PlayerOnLedgePage(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {
            climbLedge = new PlayerClimbOnLedge(config, 300, "ClimbOnLedge");
        }

        protected override InvokeResult p_Invoke()
        {
            return climbLedge.Invoke();
        }
    }
}