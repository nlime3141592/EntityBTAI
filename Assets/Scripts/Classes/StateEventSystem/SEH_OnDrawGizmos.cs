using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/State Event Handler/OnDrawGizmos (SEH)")]
    public sealed class SEH_StateEventTriggerOnDrawGizmo : StateEventHandler<IDrawGizmosEvent>
    {
        public bool showGizmo = false;

        private void OnDrawGizmos()
        {
            base.UpdateEventListener();
            iEvListener?.OnDrawGizmos(showGizmo);
        }
    }
}