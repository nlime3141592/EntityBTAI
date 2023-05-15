using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("State Event Trigger/OnCollisionExit2D (SET)")]
    public sealed class SET_OnCollisionExit2D : StateEventTrigger
    {
        private void OnCollisionStay2D(Collision2D _collision)
        {
            base.UpdateEventListener();
            iEventListener.OnCollisionExit2D(_collision);
        }
    }
}