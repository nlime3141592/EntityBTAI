namespace UnchordMetroidvania
{
    public abstract class MantisOnFloor : MantisState
    {
        public MantisOnFloor(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {
            
        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;

            return false;
        }
    }
}