namespace UnchordMetroidvania
{
    public class CheckDeadHealth<T> : TaskNodeBT<T>
    where T : EntityBase
    {
        public CheckDeadHealth(T instance)
        : base(instance)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(instance.health <= 0)
                return InvokeResult.Success;
            else
                return InvokeResult.Failure;
        }
    }
}