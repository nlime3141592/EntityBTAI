using UnityEngine;

namespace Unchord
{
    public interface ITriggerStayEvent2D : IStateEventListener
    {
        void OnTriggerStay2D(Collider2D _collider);
    }
}