using System;

namespace UnchordMetroidvania
{
    public abstract class FiniteStateMachine<T> : FiniteState<T>
    {
        public FiniteState<T> this[int index]
        {
            get => states[index];
            set => states[index] = value;
        }

        public const int c_st_MACHINE_HALT = -1;
        public const int c_st_STATE_CONTINUE = -2;
        public const int c_st_BASE_IGNORE = -3;

        public readonly int capacity;
        public readonly FiniteState<T>[] states;

        public int current { get; private set; } = c_st_MACHINE_HALT;

        public FiniteStateMachine(T _instance, int _capacity)
        : base(_instance)
        {
            // capacity = max(1, _capacity);
            capacity = _capacity < 1 ? 1 : _capacity;
            states = new FiniteState<T>[capacity];
            current = c_st_MACHINE_HALT;
        }

        public override void OnFixedUpdateAlways()
        {
            base.OnFixedUpdateAlways();
            for(int i = 0; i < capacity; ++i)
                states[i].OnFixedUpdateAlways();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            states[current].OnFixedUpdate();
        }

        public override void OnUpdateAlways()
        {
            base.OnUpdateAlways();
            for(int i = 0; i < capacity; ++i)
                states[i].OnUpdateAlways();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            states[current].OnUpdate();
        }

        public override bool CanTransit()
        {
            return base.CanTransit() && m_bCheckRunning(current, capacity);
        }

        public override int Transit()
        {
            if(current == c_st_MACHINE_HALT)
                return current;

            int next = states[current]?.Transit() ?? c_st_MACHINE_HALT;

            if(next >= 0 && next < capacity) // 상태 전이
            {
                if(!Change(next)) Stop();
            }
            else if(next == c_st_MACHINE_HALT) // 머신 종료
                Stop();
            else if(next == c_st_STATE_CONTINUE || next == c_st_BASE_IGNORE) // 상태 유지
                return current;
            else
                throw new StateTransitException("Check your transit function.");

            return current;
        }

        public bool Start(int state)
        {
            if(m_bCheckRunning(current, capacity))
                return false;

            current = state;
            states[current].OnStateBegin();
            return true;
        }

        public bool Change(int state)
        {
            if(!m_bCheckRunning(current, capacity))
                return false;

            states[current].OnStateEnd();
            current = state;
            states[current].OnStateBegin();
            return true;
        }

        public bool Stop()
        {
            if(!m_bCheckRunning(current, capacity))
                return false;

            states[current].OnStateEnd();
            current = c_st_MACHINE_HALT;
            return true;
        }

        private bool m_bCheckRunning(int _state, int _capacity)
        {
            if(_state == c_st_MACHINE_HALT)
                return false;
            else if(_state >= 0 && _state < _capacity)
                return true;
            else
                throw new IndexOutOfRangeException("Error.");
        }
    }
}