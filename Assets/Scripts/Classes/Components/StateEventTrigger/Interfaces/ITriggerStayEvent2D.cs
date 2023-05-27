using UnityEngine;

namespace Unchord
{
    public interface ITriggerStayEvent2D : IStateEvent
    {
        void OnTriggerStay2D(Collider2D _collider);
    }
}