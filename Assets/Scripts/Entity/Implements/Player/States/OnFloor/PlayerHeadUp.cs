using UnityEngine;

namespace UnchordMetroidvania
{
    public class _PlayerHeadUp : PlayerStand
    {
        public _PlayerHeadUp(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            player.cameraOffset = Vector2.up;
        }
    }
}