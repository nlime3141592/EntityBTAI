using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/State Event Handler/OnCollisionExit2D (SEH)")]
    public sealed class SEH_OnCollisionExit2D : StateEventHandler<ICollisionExitEvent2D>
    {
        private void OnCollisionStay2D(Collision2D _collision)
        {
            base.UpdateEventListener();
            iEvListener?.OnCollisionExit2D(_collision);
        }
    }
}