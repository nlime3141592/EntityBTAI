using UnityEngine;

namespace Unchord
{
    public abstract class StateEventHandler<T_IStateEvent> : ExtendedComponent<EntityController>
    where T_IStateEvent : class, IStateEventListener
    {
        protected T_IStateEvent iEvListener { get; private set; }

        protected void UpdateEventListener()
        {
            if(System.Object.ReferenceEquals(baseComponent.fsm.state, iEvListener))
                return;

            iEvListener = baseComponent.fsm.state as T_IStateEvent;
        }
    }
}