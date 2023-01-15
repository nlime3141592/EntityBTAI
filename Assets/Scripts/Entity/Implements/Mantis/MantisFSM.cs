namespace UnchordMetroidvania
{
    public class MantisFSM : FiniteStateMachineNodeBT<Mantis>
    {
        public MantisFSM(Mantis instance)
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