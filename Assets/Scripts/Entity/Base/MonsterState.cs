using UnityEngine;

namespace Unchord
{
    public abstract class MonsterState<T> : EntityState<T>
    where T : EntityMonster
    {
        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }
    }
}