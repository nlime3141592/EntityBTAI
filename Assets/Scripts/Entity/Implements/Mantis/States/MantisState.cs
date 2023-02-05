namespace UnchordMetroidvania
{
    public abstract class MantisState : MonsterState<Mantis>
    {
        protected MantisFsm fsm => instance.fsm;
        protected MantisData data => instance.data;
        protected Mantis mantis => instance;
        // protected MantisFsm fsm => instance.fsm;

        public MantisState(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {
            
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            mantis.senseData.UpdateData(mantis);
        }

        public override bool OnUpdate()
        {
            if(mantis.health <= 0)
            {
                fsm.Change(fsm.die);
                return true;
            }
            else if(base.OnUpdate())
                return true;

            return false;
        }
    }
}