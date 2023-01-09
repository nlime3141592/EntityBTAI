namespace UnchordMetroidvania
{
    public class CheckDeadHealth<T> : ConditionNodeBT<T>
    where T : EntityBase
    {
        public CheckDeadHealth(ConfigurationBT<T> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(config.instance.health <= 0)
                return InvokeResult.Success;
            else
                return InvokeResult.Failure;
        }
    }
}