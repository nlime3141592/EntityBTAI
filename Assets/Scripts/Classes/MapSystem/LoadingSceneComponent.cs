using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class LoadingSceneComponent : MonoBehaviour
    {
        private static LoadingSceneComponent m_instance;
        private static Queue<IEnumerator> s_m_mapEnumerators;
        private static int s_m_cntReservedEnumerator;

        static LoadingSceneComponent()
        {
            s_m_mapEnumerators = new Queue<IEnumerator>(2);
            s_m_cntReservedEnumerator = 0;
        }

        public static void EnqueueMapEnumerator(IEnumerator _enumerator)
        {
            s_m_mapEnumerators.Enqueue(_enumerator);
            ++s_m_cntReservedEnumerator;
        }

        private void Awake()
        {
            if(m_instance == null)
                m_instance = this;
            else
                Destroy(this.gameObject);
        }

        private void Start()
        {
            StartCoroutine(m_Process());
        }

        private void OnDestroy()
        {
            if(m_instance == this)
                m_instance = null;
        }

        private IEnumerator m_Process()
        {
            yield return FadeManager.FadeOut(0.7f);

            while(s_m_mapEnumerators.Count > 0)
                StartCoroutine(m_StartInternalCoroutine(s_m_mapEnumerators.Dequeue()));

            yield return new WaitUntil(() => s_m_cntReservedEnumerator == 0);
            yield return FadeManager.FadeIn(1.2f);
        }

        private IEnumerator m_StartInternalCoroutine(IEnumerator _enumerator)
        {
            yield return StartCoroutine(_enumerator);
            --s_m_cntReservedEnumerator;
        }
    }
}