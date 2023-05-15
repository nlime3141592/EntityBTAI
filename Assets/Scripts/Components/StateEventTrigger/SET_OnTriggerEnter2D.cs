using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("State Event Trigger/OnTriggerEnter2D (SET)")]
    public sealed class SET_OnTriggerEnter2D : StateEventTrigger
    {
        private void OnTriggerEnter2D(Collider2D _collider)
        {
            base.UpdateEventListener();
            iEventListener.OnTriggerEnter2D(_collider);
        }
    }
}