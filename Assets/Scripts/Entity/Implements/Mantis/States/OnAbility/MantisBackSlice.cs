namespace Unchord
{
    public class MantisBackSlice : MantisAttack
    {
        private bool m_bRotated;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            m_bRotated = false;

            // TODO: 테스트 코드
            // MantisBackSlice.OnFixedUpdate() 함수에서
            // AnimationController.bOnActionBegin == true가 되는 순간을 찾아 이 함수를 호출하도록 수정할 것.
            instance.lookDir.x = (Direction)(-(int)instance.lookDir.x);
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
    }
}