using System;
using UnityEngine;

namespace Unchord
{
    [Serializable]
    public class PlayerAbilityGun : PlayerAttack
    {
        // fixed data
        private float m_cooltime = 3.0f;

        // variable
        private float m_leftCooltime;

        public override void OnMachineBegin(Player _instance, int _id)
        {
            base.OnMachineBegin(_instance, _id);

            base.attackRange = new LTRB()
            {
                left = 1.0f,
                top = 1.0f,
                right = 9.0f,
                bottom = 2.0f
            };
            base.targetCount = 7;
            base.baseDamage = 1.0f;
        }

        public override bool CanTransit()
        {
            bool canAttack = m_leftCooltime <= 0;
            return canAttack;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.battleModule.SetBattleState(this);

            instance.bFixLookDir.x = true;
            instance.vm.FreezePositionX();

            m_leftCooltime = m_cooltime;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            instance.vm.SetVelocityXY(0.0f, -1.0f);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAction && instance.parryingDown)
                return Player.c_st_EMERGENCY_PARRYING;
            else
                return MachineConstant.c_lt_PASS;
        }

        public override void OnUpdateAlways()
        {
            base.OnUpdateAlways();
            m_UpdateCooltime();
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.bFixLookDir.x = false;
            instance.vm.MeltPositionX();
        }

        private void m_UpdateCooltime()
        {
            if(m_leftCooltime > 0)
                m_leftCooltime -= Time.deltaTime;
        }
    }
}