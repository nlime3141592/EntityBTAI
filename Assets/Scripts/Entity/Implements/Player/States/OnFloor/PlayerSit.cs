using UnityEngine;

namespace Unchord
{
    public class PlayerSit : PlayerStand
    {
        public override int idConstant => Player.c_st_SIT;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.sensorBuffer.Clear();
            instance.sitSlabs.Clear();

            instance.transform.BindLocal(instance.slabSensorOnSit.transform);
            instance.slabSensorOnSit.OnUpdate();
            instance.slabSensorOnSit.Sense(in instance.sensorBuffer, null, 1 << LayerMask.NameToLayer("Slab"));
            instance.sensorBuffer.GetComponents<Slab>(in instance.sitSlabs);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.offset_StandCamera = Vector2.down;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            instance.slabSensorOnSit.DebugSensor(Color.blue, Time.deltaTime);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.sitSlabs.Count > 0 && instance.iManager.jumpDown)
                return Player.c_st_JUMP_DOWN;

            return MachineConstant.c_lt_PASS;
        }
    }
}