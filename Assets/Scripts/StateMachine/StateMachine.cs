using System;
using System.Collections.Generic;
using System.Linq;

namespace Unchord
{
    public class StateMachine<T> : IStateMachine<T>
    where T : class
    {
        public bool bStarted { get; private set; }
        public bool bPaused { get; private set; }

        public int current { get; private set; }
        public IStateBase state => m_states[current];

        public event Action onMachineBegin;
        public event Action onMachinePause;
        public event Action onMachineUnpause;
        public event Action onStateChange;
        public event Action onMachineEnd;

        private IState<T> m_stateTree;
        private List<IState<T>> m_states;
        private Dictionary<int, int> m_stateMap;

        public StateMachine(int _capacity = 1)
        {
            m_states = new List<IState<T>>(_capacity < 1 ? 1 : _capacity);
        }

#region method implementation of interface IStateMachine<T>
        public void Begin(T _instance, IState<T> _stateTree, int _state)
        {
            bStarted = true;

            onMachineBegin?.Invoke();

            int id = -1;
            m_states.Clear();
            foreach(IState<T> state in _stateTree.GetStateCollectionDFS())
            {
                m_states.Add(state);
                state.OnMachineBegin(_instance, ++id);
            }

            if(m_stateMap != null)
                _state = m_stateMap[_state];

            m_stateTree = _stateTree;
            current = _state;

            m_states[current].OnStateBegin();
        }

        public void Pause(bool _bPaused)
        {
            if(bPaused == _bPaused)
                return;

            bPaused = _bPaused;

            if(_bPaused)
                onMachinePause?.Invoke();
            else
                onMachineUnpause?.Invoke();
        }

        public void Change(int _state)
        {
            m_states[current].OnStateEnd();
            current = _state;
            onStateChange?.Invoke();
            m_states[current].OnStateBegin();
        }

        public void End()
        {
            m_states[current].OnStateEnd();

            int count = m_states.Count;
            for(int i = 0; i < count; ++i)
                m_states[i].OnMachineEnd();

            onMachineEnd?.Invoke();

            current = MachineConstant.c_st_MACHINE_OFF;
            bStarted = false;
        }

        public void FixedUpdate()
        {
            if(m_bPassEvent())
                return;

            int count = m_states.Count;

            for(int i = 0; i < count; ++i)
                m_states[i].OnFixedUpdateAlways();

            m_states[current].OnFixedUpdate();
        }

        public void Update()
        {
            if(m_bPassEvent())
                return;

            int count = m_states.Count;

            for(int i = 0; i < count; ++i)
                m_states[i].OnUpdateAlways();

            m_states[current].OnUpdate();

            m_Transit();
        }

        public void LateUpdate()
        {
            if(m_bPassEvent())
                return;

            int count = m_states.Count;

            for(int i = 0; i < count; ++i)
                m_states[i].OnLateUpdateAlways();

            m_states[current].OnLateUpdate();
        }

        public void OnDrawGizmos(bool _bShow)
        {
            if(m_bPassEvent())
                return;
            else if(!_bShow)
                return;

            int count = m_states.Count;

            for(int i = 0; i < count; ++i)
                m_states[i].OnDrawGizmoAlways();

            m_states[current].OnDrawGizmo();
        }

        public void RegisterStateMap(Dictionary<int, int> _stateMap)
        {
            m_stateMap = _stateMap;
        }

        public void UnregisterStateMap()
        {
            m_stateMap = null;
        }

        public int GetMappedValue(int _state)
        {
            if(m_stateMap == null)
                return -1;

            return m_stateMap[_state];
        }

        public int GetMappedValueInverse(int _state)
        {
            if(m_stateMap == null)
                return -1;

            bool CheckCondition(KeyValuePair<int, int> _pair)
            {
                return _pair.Value == _state;
            }

            return m_stateMap.FirstOrDefault(CheckCondition).Key;
        }
#endregion

        private void m_Transit()
        {
            int next = m_states[current].Transit();

            switch(next)
            {
                case MachineConstant.c_st_MACHINE_OFF:
                    End();
                    break;

                case MachineConstant.c_lt_HALT:
                    End();
                    m_PublishHalt();
                    break;

                case MachineConstant.c_lt_CONTINUE: // current state continues, any state change event not occurs.
                case MachineConstant.c_lt_PASS: // NOTE: all inherited states returned c_lt_PASS, means no transition, current state continues.
                    break;

                default:
                    if(m_stateMap != null)
                        next = m_stateMap[next];

                    if(next < 0 || next >= m_states.Count)
                    {
                        End();
                        m_PublishHalt();
                    }
                    else if(m_states[next].CanTransit())
                        Change(next);
                    // else, current state continues.
                    break;
            }
        }

        private void m_PublishHalt()
        {
            int count = m_states.Count;

            for(int i = 0; i < count; ++i)
                m_states[i].OnMachineHalt();
        }

        private bool m_bPassEvent()
        {
            return !bStarted || bPaused;
        }
    }
}