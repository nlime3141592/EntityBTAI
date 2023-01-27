namespace UnchordMetroidvania
{
    public class UnchordFsm<T> : UnchordState<T>
    {
        public int stateId => m_currentState?.id ?? int.MinValue;
        public string stateName => m_currentState?.name ?? "";

        private T m_instance;
        private UnchordState<T> m_currentState;

        public UnchordFsm(T _instance, int _id, string _name)
        : base(_instance, _id, _name)
        {

        }

        public UnchordFsm(T _instance, int _id)
        : this(_instance, _id, "")
        {

        }

        public UnchordFsm(T _instance)
        : this(_instance, -1, "")
        {

        }

        public override void OnUpdate()
        {
            m_currentState.OnUpdate();
        }

        public override void OnFixedUpdate()
        {
            m_currentState.OnFixedUpdate();
        }

        public bool Begin(UnchordState<T> first)
        {
            if(m_currentState != null)
                return false;

            m_currentState = first;
            m_currentState.OnStateBegin();
            return true;
        }

        public bool Change(UnchordState<T> next)
        {
            if(m_currentState == null)
                return false;

            m_currentState.OnStateEnd();
            m_currentState = next;
            m_currentState.OnStateBegin();
            return true;
        }

        public bool Replay()
        {
            if(m_currentState == null)
                return false;

            m_currentState.OnStateEnd();
            m_currentState.OnStateBegin();
            return true;
        }

        public bool End()
        {
            if(m_currentState == null)
                return false;

            m_currentState.OnStateEnd();
            m_currentState = null;
            return true;
        }
    }
}