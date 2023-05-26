using System;

namespace Unchord
{
    public interface IStateMachineBase
    {
        bool bStarted { get; }
        bool bPaused { get; }

        IStateBase state { get; }
/*
        event Action<IStateMachineBase, int> onMachineBegin;
        event Action<IStateMachineBase, int> onMachinePause;
        event Action<IStateMachineBase, int> onMachineUnpause;
        event Action<IStateMachineBase, int, int> onParseTransit;
        event Action<IStateMachineBase, int, int> onStateChange;
        event Action<IStateMachineBase, int> onMachineEnd;
        event Action<IStateMachineBase, int> onMachineHalt;
*/
        void FixedUpdate();
        void Update();
        void LateUpdate();
        void OnDrawGizmos(bool _bShow);

        void Begin(int _initIdConstant);
        void Change(int _state);
        void Pause(bool _bPaused);
        void End();
    }
}