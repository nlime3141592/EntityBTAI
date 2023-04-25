using UnityEngine;

namespace Unchord
{
    public class PlayerRoll : PlayerRush
    {
        private bool m_bParryingDown;
        private float m_dirX;

        public override int idConstant => Player.c_st_ROLL;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            m_bParryingDown = false;
            m_dirX = instance.lookDir.fx;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = m_dirX * instance.moveDir.x * instance.speed_Roll;
            float vy = m_dirX * instance.moveDir.y * instance.speed_Roll - 1.0f;

            instance.vm.SetVelocityXY(vx, vy);
        }

        public override void OnUpdateAlways()
        {
            base.OnUpdateAlways();
            if(instance.parryingDown)
                m_bParryingDown = true;
            if(instance.aController.bEndOfAction)
            {
                // TODO: 이 블럭 안에 패링 입력을 받을지 고민해보기.
            }
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(!instance.senseData.datFloor.bOnHit)
                return Player.c_st_FREE_FALL;
            else if(instance.aController.bEndOfAnimation)
                return Player.c_st_IDLE_SHORT;
            else if(m_bParryingDown)
                return Player.c_st_EMERGENCY_PARRYING;
            else if(instance.jumpDown)
                return Player.c_st_JUMP_ON_FLOOR;

            return MachineConstant.c_lt_PASS;
        }
    }
}