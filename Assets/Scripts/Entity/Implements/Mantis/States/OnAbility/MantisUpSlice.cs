using UnityEngine;

namespace UnchordMetroidvania
{
    public class _MantisUpSlice : _MantisAttack, IBattleState
    {
        // fixed data
        private int m_targetCount = 12;
        private float m_baseDamage = 1.0f;
        private float m_cooltime = 0.1f;
        private LTRB m_attackRange = new LTRB()
        {
            left = 5.5f,
            top = 5.5f,
            right = 15.5f,
            bottom = 5.5f
        };
        private EntitySensorGizmoOption m_attackGizmoOption = new EntitySensorGizmoOption()
        {
            bShowGizmo = true,
            duration = 0.2f,
            color = Color.magenta
        };

        // variables
        private float m_leftCooltime;
        private bool m_bAttacked;

        public _MantisUpSlice(Mantis _mantis, int _id, string _name)
        : base(_mantis, _id, _name)
        {
            
        }

        void IBattleState.OnBattle()
        {
            float baseDamage = m_baseDamage;

            Collider2D[] colTargets = EntitySensor.OverlapBox(instance, m_attackRange, m_attackGizmoOption);
            targets.Clear();
            targets
                .FilterFromColliders(instance, colTargets, false, "Player")
                .SetTargetCount(m_targetCount);

            foreach(EntityBase target in targets)
            {
                float finalDamage = instance.battleModule.GetFinalDamage(target, baseDamage);
                target.Damage(finalDamage);
            }
        }

        public override bool CanAttack()
        {
            bool canAttack = m_leftCooltime <= 0;
            return canAttack;
        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();

            mantis.battleModule.SetBattleState(this);
            mantis.bFixLookDirX = true;

            m_leftCooltime = m_cooltime;
            m_bAttacked = false;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if(m_bAttacked)
            {
                mantis.vm.FreezePosition(true, false);
                mantis.vm.SetVelocityXY(0.0f, -1.0f);
            }
            else
            {
                // NOTE: 앞으로 살짝 전진하는 동작을 줄지 말지 결정.

                mantis.vm.FreezePosition(false, false);
                mantis.vm.SetVelocityXY(0.0f, -1.0f);
            }
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(mantis.aController.bEndOfAnimation)
            {
                // NOTE: 상태 전이하는 코드 집어넣기.
                return true;
            }

            return false;
        }
    }
}