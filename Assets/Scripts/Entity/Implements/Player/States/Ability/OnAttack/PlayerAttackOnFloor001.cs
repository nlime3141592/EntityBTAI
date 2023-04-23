using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class PlayerAttackOnFloor001 : PlayerAttackOnFloorBase, IBattleState
    {
        // TODO: 값 대입을 파일 처리할 수 있도록 할 것.
        public float baseDamage { get; private set; }
        public float speed_Step { get; private set; }
        public float coyote { get; private set; }

        private List<Entity> m_targets;

        public override void OnConstruct()
        {
            base.OnConstruct();

            baseDamage = 1.0f;
            speed_Step = 1.5f;
            coyote = 2.0f;

            idFixed = Player.c_st_ATTACK_ON_FLOOR_001;

            m_targets = new List<Entity>();
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.timerCoyote_AttackOnFloor.SetTimer(coyote);
            instance.stateNext_AttackOnFloor = Player.c_st_ATTACK_ON_FLOOR_002;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            SensorUtilities.Bind(instance.transform, instance.skillRange_AttackOnFloor001.transform);
        }

        public override void OnDrawGizmo()
        {
            base.OnDrawGizmo();

            instance.skillRange_AttackOnFloor001.DrawSensor(Color.white);
        }

        public void OnTriggerBattleState(BattleModule _btModule)
        {
            instance.sensorBuffer.Clear();
            m_targets.Clear();

            instance.skillRange_AttackOnFloor001.Sense(in instance.sensorBuffer, _btModule.targetLayer);
            instance.sensorBuffer.GetComponents<Entity>(in m_targets);

            foreach(Entity entity in m_targets)
            {
                float finalDamage = BattleModule.GetFinalDamage(instance, entity, baseDamage);
                entity.ChangeHealth(-finalDamage);
            }
        }
    }
}