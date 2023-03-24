namespace Unchord
{
    public abstract class PlayerRush : PlayerAbility
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.bFixLookDir.x = true;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            instance.bFixLookDir.x = false;
        }
    }
}