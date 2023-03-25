using UnityEngine;

namespace Unchord
{
    public abstract class MonsterState<T> : EntityState<T>
    where T : EntityMonster
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            m_CheckAggro();
        }

        protected virtual void p_OverrideAggroPriority() {}

        private void m_CheckAggro()
        {
            bool prev = instance.bAggro;
            instance.bPrevAggro = prev;

            m_DetectTargets();

            bool current = instance.aggroTargets.Count > 0;
            instance.bAggro = current;

            if(current)
            {
                if(!prev)
                {
                    instance.OnAggroBegin();
                }

                m_UpdateLookDirs();
            }
            else if(prev)
            {
                instance.OnAggroEnd();
            }
        }

        private void m_DetectTargets()
        {
            Collider2D[] colTargets = EntitySensor.OverlapBox(
                instance,
                instance.aggroRange,
                instance.aggroDebugOption,
                instance.targetLayerMask
            );

            instance.aggroTargets.FilterFromColliders(instance, colTargets, false, null, instance.targetTags.ToArray());
            p_OverrideAggroPriority();
        }

        private void m_UpdateLookDirs()
        {
            float begX = instance.transform.position.x;
            float begY = instance.transform.position.y;
            float endX = instance.aggroTargets[0].transform.position.x;
            float endY = instance.aggroTargets[0].transform.position.y;

            instance.lookDir.x = m_GetLookDir(begX, endX, instance.lookDir.x, Direction.Positive, instance.bUpdateAggroDirX);
            instance.lookDir.y = m_GetLookDir(begY, endY, instance.lookDir.y, Direction.Positive, instance.bUpdateAggroDirY);
        }

        private Direction m_GetLookDir(float basePosition, float targetPosition, Direction currentLookDir, Direction defaultLookDir, bool bCanUpdate)
        {
            if(!bCanUpdate)
                return currentLookDir;
            else if(currentLookDir != Direction.Positive && currentLookDir != Direction.Negative)
                return defaultLookDir;
            else if(targetPosition - basePosition < 0)
                return Direction.Negative;
            else
                return Direction.Positive;
        }

        private int m_GetAreaCode(Vector2 origin, Vector2 target, float rangeX1, float rangeX2, float rangeY1, float rangeY2)
        {
            int code = 0;

            float dx = target.x - origin.x;
            float dy = target.y - origin.y;

            if(dx < 0)
            {
                code += 10;
                dx = -dx;
            }
            if(dy < 0)
            {
                code += 20;
                dy = -dy;
            }

            if(dx >= rangeX2)
                code += 2;
            else if(dy >= rangeX1)
                code += 1;

            if(dy >= rangeY2)
                code += 6;
            else if(dy >= rangeY1)
                code += 3;

            return code;
        }
    }
}