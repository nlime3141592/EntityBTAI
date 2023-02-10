using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerHeadUp : PlayerStand
    {
        public PlayerHeadUp(Player _player)
        : base(_player)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            player.cameraOffset = Vector2.up;
        }
    }
}