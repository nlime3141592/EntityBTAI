using UnityEngine;

namespace Unchord
{
    public class ExcavatorAnchoring : ExcavatorAttack
    {
        public override int idConstant => Excavator.c_st_ANCHORING;

        // private PhaseController m_phaser;
        private Timer m_anchorTimer;
/*
        protected override void OnConstruct()
        {
            base.OnConstruct();

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
*/
/*
        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.bFixedLookDirByAxis.x = false;
            // instance.bFixLookDirX = true;

            m_phaser.canUpdate = false;

            int aPhase = m_phaser.Next();
            instance.aPhase = aPhase;

            if(aPhase == 1)
            {
                m_anchorTimer.Reset();
                // instance.arm.targetTransform = instance.aggroAi.targets[0].transform;
                // instance.armObj.SetActive(true);
            }
            if(aPhase == 2)
            {
                // instance.hand.Clear();
                // instance.hand.bStart = true;
            }
        }
*/
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.rightArm.nextState == -1)
                return Excavator.c_st_IDLE;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
/*
            if(m_phaser.current == 1)
                instance.arm.targetTransform = null;
            if(m_phaser.current == 2)
                instance.armObj.SetActive(false);
*/
        }

        private void m_TraceArm()
        {
            Entity target = instance.aggroTargets[0];
        }
    }
}