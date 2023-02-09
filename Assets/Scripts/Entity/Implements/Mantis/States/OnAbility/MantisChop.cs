using UnityEngine;

namespace UnchordMetroidvania
{
    public class MantisChop : MantisAttack, IBattleState
    {
        // fixed data
        private int m_targetCount = 12;
        private float m_baseDamage = 1.0f;
        private float m_cooltime = 0.1f;
        private LTRB m_attackRange = new LTRB()
        {
            left = 0.0f,
            top = 4.0f,
            right = 17.5f,
            bottom = 7.5f
        };
        private EntitySensorGizmoOption m_attackGizmoOption = new EntitySensorGizmoOption()
        {
            bShowGizmo = true,
            duration = 2.0f,
            color = Color.magenta
        };

        // variables
        private float m_leftCooltime;
        private bool m_bAttacked;

        // TODO: 포효콤보 관련 로직을 추가해야 함.

        public MantisChop(Mantis _mantis)
        : base(_mantis)
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

        public override bool CanTransit()
        {
            bool canAttack = m_leftCooltime <= 0;
            return canAttack;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            mantis.battleModule.SetBattleState(this);

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

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(mantis.aController.bEndOfAnimation)
                return MantisFsm.c_st_IDLE;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}