using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerMove : PlayerOnFloor
    {
        public PlayerMove(Player _player)
        : base(_player)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.vm.MeltPositionX();
            player.vm.MeltPositionY();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            player.senseData.UpdateMoveDir(player);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.axisInput.y > 0)
                return PlayerFsm.c_st_HEAD_UP;
            else if(player.axisInput.y < 0)
                return PlayerFsm.c_st_SIT;
            else if(player.axisInput.x == 0)
                return PlayerFsm.c_st_IDLE_SHORT;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}