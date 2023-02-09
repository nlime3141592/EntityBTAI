using System;
using System.Collections.Generic;

namespace UnchordMetroidvania
{
    public abstract class MantisAttack : MantisAbility
    {
        protected readonly List<EntityBase> targets;

        public MantisAttack(Mantis _mantis)
        : base(_mantis)
        {
            targets = new List<EntityBase>(16);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

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