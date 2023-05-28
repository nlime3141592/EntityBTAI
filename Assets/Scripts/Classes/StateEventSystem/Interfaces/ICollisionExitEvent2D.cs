using UnityEngine;

namespace Unchord
{
    public interface ICollisionExitEvent2D : IStateEventListener
    {
        void OnCollisionExit2D(Collision2D _collision);
    }
}