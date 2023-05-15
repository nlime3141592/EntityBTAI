using UnityEngine;

namespace Unchord
{
    public interface ITriggerEnterEvent2D : IStateEvent
    {
        void OnTriggerEnter2D(Collider2D _collider);
    }
}