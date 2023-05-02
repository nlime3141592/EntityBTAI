namespace Unchord
{
    public abstract class SandBagState : EntityState<SandBag>
    {
        public override void OnLateUpdate()
        {
            base.OnLateUpdate();

            instance.현재체력 = instance.health;
        }
    }
}