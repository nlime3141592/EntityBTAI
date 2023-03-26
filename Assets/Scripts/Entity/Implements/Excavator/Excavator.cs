using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class Excavator : EntityMonster
    {
        public const int c_st_SLEEP                     = 0;
        public const int c_st_WAKE_UP                   = 1;
        public const int c_st_IDLE                      = 2;
        public const int c_st_WALK                      = 3;
        public const int c_st_FREE_FALL                 = 4;
        public const int c_st_STAMPING                  = 5;
        public const int c_st_ANCHORING                 = 6;
        public const int c_st_SHOCK_WAVE                = 7; // 2페이즈부터
        public const int c_st_SHOOT_MISSILE             = 8; // 3페이즈부터
        public const int c_st_BREAK_GROUND              = 9;
        public const int c_st_BASIC_LANDING             = 10;
        public const int c_st_DIE                       = 11;
        public const int c_st_GROGGY                    = 12;

        public BattleModule battleModule;
        public BoxCollider2D terrainCollider; // Inspector에서 값 할당 필요
        public Transform aiCenter; // Inspector에서 값 할당 필요
        public Vector2 aiCenterOffset; // Inspector에서 값 할당 필요

        public ExcavatorArm arm;
        public ExcavatorHand hand;
        public GameObject armObj;

        public ExcavatorTerrainSenseData senseData;

        public int CURRENT_STATE;
        public int BOSS_PHASE;

        public Projectile projectile;
        public Vector2 projVelocity;

        public ShockWave shockwave;
        public LTRB shockRange;

        private EntitySpawnData m_spawnData;
        private LinkedListNode<EntitySpawnData> m_spawnDataNode;

#region 아직 정리 안 함.
        public float walkSpeed = 12.0f;

        public float gravity = -49.5f;
        public float minFreeFallSpeed = -42.0f;

        public Dictionary<int, int> m_stateMap;

        public int aPhase;

        public StateMachine<Excavator> fsm;
#endregion

        public virtual bool CanAggro()
        {
            return false;
        }

        protected override void InitComponents()
        {
            base.InitComponents();

            m_spawnData = new EntitySpawnData("굴착 기계", this);
            m_spawnDataNode = new LinkedListNode<EntitySpawnData>(m_spawnData);
            GameManager.instance.generatedBoss.AddLast(m_spawnDataNode);

            battleModule = GetComponent<BattleModule>();

            arm = GetComponentInChildren<ExcavatorArm>();
            hand = GetComponentInChildren<ExcavatorHand>();

            m_stateMap = new Dictionary<int, int>(13);
            fsm = new StateMachine<Excavator>(13);
        }

        protected override void InitStateMachine()
        {
            base.InitStateMachine();

            CompositeState<Excavator> root = new CompositeState<Excavator>(13);

            int idx = -1;

            root[++idx] = new ExcavatorSleep();
            root[++idx] = new ExcavatorWakeUp();
            root[++idx] = new ExcavatorIdle();
            root[++idx] = new ExcavatorWalkFront();
            root[++idx] = new ExcavatorFreeFall();
            root[++idx] = new ExcavatorStamping();
            root[++idx] = new ExcavatorAnchoring();
            root[++idx] = new ExcavatorShockWave();
            root[++idx] = new ExcavatorShootMissile();
            root[++idx] = new ExcavatorBreakFloor();
            root[++idx] = new ExcavatorLanding();
            root[++idx] = new ExcavatorDie();
            root[++idx] = new ExcavatorGroggy();

            monsterPhase = 1;

            fsm.RegisterStateMap(m_stateMap);
            fsm.Begin(this, root, c_st_SLEEP);
            RegisterMachineEvent(fsm);
        }

        protected override void InitMiscellaneous()
        {
            base.InitMiscellaneous();

            armObj.SetActive(false);

            // hitColliders.Add(GetComponent<BoxCollider2D>());
            volumeCollisions.Add(GetComponent<BoxCollider2D>());
        }

        protected override void PreFixedUpdate()
        {
            base.PreFixedUpdate();

            aiCenter.localPosition = aiCenterOffset;
            senseData.UpdateOrigins(this);
        }

        protected override void PreUpdate()
        {
            // NOTE: test input code
            float ixn = Input.GetKeyDown(KeyCode.J) ? -1 : 0;
            float ixp = Input.GetKeyDown(KeyCode.L) ? 1 : 0;
            float iyn = Input.GetKeyDown(KeyCode.I) ? -1 : 0;
            float iyp = Input.GetKeyDown(KeyCode.K) ? 1 : 0;
            float ix = ixn + ixp;
            float iy = iyn + iyp;
            // axisInput.x = ix;
            // axisInput.y = iy;

            base.PreUpdate();

            arm.yAngle = transform.eulerAngles.y;
        }

        protected override void PostUpdate()
        {
            base.PostUpdate();

            CURRENT_STATE = fsm.current;
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
            aggroRange.top = 200;
            aggroRange.right = 200;
            aggroRange.bottom = 200;
        }

        protected void OnDisable()
        {
            GameManager.instance.generatedBoss.Remove(m_spawnDataNode);
        }
    }
}