using UnityEngine;

namespace Unchord
{
    public interface ITriggerExitEvent2D : IStateEvent
    {
        void OnTriggerExit2D(Collider2D _collider);
    }
}