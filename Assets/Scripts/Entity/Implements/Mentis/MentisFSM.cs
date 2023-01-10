namespace UnchordMetroidvania
{
    public class MentisFSM : FiniteStateMachineNodeBT<Mentis>
    {
        public MentisFSM(ConfigurationBT<Mentis> config, int id, string name)
        : base(config, id, name, 2)
        {

        }

        protected override int GetNextChildIndex()
        {
            bool bAggro = config.instance.bAggro;

            if(bAggro)
                return 1;
            else
                return 0;
        }
    }
}