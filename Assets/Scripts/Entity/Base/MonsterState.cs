namespace UnchordMetroidvania
{
    public abstract class MonsterState<T> : EntityState<T>
    where T : EntityBase
    {
        public MonsterState(T _monster, int _id, string _name)
        : base(_monster, _id, _name)
        {
            
        }
    }
}