using UnityEngine;

namespace Unchord
{
    public interface ITriggerExitEvent2D : IStateEventListener
    {
        void OnTriggerExit2D(Collider2D _collider);
    }
}