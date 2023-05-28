// TODO: 삭제할지 말지 고민하기
/*
using System;
using UnityEngine;

namespace Unchord
{
    public class PhaseController
    {
        public event Action onPhase;
        public event Action onFirst;
        public event Action onLast;

        public int maxPhase
        {
            get => m_maxPhase;
            set => m_maxPhase = value < 2 ? 2 : value;
        }
        public float coyoteTime
        {
            get => m_coyoteTime;
            set => m_coyoteTime = value < 0.0f ? 0.0f : value;
        }
        public bool canUpdate
        {
            get => m_bCanUpdate;
            set => m_bCanUpdate = value;
        }

        public int current => m_currentPhase;
        public float leftCoyoteTime => m_leftCoyoteTime;

        public bool bEndOfCoyoteTime => m_leftCoyoteTime <= 0.0f;
        public bool bIsFIrst => m_currentPhase == 0;
        public bool bIsLast => m_currentPhase == m_maxPhase - 1;
        public bool bRunning => m_currentPhase >= 0 && m_currentPhase < m_maxPhase - 1;

        private int m_maxPhase;
        private float m_coyoteTime;
        private bool m_bCanUpdate;

        private int m_currentPhase;
        private float m_leftCoyoteTime;

        public PhaseController(int _maxPhase, float _coyoteTime)
        {
            maxPhase = _maxPhase;
            coyoteTime = _coyoteTime;

            Reset();
            m_bCanUpdate = true;
        }

        public void OnUpdate()
        {
            if(!m_bCanUpdate)
                return;
            else if(m_leftCoyoteTime > 0)
                m_leftCoyoteTime -= Time.deltaTime;
            else if(m_leftCoyoteTime < 0)
                m_leftCoyoteTime = 0;
        }

        public void Reset()
        {
            ResetPhase();
            m_leftCoyoteTime = 0;
        }

        public void ResetPhase()
        {
            m_currentPhase = -1;
        }

        public void SetCoyote()
        {
            m_leftCoyoteTime = m_coyoteTime;
        }

        public int Next()
        {
            m_currentPhase = (m_currentPhase + 1) % m_maxPhase;

            if(m_currentPhase == 0)
                onFirst?.Invoke();
            else if(m_currentPhase == m_maxPhase - 1)
                onLast?.Invoke();

            onPhase?.Invoke();
            return m_currentPhase;
        }
    }
}*/