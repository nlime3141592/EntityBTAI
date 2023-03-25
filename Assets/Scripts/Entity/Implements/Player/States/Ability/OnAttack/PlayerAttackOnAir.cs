using System;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    public class PlayerAttackOnAir : PlayerAttack, IBattleState
    {
        // fixed data
        private float[] m_baseDamages = new float[] { 1.0f, 1.0f };
        private PhaseController m_phaser;
        private Timer m_cooltimer;

        // variable
        private bool m_bAttackDown;

        private Direction m_lookDirX;
        private float m_speed;

        public override void OnConstruct()
        {
            base.attackRange = new LTRB()
            {
                left = 1.0f,
                top = 1.0f,
                right = 1.0f,
                bottom = 4.0f
            };
            base.targetCount = 7;
            base.baseDamage = 1.0f;

            m_phaser = new PhaseController(2, 2.0f);
            m_cooltimer = new Timer(0.1f);

            m_phaser.onFirst += OnFirstPhase;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.battleModule.SetBattleState(this);
            instance.bFixLookDir.x = true;
            instance.vm.FreezePosition(true, false);

            m_bAttackDown = false;

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
            m_speed = instance.speed_JumpOnAir;
        }

        public virtual void OnFirstPhase()
        {
            --instance.countLeft_AttackOnAir;
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

            instance.vm.SetVelocityXY(0.0f, -1.0f);

            m_speed += instance.gravity_AttackOnAir * Time.fixedDeltaTime;
            instance.vm.SetVelocityY(m_speed);
        }

        public override void OnUpdateAlways()
        {
            base.OnUpdateAlways();
            m_cooltimer.OnUpdate();
            m_phaser.OnUpdate();

            instance.DEBUG_COYOTE = m_phaser.leftCoyoteTime;

            if(!m_phaser.bEndOfCoyoteTime && instance.countLeft_AttackOnAir == instance.count_AttackOnAir)
                m_phaser.Reset();
            if(m_phaser.canUpdate && m_phaser.bEndOfCoyoteTime)
                m_phaser.Reset();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(!m_bAttackDown && instance.skill00)
                m_bAttackDown = true;
        }

        public override bool CanTransit()
        {
            if(!m_cooltimer.bEndOfTimer)
                return false;
            else if(m_phaser.bRunning)
                return true;
            else
                return instance.countLeft_AttackOnAir > 0;
        }

        public override void OnActionEnd()
        {
            base.OnActionEnd();
            
            m_phaser.canUpdate = true;
            m_phaser.SetCoyote();
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_bAttackDown)
                return Player.c_st_ATTACK_ON_AIR;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.bFixLookDir.x = false;
            instance.vm.FreezePosition(false, false);

            if(!m_phaser.canUpdate)
            {
                m_phaser.SetCoyote();
                m_phaser.canUpdate = true;
            }
        }
    }
}