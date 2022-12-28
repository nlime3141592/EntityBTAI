namespace UnchordMetroidvania
{
    public abstract class ConditionNodeBT<T_ConfigurationBT> : BranchNodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        protected ConditionNodeBT(T_ConfigurationBT config, int id, string name)
        : base(config, id, name)
        {
            
        }
    }
}