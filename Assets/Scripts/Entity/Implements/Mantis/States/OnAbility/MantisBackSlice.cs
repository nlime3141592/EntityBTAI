using System.Collections.Generic;

namespace Unchord
{
    public class MantisBackSlice : MantisAttack
    {
        public float baseDamage { get; private set; }

        public override int idConstant => Mantis.c_st_BACK_SLICE;

        private List<Entity> m_targets;

        protected override void OnConstruct()
        {
            base.OnConstruct();

            baseDamage = 1.0f;

            m_targets = new List<Entity>(1);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.bEndOfAnimation)
                return Mantis.c_st_IDLE;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnActionBegin()
        {
            if(instance.lookDir.x == Direction.Positive)
                instance.lookDir.x = Direction.Negative;
            else
                instance.lookDir.x = Direction.Positive;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            SensorUtilities.Bind(instance.transform, instance.skillRange_BackSlice_01.transform);
            instance.skillRange_BackSlice_01.OnUpdate();
        }

        public void OnTriggerBattleState(BattleModule _btModule)
        {
            instance.sensorBuffer.Clear();
            instance.skillRange_BackSlice_01.Sense(in instance.sensorBuffer, _btModule.tags, _btModule.mask);
            instance.sensorBuffer
                .IgnoreColliders(instance.battleTriggers)
                .IgnoreColliders(instance.volumeCollisions)
                .GetComponents<Entity>(in m_targets);

            foreach(Entity entity in m_targets)
            {
                float finalDamage = BattleModule.GetFinalDamage(instance, entity, baseDamage);
                entity.ChangeHealth(-finalDamage);
            }
        }
    }
}