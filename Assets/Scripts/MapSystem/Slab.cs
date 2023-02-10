using UnityEngine;

namespace UnchordMetroidvania
{
    public class Slab : MonoBehaviour
    {
        public bool bIgnored { get; private set; } = false;
        private BoxCollider2D m_collider;

        private void Start()
        {
            m_collider = GetComponent<BoxCollider2D>();
        }

        public void IgnoreCollision(Collider2D collider)
        {
            Physics2D.IgnoreCollision(m_collider, collider, true);
            bIgnored = true;
        }

        public void AcceptCollision(Collider2D collider)
        {
            Physics2D.IgnoreCollision(m_collider, collider, false);
            bIgnored = false;
        }
    }
}