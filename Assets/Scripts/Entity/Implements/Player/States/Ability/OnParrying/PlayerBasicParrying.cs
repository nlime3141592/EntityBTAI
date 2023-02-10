using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerBasicParrying : PlayerParrying
    {
        public PlayerBasicParrying(Player _player)
        : base(_player)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            player.vm.SetVelocityXY(0.0f, -1.0f);
        }
    }
}