using UnityEngine;

namespace Unchord
{
    public class StateEventTriggerOnCollisionEnter2D : StateEventTrigger
    {
        private void OnCollisionEnter2D(Collision2D _collision)
        {
            // (entity.fsm.state as IEntityStateEvent)?.OnCollisionEnter2D(_collision);
        }
    }
}