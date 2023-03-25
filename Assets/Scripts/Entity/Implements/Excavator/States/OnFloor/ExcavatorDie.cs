namespace Unchord
{
    public class ExcavatorDie : ExcavatorOnFloor
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.AllowHitFromBattleModule(false);
            instance.armObj.SetActive(false);
        }

        public override void OnUpdate()
        {
            if(instance.aController.bEndOfAnimation)
                instance.bEndOfEntity = true;
        }
    }
}