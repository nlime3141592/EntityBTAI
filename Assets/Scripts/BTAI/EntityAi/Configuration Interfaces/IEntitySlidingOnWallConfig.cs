namespace UnchordMetroidvania
{
    public interface IEntitySlidingOnWallConfig : IEntityOnWallConfig, IEntityMovementOnWallConfig
    {
        float slidingSpeed { get; set; }
        float slidingGravity { get; set; }
    }
}