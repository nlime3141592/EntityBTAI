using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerHeadUp : PlayerStand
    {
        public PlayerHeadUp(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            player.cameraOffset = Vector2.up;
        }
    }
}