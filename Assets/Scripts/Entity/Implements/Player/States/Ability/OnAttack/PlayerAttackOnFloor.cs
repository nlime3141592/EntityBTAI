using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerAttackOnFloor : PlayerAttack
    {
        // property
        public int maxActionPhase => m_maxActionPhase;
        public float coyoteTime => m_coyoteTime;

        // fixed data
        private int m_maxActionPhase = 3;
        private float[] m_baseDamages = new float[]{ 1.0f, 1.1f, 1.25f };
        private float[] m_moveVelocity = new float[]{ 1.5f, 0.8f, 2.0f };
        private float m_cooltime = 0.1f;
        private float m_coyoteTime = 2.0f;

        // variable
        private bool m_bCanUpdateCoyoteTime;
        private float m_leftCooltime;
        private float m_leftCoyoteTime;
        private int m_actionPhase = 0;

        private bool m_bParryingDown;
        private bool m_bJumpDown;
        private bool m_bRushDown;
        private bool m_bGoNextPhase;

        private float m_lookDirX;

        public PlayerAttackOnFloor(Player _player)
        : base(_player)
        {
            base.attackRange = new LTRB()
            {
                left = 1.0f,
                top = 2.0f,
                right = 4.0f,
                bottom = 2.0f
            };
            base.targetCount = 7;
            base.baseDamage = 1.0f;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.battleModule.SetBattleState(this);
            player.bFixLookDirX = true;

            m_bCanUpdateCoyoteTime = false;
            m_bParryingDown = false;
            m_bJumpDown = false;
            m_bRushDown = false;
            m_bGoNextPhase = false;

            if(m_actionPhase >= m_maxActionPhase || m_actionPhase < 0)
                m_actionPhase = 0;

            ++m_actionPhase;
            player.aPhase = m_actionPhase;
            base.baseDamage = m_baseDamages[m_actionPhase - 1];

            float ix = player.axisInput.x;
            if(ix < 0) player.lookDir.x = -1;
            else if(ix > 0) player.lookDir.x = 1;
            m_lookDirX = player.lookDir.x;

            m_leftCooltime = m_cooltime;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if(player.aController.bEndOfAction)
            {
                player.vm.FreezePosition(true, false);
                player.vm.SetVelocityXY(0.0f, -1.0f);
            }
            else
            {
                RaycastHit2D terrain = Physics2D.Raycast(player.senseData.originFloor.position, Vector2.down, 0.5f, 1 << LayerMask.NameToLayer("Terrain"));
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

        public override void OnUpdateAlways()
        {
            base.OnUpdateAlways();
            m_UpdateCooltime();
            m_UpdateCoyoteTime();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(player.aController.bBeginOfAction)
            {
                if(player.parryingDown)
                    m_bParryingDown = true;
                if(player.jumpDown)
                    m_bJumpDown = true;
                if(player.rushDown)
                    m_bRushDown = true;
                if(this.CanTransit() && player.skill00)
                    m_bGoNextPhase = true;
            }
        }

        public override bool CanTransit()
        {
            bool canAttack = m_leftCooltime <= 0;
            return canAttack;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.aController.bEndOfAction)
            {
                if(m_bParryingDown)
                    return PlayerFsm.c_st_EMERGENCY_PARRYING;
                else if(m_bJumpDown)
                    return PlayerFsm.c_st_JUMP_ON_FLOOR;
                else if(m_bRushDown)
                    return PlayerFsm.c_st_ROLL;
                else if(m_bGoNextPhase)
                    return PlayerFsm.c_st_ATTACK_ON_FLOOR;
            }

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnActionEnd()
        {
            base.OnActionEnd();
            m_leftCoyoteTime = m_coyoteTime;
            m_bCanUpdateCoyoteTime = true;
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

            // TODO: EntityState의 ChangeActionPhase 코드와 비교 후 뺄지 말지 결정.
            player.aController.ChangeActionPhase(0);
        }

        private void m_UpdateCooltime()
        {
            if(m_leftCooltime > 0)
                m_leftCooltime -= Time.deltaTime;
        }

        private void m_UpdateCoyoteTime()
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
    }
}