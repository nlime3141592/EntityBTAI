using UnityEngine;

namespace Unchord
{
    public interface ICollisionStayEvent2D : IStateEventListener
    {
        void OnCollisionStay2D(Collision2D _collision);
    }
}