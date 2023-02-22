using System.Collections.Generic;

namespace UnchordMetroidvania
{
    public class ExcavatorAttack : ExcavatorAbility, IBattleState
    {
        EntityBase IBattleState.attacker => excavator;
        List<EntityBase> IBattleState.targets => this.targets;
        LTRB IBattleState.range => attackRange;
        int IBattleState.targetCount => this.targetCount;
        float IBattleState.baseDamage => this.baseDamage;

        protected readonly List<EntityBase> targets;
        public LTRB attackRange;
        public int targetCount;
        public float baseDamage;

        public ExcavatorAttack(Excavator _instance)
        : base(_instance)
        {
            targets = new List<EntityBase>(16);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            excavator.battleModule.SetBattleState(this);
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            excavator.bUpdateAggroDirX = true;
            excavator.bFixLookDirX = false;
        }
    }
}