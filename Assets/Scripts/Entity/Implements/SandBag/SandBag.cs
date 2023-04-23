namespace Unchord
{
    public class SandBag : Entity
    {
        public const int c_st_DIE = 0;
        public const int c_st_IDLE = 1;

        public IStateMachine<SandBag> fsm;
        private IState<SandBag> m_stateTree;

        public float 현재체력;

        protected override void InitStateMachine()
        {
            base.InitStateMachine();

            fsm = new StateMachine<SandBag>(2);

            CompositeState<SandBag> root = new CompositeState<SandBag>(2);
            root[0] = new SandBagDie();
            root[1] = new SandBagIdle();

            m_stateTree = root;

            // fsm.RegisterStateMap(stateMap);
            RegisterMachineEvent(fsm);
            fsm.Begin(this, m_stateTree, SandBag.c_st_IDLE);
        }

        protected override void PostUpdate()
        {
            base.PostUpdate();

            현재체력 = base.health;
        }
    }
}