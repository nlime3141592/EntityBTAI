namespace Unchord
{
    public abstract class PlayerRush : PlayerAbility
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.bFixedLookDirByAxis.x = true;
            instance.vm.FreezePosition(false, false);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            instance.bInvincibility = instance.bBeginOfAction && !instance.bEndOfAction;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            instance.bFixedLookDirByAxis.x = false;
        }
    }
}