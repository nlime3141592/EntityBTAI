using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/State Event Handler/OnCollisionEnter2D (SEH)")]
    public sealed class SEH_OnCollisionEnter2D : StateEventHandler<ICollisionEnterEvent2D>
    {
        private void OnCollisionEnter2D(Collision2D _collision)
        {
            base.UpdateEventListener();
            iEvListener?.OnCollisionEnter2D(_collision);
        }
    }
}