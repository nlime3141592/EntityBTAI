using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class MonsterState<T> : EntityState<T>
    where T : EntityMonster
    {
        public MonsterState(T _monster, int _id, string _name)
        : base(_monster, _id, _name)
        {
            
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            m_CheckAggro();
        }

        protected virtual void p_OverrideAggroPriority() {}

        private void m_CheckAggro()
        {
            instance.bPrevAggro = instance.bAggro;

            Collider2D[] colTargets = EntitySensor.OverlapBox(
                instance,
                instance.aggroRange,
                instance.aggroDebugOption
            );

            instance.aggroTargets.FilterFromColliders(instance, colTargets, false, instance.targetTags.ToArray());
            p_OverrideAggroPriority();

            instance.bAggro = instance.aggroTargets.Count > 0;

            if(!instance.bAggro)
                return;

            float begX = instance.transform.position.x;
            float begY = instance.transform.position.y;
            float endX = instance.aggroTargets[0].transform.position.x;
            float endY = instance.aggroTargets[0].transform.position.y;

            if(instance.bUpdateAggroDirX)
                instance.lookDir.x = m_GetLookDir(begX, endX);
            if(instance.bUpdateAggroDirY)
                instance.lookDir.y = m_GetLookDir(begY, endY);
        }

        private float m_GetLookDir(float basePosition, float targetPosition)
        {
            if(targetPosition < basePosition)
                return -1;
            else
                return 1;
        }
    }
}