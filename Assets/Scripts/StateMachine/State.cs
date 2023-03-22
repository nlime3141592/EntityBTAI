using System.Collections.Generic;

namespace Unchord
{
    public abstract class State<T> : IState<T>
    where T : class
    {
        public T instance { get; private set; }
        public int id { get; protected set; }

#region method implementation of interface IState<T>
        // UnityEngine.FixedUpdate()
        public virtual void OnFixedUpdateAlways() {}
        public virtual void OnFixedUpdate() {}

        // UnityEngine.Update()
        public virtual void OnStateBegin() {}
        public virtual void OnUpdateAlways() {}
        public virtual void OnUpdate() {}
        public virtual bool CanTransit() => true;
        public virtual int Transit() => MachineConstant.c_lt_PASS;
        public virtual void OnStateEnd() {}

        // UnityEngine.LateUpdate()
        public virtual void OnLateUpdateAlways() {}
        public virtual void OnLateUpdate() {}

        // UnityEngine.OnDrawGizmos()
        public virtual void OnDrawGizmoAlways() {}
        public virtual void OnDrawGizmo() {}

        // StateMachine<T>.Begin()
        public virtual void OnMachineBegin(T _instance, int _id)
        {
            instance = _instance;
            id = _id;
        }

        // StateMachine<T>.End();
        public virtual void OnMachineEnd() {}

        // StateMachine<T>.Transit()
        public virtual void OnMachineHalt() {}
#endregion

        public virtual IEnumerable<IState<T>> GetStateCollectionDFS()
        {
            yield return this;
        }
    }
}