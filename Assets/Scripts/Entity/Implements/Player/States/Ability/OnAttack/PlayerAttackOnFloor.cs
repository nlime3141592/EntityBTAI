using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerAttackOnFloor : PlayerAttack, IBattleState
    {
        // property
        public int targetCount => m_targetCount;
        public int maxActionPhase => m_maxActionPhase;
        public float baseDamage01 => m_baseDamages[0];
        public float baseDamage02 => m_baseDamages[1];
        public float baseDamage03 => m_baseDamages[2];
        public float coyoteTime => m_coyoteTime;

        // fixed data
        private int m_targetCount = 7;
        private int m_maxActionPhase = 3;
        private float[] m_baseDamages = new float[]{ 1.0f, 1.1f, 1.25f };
        private float[] m_moveVelocity = new float[]{ 1.5f, 0.8f, 2.0f };
        private float m_cooltime = 0.1f;
        private float m_coyoteTime = 2.0f;
        private LTRB m_attackRange = new LTRB()
        {
            left = 1.0f,
            top = 2.0f,
            right = 4.0f,
            bottom = 2.0f
        };
        private EntitySensorGizmoOption m_attackGizmoOption = new EntitySensorGizmoOption()
        {
            bShowGizmo = true,
            duration = 0.2f,
            color = Color.magenta
        };

        // variable
        private bool m_bCanUpdateCoyoteTime;
        private float m_leftCooltime;
        private float m_leftCoyoteTime;
        private int m_actionPhase = 0;
        private bool m_bParryingDown;
        private bool m_bRushDown;
        private bool m_bGoNextPhase;
        private bool m_bAttacked;
        private float m_lookDirX;

        public PlayerAttackOnFloor(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        void IBattleState.OnBattle()
        {
            float baseDamage = m_baseDamages[m_actionPhase - 1];

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

            m_bAttacked = true;
        }

        public override bool CanAttack()
        {
            bool canAttack = m_leftCooltime <= 0;
            return canAttack;
        }

        public override void OnStateBegin()
        {
            player.battleModule.SetBattleState(this);
            player.bFixLookDirX = true;

            m_bCanUpdateCoyoteTime = false;
            m_bParryingDown = false;
            m_bRushDown = false;
            m_bGoNextPhase = false;
            m_bAttacked = false;

            if(m_actionPhase >= m_maxActionPhase || m_actionPhase < 0)
                m_actionPhase = 0;
            player.aController.ChangeActionPhase(++m_actionPhase);

            float ix = player.axisInput.x;
            if(ix < 0) player.lookDir.x = -1;
            else if(ix > 0) player.lookDir.x = 1;
            m_lookDirX = player.lookDir.x;

            m_leftCooltime = m_cooltime;
            base.OnStateBegin();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if(m_bAttacked)
            {
                player.vm.FreezePosition(true, false);
                player.vm.SetVelocityXY(0.0f, -1.0f);
            }
            else
            {
                RaycastHit2D terrain = Physics2D.Raycast(player.originFloor.position, Vector2.down, 0.5f, 1 << LayerMask.NameToLayer("Terrain"));
                player.moveDir.x = 1.0f;

                if(terrain.normal.y == 0)
                    player.moveDir.y = 0;
                else
                    player.moveDir.y = -terrain.normal.x / terrain.normal.y;

                float speed = m_moveVelocity[m_actionPhase - 1];
                float vx = m_lookDirX * player.moveDir.x * speed;
                float vy = m_lookDirX * player.moveDir.y * speed;
                vy -= (float)Math.Abs(vy * 0.1f);

                player.vm.FreezePosition(false, false);
                player.vm.SetVelocityXY(vx, vy);
            }
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;

            if(player.aController.bBeginOfAction)
            {
                Debug.Log("출력 중");
                if(player.parryingDown)
                    m_bParryingDown = true;
                if(player.rushDown)
                    m_bRushDown = true;
                if(this.CanAttack() && player.skill00)
                    m_bGoNextPhase = true;
            }

            if(player.aController.bEndOfAction)
            {
                if(m_bParryingDown)
                {
                    fsm.Change(fsm.emergencyParrying);
                    return true;
                }
                else if(m_bRushDown)
                {
                    fsm.Change(fsm.roll);
                    return true;
                }
                else if(m_bGoNextPhase)
                {
                    fsm.Replay();
                    return true;
                }
            }

            return false;
        }

        public override void OnActionEnd()
        {
            base.OnActionEnd();
            m_leftCoyoteTime = m_coyoteTime;
            m_bCanUpdateCoyoteTime = true;
        }

        public void UpdateCoyoteTime()
        {
            if(m_bCanUpdateCoyoteTime)
            {
                m_leftCoyoteTime -= Time.deltaTime;

                if(m_leftCoyoteTime <= 0)
                {
                    m_leftCoyoteTime = 0;
                    m_actionPhase = 0;
                }
            }
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            player.bFixLookDirX = false;
            player.vm.MeltPositionX();

            if(!m_bCanUpdateCoyoteTime)
            {
                m_bCanUpdateCoyoteTime = true;
                m_leftCoyoteTime = m_coyoteTime;
            }

            player.aController.ChangeActionPhase(0);
        }

        public void UpdateCooltime()
        {
            if(m_leftCooltime > 0)
                m_leftCooltime -= Time.deltaTime;
        }
    }
}