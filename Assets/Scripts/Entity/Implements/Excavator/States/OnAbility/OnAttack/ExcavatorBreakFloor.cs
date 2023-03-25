using UnityEngine;

namespace Unchord
{
    public class ExcavatorBreakFloor : ExcavatorAttack
    {
        public override void OnConstruct()
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

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.bUpdateAggroDirX = false;
            instance.bFixLookDir.x = true;
            instance.battleModule.targetLayerMask |= 1 << LayerMask.NameToLayer("Terrain");
            instance.AllowHitFromBattleModule(false);
            instance.SetHealth(instance.maxHealth.finalValue);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return Excavator.c_st_FREE_FALL;
            
            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            instance.battleModule.targetLayerMask &= ~(1 << LayerMask.NameToLayer("Terrain"));
        }
    }
}