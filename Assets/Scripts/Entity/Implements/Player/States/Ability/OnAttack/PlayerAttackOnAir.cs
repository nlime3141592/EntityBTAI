using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerAttackOnAir : PlayerAttack, IBattleState
    {
        // property
        public float baseDamage => m_baseDamage;

        // fixed data
        private int m_targetCount = 7;
        private float m_baseDamage = 1.0f;
        private float m_cooltime = 0.1f;
        private LTRB m_attackRange = new LTRB()
        {
            left = 1.0f,
            top = 1.0f,
            right = 1.0f,
            bottom = 4.0f
        };
        private EntitySensorGizmoOption m_attackGizmoOption = new EntitySensorGizmoOption()
        {
            bShowGizmo = true,
            duration = 0.2f,
            color = Color.magenta
        };

        // variable
        private float m_leftCooltime;

        public PlayerAttackOnAir(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        void IBattleState.OnBattle()
        {
            float baseDamage = m_baseDamage;

            Collider2D[] colTargets = EntitySensor.OverlapBox(player, m_attackRange, m_attackGizmoOption);
            targets.Clear();
            targets
                .FilterFromColliders(player, colTargets, false)
                .SetTargetCount(m_targetCount);

            foreach(EntityBase target in targets)
            {
                float finalDamage = player.battleModule.GetFinalDamage(target, baseDamage);
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

            player.battleModule.SetBattleState(this);

            player.bFixLookDirX = true;
            player.vm.FreezePositionX();
            player.vm.FreezePositionY();

            m_leftCooltime = m_cooltime;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            player.vm.SetVelocityXY(0.0f, -1.0f);
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;

            // NOTE: 디버그용 상태 전환 코드.
            else if(Input.GetKeyDown(KeyCode.Q))
                player.battleModule.TriggerBattleState();
            else if(Input.GetKeyDown(KeyCode.W))
                player.aController.bEndOfAnimation = true;

            return false;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            player.bFixLookDirX = false;
            player.vm.MeltPositionX();
            player.vm.MeltPositionY();
        }

        public void UpdateCooltime()
        {
            if(m_leftCooltime > 0)
                m_leftCooltime -= Time.deltaTime;
        }
    }
}