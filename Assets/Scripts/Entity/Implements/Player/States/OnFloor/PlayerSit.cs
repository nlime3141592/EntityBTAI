using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerSit : PlayerStand
    {
        public PlayerSit(Player _player)
        : base(_player)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            player.cameraOffset = Vector2.down;
        }
    }
}