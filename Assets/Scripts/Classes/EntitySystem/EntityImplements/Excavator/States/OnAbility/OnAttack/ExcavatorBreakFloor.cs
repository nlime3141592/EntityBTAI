using UnityEngine;

namespace Unchord
{
    public class ExcavatorBreakFloor : ExcavatorAttack
    {
        public override int idConstant => Excavator.c_st_BREAK_GROUND;
/*
        protected override void OnConstruct()
        {
            base.OnConstruct();

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
*/
        public override void OnStateBegin()
        {
            base.OnStateBegin();
            ++instance.phase;
            // instance.battleModule.targetLayerMask |= 1 << LayerMask.NameToLayer("Terrain");
            // instance.AllowHitFromBattleModule(false);
            instance.IgnoreBattleTrigger(null, false); // TODO: 배틀 트리거를 넣어줘야 함.
            instance.SetHealth(instance.maxHealth.finalValue);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.bEndOfAnimation)
                return Excavator.c_st_FREE_FALL;
            
            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            // instance.battleModule.targetLayerMask &= ~(1 << LayerMask.NameToLayer("Terrain"));
        }
    }
}