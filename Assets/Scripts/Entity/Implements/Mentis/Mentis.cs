namespace UnchordMetroidvania
{
    public class Mentis : EntityMonster
    {
        private BoxRangeBattleSkill skAggro; // 플레이어 탐지
        public BoxRangeBattleSkill skUpSlice; // 올려베기
        public BoxRangeBattleSkill skDownSlice; // 찍기
        public BoxRangeBattleSkill skBackSlice; // 방향회전베기
        public BoxRangeBattleSkill skJumpSlice; // 도약찍기

        public ConfigurationBT<Mentis> aiConfig;
        public MonsterBaseAI<Mentis> ai;

        public virtual bool CanAggro()
        {
            return false;
        }

        protected override void Start()
        {
            base.Start();
            
            aiConfig = new ConfigurationBT<Mentis>(this);
            ai = new MonsterBaseAI<Mentis>(aiConfig, -1, "ai");
        }

        protected override void p_Debug_OnPostInvoke()
        {
            base.p_Debug_OnPostInvoke();

            ai.Invoke();
        }

        public override void OnAggroBegin()
        {
            base.OnAggroBegin();

            aggroRange.left = 200;
            aggroRange.top = 200;
            aggroRange.right = 200;
            aggroRange.bottom = 200;
        }

        public override void OnAggroEnd()
        {
            base.OnAggroEnd();

            aggroRange.left = 200;
            aggroRange.top = 10;
            aggroRange.right = 20;
            aggroRange.bottom = 5;
        }
    }
}