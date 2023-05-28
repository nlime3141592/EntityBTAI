using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/State Event Handler/OnTriggerExit2D (SEH)")]
    public sealed class SEH_OnTriggerExit2D : StateEventHandler<ITriggerExitEvent2D>
    {
        private void OnTriggerExit2D(Collider2D _collider)
        {
            base.UpdateEventListener();
            iEvListener?.OnTriggerExit2D(_collider);
        }
    }
}