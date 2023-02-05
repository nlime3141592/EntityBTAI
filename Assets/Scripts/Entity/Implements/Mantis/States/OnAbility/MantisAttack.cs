using System;
using System.Collections.Generic;

namespace UnchordMetroidvania
{
    public abstract class MantisAttack : MantisAbility
    {
        protected readonly List<EntityBase> targets;

        public MantisAttack(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {
            targets = new List<EntityBase>(16);
        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();

            mantis.bUpdateAggroDirX = false;
            mantis.bFixLookDirX = true;
        }

        public abstract bool CanAttack();

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            mantis.bUpdateAggroDirX = true;
            mantis.bFixLookDirX = false;
        }
    }
}