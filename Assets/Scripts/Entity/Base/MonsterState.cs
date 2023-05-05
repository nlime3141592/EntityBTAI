namespace Unchord
{
    public abstract class MonsterState<T> : EntityState<T>
    where T : EntityMonster
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.aggroAi.onAggroBegin += OnAggroBegin;
            instance.aggroAi.onAggroEnd += OnAggroEnd;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            instance.aggroAi.OnFixedUpdate();
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.aggroAi.onAggroBegin -= OnAggroBegin;
            instance.aggroAi.onAggroEnd -= OnAggroEnd;
        }

        public virtual void OnAggroBegin() {}
        public virtual void OnAggroEnd() {}
    }
}