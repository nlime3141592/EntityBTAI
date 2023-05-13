using UnityEngine;

namespace Unchord
{
    [RequireComponent(typeof(StateEventTriggerOnCollisionEnter2D))]
    [RequireComponent(typeof(BattleModule))]
    public class ExcavatorProjectile : Entity
    {
        public const int c_st_IDLE = 0;
        public const int c_st_FLYING = 1;
        public const int c_st_EXPLOSION = 2;

        public int cntBounce;
        public Vector2 initPosition;
        public Vector2 initVelocity;
        public bool bInstanceReady = false;
        public float dx = 0;
        public float dy = 0;

        public override IStateMachineBase InitStateMachine()
        {
            StateMachine<ExcavatorProjectile> machine = new StateMachine<ExcavatorProjectile>(2);
            machine.instance = this;

            machine.Add(new ExcavatorProjectileExplosion());
            machine.Add(new ExcavatorProjectileFlying());

            machine.Begin(ExcavatorProjectile.c_st_IDLE);

            return machine;
        }

        public ExcavatorProjectile Copy()
        {
            ExcavatorProjectile proj = GameObject.Instantiate(this.gameObject).GetComponent<ExcavatorProjectile>();
            proj.transform.SetParent(transform.parent);
            return proj;
        }
    }
}