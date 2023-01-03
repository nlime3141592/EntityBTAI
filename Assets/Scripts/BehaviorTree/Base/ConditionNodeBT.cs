namespace UnchordMetroidvania
{
    public abstract class ConditionNodeBT<T> : BranchNodeBT<T>
    {
        protected ConditionNodeBT(ConfigurationBT<T> config, int id, string name)
        : base(config, id, name)
        {
            
        }

        public override void ResetChild()
        {

        }
    }
}