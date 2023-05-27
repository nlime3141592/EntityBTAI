namespace Unchord
{
    public class EntitySpawnData
    {
        public string name;
        public Entity entity;

        public EntitySpawnData(string name, Entity entity)
        {
            this.name = name;
            this.entity = entity;
        }
    }
}