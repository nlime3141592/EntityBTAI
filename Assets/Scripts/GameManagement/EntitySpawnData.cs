namespace UnchordMetroidvania
{
    public class EntitySpawnData
    {
        public string name;
        public EntityBase entity;

        public EntitySpawnData(string name, EntityBase entity)
        {
            this.name = name;
            this.entity = entity;
        }
    }
}