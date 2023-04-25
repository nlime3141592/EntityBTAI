namespace Unchord
{
    public class SandBag : Entity
    {
        public const int c_st_DIE = 0;
        public const int c_st_IDLE = 1;

        public float 현재체력;

        protected override IStateMachineBase InitStateMachine()
        {
            base.InitStateMachine();

            IStateMachine<SandBag> fsm = new StateMachine<SandBag>(2);

            StateComposite<SandBag> root = new StateComposite<SandBag>(2);
            root[0] = new SandBagDie();
            root[1] = new SandBagIdle();

            fsm.Begin(root, SandBag.c_st_IDLE);
            return fsm;
        }

        protected override void PostUpdate()
        {
            base.PostUpdate();

            현재체력 = base.health;
        }
    }
}