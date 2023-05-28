namespace Unchord
{
    public abstract class PlayerTakeDownBase : PlayerAttack
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.bFixedLookDirByAxis.x = true;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.bFixedLookDirByAxis.x = false;
        }
    }
}