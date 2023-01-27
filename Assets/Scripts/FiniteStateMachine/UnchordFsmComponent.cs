namespace UnchordMetroidvania
{
    public abstract class UnchordFsmComponent<T> // : MonoBehaviour
    {
        private UnchordFsm<T> m_fsm;

        public void SetInstance(T _instance, int _id, string _name)
        {
            m_fsm = new UnchordFsm<T>(_instance, _id, _name);
        }

        public void SetInstance(T _instance, int _id)
        {
            m_fsm = new UnchordFsm<T>(_instance, _id);
        }

        public void SetInstance(T _instance)
        {
            m_fsm = new UnchordFsm<T>(_instance);
        }

        private void FixedUpdate()
        {
            m_fsm?.OnFixedUpdate();
        }

        private void Update()
        {
            m_fsm?.OnUpdate();
        }

        private void LateUpdate()
        {
            m_fsm?.OnLateUpdate();
        }
    }
}