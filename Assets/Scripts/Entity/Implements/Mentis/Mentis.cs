using UnityEngine;

namespace UnchordMetroidvania
{
    public class Mentis : EntityMonster
    {
        public Animator mantisAnimator;
        public BattleModule battleModule;
        public BoxCollider2D terrainCollider; // 할당 필요
        public Transform originFloorL;
        public Transform originFloorR;
        public Transform originCeilL;
        public Transform originCeilR;
        public Transform originWallLT;
        public Transform originWallRT;
        public Transform originWallLB;
        public Transform originWallRB;

        public MentisIdle idle;
        public MentisUpSlice upSlice;
        public MentisFSM fsm;

        public bool bOnFloor;
        public bool bOnCeil;
        public bool bOnWallFront;
        public bool bOnWallBack;

        public int attackCount;

        public void PublishAttackCommand()
        {
            ++attackCount;
        }

        public bool CanReceiveAttackCommand()
        {
            if(attackCount > 0)
            {
                --attackCount;
                return true;
            }
            else
            {
                return false;
            }
        }

        private BoxRangeBattleSkill skAggro; // 플레이어 탐지
        public BoxRangeBattleSkill skUpSlice; // 올려베기
        public BoxRangeBattleSkill skDownSlice; // 찍기
        public BoxRangeBattleSkill skBackSlice; // 방향회전베기
        public BoxRangeBattleSkill skJumpSlice; // 도약찍기

        public MentisData data;
        public ConfigurationBT<Mentis> aiConfig;
        public MonsterBaseAI<Mentis> ai;

        public virtual bool CanAggro()
        {
            return false;
        }

        protected override void Start()
        {
            base.Start();
            
            mantisAnimator = GetComponent<Animator>();
            battleModule = GetComponent<BattleModule>();
            aiConfig = new ConfigurationBT<Mentis>(this);
            ai = new MonsterBaseAI<Mentis>(aiConfig, -1, "ai");

            skUpSlice = new BoxRangeBattleSkill(
                "UpSlice", -1,
                data.upSlice.level,
                data.upSlice.targetCount,
                data.upSlice.baseDamage,
                data.upSlice.sortType,
                data.upSlice.canDetectSelf,
                data.upSlice.range
            );

            skUpSlice.bRangeOnEditor = true;

            idle = new MentisIdle(aiConfig, 0, "Idle");
            upSlice = new MentisUpSlice(aiConfig, 1, "UpSlice");

            fsm = new MentisFSM(aiConfig, -1, "fsm");
            fsm.Alloc(0, idle);
            fsm.Alloc(1, upSlice);

            ai.Set(fsm);

            hitColliders.Add(GetComponent<BoxCollider2D>());
        }

        protected override void p_Debug_OnPostInvoke()
        {
            base.p_Debug_OnPostInvoke();

            m_FixedUpdateOrigins();
            skUpSlice.UpdateOptions(data.upSlice);

            ai.Invoke();
        }

        public override void OnAggroBegin()
        {
            base.OnAggroBegin();

            aggroRange.left = 20;
            aggroRange.top = 20;
            aggroRange.right = 20;
            aggroRange.bottom = 20;
        }

        public override void OnAggroEnd()
        {
            base.OnAggroEnd();

            aggroRange.left = 200;
            aggroRange.top = 10;
            aggroRange.right = 20;
            aggroRange.bottom = 5;
        }

        private void m_FixedUpdateOrigins()
        {
            Bounds box = terrainCollider.bounds;
            float minX = box.min.x;
            float minY = box.min.y;
            float maxX = box.max.x;
            float maxY = box.max.y;
            float y10 = minY + (maxY - minY) * 0.1f;
            float y90 = maxY - (maxY - minY) * 0.1f;

            originFloorL.position = new Vector2(minX, minY);
            originFloorR.position = new Vector2(maxX, minY);
            originCeilL.position = new Vector2(minX, maxY);
            originCeilR.position = new Vector2(maxX, maxY);
            originWallLT.position = new Vector2(minX, y90);
            originWallRT.position = new Vector2(maxX, y90);
            originWallLB.position = new Vector2(minX, y10);
            originWallRB.position = new Vector2(maxX, y10);
        }
    }
}