using System;
using UnityEngine;

namespace Unchord
{
    public abstract class PlayerAttackOnAirBase : PlayerAttack
    {
        public float baseDamage;
        protected float speed_Hop;

        protected bool bCapturedAttackDown { get; private set; }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            bCapturedAttackDown = false;

            instance.bFixedLookDirByAxis.x = false;
            instance.vm.FreezePosition(true, false);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            instance.vm.SetVelocityXY(0.0f, speed_Hop);
            speed_Hop += instance.gravity_AttackOnAir * Time.fixedDeltaTime;
        }

        public override void OnActionBegin()
        {
            base.OnActionBegin();

            instance.bFixedLookDirByAxis.x = true;
        }

        public override void OnActionEnd()
        {
            base.OnActionEnd();

            instance.bFixedLookDirByAxis.x = false;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(!instance.bBeginOfAction)
                return;

            if(instance.iManager.active000)
                bCapturedAttackDown = true;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.senseData.datCeil.bOnHit || instance.bEndOfAnimation)
                return Player.c_st_FREE_FALL;
            else if(bCanLandOnFloor())
                return Player.c_st_IDLE_SHORT;
            else if(instance.bEndOfAction)
            {
                if(bCapturedAttackDown)
                    return instance.stateNext_AttackOnAir;
            }

            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.bFixedLookDirByAxis.x = false;
            instance.vm.FreezePosition(false, false);
        }
    }
}