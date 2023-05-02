using UnityEngine;

namespace Unchord
{
    public interface IEntityStateEvent
    {
        void OnCollisionEnter2D(Collision2D _collision);
    }
}