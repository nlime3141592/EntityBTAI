namespace UnchordMetroidvania
{
    public abstract class UnchordState<T>
    {
        public readonly int id;
        public readonly string name;

        public int frameCount { get; private set; } = -1;
        public int fixedFrameCount { get; private set; } = -1;
        public int nextFrameNumber => (frameCount + 1);
        public int nextFixedFrameNumber => (fixedFrameCount + 1);

        protected T instance { get; private set; }

        public UnchordState(T _instance, int _id, string _name)
        {
            id = _id;
            name = _name;

            instance = _instance;
        }

        public virtual void OnStateBegin() {}
        public virtual void OnFixedUpdate() {}
        public virtual void OnUpdate() {}
        public virtual void OnLateUpdate() {}

        public virtual void OnStateEnd()
        {
            frameCount = -1;
            fixedFrameCount = -1;
        }
    }
}