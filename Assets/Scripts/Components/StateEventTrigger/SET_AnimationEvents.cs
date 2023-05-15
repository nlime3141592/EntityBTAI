using System;
using UnityEngine;

namespace Unchord
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("State Event Trigger/Animation Events (SET)")]
    public class SET_AnimationEvents : StateEventTrigger
    {
        public void TriggerBeginOfAnimation()
        {
            base.UpdateEventListener();
            iEventListener.OnAnimationBegin();
        }

        public void TriggerBeginOfAction()
        {
            base.UpdateEventListener();
            iEventListener.OnActionBegin();
        }

        public void TriggerEndOfAction()
        {
            base.UpdateEventListener();
            iEventListener.OnActionEnd();
        }

        public void TriggerEndOfAnimation()
        {
            base.UpdateEventListener();
            iEventListener.OnAnimationEnd();
        }
    }
}