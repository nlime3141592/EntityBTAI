using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class Mantis : EntityMonster
    {
        public const int c_st_IDLE                       = 0;
        public const int c_st_WALK_FRONT                 = 1;
        public const int c_st_WALK_BACK                  = 2;
        public const int c_st_SHOUT                      = 3;
        public const int c_st_KNIFE_GRINDING             = 4;
        public const int c_st_UP_SLICE                   = 5;
        public const int c_st_BACK_SLICE                 = 6;
        public const int c_st_CHOP                       = 7;
        public const int c_st_JUMP_CHOP                  = 8;
        public const int c_st_GROGGY                     = 9;
        public const int c_st_DIE                        = 10;

        public BattleModule battleModule;
        public BoxCollider2D terrainCollider; // Inspector에서 값 할당 필요
        public Transform aiCenter; // Inspector에서 값 할당 필요
        public Vector2 aiCenterOffset; // Inspector에서 값 할당 필요

        public StateMachine<Mantis> fsm;
        public MantisTerrainSenseData senseData;

        public int CURRENT_STATE;
        public int BOSS_PHASE;

        private EntitySpawnData m_spawnData;
        private LinkedListNode<EntitySpawnData> m_spawnDataNode;

        public Dictionary<int, int> m_stateMap;

#region 아직 정리 안 함.
        public Vector2 moveDir;

        public float wallDetectLength = 0.06f;
        public float hitLength = 0.06f;
        public float walkSpeed = 6.0f;
#endregion

        public virtual bool CanAggro()
        {
            return false;
        }

        protected override void InitComponents()
        {
            base.InitComponents();

            m_spawnData = new EntitySpawnData("사마귀", this);
            m_spawnDataNode = new LinkedListNode<EntitySpawnData>(m_spawnData);
            battleModule = GetComponent<BattleModule>();
            fsm = new StateMachine<Mantis>(11);
            m_stateMap = new Dictionary<int, int>(11);
        }

        protected override void InitStateMachine()
        {
            base.InitStateMachine();

            CompositeState<Mantis> root = new CompositeState<Mantis>(11);

            int idx = -1;
            root[++idx] = new MantisIdle();
            root[++idx] = new MantisWalkFront();
            root[++idx] = new MantisWalkBack();
            root[++idx] = new MantisShout();
            root[++idx] = new MantisKnifeGrinding();
            root[++idx] = new MantisUpSlice();
            root[++idx] = new MantisBackSlice();
            root[++idx] = new MantisChop();
            root[++idx] = new MantisJumpChop();
            root[++idx] = new MantisGroggy();
            root[++idx] = new MantisDie();

            fsm.RegisterStateMap(m_stateMap);
            fsm.Begin(this, root, c_st_IDLE);
            RegisterMachineEvent(fsm);
        }

        protected override void InitMiscellaneous()
        {
            base.InitMiscellaneous();

            volumeCollisions.Add(GetComponent<BoxCollider2D>());
            // hitColliders.Add(GetComponent<BoxCollider2D>());

            GameManager.instance.generatedBoss.AddLast(m_spawnDataNode);
        }

        protected override void PreFixedUpdate()
        {
            base.PreFixedUpdate();

            aiCenter.localPosition = aiCenterOffset;
            senseData.UpdateOrigins(this);
        }

        protected override void PostUpdate()
        {
            base.PostUpdate();

            // BOSS_PHASE = fsm.mode;
            // Debug.Log(string.Format("Current State: {0}", fsm.stateId));
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

        protected void OnDisable()
        {
            GameManager.instance.generatedBoss.Remove(m_spawnDataNode);
        }

        public float GetAxisInputX()
        {
            if(!bAggro)
                return 0;

            float tx = aggroTargets[0].transform.position.x;
            float px = transform.position.x;

            if(tx - px < 0)
                return -1;
            else
                return 1;
        }
    }
}