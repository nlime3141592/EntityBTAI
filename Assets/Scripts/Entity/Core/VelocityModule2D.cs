using UnityEngine;

namespace UnchordMetroidvania
{
    public class VelocityModule2D
    {
        public Vector2 velocity => m_rigid.velocity;
        public float x => m_rigid.velocity.x;
        public float y => m_rigid.velocity.y;

        private Vector2 m_tvel;
        private Vector2 m_cvel;
        private Rigidbody2D m_rigid;

        public VelocityModule2D(Rigidbody2D rigidbody)
        {
            m_rigid = rigidbody;
        }

        public void SetVelocityXY(float x, float y)
        {
            m_tvel.Set(x, y);
            m_ApplyVelocity();
        }

        public void SetVelocityX(float x)
        {
            float y = m_rigid.velocity.y;
            SetVelocityXY(x, y);
        }

        public void SetVelocityY(float y)
        {
            float x = m_rigid.velocity.x;
            SetVelocityXY(x, y);
        }

        public void FreezePosition(bool bFreezeX, bool bFreezeY)
        {
            if(bFreezeX) FreezePositionX();
            else MeltPositionX();
            if(bFreezeY) FreezePositionY();
            else MeltPositionY();
        }

        public void FreezePositionX()
        {
            m_rigid.constraints |= RigidbodyConstraints2D.FreezePositionX;
        }

        public void FreezePositionY()
        {
            m_rigid.constraints |= RigidbodyConstraints2D.FreezePositionY;
        }

        public void MeltPositionX()
        {
            m_rigid.constraints &= ~(RigidbodyConstraints2D.FreezePositionX);
        }

        public void MeltPositionY()
        {
            m_rigid.constraints &= ~(RigidbodyConstraints2D.FreezePositionY);
        }

        private void m_ApplyVelocity()
        {
            m_cvel = m_tvel;
            m_rigid.velocity = m_tvel;
        }
    }
}