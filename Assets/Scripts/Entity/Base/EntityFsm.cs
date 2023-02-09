namespace UnchordMetroidvania
{
    public abstract class EntityFsm<T> : BehaviourFsm<T>
    where T : EntityBase
    {
        public EntityFsm(T _instance, int _capacity)
        : base(_instance, _capacity)
        {

        }

        public override int Transit()
        {
            int current = base.Transit();
            instance.aState = current;
            instance.aController.ChangeActionPhase(instance.aPhase);
            instance.aController.ChangeAnimation(instance.aState);
            return current;
        }
    }
}