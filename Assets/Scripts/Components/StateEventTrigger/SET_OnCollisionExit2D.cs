using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/State Event Trigger/OnCollisionExit2D (SET)")]
    public sealed class SET_OnCollisionExit2D : StateEventTrigger<ICollisionExitEvent2D>
    {
        private void OnCollisionStay2D(Collision2D _collision)
        {
            base.UpdateEventListener();
            iEventListener?.OnCollisionExit2D(_collision);
        }
    }
}