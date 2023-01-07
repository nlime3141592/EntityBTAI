using UnityEngine;

namespace UnchordMetroidvania
{
    public class _PlayerSit : PlayerStand
    {
        public _PlayerSit(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            player.cameraOffset = Vector2.down;
        }
    }
}