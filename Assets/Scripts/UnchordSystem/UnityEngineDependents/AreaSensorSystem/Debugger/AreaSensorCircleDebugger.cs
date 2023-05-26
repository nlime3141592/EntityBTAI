using UnityEngine;

namespace Unchord
{
    public sealed class AreaSensorCircleDebugger : AreaSensorDebugger
    {
        public AreaSensorCircle circle;

        protected override void Update()
        {
            transform.BindLocal(circle.transform);
            circle.OnUpdate();
            circle.DebugSensor(base.color, Time.deltaTime);
        }
    }
}