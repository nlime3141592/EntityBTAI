using System.Collections.Generic;

namespace UnchordMetroidvania
{
    public sealed class UnchordStateComposite<T> : UnchordState<T>
    {
        public UnchordState<T> this[int _index]
        {
            get => m_states[_index];
            set => m_states[_index] = value;
        }

        private List<UnchordState<T>> m_states;

        public UnchordStateComposite(T _instance, int _id, string _name, int _capacity = 1)
        : base(_instance, _id, _name)
        {
            m_states = new List<UnchordState<T>>(_capacity);
        }

        public UnchordStateComposite(T _instance, int _id, int _capacity = 1)
        : this(_instance, _id, "", _capacity)
        {

        }

        public UnchordStateComposite(T _instance, int _capacity = 1)
        : this(_instance, -1, "", _capacity)
        {

        }

        public void Add(UnchordState<T> state) => m_states.Add(state);
        public bool Remove(UnchordState<T> state) => m_states.Remove(state);
        public void RemoveAt(int index) => m_states.RemoveAt(index);
        public void Clear() => m_states.Clear();
    }
}