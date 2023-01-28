namespace UnchordMetroidvania
{
    public abstract class MantisState : EntityState<Mantis>
    {
        protected Mantis mantis => instance;
        // protected MantisFsm fsm => instance.fsm;

        public MantisState(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {
            
        }
    }
}