namespace UnchordMetroidvania
{
    public class _MantisIdle : MantisOnFloor
    {
        public _MantisIdle(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {

        }

        protected override void p_OnStateBegin()
        {
            p_OnStateBegin();

            mantis.vm.FreezePositionX();
            mantis.vm.MeltPositionY();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            mantis.vm.SetVelocityY(-1.0f);
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            
            return false;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            mantis.vm.MeltPositionX();
        }
    }
}