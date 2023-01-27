using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerSit : PlayerStand
    {
        public PlayerSit(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            player.cameraOffset = Vector2.down;
        }
    }
}