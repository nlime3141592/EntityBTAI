namespace UnchordMetroidvania
{
    public class CheckAggroChange<T> : TaskNodeBT<T>
    where T : EntityMonster
    {
        public CheckAggroChange(T instance)
        : base(instance)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            bool prev = instance.bPrevAggro;
            bool cur = instance.bAggro;

            if(prev != cur)
            {
                instance.bPrevAggro = instance.bAggro;

                if(cur)
                    instance.OnAggroBegin();
                else
                    instance.OnAggroEnd();

                return InvokeResult.Success;
            }
            else
            {
                return InvokeResult.Failure;
            }
        }
    }
}