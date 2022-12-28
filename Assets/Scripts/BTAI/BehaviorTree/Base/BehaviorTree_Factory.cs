namespace UnchordMetroidvania
{
    public static class BehaviorTree
    {
        #region Controls
        public static IfElseNodeBT<T_ConfigurationBT> IfOnly<T_ConfigurationBT>(T_ConfigurationBT config)
        where T_ConfigurationBT : IConfigurationBT => new IfElseNodeBT<T_ConfigurationBT>(config, -1, "IfOnly", 2);

        public static IfElseNodeBT<T_ConfigurationBT> IfElse<T_ConfigurationBT>(T_ConfigurationBT config)
        where T_ConfigurationBT : IConfigurationBT => new IfElseNodeBT<T_ConfigurationBT>(config, -1, "IfElse", 3);

        public static IfElseReactiveNodeBT<T_ConfigurationBT> IfOnlyReactive<T_ConfigurationBT>(T_ConfigurationBT config)
        where T_ConfigurationBT : IConfigurationBT => new IfElseReactiveNodeBT<T_ConfigurationBT>(config, -1, "IfOnlyReactive", 2);

        public static IfElseReactiveNodeBT<T_ConfigurationBT> IfElseReactive<T_ConfigurationBT>(T_ConfigurationBT config)
        where T_ConfigurationBT : IConfigurationBT => new IfElseReactiveNodeBT<T_ConfigurationBT>(config, -1, "IfElseReactive", 3);

        public static ParallelNodeBT<T_ConfigurationBT> Parallel<T_ConfigurationBT>(T_ConfigurationBT config, int initCapacity)
        where T_ConfigurationBT : IConfigurationBT => new ParallelNodeBT<T_ConfigurationBT>(config, initCapacity);

        public static SelectorNodeBT<T_ConfigurationBT> Selector<T_ConfigurationBT>(T_ConfigurationBT config, int initCapacity)
        where T_ConfigurationBT : IConfigurationBT => new SelectorNodeBT<T_ConfigurationBT>(config, initCapacity);

        public static SequenceNodeBT<T_ConfigurationBT> Sequence<T_ConfigurationBT>(T_ConfigurationBT config, int initCapacity)
        where T_ConfigurationBT : IConfigurationBT => new SequenceNodeBT<T_ConfigurationBT>(config, initCapacity);
        #endregion

        #region Decorators
        public static InverterNodeBT<T_ConfigurationBT> Interter<T_ConfigurationBT>(T_ConfigurationBT config, NodeBT<T_ConfigurationBT> node)
        where T_ConfigurationBT : IConfigurationBT => new InverterNodeBT<T_ConfigurationBT>(config, node);

        public static LoopNodeBT<T_ConfigurationBT> Loop<T_ConfigurationBT>(T_ConfigurationBT config, NodeBT<T_ConfigurationBT> node, int loopFrame)
        where T_ConfigurationBT : IConfigurationBT => new LoopNodeBT<T_ConfigurationBT>(config, node, loopFrame);

        public static PostDelayNodeBT<T_ConfigurationBT> PostDelay<T_ConfigurationBT>(T_ConfigurationBT config, NodeBT<T_ConfigurationBT> node, int delayFrame)
        where T_ConfigurationBT : IConfigurationBT => new PostDelayNodeBT<T_ConfigurationBT>(config, node, delayFrame);

        public static PreDelayNodeBT<T_ConfigurationBT> PreDelay<T_ConfigurationBT>(T_ConfigurationBT config, NodeBT<T_ConfigurationBT> node, int delayFrame)
        where T_ConfigurationBT : IConfigurationBT => new PreDelayNodeBT<T_ConfigurationBT>(config, node, delayFrame);

        public static RetryNodeBT<T_ConfigurationBT> Retry<T_ConfigurationBT>(T_ConfigurationBT config, NodeBT<T_ConfigurationBT> node, int tryCount)
        where T_ConfigurationBT : IConfigurationBT => new RetryNodeBT<T_ConfigurationBT>(config, node, tryCount);
        #endregion

        #region Tasks
        public static WaitNodeBT<T_ConfigurationBT> Wait<T_ConfigurationBT>(T_ConfigurationBT config, NodeBT<T_ConfigurationBT> node, int waitFrame)
        where T_ConfigurationBT : IConfigurationBT => new WaitNodeBT<T_ConfigurationBT>(config, waitFrame);
        #endregion
    }
}