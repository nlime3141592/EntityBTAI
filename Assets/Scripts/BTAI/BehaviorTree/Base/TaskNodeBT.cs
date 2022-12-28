namespace UnchordMetroidvania
{
    public abstract class TaskNodeBT<T_ConfigurationBT> : NodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        protected TaskNodeBT(T_ConfigurationBT config, int id, string name)
        : base(config, id, name)
        {

        }
    }
}