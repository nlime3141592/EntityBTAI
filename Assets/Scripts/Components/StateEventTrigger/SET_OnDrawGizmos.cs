using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/State Event Trigger/OnDrawGizmos (SET)")]
    public sealed class StateEventTriggerOnDrawGizmo : StateEventTrigger<IDrawGizmosEvent>
    {
        public bool showGizmo = false;

        private void OnDrawGizmos()
        {
            base.UpdateEventListener();
            iEventListener?.OnDrawGizmos(showGizmo);
        }
    }
}