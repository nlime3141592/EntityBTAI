using System;
using UnityEngine;

namespace Unchord
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

        private Direction m_lookDirX;

        public override void OnConstruct()
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

            instance.battleModule.SetBattleState(this);
            instance.bFixLookDir.x = true;

            m_bParryingDown = false;
            m_bJumpDown = false;
            m_bRushDown = false;
            m_bAttackDown = false;
            m_bMove = false;

            m_phaser.canUpdate = false;

            instance.aPhase = m_phaser.Next();
            base.baseDamage = m_baseDamages[m_phaser.current];
            m_cooltimer.Reset();

/*
            // 상태 시작 시 입력 방향을 감지하고 방향 전환을 함.
            float ix = instance.axisInput.x;
            if(ix < 0) instance.lookDir.x = -1;
            else if(ix > 0) instance.lookDir.x = 1;
            m_lookDirX = instance.lookDir.x;
*/
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            // 피격 판정 시에만 방향 전환 고정.
            float ix = instance.axis.x;
            if(!instance.aController.bBeginOfAction || instance.aController.bEndOfAction)
            {
                if(ix < 0) instance.lookDir.x = Direction.Negative;
                else if(ix > 0) instance.lookDir.x = Direction.Positive;
            }
            m_lookDirX = instance.lookDir.x;

            if(instance.aController.bEndOfAction)
            {
                instance.vm.FreezePosition(true, false);
                instance.vm.SetVelocityXY(0.0f, -1.0f);
            }
            else
            {
                RaycastHit2D terrain = Physics2D.Raycast(instance.senseData.originFloor.position, Vector2.down, 0.5f, 1 << LayerMask.NameToLayer("Terrain"));
                instance.moveDir.x = 1.0f;

                if(terrain.normal.y == 0)
                    instance.moveDir.y = 0;
                else
                    instance.moveDir.y = -terrain.normal.x / terrain.normal.y;

                float speed = m_moveVelocity[m_phaser.current];
                float vx = (float)m_lookDirX * instance.moveDir.x * speed;
                float vy = (float)m_lookDirX * instance.moveDir.y * speed;
                vy -= (float)Math.Abs(vy * 0.1f);

                instance.vm.FreezePosition(false, false);
                instance.vm.SetVelocityXY(vx, vy);
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

            if(instance.aController.bBeginOfAction)
            {
                if(instance.parryingDown)
                    m_bParryingDown = true;
                if(instance.jumpDown)
                    m_bJumpDown = true;
                if(instance.rushDown)
                    m_bRushDown = true;
                if(this.CanTransit() && instance.skill00)
                    m_bAttackDown = true;
                if(instance.axis.x != 0)
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

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAction)
            {
                if(m_bParryingDown)
                    return Player.c_st_EMERGENCY_PARRYING;
                else if(m_bJumpDown)
                    return Player.c_st_JUMP_ON_FLOOR;
                else if(m_bRushDown)
                    return Player.c_st_ROLL;
                else if(m_bAttackDown)
                    return Player.c_st_ATTACK_ON_FLOOR;
                else if(m_bMove)
                {
                    if(instance.bIsRun)
                        return Player.c_st_RUN;
                    else
                        return Player.c_st_WALK;
                }
            }

            return MachineConstant.c_lt_PASS;
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

            instance.bFixLookDir.x = false;
            instance.vm.MeltPositionX();

            if(!m_phaser.canUpdate)
            {
                m_phaser.SetCoyote();
                m_phaser.canUpdate = true;
            }
        }
    }
}