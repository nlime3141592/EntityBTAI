using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/State Event Handler/OnTriggerStay2D (SEH)")]
    public sealed class SEH_OnTriggerStay2D : StateEventHandler<ITriggerStayEvent2D>
    {
        private void OnTriggerStay2D(Collider2D _collider)
        {
            base.UpdateEventListener();
            iEvListener?.OnTriggerStay2D(_collider);
        }
    }
}