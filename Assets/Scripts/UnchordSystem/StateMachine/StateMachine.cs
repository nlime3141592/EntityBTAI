using System;
using System.Collections.Generic;

namespace Unchord
{
    // 사용법
    // 1. IStateMachine<T>.instance에 개체를 지정한다.
    // 2. IStateMachine<T>.Add(IState<T>) 함수로 상태를 추가한다.
    // 3. IStateMachine<T>.Begin(int) 함수로 상태 기계를 시작한다.

    public sealed class StateMachine<T> : IStateMachine<T>
    where T : class
    {
        public T instance
        {
            get
            {
                if(bStarted)
                    return m_instance;
                else
                    return default(T);
            }
            set
            {
                if(!bStarted)
                    m_instance = value;
                else
                    // Context: state machine changes 'state' every frame using target instance and set of state-class.
                    // Problem: target instance align statement denied on this state machine.
                    // Solution: remove align statement where exists external.
                    // NOTE: 상태 기계 실행 중 변경될 수 없는 값임.
                    throw new StateMachineException(this, typeof(T));
            }
        }

        public bool bStarted { get; private set; }
        public bool bPaused { get; private set; }

        public IStateBase state => m_states[m_idMap[m_currentIdConstant]];

        public event Action<IStateMachineBase, int> onMachineBegin; // param: first id constant
        public event Action<IStateMachineBase, int> onMachinePause; // param: paused id constant
        public event Action<IStateMachineBase, int> onMachineUnpause; // param: unpaused id constant
        public event Action<IStateMachineBase, int, int> onParseTransit; // param: transited id constant
        public event Action<IStateMachineBase, int, int> onStateChange; // param: prev, current id constant
        public event Action<IStateMachineBase, int> onMachineEnd; // param: final id constant
        public event Action<IStateMachineBase, int> onMachineHalt; // param: halted id constant

        public readonly int capacity;
        private readonly IState<T>[] m_states;
        private readonly Dictionary<int, int> m_idMap;
        private T m_instance;
        private int m_currentIdConstant;

        public StateMachine(int _capacity)
        {
            if(_capacity < 1)
                // NOTE: 상태 기계는 반드시 하나 이상의 상태를 가질 수 있어야 함.
                throw new StateMachineException(this, typeof(T));

            capacity = _capacity;

            bStarted = false;
            bPaused = false;

            m_states = new IState<T>[_capacity];
            m_idMap = new Dictionary<int, int>(_capacity);
            m_currentIdConstant = MachineConstant.c_st_MACHINE_OFF;
        }

        public void Begin(int _initIdConstant)
        {
            m_idMap.Clear();

            for(int i = 0; i < capacity; ++i)
            {
                State<T> state = m_states[i] as State<T>;

                if(m_states[i] != null)
                {
                    m_idMap.Add(state.idConstant, i);
                    state.machine = this;
                    state.instance = m_instance;
                }
            }

            m_currentIdConstant = _initIdConstant;
            bStarted = true;
            bPaused = false;
            onMachineBegin?.Invoke(this, _initIdConstant);

            m_states[m_idMap[m_currentIdConstant]].OnStateBegin();
        }

        public void Pause(bool _bPaused)
        {
            if(bPaused == _bPaused)
                return;

            bPaused = _bPaused;

            if(_bPaused) onMachinePause?.Invoke(this, m_currentIdConstant);
            else onMachineUnpause?.Invoke(this, m_currentIdConstant);
        }

        public void Change(int _nextIdConstant)
        {
            int prev = m_currentIdConstant;

            m_states[m_idMap[m_currentIdConstant]].OnStateEnd();
            m_currentIdConstant = _nextIdConstant;
            onStateChange?.Invoke(this, prev, _nextIdConstant);
            m_states[m_idMap[m_currentIdConstant]].OnStateBegin();
        }

        public void End()
        {
            if(!bStarted)
                return;

            m_states[m_idMap[m_currentIdConstant]].OnStateEnd();
            onMachineEnd?.Invoke(this, m_currentIdConstant);

            bStarted = false;
            bPaused = false;
            m_currentIdConstant = MachineConstant.c_st_MACHINE_OFF;
        }

        public void FixedUpdate()
        {
            if(m_bPassEvent())
                return;

            for(int i = 0; i < m_states.Length; ++i)
                m_states[m_idMap[m_currentIdConstant]].OnFixedUpdateAlways();

            m_states[m_idMap[m_currentIdConstant]].OnFixedUpdate();
        }

        public void Update()
        {
            if(m_bPassEvent())
                return;

            // Update Logic
            for(int i = 0; i < m_states.Length; ++i)
                m_states[m_idMap[m_currentIdConstant]].OnUpdateAlways();

            m_states[m_idMap[m_currentIdConstant]].OnUpdate();

            // Parse Logic
            int current = m_currentIdConstant;
            int pure = m_states[m_idMap[m_currentIdConstant]].Transit();
            int parsed = m_ParseTransit(pure);
            onParseTransit?.Invoke(this, current, parsed);

            // Transit Logic
            if(parsed >= 0 && parsed != current && parsed < capacity)
                Change(parsed);
        }

        public void LateUpdate()
        {
            if(m_bPassEvent())
                return;

            for(int i = 0; i < m_states.Length; ++i)
                m_states[m_idMap[m_currentIdConstant]].OnLateUpdateAlways();

            m_states[m_idMap[m_currentIdConstant]].OnLateUpdate();
        }

        public void OnDrawGizmos(bool _bShow)
        {
            if(m_bPassEvent())
                return;
            else if(!_bShow)
                return;

            for(int i = 0; i < m_states.Length; ++i)
                m_states[m_idMap[m_currentIdConstant]].OnDrawGizmoAlways();

            m_states[m_idMap[m_currentIdConstant]].OnDrawGizmo();
        }

        public bool Add(IState<T> _state)
        {
            int idExecution = -1;

            for(int i = capacity - 1; i >= 0; --i)
                if(m_states[i] == null)
                    idExecution = i;
                else if(m_states[i] == _state)
                    return false; // NOTE: already contains.

            if(idExecution >= 0)
            {
                m_states[idExecution] = _state;
                return true;
            }

            return false;
        }

        public bool Remove(IState<T> _state)
        {
            for(int i = capacity - 1; i >= 0; --i)
            {
                if(m_states[i] == _state)
                {
                    m_states[i] = null;
                    return true;
                }
            }

            return false;
        }

        private int m_ParseTransit(int _nextIdConstant)
        {
            switch(_nextIdConstant)
            {
                case MachineConstant.c_st_MACHINE_OFF:
                    End();
                    return _nextIdConstant;

                case MachineConstant.c_lt_HALT:
                    End();
                    onMachineHalt?.Invoke(this, m_currentIdConstant);
                    return _nextIdConstant;

                case MachineConstant.c_lt_CONTINUE: // current state continues, any state change event not occurs.
                case MachineConstant.c_lt_PASS: // NOTE: all inherited states returned c_lt_PASS, means no transition, current state continues.
                    return m_currentIdConstant;

                default:
                    if(m_states[m_idMap[m_currentIdConstant]].CanTransit())
                        return _nextIdConstant;
                    else
                        return m_currentIdConstant;
            }
        }

        private bool m_bPassEvent()
        {
            return !bStarted || bPaused;
        }
    }
}