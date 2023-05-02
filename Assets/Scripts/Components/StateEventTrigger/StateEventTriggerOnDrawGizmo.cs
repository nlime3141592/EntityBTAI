using UnityEngine;

namespace Unchord
{
    public class StateEventTriggerOnDrawGizmo : StateEventTrigger
    {
        public bool showGizmo = false;

        private void OnDrawGizmos()
        {
            entity.fsm?.OnDrawGizmos(showGizmo);
        }
    }
}