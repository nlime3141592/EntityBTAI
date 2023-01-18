using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerTakeDown : _PlayerAbility, IBattleState
    {
        // property
        public int targetCount => m_targetCount;
        public int maxActionPhase => m_maxActionPhase;
        public float baseDamage => m_baseDamage;

        // fixed data
        private List<EntityBase> m_targets;
        private int m_targetCount = 7;
        private int m_maxActionPhase = 3;
        private float m_baseDamage = 1.0f;
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

        public PlayerTakeDown(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
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
            player.battleModule.SetBattleState(this);
            player.bFixLookDirX = true;

            if(m_actionPhase >= m_maxActionPhase || m_actionPhase < 0)
                m_actionPhase = 0;
            player.pAnimator.SetInteger("actionPhase", ++m_actionPhase);
            player.vm.FreezePosition(m_actionPhase != 2, m_actionPhase == 1);

            // NOTE: Test battle trigger.
            // 애니메이션 이벤트에 BattleModule.TriggerBattleState()를 등록해야 함.
            if(m_actionPhase == 3) player.battleModule.TriggerBattleState();

            m_leftCooltime = m_cooltime;
            base.OnStateBegin();
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

        public override bool OnUpdate()
        {
            // NOTE: Test input code.
            if(Input.GetKeyDown(KeyCode.Return))
                this.OnAnimationEnd();

            if(base.OnUpdate())
                return true;
            else if(m_actionPhase == 1 && p_bEndOfAnimation)
            {
                fsm.Replay();
                return true;
            }
            else if(m_actionPhase == 2 && fsm.bOnFloor)
            {
                fsm.Replay();
                return true;
            }
            else if(m_actionPhase == 3 && p_bEndOfAnimation)
            {
                fsm.Change(fsm.idleShort);
                return true;
            }

            return false;
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