using System;
using System.Collections.Generic;

namespace Unchord
{
    public class CompositeState<T> : State<T>, ICompositeState<T>
    where T : class
    {
        public IState<T> this[int _index]
        {
            get => m_states[_index];
            set => m_states[_index] = value;
        }

        public int capacity { get; private set; }

        private IState<T>[] m_states;

        public CompositeState(int _capacity = 1)
        {
            id = -1;
            capacity = _capacity < 1 ? 1 : _capacity;
            m_states = new IState<T>[capacity];
        }

#region method overriding of interface IStateBase
        public override void OnFixedUpdateAlways()
        {
            throw new NotImplementedException("Composite state cannot implement events.");
        }

        public override void OnFixedUpdate()
        {
            throw new NotImplementedException("Composite state cannot implement events.");
        }

        public override void OnUpdateAlways()
        {
            throw new NotImplementedException("Composite state cannot implement events.");
        }

        public override void OnUpdate()
        {
            throw new NotImplementedException("Composite state cannot implement events.");
        }

        public override int Transit()
        {
            // throw new NotImplementedException("Composite state cannot implement events.");
            return MachineConstant.c_lt_HALT;
        }

        public override void OnLateUpdateAlways()
        {
            throw new NotImplementedException("Composite state cannot implement events.");
        }

        public override void OnLateUpdate()
        {
            throw new NotImplementedException("Composite state cannot implement events.");
        }

        public override void OnDrawGizmoAlways()
        {
            throw new NotImplementedException("Composite state cannot implement events.");
        }

        public override void OnDrawGizmo()
        {
            throw new NotImplementedException("Composite state cannot implement events.");
        }
#endregion

#region method overriding of interface IState<T>
        public override void OnMachineBegin(T _instance, int _id)
        {
            throw new NotImplementedException("Composite state cannot implement events.");
        }

        public override void OnMachineEnd()
        {
            throw new NotImplementedException("Composite state cannot implement events.");
        }

        public override void OnMachineHalt()
        {
            throw new NotImplementedException("Composite state cannot implement events.");
        }

        public sealed override IEnumerable<IState<T>> GetStateCollectionDFS()
        {
            foreach(IState<T> currentState in GetInternalStateCollection())
            foreach(IState<T> dfsState in currentState.GetStateCollectionDFS())
                yield return dfsState;
        }
#endregion

        protected IEnumerable<IState<T>> GetInternalStateCollection()
        {
            for(int i = 0; i < capacity; ++i)
                if(m_states[i] != null)
                    yield return m_states[i];
        }
    }
}