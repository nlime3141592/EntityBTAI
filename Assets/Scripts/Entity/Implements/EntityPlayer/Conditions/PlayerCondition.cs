namespace UnchordMetroidvania
{
    public abstract class PlayerCondition : ConditionNodeBT<EntityPlayer>
    {
        public PlayerCondition(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {
            
        }
    }
}