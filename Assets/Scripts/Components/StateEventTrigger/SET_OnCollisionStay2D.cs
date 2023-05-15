using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("State Event Trigger/OnCollisionStay2D (SET)")]
    public sealed class SET_OnCollisionStay2D : StateEventTrigger
    {
        private void OnCollisionStay2D(Collision2D _collision)
        {
            base.UpdateEventListener();
            iEventListener.OnCollisionStay2D(_collision);
        }
    }
}