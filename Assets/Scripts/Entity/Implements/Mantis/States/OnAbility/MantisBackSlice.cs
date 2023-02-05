namespace UnchordMetroidvania
{
    public class MantisBackSlice : MantisAttack
    {
        private bool m_bRotated;

        public MantisBackSlice(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {
            
        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();

            m_bRotated = false;

            // TODO: 테스트 코드
            // MantisBackSlice.OnFixedUpdate() 함수에서
            // AnimationController.bOnActionBegin == true가 되는 순간을 찾아 이 함수를 호출하도록 수정할 것.
            mantis.lookDir.x = -mantis.lookDir.x;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
/*
            if(!m_bRotated && mantis.aController.bBeginOfAction)
            {
                m_bRotated = true;
                mantis.lookDir.x = -mantis.lookDir.x;
            }
*/
        }

        public override bool CanAttack()
        {
            return true;
        }
    }
}