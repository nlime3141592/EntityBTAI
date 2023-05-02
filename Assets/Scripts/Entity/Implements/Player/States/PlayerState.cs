using UnityEngine;

namespace Unchord
{
    public abstract class PlayerState : EntityState<Player>
    {
        public override void OnFixedUpdate()
        {
            m_SetLookDir();
            base.OnFixedUpdate();
            instance.senseData.OnFixedUpdate(instance);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            instance.CURRENT_STATE = idConstant;
            instance.iManager.UpdateInputs(true);
        }

        public override void OnLateUpdate()
        {
            base.OnLateUpdate();

            instance.CURRENT_TYPE = machine.state.GetType().ToString();

            float dT = Time.deltaTime;

            instance.timerCoyote_AttackOnFloor.OnUpdate(dT);
            instance.timerCoyote_AttackOnAir.OnUpdate(dT);
        }

        private void m_SetLookDir()
        {
            instance.lookDir.x = m_GetLookDir(
                instance.iManager.ix,
                instance.lookDir.x,
                instance.bFixedLookDirByAxis.x
            );

            instance.lookDir.y = m_GetLookDir(
                instance.iManager.iy,
                instance.lookDir.y,
                instance.bFixedLookDirByAxis.y
            );
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
    }
}