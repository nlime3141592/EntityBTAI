namespace UnchordMetroidvania
{
    public interface IEntityInputConfig : IConfigurationBT, IEntityConfig
    {
        IEntityInputConfig inputConfig { get; }

        float xNegative { get; set; }
        float xPositive { get; set; }
        float yNegative { get; set; }
        float yPositive { get; set; }

        float xInput { get; }
        float yInput { get; }
    }
}