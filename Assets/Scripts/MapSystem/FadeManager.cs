using System;
using System.Collections;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public static class FadeManager
    {
        private const float c_MIN_SPEED = 0.001f;

        public static float fadeValue { get; private set; } = 1;

        public static IEnumerator FadeIn(float speed)
        {
            if(fadeValue != 1)
                yield break;

            if(speed < c_MIN_SPEED)
                speed = c_MIN_SPEED;

            float v = fadeValue;

            while(v > 0)
            {
                v -= (speed * Time.unscaledDeltaTime);
                fadeValue = v < 0 ? 0 : v;
                yield return null;
            }
        }

        public static IEnumerator FadeOut(float speed)
        {
            if(fadeValue != 0)
                yield break;

            if(speed < c_MIN_SPEED)
                speed = c_MIN_SPEED;

            float v = fadeValue;

            while(v < 1)
            {
                v += (speed * Time.unscaledDeltaTime);
                fadeValue = v > 1 ? 1 : v;
                yield return null;
            }
        }
    }
}