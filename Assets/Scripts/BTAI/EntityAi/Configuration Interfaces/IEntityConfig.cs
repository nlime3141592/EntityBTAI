namespace UnchordMetroidvania
{
    public interface IEntityConfig : IConfigurationBT
    {
        int currentState { get; set; }
        float fixedDeltaTime { get; set; }
    }
}