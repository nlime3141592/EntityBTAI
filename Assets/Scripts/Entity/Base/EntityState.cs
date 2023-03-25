using UnityEngine;

namespace Unchord
{
    public abstract class EntityState<T> : State<T>
    where T : Entity
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();

            int fixedStateNumber = instance.machineInterface.GetMappedValueInverse(instance.machineInterface.current);

            instance.aController.Reset();
            instance.aController.ChangeAnimation(fixedStateNumber);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            m_Rotate();
        }

        public virtual void OnAnimationBegin() {}
        public virtual void OnActionBegin() {}
        public virtual void OnActionEnd() {}
        public virtual void OnAnimationEnd() {}

        private void m_Rotate()
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