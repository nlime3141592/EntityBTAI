using UnityEngine;

namespace Unchord
{
    public interface ICollisionStayEvent2D : IStateEvent
    {
        void OnCollisionStay2D(Collision2D _collision);
    }
}