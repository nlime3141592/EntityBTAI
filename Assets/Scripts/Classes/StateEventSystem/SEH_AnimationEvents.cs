using System;
using UnityEngine;

namespace Unchord
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Unchord System/State Event Handler/Animation Events (SEH)")]
    public class SEH_AnimationEvents : StateEventHandler<IAnimationEvents>
    {
        public void TriggerBeginOfAnimation()
        {
            base.UpdateEventListener();
            iEvListener?.OnAnimationBegin();
        }

        public void TriggerBeginOfAction()
        {
            base.UpdateEventListener();
            iEvListener?.OnActionBegin();
        }

        public void TriggerEndOfAction()
        {
            base.UpdateEventListener();
            iEvListener?.OnActionEnd();
        }

        public void TriggerEndOfAnimation()
        {
            base.UpdateEventListener();
            iEvListener?.OnAnimationEnd();
        }
    }
}