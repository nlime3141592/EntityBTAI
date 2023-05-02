using UnityEngine;

namespace Unchord
{
    [RequireComponent(typeof(StateEventTriggerOnTriggerEnter2D))]
    public class ExcavatorProjectile : Entity
    {
        public const int c_st_FLYING = 0;
        public const int c_st_EXPLOSION = 1;

        public int cntBounce;
        public Vector2 initPosition;
        public Vector2 initVelocity;
        public float dx = 0;
        public float dy = 0;

        protected override IStateMachineBase InitStateMachine()
        {
            StateMachine<ExcavatorProjectile> machine = new StateMachine<ExcavatorProjectile>(2);
            machine.instance = this;

            machine.Add(new ExcavatorProjectileExplosion());
            machine.Add(new ExcavatorProjectileFlying());

            machine.Begin(ExcavatorProjectile.c_st_FLYING);

            return machine;
        }
    }
}