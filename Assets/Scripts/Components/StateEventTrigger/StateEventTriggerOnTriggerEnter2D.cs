using UnityEngine;

namespace Unchord
{
    public class StateEventTriggerOnTriggerEnter2D : StateEventTrigger
    {
        private void OnTriggerEnter2D(Collider2D _collider)
        {
            (entity.fsm.state as IEntityStateEvent)?.OnTriggerEnter2D(_collider);
        }
    }
}