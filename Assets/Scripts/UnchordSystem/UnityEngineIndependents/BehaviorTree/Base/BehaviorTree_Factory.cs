namespace UnchordMetroidvania
{
    public static class BehaviorTree
    {
        #region Controls
        public static IfOnlyNodeBT<T> IfOnly<T>(T instance) => new IfOnlyNodeBT<T>(instance);
        public static IfElseNodeBT<T> IfElse<T>(T instance) => new IfElseNodeBT<T>(instance);
        public static ParallelNodeBT<T> Parallel<T>(T instance, int capacity) => new ParallelNodeBT<T>(instance, capacity);
        public static SelectorNodeBT<T> Selector<T>(T instance, int capacity) => new SelectorNodeBT<T>(instance, capacity);
        public static SequenceNodeBT<T> Sequence<T>(T instance, int capacity) => new SequenceNodeBT<T>(instance, capacity);
        #endregion

        #region Decorators
        public static InverterNodeBT<T> Inverter<T>(T instance) => new InverterNodeBT<T>(instance);
        public static LoopNodeBT<T> Loop<T>(T instance) => new LoopNodeBT<T>(instance);
        public static RetryNodeBT<T> Retry<T>(T instance) => new RetryNodeBT<T>(instance);
        #endregion

        #region Tasks
        public static FailureNodeBT<T> Failure<T>(T instance) => new FailureNodeBT<T>(instance);        
        public static RunningNodeBT<T> Running<T>(T instance) => new RunningNodeBT<T>(instance);
        public static SuccessNodeBT<T> Success<T>(T instance) => new SuccessNodeBT<T>(instance);
        public static WaitNodeBT<T> Wait<T>(T instance) => new WaitNodeBT<T>(instance);
        #endregion
    }
}