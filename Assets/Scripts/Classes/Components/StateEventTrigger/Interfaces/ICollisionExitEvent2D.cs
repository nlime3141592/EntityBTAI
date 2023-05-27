using UnityEngine;

namespace Unchord
{
    public interface ICollisionExitEvent2D : IStateEvent
    {
        void OnCollisionExit2D(Collision2D _collision);
    }
}