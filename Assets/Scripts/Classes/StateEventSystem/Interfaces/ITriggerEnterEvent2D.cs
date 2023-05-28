using UnityEngine;

namespace Unchord
{
    public interface ITriggerEnterEvent2D : IStateEventListener
    {
        void OnTriggerEnter2D(Collider2D _collider);
    }
}