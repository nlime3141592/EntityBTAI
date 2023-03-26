namespace Unchord
{
    public abstract class PlayerRush : PlayerAbility
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.bFixedLookDirByAxis.x = true;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            instance.bInvincibility = instance.aController.bBeginOfAction && !instance.aController.bEndOfAction;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            instance.bFixedLookDirByAxis.x = false;
        }
    }
}