using System;
using System.Collections.Generic;

namespace Unchord
{
    public interface IStateMachineRemote
    {
        int current { get; }
        bool bStarted { get; }
        bool bPaused { get; }

        event Action onMachineBegin;
        event Action onMachinePause;
        event Action onMachineUnpause;
        event Action onStateChange;
        event Action onMachineEnd;

        void FixedUpdate();
        void Update();
        void LateUpdate();
        void OnDrawGizmos(bool _bShow);

        void RegisterStateMap(Dictionary<int, int> _stateMap);
        void UnregisterStateMap();

        int GetMappedValue(int _state);
        int GetMappedValueInverse(int _state);

        void Pause(bool _bPaused);
        void End();
    }
}