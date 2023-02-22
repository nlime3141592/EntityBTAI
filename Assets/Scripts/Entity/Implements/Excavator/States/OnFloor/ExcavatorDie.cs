namespace UnchordMetroidvania
{
    public class ExcavatorDie : ExcavatorOnFloor
    {
        public ExcavatorDie(Excavator _instance)
        : base(_instance)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            excavator.AllowHitFromBattleModule(false);
            excavator.armObj.SetActive(false);
        }

        public override void OnUpdate()
        {
            if(excavator.aController.bEndOfAnimation)
                excavator.bEndOfEntity = true;
        }
    }
}