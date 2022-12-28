namespace UnchordMetroidvania
{
    public abstract class BranchNodeBT<T_ConfigurationBT> : NodeBT<T_ConfigurationBT>
    where T_ConfigurationBT : IConfigurationBT
    {
        protected BranchNodeBT(T_ConfigurationBT config, int id, string name)
        : base(config, id, name)
        {

        }
    }
}