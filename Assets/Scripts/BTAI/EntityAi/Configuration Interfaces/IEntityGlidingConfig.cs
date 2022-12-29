namespace UnchordMetroidvania
{
    public interface IEntityGlidingConfig : IEntityFreeFallConfig
    {
        float glidingAcceleration { get; set; }
        float glidingSpeed { get; set; }
    }
}