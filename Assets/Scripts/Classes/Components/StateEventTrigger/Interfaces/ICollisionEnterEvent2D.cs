using UnityEngine;

namespace Unchord
{
    public interface ICollisionEnterEvent2D : IStateEvent
    {
        void OnCollisionEnter2D(Collision2D _collision);
    }
}