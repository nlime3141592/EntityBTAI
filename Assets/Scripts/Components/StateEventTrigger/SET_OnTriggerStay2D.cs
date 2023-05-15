using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("State Event Trigger/OnTriggerStay2D (SET)")]
    public sealed class SET_OnTriggerStay2D : StateEventTrigger
    {
        private void OnTriggerStay2D(Collider2D _collider)
        {
            base.UpdateEventListener();
            iEventListener.OnTriggerStay2D(_collider);
        }
    }
}