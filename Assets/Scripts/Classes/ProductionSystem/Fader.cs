using UnityEngine;

namespace Unchord
{
    public class Fader
    {
        public float value { get; private set; } = 1;

        public Fader(float _initvalue = 0)
        {
            value = UnchordUtility.Mid(0, 1, _initvalue);
        }

        // NOTE: Fade Out 상태라면 true를 반환합니다.
        public bool TryFadeOut(float _speed)
        {
            float dV = _speed * Time.deltaTime;
            float next = UnchordUtility.Min(1, value + dV);

            value = next;
            return value >= 1;
        }

        // NOTE: Fade In 상태라면 true를 반환합니다.
        public bool TryFadeIn(float _speed)
        {
            float dV = _speed * Time.deltaTime;
            float next = UnchordUtility.Max(0, value - dV);

            value = next;
            return value <= 0;
        }

        public Command GetFadeOutCommand(float _speed)
        {
            return () => TryFadeOut(_speed);
        }

        public Command GetFadeInCommand(float _speed)
        {
            return () => TryFadeIn(_speed);
        }
    }
}