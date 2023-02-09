namespace UnchordMetroidvania
{
    public abstract class FiniteState<T>
    {
        protected T instance { get; private set; }

        public FiniteState(T _instance)
        {
            instance = _instance;
        }

        public virtual void OnStateBegin() {}
        public virtual void OnFixedUpdateAlways() {}
        public virtual void OnFixedUpdate() {}
        public virtual void OnUpdateAlways() {}
        public virtual void OnUpdate() {}
        public virtual bool CanTransit() { return true; }
        public virtual int Transit() { return FiniteStateMachine.c_st_BASE_IGNORE; }
        public virtual void OnStateEnd() {}
    }
}