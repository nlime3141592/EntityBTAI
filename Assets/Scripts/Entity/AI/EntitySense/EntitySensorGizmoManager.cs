namespace UnchordMetroidvania
{
    public class EntitySensorGizmoManager
    {
        private EntitySensorGizmo m_root = null;
        private int m_count = 0;

        public void Add(EntitySensorGizmo gizmo)
        {
            if(m_root == null)
            {
                m_root = gizmo;
                gizmo.next = gizmo;
                gizmo.prev = gizmo;
                ++m_count;
            }
            else
            {
                m_root.next = gizmo;
                gizmo.prev = m_root.prev;
                gizmo.next = m_root;
                m_root.prev = gizmo;
                ++m_count;
            }
        }

        public bool Remove(EntitySensorGizmo gizmo)
        {
            if(gizmo == null)
                return false;

            bool bCanRemove = (gizmo == m_root);
            EntitySensorGizmo ptr = m_root;

            for(int i = m_count - 1; i >= 0 && !bCanRemove; --i)
            {
                ptr = ptr.prev;
                bCanRemove |= (ptr == gizmo);
            }

            if(!bCanRemove)
                return false;
            else
                return m_RemoveInternal(gizmo);
        }

        private bool m_RemoveInternal(EntitySensorGizmo gizmo)
        {
            if(gizmo == m_root)
                m_root = (m_count == 1) ? null : gizmo.next;

            gizmo.next.prev = gizmo.prev;
            gizmo.prev.next = gizmo.next;
            --m_count;
            return true;
        }

        public void OnDrawGizmos(float deltaTime)
        {
            EntitySensorGizmo ptr = m_root;

            for(int i = m_count - 1; i >= 0; --i)
            {
                ptr = ptr.prev;

                if(!ptr.OnDrawGizmos(deltaTime))
                    m_RemoveInternal(ptr);
            }
        }
    }
}