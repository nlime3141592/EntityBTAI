using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerAttackOnFloor : PlayerAttack
    {
        // fixed data
        private float[] m_baseDamages = new float[]{ 1.0f, 1.1f, 1.25f };
        private float[] m_moveVelocity = new float[]{ 1.5f, 0.8f, 2.0f };
        private PhaseController m_phaser;
        private Timer m_cooltimer;

        // variable
        private bool m_bParryingDown;
        private bool m_bJumpDown;
        private bool m_bRushDown;
        private bool m_bAttackDown;
        private bool m_bMove;

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

            m_phaser = new PhaseController(3, 1.0f);
            m_cooltimer = new Timer(0.1f);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.battleModule.SetBattleState(this);
            player.bFixLookDirX = true;

            m_bParryingDown = false;
            m_bJumpDown = false;
            m_bRushDown = false;
            m_bAttackDown = false;
            m_bMove = false;

            m_phaser.canUpdate = false;

            player.aPhase = m_phaser.Next();
            base.baseDamage = m_baseDamages[m_phaser.current];
            m_cooltimer.Reset();

/*
            // 상태 시작 시 입력 방향을 감지하고 방향 전환을 함.
            float ix = player.axisInput.x;
            if(ix < 0) player.lookDir.x = -1;
            else if(ix > 0) player.lookDir.x = 1;
            m_lookDirX = player.lookDir.x;
*/
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            // 피격 판정 시에만 방향 전환 고정.
            float ix = player.axisInput.x;
            if(!player.aController.bBeginOfAction || player.aController.bEndOfAction)
                player.lookDir.x = ix < 0 ? -1 : 1;
            m_lookDirX = player.lookDir.x;

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

                float speed = m_moveVelocity[m_phaser.current];
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
            m_cooltimer.OnUpdate();
            m_phaser.OnUpdate();

            if(m_phaser.canUpdate && m_phaser.bEndOfCoyoteTime)
                m_phaser.Reset();
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
                    m_bAttackDown = true;
                if(player.axisInput.x != 0)
                    m_bMove = true;
            }
        }

        public override bool CanTransit()
        {
            bool canAttack = m_cooltimer.bEndOfTimer;
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
                else if(m_bAttackDown)
                    return PlayerFsm.c_st_ATTACK_ON_FLOOR;
                else if(m_bMove)
                {
                    if(player.bIsRun)
                        return PlayerFsm.c_st_RUN;
                    else
                        return PlayerFsm.c_st_WALK;
                }
            }

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnActionEnd()
        {
            base.OnActionEnd();

            m_phaser.SetCoyote();
            m_phaser.canUpdate = true;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            player.bFixLookDirX = false;
            player.vm.MeltPositionX();

            if(!m_phaser.canUpdate)
            {
                m_phaser.SetCoyote();
                m_phaser.canUpdate = true;
            }

            // TODO: EntityState의 ChangeActionPhase 코드와 비교 후 뺄지 말지 결정.
            player.aController.ChangeActionPhase(0);
        }
    }
}