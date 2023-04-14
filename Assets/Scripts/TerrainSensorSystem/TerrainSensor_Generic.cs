using System;
using UnityEngine;

namespace Unchord
{
    public abstract class TerrainSensor<T> : TerrainSensorBase
    where T : Entity
    {
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
    }
}