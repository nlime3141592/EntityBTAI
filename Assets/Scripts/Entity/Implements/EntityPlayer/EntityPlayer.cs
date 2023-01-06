using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class EntityPlayer : EntityBase
    {
        public LTRB ltrb;
        public TestBoxSkill btSkill;

        #region Components
        public BattleModule btModule;
        public ElongatedHexagonCollider2D hCol;
        #endregion

        #region Stat
        public Stat runSpeed;
        public Stat glidingGravity;
        public Stat jumpGravity;
        public Stat jumpSpeedBase;
        #endregion

        // Run Options
        public bool bIsRun;

        // Jump Options
        public int maxAirJumpCount = 1;
        public int leftAirJumpCount = 0;

        // Ledge Options
        public bool bOnHoldLedge = false;
        // public bool bOnLedgeEnd = false;

        // Input Messengers
        public readonly InputMessenger jumpBeginMessenger = new InputMessenger();
        public readonly InputMessenger jumpCancelMessenger = new InputMessenger();
        public readonly InputMessenger ledgeEndMessenger = new InputMessenger();

        // 인스펙터에서 서로 다른 10개의 Transform Component를 할당해야 함.
        public Transform originParent;
        public TerrainCheckResult[] terrainCheckResults;
        public Transform[] origins;
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

        public ConfigurationBT<EntityPlayer> aiConfig { get; private set; }
        public PlayerTerrainCheckPage terrainPage { get; private set; }
        public PlayerAbilityPage abilityPage { get; private set; }
        public PlayerFSM fsm { get; private set; }
        public PlayerOnAirPage airPage { get; private set; }
        public PlayerOnFloorPage floorPage { get; private set; }
        public PlayerOnWallPage wallPage { get; private set; }
        public PlayerOnLedgePage ledgePage { get; private set; }

        #region Debug Options
        public int CURRENT_TASK_ID = -1;
        #endregion

        protected override void Start()
        {
            base.Start();

            btSkill = new TestBoxSkill("TestBoxSkill", 315789474, 0, 10, TargetSortType.None, false, ltrb);
            btSkill.bRangeOnEditor = true;

            btModule = GetComponent<BattleModule>();
            hCol = GetComponent<ElongatedHexagonCollider2D>();

            m_AllocAI();
        }

        private void m_AllocAI()
        {
            aiConfig = new ConfigurationBT<EntityPlayer>(this);

            terrainPage = new PlayerTerrainCheckPage(
                origin_F, origin_H,
                origin_LT, origin_RT, origin_LB, origin_RB,
                origin_HL, origin_HR, origin_FL, origin_FR
            );
            abilityPage = new PlayerAbilityPage(aiConfig, 4, "PlayerAbilityPage");

            fsm = new PlayerFSM(aiConfig, 1, "PlayerFSM", terrainPage, abilityPage);
            airPage = new PlayerOnAirPage(aiConfig, 0, "PlayerAirPage");
            floorPage = new PlayerOnFloorPage(aiConfig, 1, "PlayerFloorPage");
            wallPage = new PlayerOnWallPage(aiConfig, 2, "PlayerWallPage");
            ledgePage = new PlayerOnLedgePage(aiConfig, 3, "PlayerLedgePage");

            fsm.Alloc(0, airPage);
            fsm.Alloc(1, floorPage);
            fsm.Alloc(2, wallPage);
            fsm.Alloc(3, ledgePage);

            base.RegisterAI<EntityPlayer>(fsm);
        }

        // private PlayerJumpOnFloor jumpA;

        protected override void p_OnPreFrame()
        {
            base.p_OnPreFrame();
        }

        protected override void p_OnPreInvoke()
        {
            base.p_OnPreInvoke();
            terrainPage.Invoke();
            m_FixedUpdateOrigins();
        }

        protected override void p_Debug_OnPostInvoke()
        {
            /*
            base.p_Debug_OnPostInvoke();
            ++(aiConfig.curFps);
            jumpA.jumpGravityY = jumpGravity;
            jumpA.jumpSpeedStatY = jumpSpeedBase;
            jumpA.speedStatX = bIsRun ? runSpeed : baseMoveSpeed;
            jumpA.Invoke();
            */
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

        protected override void p_OnPostInvoke()
        {
            base.p_OnPostInvoke();
            m_ClearMessengers();
            CURRENT_TASK_ID = aiConfig.curTask?.id ?? -1;
        }

        private void m_ClearMessengers()
        {
            jumpBeginMessenger.Clear();
            jumpCancelMessenger.Clear();
            ledgeEndMessenger.Clear();
        }

        public void OnLedgeAnimationEnd()
        {
            ledgeEndMessenger.Publish();
        }

        protected override void Update()
        {
            if(!canInput)
                return;

            base.Update();

            base.axisInput.x = Input.GetAxisRaw("Horizontal");
            base.axisInput.y = Input.GetAxisRaw("Vertical");

            // base.moveDir = base.axisInput;

            if(Input.GetKeyDown(KeyCode.G))
                btModule.UseBattleSkill(btSkill);

            if(Input.GetKeyDown(KeyCode.Space))
                jumpBeginMessenger.Publish();
            else if(Input.GetKeyUp(KeyCode.Space))
                jumpCancelMessenger.Publish();

            if(Input.GetKeyDown(KeyCode.Return))
                OnLedgeAnimationEnd();

            // bIsRun = Input.GetKey(KeyCode.LeftControl);
        }
    }
}
