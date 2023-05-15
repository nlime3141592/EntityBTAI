using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/State Event Trigger/OnCollisionStay2D (SET)")]
    public sealed class SET_OnCollisionStay2D : StateEventTrigger<ICollisionStayEvent2D>
    {
        private void OnCollisionStay2D(Collision2D _collision)
        {
            base.UpdateEventListener();
            iEventListener?.OnCollisionStay2D(_collision);
        }
    }
}