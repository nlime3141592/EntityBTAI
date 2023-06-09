using UnityEngine;

namespace Unchord
{
    public class Fader
    {
        public float value { get; private set; } = 1;
        public float speedFadeOut = 1.0f;
        public float speedFadeIn = 1.0f;

        public Fader(float _initvalue = 0)
        {
            value = UnchordUtility.Mid(0, 1, _initvalue);
        }

        public void TryFadeOut(CommandQueueCallback _callbackOnEnd)
        {
            float dV = speedFadeOut * Time.deltaTime;
            float next = UnchordUtility.Min(1, value + dV);

            value = next;

            if(value >= 1)
                _callbackOnEnd();
        }

        public void TryFadeIn(CommandQueueCallback _callbackOnEnd)
        {
            float dV = speedFadeIn * Time.deltaTime;
            float next = UnchordUtility.Max(0, value - dV);

            value = next;

            if(value <= 0)
                _callbackOnEnd();
        }
    }
}