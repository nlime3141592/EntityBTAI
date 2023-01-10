using UnityEngine;

namespace UnchordMetroidvania
{
    public class _PlayerFSM
    {
        public int state => currentState?.id ?? int.MinValue;
        public int totalFps { get; private set; } = -1;
        public int fps { get; private set; } = -1;
        public int nextFps => (fps + 1);

        private PlayerState currentState = null;

        public void OnUpdate()
        {
            currentState.OnUpdate();
        }

        public void OnFixedUpdate()
        {
            ++totalFps;
            ++fps;
            currentState.OnFixedUpdate();
        }

        public void Begin(PlayerState initState)
        {
            if(currentState != null)
                return;

            totalFps = -1;
            fps = -1;
            currentState = initState;
            currentState.OnStateBegin();
        }

        public void Change(PlayerState nextState)
        {
            if(currentState == null)
                return;

            currentState.OnStateEnd();
            fps = -1;
            currentState = nextState;
            currentState.OnStateBegin();
        }

        public void End()
        {
            if(currentState == null)
                return;

            currentState.OnStateEnd();
            currentState = null;
        }

        public void OnAnimationEnd()
        {
            currentState.OnAnimationEnd();
        }
    }
}