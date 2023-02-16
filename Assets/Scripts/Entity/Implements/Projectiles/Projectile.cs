using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    [RequireComponent(typeof(BattleModule))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Projectile : EntityBase
    {
        [HideInInspector] public BattleModule battleModule;
        [HideInInspector] public CircleCollider2D cCol;
        public int cntBounce = 1;
        public int CURRENT_STATE = -1;

        private Vector2 m_velocity;
        private int m_cntBounce = 1;

        protected override void Start()
        {
            base.Start();

            TryGetComponent<BattleModule>(out battleModule);
            TryGetComponent<CircleCollider2D>(out cCol);

            gameObject.SetActive(false);

            cCol.enabled = false;
            physics.simulated = false;

            aController.ChangeAnimation(0);
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();

            if(m_cntBounce == cntBounce)
            {
                vm.SetVelocityXY(m_velocity.x, m_velocity.y);
                float dv = Time.fixedDeltaTime * Physics2D.gravity.y / 2;
                m_velocity.y += dv;
            }
        }

        protected override void Update()
        {
            base.Update();

            CURRENT_STATE = aController.id;

            if(aController.id == 1)
                Destroy(this.gameObject);
        }

        public void Ignore(Collider2D other, Vector2 velocity)
        {
            Physics2D.IgnoreCollision(cCol, other, true);

            cCol.enabled = true;
            physics.simulated = true;

            m_velocity = velocity;
            m_cntBounce = cntBounce;

            gameObject.SetActive(true);
        }

        public void OnCollisionEnter2D(Collision2D col)
        {
            if(m_cntBounce > 0)
                --m_cntBounce;
            if(m_cntBounce == 0)
                aController.ChangeAnimation(1);
        }
    }
}