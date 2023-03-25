namespace Unchord
{
    public abstract class PlayerState : EntityState<Player>
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.senseData.UpdateData(instance);
        }
    }
}