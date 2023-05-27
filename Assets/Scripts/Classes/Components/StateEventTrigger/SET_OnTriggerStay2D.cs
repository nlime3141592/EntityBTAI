using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/State Event Trigger/OnTriggerStay2D (SET)")]
    public sealed class SET_OnTriggerStay2D : StateEventTrigger<ITriggerStayEvent2D>
    {
        private void OnTriggerStay2D(Collider2D _collider)
        {
            base.UpdateEventListener();
            iEventListener?.OnTriggerStay2D(_collider);
        }
    }
}