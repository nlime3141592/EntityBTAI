using UnityEngine;

namespace Unchord
{
    public abstract class StateEventTrigger<T_IStateEvent> : ExtendedComponent<EntityController>
    where T_IStateEvent : class, IStateEvent
    {
        protected T_IStateEvent iEventListener { get; private set; }

        protected void UpdateEventListener()
        {
            if(System.Object.ReferenceEquals(baseComponent.fsm.state, iEventListener))
                return;

            iEventListener = baseComponent.fsm.state as T_IStateEvent;
        }
    }
}