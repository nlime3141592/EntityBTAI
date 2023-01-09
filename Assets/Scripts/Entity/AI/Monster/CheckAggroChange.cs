namespace UnchordMetroidvania
{
    public class CheckAggroChange<T> : ConditionNodeBT<T>
    where T : EntityMonster
    {
        public CheckAggroChange(ConfigurationBT<T> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            bool prev = config.instance.bPrevAggro;
            bool cur = config.instance.bAggro;

            if(prev != cur)
            {
                config.instance.bPrevAggro = config.instance.bAggro;

                if(cur)
                    config.instance.OnAggroBegin();
                else
                    config.instance.OnAggroEnd();

                return InvokeResult.Success;
            }
            else
            {
                return InvokeResult.Failure;
            }
        }
    }
}