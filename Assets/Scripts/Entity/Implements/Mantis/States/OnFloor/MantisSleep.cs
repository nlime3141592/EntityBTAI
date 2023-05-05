namespace Unchord
{
    public class MantisSleep : MantisOnFloor
    {
        public override int idConstant => Mantis.c_st_SLEEP;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            // TODO: 이 곳에서 sleep 상태 시 어그로 영역을 이용한 타겟 감지를 수행.
        }
    }
}