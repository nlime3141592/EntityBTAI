using UnityEngine;

namespace Unchord
{
    public interface IEntityStateEvent
    {
        void OnTriggerEnter2D(Collider2D _collider);
        void OnCollisionEnter2D(Collision2D _collision);
    }
}