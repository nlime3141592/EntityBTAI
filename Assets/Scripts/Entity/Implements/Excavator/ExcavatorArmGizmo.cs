using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    [RequireComponent(typeof(ExcavatorArm))]
    public class ExcavatorArmGizmo : MonoBehaviour
    {
        public Color armColor;
        public Color traceColor;

        private ExcavatorArm m_arm;

        private void Start()
        {
            m_arm = GetComponent<ExcavatorArm>();
        }

        private void OnDrawGizmos()
        {
            if(!Application.isPlaying)
                return;

            m_DrawArm();

            m_DrawLine(m_arm.debug_joint, m_arm.debug_hand, traceColor);
            m_DrawLine(m_arm.debug_joint, m_arm.debug_target, traceColor);
        }

        private void m_DrawLine(Vector2 from, Vector2 to, Color color)
        {
            Color tmp = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawLine(from, to);
            Gizmos.color = tmp;
        }

        private void m_DrawLine(Transform from, Transform to, Color color)
        {
            if(from == null || to == null || color.a == 0)
                return;

            m_DrawLine(from.position, to.position, color);
        }

        private void m_DrawSphere(Vector2 center, float radius, Color color)
        {
            Color tmp = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawWireSphere(center, radius);
            Gizmos.color = tmp;
        }

        private void m_DrawSphere(Transform center, float radius, Color color)
        {
            if(center == null || radius <= 0 || color.a == 0)
                return;

            m_DrawSphere(center.position, radius, color);
        }

        private void m_DrawArm()
        {
            List<Transform> joints = m_arm.joints;
            Transform prev = null;
            Transform current = null;

            for(int i = 0; i < joints.Count; ++i)
            {
                if(joints[i] == null)
                    continue;
                else if(prev == null)
                    prev = joints[i];
                else
                {
                    current = joints[i];
                    m_DrawLine(prev, current, armColor);
                    prev = current;
                }
            }
        }
    }
}