using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerRun : PlayerMove
    {
        public PlayerRun(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = player.axisInput.x * player.moveDir.x * player.runSpeed;
            float vy = player.axisInput.x * player.moveDir.y * player.runSpeed;

            player.vm.SetVelocityXY(vx, vy);
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(!player.bIsRun)
            {
                player.fsm.Change(player.walk);
                return true;
            }

            return false;
        }
    }
}