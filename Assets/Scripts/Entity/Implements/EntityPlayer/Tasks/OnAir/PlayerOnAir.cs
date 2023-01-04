namespace UnchordMetroidvania
{
    public abstract class PlayerOnAir : PlayerAction
    {
        public PlayerOnAir(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }
    }
}