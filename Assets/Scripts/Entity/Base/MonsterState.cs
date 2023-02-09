using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class MonsterState<T> : EntityState<T>
    where T : EntityMonster
    {
        public MonsterState(T _monster)
        : base(_monster)
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
            m_DetectTargets();
            instance.bAggro = instance.aggroTargets.Count > 0;
            if(!instance.bAggro) return;
            m_UpdateLookDirs();
        }

        private void m_DetectTargets()
        {
            Collider2D[] colTargets = EntitySensor.OverlapBox(
                instance,
                instance.aggroRange,
                instance.aggroDebugOption
            );

            instance.aggroTargets.FilterFromColliders(instance, colTargets, false, instance.targetTags.ToArray());
            p_OverrideAggroPriority();
        }

        private void m_UpdateLookDirs()
        {
            float begX = instance.transform.position.x;
            float begY = instance.transform.position.y;
            float endX = instance.aggroTargets[0].transform.position.x;
            float endY = instance.aggroTargets[0].transform.position.y;

            instance.lookDir.x = m_GetLookDir(begX, endX, instance.lookDir.x, 1, instance.bUpdateAggroDirX);
            instance.lookDir.y = m_GetLookDir(begY, endY, instance.lookDir.y, 1, instance.bUpdateAggroDirY);
        }

        private float m_GetLookDir(float basePosition, float targetPosition, float currentLookDir, float defaultLookDir, bool bCanUpdate)
        {
            if(!bCanUpdate)
                return currentLookDir;
            else if(currentLookDir != 1 && currentLookDir != -1)
                return defaultLookDir;
            else if(targetPosition - basePosition < 0)
                return -1;
            else
                return 1;
        }
    }
}