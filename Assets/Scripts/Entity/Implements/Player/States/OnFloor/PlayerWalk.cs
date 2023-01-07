using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerWalk : PlayerMove
    {
        public PlayerWalk(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = player.axisInput.x * player.moveDir.x * player.walkSpeed;
            float vy = player.axisInput.x * player.moveDir.y * player.walkSpeed - 0.1f;

            player.vm.SetVelocityXY(vx, vy);
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.bIsRun)
            {
                player.fsm.Change(player.run);
                return true;
            }

            return false;
        }
    }
}