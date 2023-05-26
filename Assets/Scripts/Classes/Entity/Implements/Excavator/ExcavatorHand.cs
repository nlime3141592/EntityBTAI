using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class ExcavatorHand : MonoBehaviour
    {
        public Transform overlapPoint;
        public bool bStart;
        public bool bHit;
        public bool bReturn;
        public float speed = 30.0f;

        public Timer idleTimer;
        private Vector2 m_startLocalPos;

        #region Unity Event Functions
        private void Start()
        {
            idleTimer = new Timer(1.0f);
            m_startLocalPos = transform.localPosition;
        }

        public void Clear()
        {
            idleTimer.Reset();
            bStart = false;
            bHit = false;
            bReturn = false;
            transform.localPosition = m_startLocalPos;
        }

        private void FixedUpdate()
        {
            if(!bStart)
                return;

            if(!bHit)
            {
                transform.localPosition += Vector3.right * speed * Time.fixedDeltaTime;
                bHit = Physics2D.OverlapCircle(overlapPoint.position, 0.5f, 1 << LayerMask.NameToLayer("Terrain"));
            }
            else if(idleTimer.bEndOfTimer && !bReturn)
            {
                transform.localPosition -= Vector3.right * speed * Time.fixedDeltaTime;

                if(transform.localPosition.x <= m_startLocalPos.x)
                {
                    bReturn = true;
                    bStart = false;
                }
            }
        }

        private void Update()
        {
            if(!bStart)
                return;
            if(bHit && !idleTimer.bEndOfTimer)
                idleTimer.OnUpdate();
        }
        #endregion
    }
}