using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class MantisUpSlice : TaskNodeBT<Mantis>, IBattleState
    {
        // property

        // fixed data
        private List<EntityBase> m_targets;
        private int m_targetCount = 1;
        private float m_baseDamage = 1.0f;
        private LTRB m_attackRange = new LTRB()
        {
            left = 5.5f,
            top = 5.5f,
            right = 15.5f,
            bottom = 5.5f
        };
        private EntitySensorGizmoOption m_attackGizmoOption = new EntitySensorGizmoOption()
        {
            bShowGizmo = true,
            duration = 0.2f,
            color = Color.magenta
        };

        // variable
        private bool m_bInvoked = false;

        public MantisUpSlice(Mantis instance)
        : base(instance)
        {
            m_targets = new List<EntityBase>(m_targetCount);
        }

        void IBattleState.OnBattle()
        {
            float baseDamage = m_baseDamage;

            Collider2D[] colTargets = EntitySensor.OverlapBox(instance, m_attackRange, m_attackGizmoOption);
            m_targets.Clear();
            m_targets
                .FilterFromColliders(instance, colTargets, false, "Player")
                .SetTargetCount(m_targetCount);

            foreach(EntityBase target in m_targets)
            {
                float finalDamage = instance.battleModule.GetFinalDamage(target, baseDamage);
                target.Damage(finalDamage);
            }
        }

        protected override void p_OnInvokeBegin()
        {
            if(m_bInvoked)
                return;

            instance.battleModule.SetBattleState(this);
            m_bInvoked = true;
            base.p_OnInvokeBegin();

            instance.bFixLookDirX = true;
            instance.mantisAnimator.SetInteger("state", 1);
        }

        protected override InvokeResult p_Invoke()
        {
            return instance.animationResult;
        }

        protected override void p_OnSuccess()
        {
            base.p_OnSuccess();

            instance.mantisAnimator.SetInteger("state", 0);
            instance.animationResult = InvokeResult.Running;
            instance.bFixLookDirX = false;
            m_bInvoked = false;
        }

        public override void ResetNode()
        {
            base.ResetNode();

            instance.bFixLookDirX = false;
            m_bInvoked = false;
        }
    }
}