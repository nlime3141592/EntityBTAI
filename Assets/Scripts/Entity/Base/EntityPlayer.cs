using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class EntityPlayer : EntityBase
    {
        public LTRB ltrb;
        public TestBoxSkill btSkill;

        public BattleModule btModule;
        public ElongatedHexagonCollider2D hCol;

        public bool canInput = true;

        #region Terrain Checker
        private _EntityPlayerTerrainCheckPage terrainPage; // Behavior Tree Node.

        // 인스펙터에서 서로 다른 10개의 Transform Component를 할당해야 함.
        public Transform origin_F;
        public Transform origin_H;
        public Transform origin_LT;
        public Transform origin_RT;
        public Transform origin_LB;
        public Transform origin_RB;
        public Transform origin_HL;
        public Transform origin_HR;
        public Transform origin_FL;
        public Transform origin_FR;
        #endregion

        public TEST_STAT_BONUS_TABLE bonusStrength;
        public TEST_STAT_BONUS_TABLE bonusDefence;

        private ConfigurationBT<EntityPlayer> aiConfig;
        private EntityMove<EntityPlayer> moveAction;
        private _EntityPlayerFSM fsm;

        protected override void Start()
        {
            base.Start();

            btSkill = new TestBoxSkill(
                "TestBoxSkill", 315789474, 0,
                10,
                TargetSortType.None, false,
                ltrb
            );
            btSkill.bRangeOnEditor = true;
            btModule = GetComponent<BattleModule>();
            hCol = GetComponent<ElongatedHexagonCollider2D>();

            bonusStrength.Start(strength);
            bonusDefence.Start(defence);

            terrainPage = new _EntityPlayerTerrainCheckPage(
                origin_F, origin_H,
                origin_LT, origin_RT, origin_LB, origin_RB,
                origin_HL, origin_HR, origin_FL, origin_FR
            );

            aiConfig = new ConfigurationBT<EntityPlayer>(this);
            moveAction = new EntityMove<EntityPlayer>(aiConfig, 0, "Move");
            fsm = new _EntityPlayerFSM(aiConfig, 1, "FSM", terrainPage);

            fsm.Alloc(0, moveAction);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdateAI<EntityPlayer>(aiConfig, fsm);
        }

        protected override void p_OnPreFrame()
        {
            base.p_OnPreFrame();
            moveAction.SetSpeed(baseMoveSpeed);
        }
        
        protected override void p_OnPreInvoke()
        {
            base.p_OnPreInvoke();
            m_FixedUpdateOrigins();
            terrainPage.Invoke();
        }

        private void m_FixedUpdateOrigins()
        {
            Bounds head = hCol.head.bounds;
            Bounds feet = hCol.feet.bounds;

            origin_F.position = new Vector2(feet.center.x, feet.min.y);
            origin_H.position = new Vector2(head.center.x, head.max.y);

            origin_LT.position = new Vector2(head.min.x, head.center.y);
            origin_RT.position = new Vector2(head.max.x, head.center.y);
            origin_LB.position = new Vector2(feet.min.x, feet.center.y);
            origin_RB.position = new Vector2(feet.max.x, feet.center.y);

            origin_HL.position = new Vector2(head.min.x, head.max.y);
            origin_HR.position = new Vector2(head.max.x, head.max.y);
            origin_FL.position = new Vector2(feet.min.x, feet.min.y);
            origin_FR.position = new Vector2(feet.max.x, feet.min.y);
        }

        protected override void Update()
        {
            if(!canInput)
                return;

            base.Update();

            base.axisInput.x = Input.GetAxisRaw("Horizontal");
            base.axisInput.y = Input.GetAxisRaw("Vertical");

            base.moveDir = base.axisInput;

            if(Input.GetKeyDown(KeyCode.Space))
                btModule.UseBattleSkill(btSkill);
        }
    }

    [Serializable]
    public class TEST_STAT_BONUS_TABLE
    {
        [Tooltip("테스트는 0~100까지 적용 가능합니다.")]
        [Range(0, 100)] public float flatValue = 0;

        [Tooltip("테스트는 0%p~200%p까지 적용 가능합니다.")]
        [Range(0, 2)] public float percentPoint = 0;

        [Tooltip("테스트는 0%~200%까지 적용 가능합니다.")]
        [Range(0, 2)] public float percent = 0;

        private StatModifier modFlatValue;
        private StatModifier modPercentPoint;
        private StatModifier modPercent;

        public void Start(Stat stat)
        {
            modFlatValue = new StatModifier(flatValue, StatModType.Flat);
            modPercentPoint = new StatModifier(percentPoint, StatModType.PercentAdd);
            modPercent = new StatModifier(percent, StatModType.PercentMul);

            stat.AddModifier(modFlatValue);
            stat.AddModifier(modPercentPoint);
            stat.AddModifier(modPercent);
        }
    }
}