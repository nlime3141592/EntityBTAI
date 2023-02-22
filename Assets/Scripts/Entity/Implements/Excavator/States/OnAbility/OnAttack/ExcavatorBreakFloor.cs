using UnityEngine;

namespace UnchordMetroidvania
{
    public class ExcavatorBreakFloor : ExcavatorAttack
    {
        public ExcavatorBreakFloor(Excavator _instance)
        : base(_instance)
        {
            base.attackRange = new LTRB()
            {
                left = 0.5f,
                top = -8.5f,
                right = 0.5f,
                bottom = 9.5f
            };
            base.targetCount = 16;
            base.baseDamage = 1.0f;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            excavator.bUpdateAggroDirX = false;
            excavator.bFixLookDirX = true;
            excavator.battleModule.targetLayerMask |= 1 << LayerMask.NameToLayer("Terrain");
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(excavator.aController.bEndOfAnimation)
                return ExcavatorFsm.c_st_FREE_FALL;
            
            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            excavator.battleModule.targetLayerMask &= ~(1 << LayerMask.NameToLayer("Terrain"));
        }
    }
}