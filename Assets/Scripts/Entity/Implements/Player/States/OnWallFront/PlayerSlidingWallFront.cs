using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerSlidingWallFront : PlayerOnWallFront
    {
        public PlayerSlidingWallFront(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnStateBegin()
        {
            player.vm.FreezePositionX();
            player.vm.MeltPositionY();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vy = player.vm.y;
            float dV = data.slidingWallGravity * Time.fixedDeltaTime;

            vy += dV;
            if(vy < data.minSlidingWallFrontSpeed)
                vy = data.minSlidingWallFrontSpeed;

            player.vm.SetVelocityXY(0.0f, vy);
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.axisInput.x != 0)
            {
                player.fsm.Change(player.idleWallFront);
                return true;
            }

            return false;
        }

        public override void OnStateEnd()
        {
            player.vm.MeltPositionX();
            player.vm.MeltPositionY();
        }
    }
}