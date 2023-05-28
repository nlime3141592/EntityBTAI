using UnityEngine;

namespace Unchord
{
    public interface ICollisionEnterEvent2D : IStateEventListener
    {
        void OnCollisionEnter2D(Collision2D _collision);
    }
}