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

            SensorUtilities.Bind(instance.transform, instance.slabSensorOnBody.transform);
            instance.slabSensorOnBody.OnUpdate();
            m_IgnoreSlabs();

            instance.slabSensorOnBody.DebugSensor(Color.red, Time.deltaTime);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            instance.iManager.UpdateInputs(true);
        }

        public override void OnLateUpdate()
        {
            base.OnLateUpdate();

            m_IgnoreSlabs();

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

        private void m_IgnoreSlabs()
        {
            System.Collections.Generic.List<Slab> current = instance.prevOverlapSlabs;
            System.Collections.Generic.List<Slab> prev = instance.curOverlapSlabs;
            System.Collections.Generic.List<Slab> sit = instance.downJumpedSlabs;
            instance.curOverlapSlabs = current;
            instance.prevOverlapSlabs = prev;

            instance.sensorBuffer.Clear();
            current.Clear();

            instance.slabSensorOnBody.Sense(in instance.sensorBuffer, null, 1 << LayerMask.NameToLayer("Terrain"));
            instance.sensorBuffer.GetComponents<Slab>(in current);

            for(int i = 0; i < sit.Count; ++i)
                if(!current.Contains(sit[i]))
                    current.Add(sit[i]);

            for(int i = 0; i < current.Count; ++i)
            {
                if(!prev.Contains(current[i]))
                {
                    current[i].IgnoreCollision(instance.hCol.head);
                    current[i].IgnoreCollision(instance.hCol.body);
                    current[i].IgnoreCollision(instance.hCol.feet);
                }
            }

            for(int i = 0; i < prev.Count; ++i)
            {
                if(!current.Contains(prev[i]))
                {
                    prev[i].AcceptCollision(instance.hCol.head);
                    prev[i].AcceptCollision(instance.hCol.body);
                    prev[i].AcceptCollision(instance.hCol.feet);
                }
            }
        }
    }
}