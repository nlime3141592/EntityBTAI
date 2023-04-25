namespace Unchord
{
    public abstract class State<T> : IStateImpl<T>
    where T : class
    {
        public abstract int idConstant { get; }

        protected internal IStateMachine<T> machine { get; internal set; }
        protected internal T instance => machine.instance;

        public virtual void OnConstruct() {}

        public virtual void OnMachineBegin() {}
        public virtual void OnMachineEnd() {}
        public virtual void OnMachineHalt() {}

        public virtual void OnStateBegin() {}
        public virtual void OnStateEnd() {}

        public virtual bool CanTransit() => true;
        public virtual int Transit() => MachineConstant.c_lt_PASS;

        public virtual void OnFixedUpdateAlways() {}
        public virtual void OnFixedUpdate() {}

        public virtual void OnUpdateAlways() {}
        public virtual void OnUpdate() {}

        public virtual void OnLateUpdateAlways() {}
        public virtual void OnLateUpdate() {}

        public virtual void OnDrawGizmoAlways() {}
        public virtual void OnDrawGizmo() {}

        public State()
        {
            OnConstruct();
        }
    }
}