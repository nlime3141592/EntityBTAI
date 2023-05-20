using UnityEngine;

namespace Unchord
{
    public sealed class AreaSensorBoxDebugger : AreaSensorDebugger
    {
        public AreaSensorBox box;

        protected override void Update()
        {
            transform.BindLocal(box.transform);
            box.OnUpdate();
            box.DebugSensor(base.color, Time.deltaTime);
        }
    }
}