namespace Unchord
{
    public sealed class StateComposite<T> : IStateComposite<T>
    where T : class
    {
        IState<T> IStateComposite<T>.this[int _index]
        {
            get => m_states[_index];
            set => m_states[_index] = value;
        }

        IStateBase IStateCompositeBase.this[int _index]
        {
            get => m_states[_index];
        }

        public IState<T> this[int _index]
        {
            get => m_states[_index];
            set => m_states[_index] = value;
        }

        public int capacity { get; private set; }

        private IState<T>[] m_states;

        public StateComposite(int _capacity = 1)
        {
            capacity = _capacity < 1 ? 1 : _capacity;
            m_states = new IState<T>[capacity];
        }
    }
}