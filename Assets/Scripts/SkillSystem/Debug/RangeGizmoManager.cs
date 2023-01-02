namespace UnchordMetroidvania
{
    public class RangeGizmoManager
    {
        private RangeGizmo m_root = null;
        private int m_count = 0;

        public void Add(RangeGizmo gizmo)
        {
            if(m_root == null)
            {
                m_root = gizmo;
                gizmo.ptrNext = gizmo;
                gizmo.ptrPrev = gizmo;
                ++m_count;
            }
            else
            {
                m_root.ptrNext = gizmo;
                gizmo.ptrPrev = m_root.ptrPrev;
                gizmo.ptrNext = m_root;
                m_root.ptrPrev = gizmo;
                ++m_count;
            }
        }

        public bool Remove(RangeGizmo gizmo)
        {
            if(gizmo == null)
                return false;

            bool bCanRemove = (gizmo == m_root);
            RangeGizmo ptr = m_root;

            for(int i = m_count - 1; i >= 0 && !bCanRemove; --i)
            {
                ptr = ptr.ptrPrev;
                bCanRemove |= (ptr == gizmo);
            }

            if(!bCanRemove)
                return false;
            else 
                return m_RemoveInternal(gizmo);
        }

        private bool m_RemoveInternal(RangeGizmo gizmo)
        {
            if(gizmo == m_root)
                m_root = (m_count == 1) ? null : gizmo.ptrNext;

            gizmo.ptrNext.ptrPrev = gizmo.ptrPrev;
            gizmo.ptrPrev.ptrNext = gizmo.ptrNext;
            --m_count;
            return true;
        }

        public void OnDrawGizmos(float deltaTime)
        {
            RangeGizmo ptr = m_root;

            for(int i = m_count - 1; i >= 0; --i)
            {
                ptr = ptr.ptrPrev;

                if(!ptr.OnDrawGizmos(deltaTime))
                    m_RemoveInternal(ptr);
            }
        }
    }
}