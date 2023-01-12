namespace UnchordMetroidvania
{
    public class MentisFSM : FiniteStateMachineNodeBT<Mentis>
    {
        public MentisFSM(Mentis instance)
        : base(instance, 2)
        {

        }

        protected override int GetNextChildIndex()
        {
            bool bAggro = instance.bAggro;

            if(bAggro)
                return 1;
            else
                return 0;
        }
    }
}