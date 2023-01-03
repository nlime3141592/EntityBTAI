/*
using UnityEngine;

namespace UnchordMetroidvania
{
    public class EntityPlayer : EntityMovable
    {
        #region Debug Region
        public int CURRENT_STATE = -1;
        public float runSpeedWeight = 1.5f;
        public bool isRun = false;
        #endregion

        #region Components
        [Header("Components registered by script")]
        public Rigidbody2D physicsModule;
        public VelocityController2D velocityModule;
        public ElongatedHexagonCollider2D hexCollider;
        
        [Header("Components requires registration on inspector")]
        public Transform fOrigin;
        public Transform flOrigin;
        public Transform frOrigin;
        public Transform hOrigin;
        public Transform hlOrigin;
        public Transform hrOrigin;
        public Transform lbOrigin;
        public Transform rbOrigin;
        public Transform ltOrigin;
        public Transform rtOrigin;
        #endregion

        #region Behavior Tree Configurations
        private IEntityPlayerConfig pConfig;
        #endregion

        #region Behavior Tree Tasks
        private FloorChecker m_floorChecker;
        private CeilChecker m_ceilChecker;
        private WallChecker m_lbWallChecker;
        private WallChecker m_rbWallChecker;
        private WallChecker m_ltWallChecker;
        private WallChecker m_rtWallChecker;
        private LedgeChecker m_lbLedgeChecker;
        private LedgeChecker m_rbLedgeChecker;
        private LedgeChecker m_ltLedgeChecker;
        private LedgeChecker m_rtLedgeChecker;

        public const int c_st_WALK_ON_FLOOR                     = 0;
        public const int c_st_RUN_ON_FLOOR                      = 1;
        public const int c_st_IDLE_BASIC_ON_FLOOR               = 2;
        public const int c_st_IDLE_LONG_ON_FLOOR                = 3;
        public const int c_st_SIT_ON_FLOOR                      = 4;
        public const int c_st_HEAD_UP_ON_FLOOR                  = 5;
        public const int c_st_FREE_FALL                         = 6;
        public const int c_st_GLIDING                           = 7;
        public const int c_st_IDLE_WALL                         = 8;
        public const int c_st_SLIDING_WALL                      = 9;

        private EntityMoveOnFloor<IEntityPlayerConfig> m_walk;
        private EntityMoveOnFloor<IEntityPlayerConfig> m_run;
        private EntityIdleOnFloor<IEntityPlayerConfig> m_idleBasic;
        private EntityIdleOnFloor<IEntityPlayerConfig> m_idleLong;
        private EntityIdleOnFloor<IEntityPlayerConfig> m_sit;
        private EntityIdleOnFloor<IEntityPlayerConfig> m_headUp;

        private EntityFreeFallOnAir<IEntityPlayerConfig> m_freeFall;
        private EntityGlidingOnAir<IEntityPlayerConfig> m_gliding;

        private EntityIdleOnWall<IEntityPlayerConfig> m_idleWall;
        private EntitySlidingOnWall<IEntityPlayerConfig> m_slidingWall;
        #endregion

        #region Behavior Tree Page Sytsem
        private EntityFSM m_fsm;
        private EntityPlayerAirPage m_airPage;
        private EntityPlayerFloorPage m_floorPage;
        private EntityWallPage m_wallPage;
        private EntityLedgePage m_ledgePage;
        private EntityAbilityPage m_abilityPage;
        private EntityPlayerTerrainCheckPage m_terrainCheckPage;
        #endregion

        private void Start()
        {
            // Components
            physicsModule = GetComponent<Rigidbody2D>();
            velocityModule = GetComponent<VelocityController2D>();
            hexCollider = GetComponent<ElongatedHexagonCollider2D>();

            velocityModule.Subscribe(physicsModule);

            // Configurations
            pConfig = new EntityPlayerConfig();

            pConfig.physics = physicsModule;
            pConfig.velModule = velocityModule;

            // Tasks
            m_floorChecker = new FloorChecker(pConfig.floorConfig);
            m_ceilChecker = new CeilChecker(pConfig.ceilConfig);
            m_lbWallChecker = new WallChecker(pConfig.lbWallConfig, -1);
            m_rbWallChecker = new WallChecker(pConfig.rbWallConfig, 1);
            m_ltWallChecker = new WallChecker(pConfig.ltWallConfig, -1);
            m_rtWallChecker = new WallChecker(pConfig.rtWallConfig, 1);
            m_lbLedgeChecker = new LedgeChecker(pConfig.lbLedgeConfig, -1, 1);
            m_rbLedgeChecker = new LedgeChecker(pConfig.rbLedgeConfig, 1, 1);
            m_ltLedgeChecker = new LedgeChecker(pConfig.ltLedgeConfig, -1, -1);
            m_rtLedgeChecker = new LedgeChecker(pConfig.rtLedgeConfig, 1, -1);

            m_walk = new EntityMoveOnFloor<IEntityPlayerConfig>(pConfig, pConfig.floorConfig, c_st_WALK_ON_FLOOR, "Walk");
            m_run = new EntityMoveOnFloor<IEntityPlayerConfig>(pConfig, pConfig.floorConfig, c_st_RUN_ON_FLOOR, "Run");
            m_idleBasic = new EntityIdleOnFloor<IEntityPlayerConfig>(pConfig, pConfig.floorConfig, c_st_IDLE_BASIC_ON_FLOOR, "IdleBasic");
            m_idleLong = new EntityIdleOnFloor<IEntityPlayerConfig>(pConfig, pConfig.floorConfig, c_st_IDLE_LONG_ON_FLOOR, "IdleLong");
            m_sit = new EntityIdleOnFloor<IEntityPlayerConfig>(pConfig, pConfig.floorConfig, c_st_SIT_ON_FLOOR, "Sit");
            m_headUp = new EntityIdleOnFloor<IEntityPlayerConfig>(pConfig, pConfig.floorConfig, c_st_HEAD_UP_ON_FLOOR, "HeadUp");

            m_freeFall = new EntityFreeFallOnAir<IEntityPlayerConfig>(pConfig, c_st_FREE_FALL, "FreeFall");
            m_gliding = new EntityGlidingOnAir<IEntityPlayerConfig>(pConfig, c_st_GLIDING, "Gliding");

            m_idleWall = new EntityIdleOnWall<IEntityPlayerConfig>(pConfig, c_st_IDLE_WALL, "IdleWall");
            m_slidingWall = new EntitySlidingOnWall<IEntityPlayerConfig>(pConfig, c_st_SLIDING_WALL, "SlidingWall");

            // Page System
            // TODO: m_xxxPage = new EntityXxxPage();
            m_airPage = new EntityPlayerAirPage(
                pConfig,
                m_freeFall,
                m_gliding
            );
            m_floorPage = new EntityPlayerFloorPage(
                pConfig,
                m_walk, m_run,
                m_idleBasic, m_idleLong,
                m_sit, m_headUp
            );
            m_wallPage = new EntityPlayerWallPage(
                pConfig,
                m_idleWall,
                m_slidingWall
            );
            m_terrainCheckPage = new EntityPlayerTerrainCheckPage(
                pConfig.floorConfig,
                m_floorChecker, m_ceilChecker,
                m_lbWallChecker, m_rbWallChecker,
                m_ltWallChecker, m_rtWallChecker,
                m_lbLedgeChecker, m_rbLedgeChecker,
                m_ltLedgeChecker, m_rtLedgeChecker
            );

            m_fsm = new EntityFSM();
            m_fsm.SetAirPage(m_airPage);
            m_fsm.SetFloorPage(m_floorPage);
            m_fsm.SetWallPage(m_wallPage);
            m_fsm.SetLedgePage(m_ledgePage);
            m_fsm.SetAbilityPage(m_abilityPage);
            m_fsm.SetTerrainCheckPage(m_terrainCheckPage);
        }

        private void FixedUpdateOrigins()
        {
            Bounds feet = hexCollider.feet.bounds;
            Bounds head = hexCollider.head.bounds;

            fOrigin.position = new Vector2(feet.center.x, feet.min.y);
            flOrigin.position = new Vector2(feet.min.x, feet.min.y);
            frOrigin.position = new Vector2(feet.max.x, feet.min.y);

            hOrigin.position = new Vector2(head.center.x, head.max.y);
            hlOrigin.position = new Vector2(head.min.x, head.max.y);
            hrOrigin.position = new Vector2(head.max.x, head.max.y);

            lbOrigin.position = new Vector2(feet.min.x, feet.center.y);
            rbOrigin.position = new Vector2(feet.max.x, feet.center.y);
            ltOrigin.position = new Vector2(head.min.x, head.center.y);
            rtOrigin.position = new Vector2(head.max.x, head.center.y);
        }

        private void FixedUpdateTerrainCheckerConfigs()
        {
            int layerMask = 1 << LayerMask.NameToLayer("Terrain");

            pConfig.floorConfig.tOrigin = this.fOrigin;
            pConfig.floorConfig.dLength = 0.5f;
            pConfig.floorConfig.hLength = 0.04f;
            pConfig.floorConfig.layerMask = layerMask;

            pConfig.ceilConfig.tOrigin = this.hOrigin;
            pConfig.ceilConfig.dLength = 0.5f;
            pConfig.ceilConfig.hLength = 0.04f;
            pConfig.ceilConfig.layerMask = layerMask;

            pConfig.lbWallConfig.tOrigin = this.lbOrigin;
            pConfig.lbWallConfig.dLength = 0.2f;
            pConfig.lbWallConfig.hLength = 0.04f;
            pConfig.lbWallConfig.layerMask = layerMask;

            pConfig.rbWallConfig.tOrigin = this.rbOrigin;
            pConfig.rbWallConfig.dLength = 0.2f;
            pConfig.rbWallConfig.hLength = 0.04f;
            pConfig.rbWallConfig.layerMask = layerMask;

            pConfig.ltWallConfig.tOrigin = this.ltOrigin;
            pConfig.ltWallConfig.dLength = 0.2f;
            pConfig.ltWallConfig.hLength = 0.04f;
            pConfig.ltWallConfig.layerMask = layerMask;

            pConfig.rtWallConfig.tOrigin = this.rtOrigin;
            pConfig.rtWallConfig.dLength = 0.2f;
            pConfig.rtWallConfig.hLength = 0.04f;
            pConfig.rtWallConfig.layerMask = layerMask;

            pConfig.lbLedgeConfig.tOrigin = this.flOrigin;
            pConfig.lbLedgeConfig.dLength = 0.2f;
            pConfig.lbLedgeConfig.hLength = 0.04f;
            pConfig.lbLedgeConfig.height = lbOrigin.position.y - flOrigin.position.y;
            pConfig.lbLedgeConfig.ledgerp = 0.1f;

            pConfig.rbLedgeConfig.tOrigin = this.flOrigin;
            pConfig.rbLedgeConfig.dLength = 0.2f;
            pConfig.rbLedgeConfig.hLength = 0.04f;
            pConfig.rbLedgeConfig.height = lbOrigin.position.y - flOrigin.position.y;
            pConfig.rbLedgeConfig.ledgerp = 0.1f;

            pConfig.ltLedgeConfig.tOrigin = this.hlOrigin;
            pConfig.ltLedgeConfig.dLength = 0.2f;
            pConfig.ltLedgeConfig.hLength = 0.04f;
            pConfig.ltLedgeConfig.height = hlOrigin.position.y - ltOrigin.position.y;
            pConfig.ltLedgeConfig.ledgerp = 0.1f;

            pConfig.rtLedgeConfig.tOrigin = this.frOrigin;
            pConfig.rtLedgeConfig.dLength = 0.2f;
            pConfig.rtLedgeConfig.hLength = 0.04f;
            pConfig.rtLedgeConfig.height = frOrigin.position.y - rtOrigin.position.y;
            pConfig.rtLedgeConfig.ledgerp = 0.1f;
        }

        private void FixedUpdatePlayerTerrainConfigs()
        {
            m_fsm.bDetectFloor = pConfig.floorConfig.bDetected;
            m_fsm.bHitFloor = pConfig.floorConfig.bHit;
            m_fsm.bDetectCeil = pConfig.ceilConfig.bDetected;
            m_fsm.bHitCeil = pConfig.ceilConfig.bHit;
            m_fsm.bDetectWall = (pConfig.ltWallConfig.bDetected && pConfig.lbWallConfig.bDetected && pConfig.lookDirX == -1) || (pConfig.rtWallConfig.bDetected && pConfig.rbWallConfig.bDetected && pConfig.lookDirX == 1);
            m_fsm.bHitWall = (pConfig.ltWallConfig.bHit && pConfig.lbWallConfig.bHit && pConfig.lookDirX == -1) || (pConfig.rtWallConfig.bHit && pConfig.rbWallConfig.bHit && pConfig.lookDirX == 1);
        }

        private void FixedUpdate()
        {
            pConfig.AddFps();

            FixedUpdateOrigins();
            FixedUpdateTerrainCheckerConfigs();
            FixedUpdatePlayerTerrainConfigs();

            // Debugs
            m_run.speedWeight = runSpeedWeight;
            pConfig.isRun = this.isRun;

            m_fsm.Invoke(pConfig.curFps);
            CURRENT_STATE = pConfig.currentState;
        }

        private void Update()
        {
            pConfig.xNegative = Input.GetKey(KeyCode.LeftArrow) ? 1.0f : 0.0f;
            pConfig.xPositive = Input.GetKey(KeyCode.RightArrow) ? 1.0f : 0.0f;
            pConfig.yNegative = Input.GetKey(KeyCode.DownArrow) ? 1.0f : 0.0f;
            pConfig.yPositive = Input.GetKey(KeyCode.UpArrow) ? 1.0f : 0.0f;

            pConfig.UpdateLookDir();
        }
    }
}
*/