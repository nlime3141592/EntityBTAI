using UnityEngine;

namespace Unchord
{
    public interface IEntityStateEvent
    {
        void OnTriggerEnter2D(Collider2D _collider);
        void OnTriggerExit2D(Collider2D _collider);
        void OnTriggerStay2D(Collider2D _collider);

        void OnCollisionEnter2D(Collision2D _collision);
        void OnCollisionExit2D(Collision2D _collision);
        void OnCollisionStay2D(Collision2D _collision);

        void OnAnimationBegin();
        void OnActionBegin();
        void OnActionEnd();
        void OnAnimationEnd();
    }
}