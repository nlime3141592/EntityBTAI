using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerBasicParrying : PlayerParrying
    {
        public PlayerBasicParrying(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            player.vm.SetVelocityXY(0.0f, -1.0f);
        }
    }
}