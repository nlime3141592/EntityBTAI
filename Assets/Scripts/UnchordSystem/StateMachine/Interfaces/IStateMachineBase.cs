using System;

namespace Unchord
{
    public interface IStateMachineBase
    {
        bool bStarted { get; }
        bool bPaused { get; }

        IStateBase state { get; }

        // event Action onMachineBegin;
        // event Action onMachinePause;
        // event Action onMachineUnpause;
        // event Action onStateChange;
        // event Action onMachineEnd;

        void FixedUpdate();
        void Update();
        void LateUpdate();
        void OnDrawGizmos(bool _bShow);

        void Begin(IStateBase _stateTree, int _initIdConstant);
        void Change(int _state);
        void Pause(bool _bPaused);
        void End();
    }
}