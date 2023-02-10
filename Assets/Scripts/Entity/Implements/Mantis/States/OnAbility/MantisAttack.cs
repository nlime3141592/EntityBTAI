using System;
using System.Collections.Generic;

namespace UnchordMetroidvania
{
    public abstract class MantisAttack : MantisAbility, IBattleState
    {
        EntityBase IBattleState.attacker => mantis;
        List<EntityBase> IBattleState.targets => this.targets;
        LTRB IBattleState.range => attackRange;
        int IBattleState.targetCount => this.targetCount;
        float IBattleState.baseDamage => this.baseDamage;

        protected readonly List<EntityBase> targets;
        public LTRB attackRange;
        public int targetCount;
        public float baseDamage;

        public MantisAttack(Mantis _mantis)
        : base(_mantis)
        {
            targets = new List<EntityBase>(16);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            mantis.battleModule.SetBattleState(this);
            mantis.bUpdateAggroDirX = false;
            mantis.bFixLookDirX = true;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            mantis.bUpdateAggroDirX = true;
            mantis.bFixLookDirX = false;
        }
    }
}