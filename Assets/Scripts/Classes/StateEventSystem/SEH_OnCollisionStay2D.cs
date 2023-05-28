using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/State Event Handler/OnCollisionStay2D (SEH)")]
    public sealed class SEH_OnCollisionStay2D : StateEventHandler<ICollisionStayEvent2D>
    {
        private void OnCollisionStay2D(Collision2D _collision)
        {
            base.UpdateEventListener();
            iEvListener?.OnCollisionStay2D(_collision);
        }
    }
}