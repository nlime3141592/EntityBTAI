using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/State Event Trigger/OnTriggerEnter2D (SET)")]
    public sealed class SET_OnTriggerEnter2D : StateEventTrigger<ITriggerEnterEvent2D>
    {
        private void OnTriggerEnter2D(Collider2D _collider)
        {
            base.UpdateEventListener();
            iEventListener?.OnTriggerEnter2D(_collider);
        }
    }
}