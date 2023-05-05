using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class PlayerAttackOnFloor003 : PlayerAttackOnFloorBase, IBattleState
    {
        // TODO: 값 대입을 파일 처리할 수 있도록 할 것.
        public float baseDamage { get; private set; }
        public float speed_Step { get; private set; }
        public float coyote { get; private set; }

        private List<Entity> m_targets;

        public override int idConstant => Player.c_st_ATTACK_ON_FLOOR_003;

        protected override void OnConstruct()
        {
            base.OnConstruct();

            baseDamage = 1.25f;
            speed_Step = 2.0f;
            coyote = 2.0f;

            m_targets = new List<Entity>();
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.timerCoyote_AttackOnFloor.SetTimer(coyote);
            instance.stateNext_AttackOnFloor = Player.c_st_ATTACK_ON_FLOOR_001;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            SensorUtilities.Bind(instance.transform, instance.skillRange_AttackOnFloor003_01.transform);
            instance.skillRange_AttackOnFloor003_01.OnUpdate();
        }

        public void OnTriggerBattleState(BattleModule _btModule)
        {
            instance.sensorBuffer.Clear();
            m_targets.Clear();

            instance.skillRange_AttackOnFloor003_01.Sense(in instance.sensorBuffer, _btModule.tags, _btModule.mask);
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