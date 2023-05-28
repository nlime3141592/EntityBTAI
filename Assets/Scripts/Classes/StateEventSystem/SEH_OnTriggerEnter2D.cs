using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/State Event Handler/OnTriggerEnter2D (SEH)")]
    public sealed class SEH_OnTriggerEnter2D : StateEventHandler<ITriggerEnterEvent2D>
    {
        private void OnTriggerEnter2D(Collider2D _collider)
        {
            base.UpdateEventListener();
            iEvListener?.OnTriggerEnter2D(_collider);
        }
    }
}