using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/State Event Trigger/OnTriggerExit2D (SET)")]
    public sealed class SET_OnTriggerExit2D : StateEventTrigger<ITriggerExitEvent2D>
    {
        private void OnTriggerExit2D(Collider2D _collider)
        {
            base.UpdateEventListener();
            iEventListener?.OnTriggerExit2D(_collider);
        }
    }
}