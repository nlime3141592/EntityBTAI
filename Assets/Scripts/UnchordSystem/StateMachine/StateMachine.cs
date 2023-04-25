using System;
using System.Collections.Generic;

namespace Unchord
{
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
                    throw new StateMachineSetInstanceException(this, typeof(T));
            }
        }

        public bool bStarted { get; private set; }
        public bool bPaused { get; private set; }

        public IStateBase state => m_states[m_currentIdExecution];
        private int m_currentIdExecution;

        // public event Action onMachineBegin;
        // public event Action onMachinePause;
        // public event Action onMachineUnpause;
        // public event Action onStateChange;
        // public event Action onMachineEnd;

        private List<IStateImpl<T>> m_states;
        private Dictionary<int, int> m_idMap;
        private T m_instance;

        public StateMachine(int _capacity = 1)
        {
            int capacity = _capacity < 1 ? 1 : _capacity;

            bStarted = false;
            bPaused = false;

            m_states = new List<IStateImpl<T>>(capacity);
            m_idMap = new Dictionary<int, int>(capacity);
            m_currentIdExecution = MachineConstant.c_st_MACHINE_OFF;
        }

        public void Begin(IStateBase _stateTree, int _initIdConstant)
        {
            if(bStarted)
                return;
            else if(m_instance == default(T))
                // Context: state machine will begin.
                // Problem: target instance doesn't set.
                // Solution: set instance using align statement on external sides.
                throw new StateMachineNullInstanceException(this, typeof(T));

            m_states.Clear();

            int idAllocator = -1;
            m_rec_Begin(_stateTree, ref idAllocator);

            m_currentIdExecution = m_idMap[_initIdConstant];
            bStarted = true;
            bPaused = false;
            // onMachineBegin?.Invoke();

            m_states[m_currentIdExecution].OnStateBegin();
        }

        public void Pause(bool _bPaused)
        {
            if(!bStarted || bPaused == _bPaused)
                return;

            bPaused = _bPaused;

            // if(_bPaused) onMachinePause?.Invoke();
            // else onMachineUnpause?.Invoke();
        }

        public void Change(int _nextIdConstant)
        {
            if(!bStarted)
                return;

            m_states[m_currentIdExecution].OnStateEnd();
            m_currentIdExecution = m_idMap[_nextIdConstant];
            // onStateChange?.Invoke();
            m_states[m_currentIdExecution].OnStateBegin();
        }

        public void End()
        {
            if(!bStarted)
                return;

            m_states[m_currentIdExecution].OnStateEnd();

            int count = m_states.Count;
            for(int i = 0; i < count; ++i)
                m_states[i].OnMachineEnd();

            // onMachineEnd?.Invoke();

            bStarted = false;
            bPaused = false;
            m_currentIdExecution = MachineConstant.c_st_MACHINE_OFF;
        }

        public void FixedUpdate()
        {
            if(m_bPassEvent())
                return;

            for(int i = 0; i < m_states.Count; ++i)
                m_states[m_currentIdExecution].OnFixedUpdateAlways();

            m_states[m_currentIdExecution].OnFixedUpdate();
        }

        public void Update()
        {
            if(m_bPassEvent())
                return;

            for(int i = 0; i < m_states.Count; ++i)
                m_states[m_currentIdExecution].OnUpdateAlways();

            m_states[m_currentIdExecution].OnUpdate();

            int nextIdConstant = m_states[m_currentIdExecution].Transit();
            m_ParseTransit(nextIdConstant);
        }

        public void LateUpdate()
        {
            if(m_bPassEvent())
                return;

            for(int i = 0; i < m_states.Count; ++i)
                m_states[m_currentIdExecution].OnLateUpdateAlways();

            m_states[m_currentIdExecution].OnLateUpdate();
        }

        public void OnDrawGizmos(bool _bShow)
        {
            if(m_bPassEvent())
                return;
            else if(!_bShow)
                return;

            for(int i = 0; i < m_states.Count; ++i)
                m_states[m_currentIdExecution].OnDrawGizmoAlways();

            m_states[m_currentIdExecution].OnDrawGizmo();
        }

        private void m_rec_Begin(IStateBase _state, ref int _idAllocator)
        {
            if(_state is IStateCompositeBase)
            {
                IStateCompositeBase composite = _state as IStateCompositeBase;

                for(int i = 0; i < composite.capacity; ++i)
                    m_rec_Begin(composite[i], ref _idAllocator);
            }
            else if(_state is IStateImpl<T>)
            {
                State<T> state = _state as State<T>;

                int id = ++_idAllocator;

                state.machine = this;
                m_idMap.Add(state.idConstant, id);
                m_states.Add(state);
            }
        }

        private bool m_ParseTransit(int _nextIdConstant)
        {
            switch(_nextIdConstant)
            {
                case MachineConstant.c_st_MACHINE_OFF:
                    End();
                    return true;

                case MachineConstant.c_lt_HALT:
                    End();
                    m_PublishHalt();
                    return true;

                case MachineConstant.c_lt_CONTINUE: // current state continues, any state change event not occurs.
                case MachineConstant.c_lt_PASS: // NOTE: all inherited states returned c_lt_PASS, means no transition, current state continues.
                    return false;

                default:
                    int idExecution = m_idMap[_nextIdConstant];

                    if(idExecution < 0 || idExecution >= m_states.Count)
                    {
                        End();
                        m_PublishHalt();
                        return true;
                    }
                    else if(m_states[idExecution].CanTransit())
                    {
                        Change(_nextIdConstant);
                        return true;
                    }
                    else
                    {
                        // current state continues.
                        return false;
                    }
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