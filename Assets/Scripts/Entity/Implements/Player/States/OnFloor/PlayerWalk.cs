using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerWalk : PlayerMove
    {
        public PlayerWalk(Player _player)
        : base(_player)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = player.axisInput.x * player.moveDir.x * data.walkSpeed;
            float vy = player.axisInput.x * player.moveDir.y * data.walkSpeed - 0.1f;

            player.vm.SetVelocityXY(vx, vy);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.bIsRun)
                return PlayerFsm.c_st_RUN;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}