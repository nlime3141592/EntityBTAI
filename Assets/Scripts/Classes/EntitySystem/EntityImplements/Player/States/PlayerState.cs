using System.Collections.Generic;
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

            instance.transform.BindLocal(instance.slabSensorOnBody.transform);
            instance.slabSensorOnBody.OnUpdate();
            m_IgnoreSlabs();

            instance.slabSensorOnBody.DebugSensor(Color.red, Time.deltaTime);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            instance.iManager.UpdateInputs(instance.bGameStarted);
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

        protected bool bCanLandOnFloor()
        {
            bool bHitSlab;
            Slab hitSlab;

            TerrainSenseData dat = instance.senseData.datFloor;

            if(!dat.bOnHit)
                return false;

            bHitSlab = dat.hitData.collider.gameObject.TryGetComponent<Slab>(out hitSlab);

            if(bHitSlab)
                return !instance.downJumpedSlabs.Contains(hitSlab);
            else
                return true;
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
            List<Slab> current = instance.prevOverlapSlabs;
            List<Slab> prev = instance.curOverlapSlabs;
            List<Slab> dJumped = instance.downJumpedSlabs;

            instance.curOverlapSlabs = current;
            instance.prevOverlapSlabs = prev;

            instance.sensorBuffer.Clear();
            current.Clear();

            instance.slabSensorOnBody.Sense(in instance.sensorBuffer, null, 1 << LayerMask.NameToLayer("Slab"));
            instance.sensorBuffer.GetComponents<Slab>(in current);

            for(int i = 0; i < dJumped.Count; ++i)
                if(!current.Contains(dJumped[i]))
                    current.Add(dJumped[i]);

            for(int i = 0; i < current.Count; ++i)
            {
                if(prev.Count == 0 || !prev.Contains(current[i]))
                {
                    current[i].IgnoreCollision(instance.hCol.head);
                    current[i].IgnoreCollision(instance.hCol.body);
                    current[i].IgnoreCollision(instance.hCol.feet);
                }
            }

            for(int i = 0; i < prev.Count; ++i)
            {
                if(current.Count == 0 || !current.Contains(prev[i]))
                {
                    prev[i].AcceptCollision(instance.hCol.head);
                    prev[i].AcceptCollision(instance.hCol.body);
                    prev[i].AcceptCollision(instance.hCol.feet);
                }
            }
        }
    }
}