namespace Unchord
{
    public class ExcavatorDie : ExcavatorOnFloor
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();
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