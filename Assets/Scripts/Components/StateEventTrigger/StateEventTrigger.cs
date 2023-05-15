using UnityEngine;

namespace Unchord
{
    public abstract class StateEventTrigger : ExtendedComponent<EntityController>
    {
        protected IEntityStateEvent iEventListener { get; private set; }

        protected void UpdateEventListener()
        {
            if(baseComponent.fsm.state != iEventListener)
                iEventListener = baseComponent.fsm.state as IEntityStateEvent;
        }
    }
}