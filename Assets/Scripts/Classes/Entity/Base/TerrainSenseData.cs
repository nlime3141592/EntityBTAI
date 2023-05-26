using UnityEngine;

namespace Unchord
{
    // NOTE: TerrainSensor로 옮기는 중이고, 추후에 삭제할 예정.
    public abstract class TerrainSenseData<T>
    where T : Entity
    {
        public LayerMask targetLayer;

        public virtual void UpdateOrigins(T instane) {}
        public virtual void UpdateData(T instance) {}
        public virtual void UpdateMoveDir(T instance) {}

#region Unity Event Functions
        public void OnFixedUpdate(T _instance)
        {
            SetOrigins(_instance);
            DetectTerrains(_instance);
            SetDirectionVector(_instance);
        }

        public void OnUpdate(T _instance)
        {

        }
#endregion

#region Fixed Update Logics
        protected virtual void SetOrigins(T _instance)
        {

        }

        protected virtual void DetectTerrains(T _instance)
        {

        }

        protected virtual void SetDirectionVector(T _instance)
        {

        }
#endregion

        protected RaycastHit2D GetDetectData(out bool _bDetected, out bool _bHit, Transform _transform, Vector2 _direction, float _dLength, float _hLength, int _layerMask)
        {
            RaycastHit2D rdat = Physics2D.Raycast(_transform.position, _direction, _dLength, _layerMask);

            if(_bDetected = rdat)
                _bHit = rdat.distance <= _hLength;
            else
                _bHit = false;

            return rdat;
        }
    }
}