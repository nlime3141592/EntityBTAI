namespace Unchord
{
    public interface IStateBase
    {
        int idConstant { get; }

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