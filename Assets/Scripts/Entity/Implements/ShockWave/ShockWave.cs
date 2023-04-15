using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [RequireComponent(typeof(BattleModule))]
    public class ShockWave : Entity, IBattleState
    {
        [HideInInspector] public BattleModule battleModule;
        public Transform container;
        public EntitySensorGizmoOption gizmo;

        protected List<Entity> targets;
        public LTRB attackRange;
        public int targetCount = 7;
        public float baseDamage = 1.0f;

        private List<Collider2D> colliders;
        private bool cpy = false;
        private bool m_bActionEnd = false;

        private int m_leftWave = 1;
        private int m_direction;
        private Vector2 m_position;

        protected override void InitComponents()
        {
            base.InitComponents();

            targets = new List<Entity>();

            TryGetComponent<BattleModule>(out battleModule);

            // battleModule.SetBattleState(this);

            vm.FreezePosition(true, true);

            aController.SetState(0);

            if(cpy)
            {
                transform.position = m_position;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        protected override void PostUpdate()
        {
            base.PostUpdate();

            bool canNext = CanNextWave();

            if(!m_bActionEnd && aController.bBeginOfAction && CanNextWave())
            {
                ShockWave wave = Copy();
                wave.InitBaseDamage(this.baseDamage);
                wave.InitRange(this.attackRange);

                float dx = attackRange.left + attackRange.right;
                wave.InitIgnore(colliders);
                wave.InitBaseDamage(this.baseDamage);
                wave.InitDirection(this.m_direction);
                wave.InitPosition(this.m_position + new Vector2(dx * (float)m_direction, 0));
                wave.InitRange(this.attackRange);
                wave.InitLeftWave(m_leftWave - 1);
                wave.InitShow();
                m_bActionEnd = true;
            }
            if(aController.bEndOfAnimation)
            {
                Destroy(this.gameObject);
            }
        }

        public void InitIgnore(List<Collider2D> ignores)
        {
            colliders = ignores;
        }

        public void InitBaseDamage(float _baseDamage)
        {
            baseDamage = _baseDamage;
        }

        public void InitRange(LTRB range)
        {
            attackRange = range;
        }

        public void InitPosition(Vector2 position)
        {
            m_position = position;
        }

        public void InitDirection(int dir)
        {
            m_direction = dir;
        }

        public void InitLeftWave(int left)
        {
            m_leftWave = left;
        }

        public void InitShow()
        {
            cpy = true;
            m_bActionEnd = false;
            aController.Reset();
            battleModule.SetIgnoreColliders(colliders);
            gameObject.SetActive(true);
        }

        public ShockWave Copy()
        {
            ShockWave wave = GameObject.Instantiate(this.gameObject).GetComponent<ShockWave>();
            wave.transform.SetParent(container);
            return wave;
        }

        private bool CanNextWave()
        {
            if(m_leftWave <= 0)
                return false;
            Collider2D[] cols = EntitySensor.OverlapBox(this, attackRange, gizmo, 1 << LayerMask.NameToLayer("Terrain"));
            return cols != null && cols.Length == 0;
        }

        public void OnTriggerBattleState()
        {
            
        }
    }
}