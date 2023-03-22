using System.Collections.Generic;

namespace Unchord
{
    public interface IState<T> : IStateBase
    where T : class
    {
        T instance { get; }
        int id { get; }

        // UnityEngine.FixedUpdate()
        void OnFixedUpdateAlways();
        void OnFixedUpdate();

        // UnityEngine.Update()
        void OnStateBegin();
        void OnUpdateAlways();
        void OnUpdate();
        bool CanTransit();
        int Transit();
        void OnStateEnd();

        // UnityEngine.LateUpdate()
        void OnLateUpdateAlways();
        void OnLateUpdate();

        // UnityEngine.OnDrawGizmos()
        void OnDrawGizmoAlways();
        void OnDrawGizmo();

        // StateMachine<T>.Begin()
        void OnMachineBegin(T _instance, int _id);

        // StateMachine<T>.End()
        void OnMachineEnd();

        // StateMachine<T>.Transit()
        void OnMachineHalt();

        IEnumerable<IState<T>> GetStateCollectionDFS();
    }
}