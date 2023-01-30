using System;
using System.Collections.Generic;

namespace UnchordMetroidvania
{
    public abstract class _MantisAttack : MantisAbility
    {
        protected readonly List<EntityBase> targets;

        public _MantisAttack(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {

        }

        public abstract bool CanAttack();

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(mantis.aController.bEndOfAnimation)
            {
                return true;
            }

            return false;
        }
    }
}