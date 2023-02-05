namespace UnchordMetroidvania
{
    public abstract class MantisAbility : MantisState
    {
        public MantisAbility(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();
        }
    }
}