using System;
using UnityEngine;

namespace Unchord
{
    public abstract class EntityState<T> : State<T>,
    ICollisionEnterEvent2D, ICollisionExitEvent2D, ICollisionStayEvent2D,
    ITriggerEnterEvent2D, ITriggerExitEvent2D, ITriggerStayEvent2D,
    IAnimationEvents
    where T : Entity
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.bBeginOfAnimation = false;
            instance.bBeginOfAction = false;
            instance.bEndOfAction = false;
            instance.bEndOfAnimation = false;
            instance.animator.SetInteger("state", this.idConstant);
        }
 
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            m_FixedUpdateRotation();
        }

        public virtual void OnTriggerEnter2D(Collider2D _collider) {}
        public virtual void OnTriggerExit2D(Collider2D _collider) {}
        public virtual void OnTriggerStay2D(Collider2D _collider) {}

        public virtual void OnCollisionEnter2D(Collision2D _collision) {}
        public virtual void OnCollisionExit2D(Collision2D _collision) {}
        public virtual void OnCollisionStay2D(Collision2D _collision) {}

        public virtual void OnAnimationBegin()
        {
            instance.bBeginOfAnimation = true;
            instance.bBeginOfAction = false;
            instance.bEndOfAction = false;
            instance.bEndOfAnimation = false;
        }

        public virtual void OnActionBegin()
        {
            instance.bBeginOfAction = true;
        }

        public virtual void OnActionEnd()
        {
            instance.bEndOfAction = true;
        }

        public virtual void OnAnimationEnd()
        {
            instance.bEndOfAnimation = true;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
        }

        private void m_FixedUpdateRotation()
        {
            float x = m_GetEulerRotation(instance.lookDir.y, instance.eulerRotation.x);
            float y = m_GetEulerRotation(instance.lookDir.x, instance.eulerRotation.y);
            instance.eulerRotation.Set(x, y, 0);
            instance.transform.localEulerAngles = instance.eulerRotation;
        }

        private float m_GetEulerRotation(Direction _dir, float _currentEulerAngle)
        {
            if(_dir == Direction.Negative)
                return 180.0f;
            else if(_dir == Direction.Positive)
                return 0.0f;
            else
                return _currentEulerAngle;
        }
    }
}