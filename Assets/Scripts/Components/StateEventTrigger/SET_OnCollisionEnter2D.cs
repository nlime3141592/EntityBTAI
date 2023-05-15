using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("State Event Trigger/OnCollisionEnter2D (SET)")]
    public sealed class SET_OnCollisionEnter2D : StateEventTrigger
    {
        private void OnCollisionEnter2D(Collision2D _collision)
        {
            base.UpdateEventListener();
            iEventListener.OnCollisionEnter2D(_collision);
        }
    }
}