using System.Collections.Generic;

namespace Unchord
{
    public class MantisUpSlice : MantisAttack, IBattleState
    {
        public float baseDamage { get; private set; }

        public override int idConstant => Mantis.c_st_UP_SLICE;

        // fixed data
        private float m_cooltime = 0.1f;

        // variables
        private float m_leftCooltime;
        private bool m_bAttacked;

        private List<Entity> m_targets;

        protected override void OnConstruct()
        {
            base.OnConstruct();

            baseDamage = 1.0f;

            m_targets = new List<Entity>(1);
        }

        // TODO: 포효콤보 관련 로직을 추가해야 함.
/*
        protected override void OnConstruct()
        {
            base.OnConstruct();

            base.attackRange = new LTRB()
            {
                left = 0.0f,
                top = 16.0f,
                right = 17.5f,
                bottom = 4.0f
            };
            base.targetCount = 12;
            base.baseDamage = 1.0f;
        }
*/
/*
        public override bool CanTransit()
        {
            bool canAttack = m_leftCooltime <= 0;
            return canAttack;
        }
*/
        public override void OnStateBegin()
        {
            base.OnStateBegin();

            m_leftCooltime = m_cooltime;
            m_bAttacked = false;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if(m_bAttacked)
            {
                instance.vm.FreezePosition(true, false);
                instance.vm.SetVelocityXY(0.0f, -1.0f);
            }
            else
            {
                // NOTE: 앞으로 살짝 전진하는 동작을 줄지 말지 결정.

                instance.vm.FreezePosition(false, false);
                instance.vm.SetVelocityXY(0.0f, -1.0f);
            }
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

        public override void OnUpdate()
        {
            base.OnUpdate();

            SensorUtilities.Bind(instance.transform, instance.skillRange_UpSlice_01.transform);
            instance.skillRange_UpSlice_01.OnUpdate();
        }

        public void OnTriggerBattleState(BattleModule _btModule)
        {
            instance.sensorBuffer.Clear();
            instance.skillRange_UpSlice_01.Sense(in instance.sensorBuffer, _btModule.tags, _btModule.mask);
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