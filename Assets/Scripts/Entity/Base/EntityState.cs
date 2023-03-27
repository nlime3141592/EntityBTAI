using System;
using UnityEngine;

namespace Unchord
{
    public abstract class EntityState<T> : State<T>
    where T : Entity
    {
        public int idFixed { get; protected set; } = -1;

        public override void OnMachineBegin(T _instance, int _id)
        {
            base.OnMachineBegin(_instance, _id);

            if(idFixed < 0)
                Debug.AssertFormat(false, "state \"{0}\" not initialized idFixed value. override OnConstruct() and initialize idFixed value.", this.GetType().Name);
            else
                _instance.stateMap.Add(idFixed, _id);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            if(idFixed == -1)
                idFixed = instance.machineInterface.GetMappedValueInverse(instance.machineInterface.current);

            instance.aController.Reset();
            instance.aController.SetState(idFixed);

            instance.aController.onBeginOfAnimation += OnAnimationBegin;
            instance.aController.onBeginOfAction += OnActionBegin;
            instance.aController.onEndOfAction += OnActionEnd;
            instance.aController.onEndOfAnimation += OnAnimationEnd;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            m_SetLookDir();
            m_Rotate();
        }

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

        public override void OnMachineEnd()
        {
            base.OnMachineEnd();

            idFixed = -1;
        }

        private void m_SetLookDir()
        {
            instance.lookDir.x = m_GetLookDir(instance.axis.x, instance.lookDir.x, instance.bFixedLookDirByAxis.x);
            instance.lookDir.y = m_GetLookDir(instance.axis.y, instance.lookDir.y, instance.bFixedLookDirByAxis.y);
        }

        private Direction m_GetLookDir(float _axis, Direction _current, bool _bFixed)
        {
            if(_bFixed)
                return _current;
            else if(_axis < 0)
                return Direction.Negative;
            else if(_axis > 0)
                return Direction.Positive;
            else
                return _current;
        }

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