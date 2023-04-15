using System;
using System.Collections.Generic;

namespace Unchord
{
    public abstract class MantisAttack : MantisAbility, IBattleState
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
            instance.bUpdateAggroDirX = false;
            instance.bFixedLookDirByAxis.x = true;
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