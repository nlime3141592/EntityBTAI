namespace Unchord
{
    public abstract class TerrainSenseData<T>
    where T : Entity
    {
        public abstract void UpdateOrigins(T instane);
        public abstract void UpdateData(T instance);
        public abstract void UpdateMoveDir(T instance);
    }
}