using System;

using UnityEngine;

namespace Unchord
{
    [ExecuteAlways]
    public class ElongatedHexagonCollider2D : MonoBehaviour
    {
        private const float c_SQRT_2_DIV_2 = 0.707106781186547524f;

        public BoxCollider2D head;
        public BoxCollider2D body;
        public BoxCollider2D feet;

        public m_Direction direction = m_Direction.Vertical;
        public Vector2 size;

        private Vector2 m_headSize;
        private Vector2 m_bodySize;

        public enum m_Direction : int
        {
            Vertical = 0,
            Horizontal = 1
        }

        private void Update()
        {
            if(head == null || body == null || feet == null)
                return;
            else if(direction == m_Direction.Vertical)
                SetVerticalCollider();
            else if(direction == m_Direction.Horizontal)
                SetHorizontalCollider();
        }

        private void SetVerticalCollider()
        {
            if(size.y < size.x)
                size.y = size.x;

            float sh = size.x * c_SQRT_2_DIV_2;
            float bh = size.y - sh;
            float bh_half = bh * 0.5f;

            m_headSize.Set(sh, sh);
            m_bodySize.Set(size.x, bh);

            head.offset = Vector2.zero;
            body.offset = Vector2.zero;
            feet.offset = Vector2.zero;

            head.size = m_headSize;
            body.size = m_bodySize;
            feet.size = m_headSize;

            head.transform.localPosition = Vector2.up * bh_half;
            body.transform.localPosition = Vector2.zero;
            feet.transform.localPosition = Vector2.down * bh_half;

            head.transform.eulerAngles = Vector3.forward * 45.0f;
            body.transform.eulerAngles = Vector3.zero;
            feet.transform.eulerAngles = Vector3.forward * 45.0f;
        }

        private void SetHorizontalCollider()
        {
            if(size.x < size.y)
                size.x = size.y;

            float sh = size.y * c_SQRT_2_DIV_2;
            float bw = size.x - sh;
            float bw_half = bw * 0.5f;

            m_headSize.Set(sh, sh);
            m_bodySize.Set(bw, size.y);

            head.offset = Vector2.zero;
            body.offset = Vector2.zero;
            feet.offset = Vector2.zero;

            head.size = m_headSize;
            body.size = m_bodySize;
            feet.size = m_headSize;

            head.transform.localPosition = Vector2.right * bw_half;
            body.transform.localPosition = Vector2.zero;
            feet.transform.localPosition = Vector2.left * bw_half;

            head.transform.eulerAngles = Vector3.forward * 45.0f;
            body.transform.eulerAngles = Vector3.zero;
            feet.transform.eulerAngles = Vector3.forward * 45.0f;
        }
    }
}