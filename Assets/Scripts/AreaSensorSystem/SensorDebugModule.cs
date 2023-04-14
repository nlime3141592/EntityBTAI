using UnityEngine;

namespace Unchord
{
    [ExecuteAlways]
    public sealed class SensorDebugModule : MonoBehaviour
    {
        public Sensor_SO sensor;

        public bool bDrawSensor = true;
        public Color clrSensor = Color.green;

        public bool bDrawBasis = false;
        public Color clrAxisPX = Color.red;
        public Color clrAxisPY = Color.yellow;
        public Color clrAxisNX = Color.cyan;
        public Color clrAxisNY = Color.white;

        private void Update()
        {
            if(sensor == null)
                return;

            sensor.OnUpdate();
        }

        private void OnDrawGizmos()
        {
            if(sensor == null)
                return;

            if(bDrawSensor)
                sensor.DrawSensor(clrSensor);
            if(bDrawBasis)
                sensor.DrawBasis(clrAxisPX, clrAxisPY, clrAxisNX, clrAxisNY);
        }
    }
}