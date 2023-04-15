using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [RequireComponent(typeof(BattleModule))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Projectile : Entity, IBattleState // NOTE: IBattleState는 State에 상속시키는 것이 일반적이므로, 시간이 날 때 수정해야 한다.
    {
        [HideInInspector] public BattleModule battleModule;
        [HideInInspector] public CircleCollider2D cCol;
        public int cntBounce = 1;
        public int CURRENT_STATE = -1;
        public Transform container;

        protected List<Entity> targets;
        public LTRB attackRange;
        public int targetCount = 7;
        public float baseDamage = 1.0f;

        private List<Collider2D> colliders;
        private Vector2 m_velocity;
        private Vector2 m_position;
        private bool cpy = false;
        private Timer m_timer;

        protected override void InitComponents()
        {
            base.InitComponents();

            targets = new List<Entity>();

            TryGetComponent<BattleModule>(out battleModule);
            TryGetComponent<CircleCollider2D>(out cCol);

            // NOTE: Projectile의 상태 클래스를 따로 만들어 관리하는 방법을 생각해 봐야 한다.
            // battleModule.SetBattleState(this);

            cCol.enabled = false;
            vm.FreezePosition(true, true);

            aController.SetState(0);
            m_timer = new Timer(10.0f);

            if(cpy)
            {
                // Init Options
                if(colliders != null)
                    foreach(Collider2D c in colliders)
                        Physics2D.IgnoreCollision(cCol, c, true);

                cCol.enabled = true;
                vm.FreezePosition(false, false);
                vm.SetVelocityXY(m_velocity.x, m_velocity.y);
                transform.position = m_position;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        protected override void PostFixedUpdate()
        {
            base.PostFixedUpdate();

            if(aController.id == 1)
                vm.SetVelocityY(-1.0f);
        }

        protected override void PostUpdate()
        {
            base.PostUpdate();

            CURRENT_STATE = aController.id;

            if(aController.id == 0)
            {
                if(m_timer.bEndOfTimer)
                    m_LandProjectile();
                else
                    m_timer.OnUpdate();
            }
            if(aController.id == 1 && aController.bEndOfAnimation)
                Destroy(this.gameObject);
        }

        public Projectile Copy()
        {
            Projectile proj = GameObject.Instantiate(this.gameObject).GetComponent<Projectile>();
            proj.transform.SetParent(container);
            return proj;
        }

        public void InitIgnore(List<Collider2D> ignores)
        {
            colliders = ignores;
        }

        public void InitVelocity(Vector2 velocity)
        {
            m_velocity = velocity;
        }

        public void InitPosition(Vector2 position)
        {
            m_position = position;
        }

        public void InitShow()
        {
            cpy = true;
            battleModule.SetIgnoreColliders(colliders);
            gameObject.SetActive(true);
        }

        public void OnCollisionEnter2D(Collision2D col)
        {
            if(cntBounce > 0)
                --cntBounce;

            if(cntBounce == 0)
                m_LandProjectile();
        }

        private void m_LandProjectile()
        {
            vm.FreezePosition(true, true);
            vm.SetVelocityY(0);
            aController.SetState(1);
        }

        public void OnTriggerBattleState()
        {
            
        }
    }
}