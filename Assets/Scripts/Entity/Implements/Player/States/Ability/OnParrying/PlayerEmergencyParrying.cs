using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerEmergencyParrying : PlayerParrying
    {
        public PlayerEmergencyParrying(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            player.vm.SetVelocityXY(0.0f, -1.0f);
        }
    }
}