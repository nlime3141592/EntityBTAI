namespace UnchordMetroidvania
{
    public class MantisWalkFront : MantisWalk
    {
        private float m_ix;

        public MantisWalkFront(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {
            
        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();
            m_ix = mantis.lookDir.x;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = m_ix * mantis.moveDir.x * data.walkSpeed;
            float vy = m_ix * mantis.moveDir.y * data.walkSpeed - 0.1f;

            mantis.vm.SetVelocityXY(vx, vy);
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(mantis.senseData.bOnWallFront)
            {
                fsm.Change(fsm.idle);
                return true;
            }
            return false;
        }
    }
}