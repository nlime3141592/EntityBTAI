using UnityEngine;

namespace Unchord
{
    public abstract class PlayerAttackOnFloorBase : PlayerAttack
    {
        protected bool bCapturedParryingDown { get; private set; }
        protected bool bCapturedJumpDown { get; private set; }
        protected bool bCapturedRushDown { get; private set; }
        protected bool bCapturedAttackDown { get; private set; }
        protected bool bCapturedMove { get; private set; }

        private bool m_bCanStepFront;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            bCapturedParryingDown = false;
            bCapturedJumpDown = false;
            bCapturedRushDown = false;
            bCapturedAttackDown = false;
            bCapturedMove = false;

            instance.vm.FreezePosition(true, false);

            m_bCanStepFront = false;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = 0.0f;
            float vy = -1.0f;

            if(m_bCanStepFront)
            {
                vx = instance.lookDir.fx * instance.moveDir.x;
                vy = instance.lookDir.fy * instance.moveDir.y;
                vy -= (float)Mathf.Abs(vy * 0.1f);
            }

            instance.vm.SetVelocityXY(vx, vy);
        }

        public override void OnActionBegin()
        {
            base.OnActionBegin();

            instance.vm.FreezePosition(false, false);
            m_bCanStepFront = true;
        }

        public override void OnActionEnd()
        {
            base.OnActionEnd();

            instance.vm.FreezePosition(true, false);
            m_bCanStepFront = false;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(!instance.bBeginOfAction)
                return;

            if(instance.iManager.parryingDown)
                bCapturedParryingDown = true;
            if(instance.iManager.jumpDown)
                bCapturedJumpDown = true;
            if(instance.iManager.rushDown)
                bCapturedRushDown = true;
            if(this.CanTransit() && instance.iManager.active000)
                bCapturedAttackDown = true;
            if(instance.iManager.ix != 0)
                bCapturedMove = true;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(!instance.senseData.datFloor.bOnHit)
                return Player.c_st_FREE_FALL;
            else if(instance.bEndOfAnimation)
                return Player.c_st_IDLE_SHORT;
            else if(instance.bEndOfAction)
            {
                if(bCapturedParryingDown)
                    return Player.c_st_EMERGENCY_PARRYING;
                else if(bCapturedJumpDown)
                    return Player.c_st_JUMP_ON_FLOOR;
                else if(bCapturedRushDown)
                    return Player.c_st_ROLL;
                else if(bCapturedAttackDown)
                    return instance.stateNext_AttackOnFloor;
                else if(bCapturedMove)
                {
                    if(instance.bIsRun)
                        return Player.c_st_RUN;
                    else
                        return Player.c_st_WALK;
                }
            }

            return MachineConstant.c_lt_PASS;
        }
    }
}