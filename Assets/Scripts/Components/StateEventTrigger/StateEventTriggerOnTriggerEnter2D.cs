using UnityEngine;

namespace Unchord
{
    public class StateEventTriggerOnTriggerEnter2D : StateEventTrigger
    {
        private void OnTriggerEnter2D(Collision2D _collision)
        {
            (entity.fsm.state as IEntityStateEvent)?.OnCollisionEnter2D(_collision);
        }
    }
}