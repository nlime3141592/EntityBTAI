namespace UnchordMetroidvania
{
    public abstract class BranchNodeBT<T> : NodeBT<T>
    {
        protected BranchNodeBT(ConfigurationBT<T> config, int id, string name)
        : base(config, id, name)
        {

        }

        public abstract void ResetChild();
    }
}