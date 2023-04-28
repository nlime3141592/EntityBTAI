namespace Unchord
{
    public class ExcavatorDie : ExcavatorOnFloor
    {
        public override int idConstant => Excavator.c_st_DIE;

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.bDeadState = true;
            // instance.AllowHitFromBattleModule(false);
            instance.IgnoreBattleTrigger(null, false); // TODO: 배틀 트리거를 넣어줘야 함.
            instance.armObj.SetActive(false);
        }

        public override void OnUpdate()
        {
            if(instance.aController.bEndOfAnimation)
                instance.bEndOfEntity = true;
        }
    }
}