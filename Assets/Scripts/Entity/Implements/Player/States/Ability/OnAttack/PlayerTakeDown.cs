using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerTakeDown : PlayerAbility, IBattleState
    {
        // property
        public int targetCount => m_targetCount;
        public int maxActionPhase => m_maxActionPhase;
        public float baseDamage => m_baseDamage;

        // fixed data
        private List<EntityBase> m_targets;
        private int m_targetCount = 7;
        private int m_maxActionPhase = 3;
        private float m_baseDamage = 0.7f;
        private float m_cooltime;
        private LTRB m_attackRange = new LTRB()
        {
            left = 3.0f,
            top = 0.5f,
            right = 3.0f,
            bottom = 3.0f
        };
        private EntitySensorGizmoOption m_attackGizmoOption = new EntitySensorGizmoOption()
        {
            bShowGizmo = true,
            duration = 0.2f,
            color = Color.magenta
        };

        // variable
        private int m_actionPhase;
        private float m_leftCooltime;

        public PlayerTakeDown(Player _player)
        : base(_player)
        {
            m_targets = new List<EntityBase>(m_targetCount);
        }

        void IBattleState.OnBattle()
        {
            float baseDamage = m_baseDamage;

            Collider2D[] colTargets = EntitySensor.OverlapBox(player, m_attackRange, m_attackGizmoOption);
            m_targets.Clear();
            m_targets
                .FilterFromColliders(player, colTargets, false)
                .SetTargetCount(m_targetCount);

            foreach(EntityBase target in m_targets)
            {
                float finalDamage = player.battleModule.GetFinalDamage(target, baseDamage);
                target.Damage(finalDamage);
            }
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.battleModule.SetBattleState(this);
            player.bFixLookDirX = true;

            if(m_actionPhase >= m_maxActionPhase || m_actionPhase < 0)
                m_actionPhase = 0;

            player.aPhase = ++m_actionPhase;

            player.vm.FreezePosition(m_actionPhase != 2, m_actionPhase == 1);

            m_leftCooltime = m_cooltime;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            player.vm.SetVelocityX(0.0f);

            if(m_actionPhase == 1)
                player.vm.SetVelocityY(0.0f);
            else if(m_actionPhase == 2)
                player.vm.SetVelocityY(-30.0f);
            else if(m_actionPhase == 3)
                player.vm.SetVelocityY(-1.0f);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit == FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(m_actionPhase == 1 && player.aController.bEndOfAnimation)
                return PlayerFsm.c_st_TAKE_DOWN;
            else if(m_actionPhase == 2 && player.senseData.bOnFloor)
                return PlayerFsm.c_st_TAKE_DOWN;
            else if(m_actionPhase == 3)
            {
                if(player.aController.bEndOfAnimation)
                    return PlayerFsm.c_st_IDLE_SHORT;
                if(player.aController.bEndOfAction && player.parryingDown)
                    return PlayerFsm.c_st_EMERGENCY_PARRYING;
            }

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            if(m_actionPhase == 3)
                player.bFixLookDirX = false;
        }

        public void UpdateCooltime()
        {
            if(m_leftCooltime > 0)
                m_leftCooltime -= Time.deltaTime;
        }
    }
}