namespace UnchordMetroidvania
{
    public abstract class MantisAbility : MantisState
    {
        public MantisAbility(Mantis _mantis)
        : base(_mantis)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
        }
    }
}