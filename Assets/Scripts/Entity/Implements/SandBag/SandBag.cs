namespace Unchord
{
    public class SandBag : Entity
    {
        public const int c_st_DIE = 0;
        public const int c_st_IDLE = 1;

        public float 현재체력;

        public override IStateMachineBase InitStateMachine()
        {
            IStateMachine<SandBag> fsm = new StateMachine<SandBag>(2);

            fsm.Add(new SandBagDie());
            fsm.Add(new SandBagIdle());

            fsm.instance = this;
            fsm.Begin(SandBag.c_st_IDLE);
            return fsm;
        }
    }
}