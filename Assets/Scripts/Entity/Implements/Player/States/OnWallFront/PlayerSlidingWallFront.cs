using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerSlidingWallFront : PlayerOnWallFront
    {
        public PlayerSlidingWallFront(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();

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
                fsm.Change(fsm.idleWallFront);
                return true;
            }

            return false;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            player.vm.MeltPositionX();
            player.vm.MeltPositionY();
        }
    }
}