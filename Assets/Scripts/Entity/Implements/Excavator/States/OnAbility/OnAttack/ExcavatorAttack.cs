using System.Collections.Generic;

namespace Unchord
{
    public class ExcavatorAttack : ExcavatorAbility, IBattleState
    {
        protected List<Entity> targets;
        public LTRB attackRange;
        public int targetCount;
        public float baseDamage;

        public override void OnConstruct()
        {
            base.OnConstruct();

            targets = new List<Entity>(16);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            // instance.battleModule.SetBattleState(this);
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.bUpdateAggroDirX = true;
            instance.bFixedLookDirByAxis.x = false;
        }

        public void OnTriggerBattleState()
        {
            
        }
    }
}