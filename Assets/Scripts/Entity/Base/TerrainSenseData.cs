namespace UnchordMetroidvania
{
    public abstract class TerrainSenseData<T>
    where T : EntityBase
    {
        public abstract void UpdateOrigins(T instane);
        public abstract void UpdateData(T instance);
        public abstract void UpdateMoveDir(T instance);
    }
}