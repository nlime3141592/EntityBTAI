namespace UnchordMetroidvania
{
    public interface IEntityFreeFallConfig : IEntityMovementConfig
    {
        float freeFallGravity { get; set; }
        float minFreeFallSpeed { get; set; }
    }
}