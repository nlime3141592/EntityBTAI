using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    public abstract class PlayerAttack : PlayerAbility, IBattleState
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
            targets = new List<Entity>(16);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.battleModule.SetBattleState(this);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            instance.battleModule.ClearBattleState();
        }
    }
}