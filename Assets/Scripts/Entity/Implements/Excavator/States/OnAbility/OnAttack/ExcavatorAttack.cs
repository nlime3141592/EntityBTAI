using System.Collections.Generic;

namespace Unchord
{
    public class ExcavatorAttack : ExcavatorAbility, IBattleState
    {
        Entity IBattleState.attacker => instance;
        List<Entity> IBattleState.targets => this.targets;
        LTRB IBattleState.range => attackRange;
        int IBattleState.targetCount => this.targetCount;
        float IBattleState.baseDamage => this.baseDamage;

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
            instance.battleModule.SetBattleState(this);
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.bUpdateAggroDirX = true;
            instance.bFixLookDir.x = false;
        }
    }
}