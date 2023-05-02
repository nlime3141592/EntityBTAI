using UnityEngine;

namespace Unchord
{
    public class MantisChop : MantisAttack //, IBattleState
    {
        public override int idConstant => Mantis.c_st_CHOP;

        // fixed data
        private float m_cooltime = 0.1f;

        // variables
        private float m_leftCooltime;
        private bool m_bAttacked;

        // TODO: 포효콤보 관련 로직을 추가해야 함.

        protected override void OnConstruct()
        {
            base.OnConstruct();

            base.attackRange = new LTRB()
            {
                left = 0.0f,
                top = 4.0f,
                right = 17.5f,
                bottom = 7.5f
            };
            base.targetCount = 12;
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

            // instance.battleModule.SetBattleState(this);

            m_leftCooltime = m_cooltime;
            m_bAttacked = false;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if(m_bAttacked)
            {
                instance.vm.FreezePosition(true, false);
                instance.vm.SetVelocityXY(0.0f, -1.0f);
            }
            else
            {
                // NOTE: 앞으로 살짝 전진하는 동작을 줄지 말지 결정.

                instance.vm.FreezePosition(false, false);
                instance.vm.SetVelocityXY(0.0f, -1.0f);
            }
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return Mantis.c_st_IDLE;

            return MachineConstant.c_lt_PASS;
        }
    }
}