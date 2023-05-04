using System;
using UnityEngine;

namespace Unchord
{
    public abstract class EntityState<T> : State<T>, IEntityStateEvent
    where T : Entity
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.aController.Reset();
            instance.aController.SetState(idConstant);

            instance.aController.onBeginOfAnimation += OnAnimationBegin;
            instance.aController.onBeginOfAction += OnActionBegin;
            instance.aController.onEndOfAction += OnActionEnd;
            instance.aController.onEndOfAnimation += OnAnimationEnd;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            m_FixedUpdateRotate();
        }

        public virtual void OnTriggerEnter2D(Collider2D _collider) {}
        public virtual void OnCollisionEnter2D(Collision2D _collision) {}

        public virtual void OnAnimationBegin() {}
        public virtual void OnActionBegin() {}
        public virtual void OnActionEnd() {}
        public virtual void OnAnimationEnd() {}

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.aController.onBeginOfAnimation -= OnAnimationBegin;
            instance.aController.onBeginOfAction -= OnActionBegin;
            instance.aController.onEndOfAction -= OnActionEnd;
            instance.aController.onEndOfAnimation -= OnAnimationEnd;
        }

        private void m_FixedUpdateRotate()
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