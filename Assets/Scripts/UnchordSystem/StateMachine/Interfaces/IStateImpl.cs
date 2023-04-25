namespace Unchord
{
    public interface IStateImpl<T> : IState<T>
    where T : class
    {
        int idConstant { get; }

        void OnConstruct();

        void OnMachineBegin();
        void OnMachineEnd();
        void OnMachineHalt();

        void OnStateBegin();
        void OnStateEnd();

        bool CanTransit();
        int Transit();

        void OnFixedUpdateAlways();
        void OnFixedUpdate();

        void OnUpdateAlways();
        void OnUpdate();

        void OnLateUpdateAlways();
        void OnLateUpdate();

        void OnDrawGizmoAlways();
        void OnDrawGizmo();
    }
}