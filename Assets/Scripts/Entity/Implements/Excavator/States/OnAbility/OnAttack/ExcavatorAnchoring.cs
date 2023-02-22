using UnityEngine;

namespace UnchordMetroidvania
{
    public class ExcavatorAnchoring : ExcavatorAttack
    {
        private PhaseController m_phaser;
        private Timer m_anchorTimer;

        public ExcavatorAnchoring(Excavator _instance)
        : base(_instance)
        {
            base.attackRange = new LTRB()
            {
                left = 1.0f,
                top = 1.0f,
                right = 1.0f,
                bottom = 1.0f
            };
            base.targetCount = 7;
            base.baseDamage = 1.0f;

            m_phaser = new PhaseController(4, 0.0f);
            m_anchorTimer = new Timer(2.0f);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            excavator.bUpdateAggroDirX = true;
            excavator.bFixLookDirX = false;
            // excavator.bFixLookDirX = true;

            m_phaser.canUpdate = false;

            int aPhase = m_phaser.Next();
            excavator.aPhase = aPhase;

            if(aPhase == 1)
            {
                m_anchorTimer.Reset();
                excavator.arm.targetTransform = excavator.aggroTargets[0].transform;
                excavator.armObj.SetActive(true);
            }
            if(aPhase == 2)
            {
                excavator.hand.Clear();
                excavator.hand.bStart = true;
            }
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(excavator.aPhase == 1)
            {
                m_anchorTimer.OnUpdate();
            }
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(m_phaser.current == 0 && excavator.aController.bEndOfAnimation)
                return ExcavatorFsm.c_st_ANCHORING;
            else if(m_phaser.current == 1 && m_anchorTimer.bEndOfTimer)
                return ExcavatorFsm.c_st_ANCHORING;
            else if(m_phaser.current == 2 && excavator.hand.bReturn)
                return ExcavatorFsm.c_st_ANCHORING;
            else if(m_phaser.current == 3 && excavator.aController.bEndOfAnimation)
                return ExcavatorFsm.c_st_IDLE;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            if(m_phaser.current == 1)
                excavator.arm.targetTransform = null;
            if(m_phaser.current == 2)
                excavator.armObj.SetActive(false);
        }

        private void m_TraceArm()
        {
            EntityBase target = excavator.aggroTargets[0];
        }
    }
}