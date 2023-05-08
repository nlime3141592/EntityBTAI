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

        // Prefabs
        public ExcavatorProjectile projectile;
        public ExcavatorWave wave;

        private EntitySpawnData m_spawnData;
        private LinkedListNode<EntitySpawnData> m_spawnDataNode;

#region 아직 정리 안 함.
        public float walkSpeed = 12.0f;

        public float gravity = -49.5f;
        public float minFreeFallSpeed = -42.0f;

        public int aPhase;

        public int waveLength = 15;
#endregion

        public virtual bool CanAggro()
        {
            return false;
        }

        protected override void OnAwakeEntity()
        {
            base.OnAwakeEntity();

            m_spawnData = new EntitySpawnData("굴착 기계", this);
            m_spawnDataNode = new LinkedListNode<EntitySpawnData>(m_spawnData);
            GameManager.instance.generatedBoss.AddLast(m_spawnDataNode);

            battleModule = GetComponent<BattleModule>();

            arm = GetComponentInChildren<ExcavatorArm>();
            hand = GetComponentInChildren<ExcavatorHand>();
        }

        protected override void OnStartEntity()
        {
            base.OnStartEntity();

            armObj.SetActive(false);
            volumeCollisions.Add(GetComponent<BoxCollider2D>());
        }

        protected override IStateMachineBase InitStateMachine()
        {
            StateMachine<Excavator> machine = new StateMachine<Excavator>(13);
            machine.instance = this;

            machine.Add(new ExcavatorSleep());
            machine.Add(new ExcavatorWakeUp());
            machine.Add(new ExcavatorIdle());
            machine.Add(new ExcavatorWalkFront());
            machine.Add(new ExcavatorFreeFall());
            machine.Add(new ExcavatorStamping());
            machine.Add(new ExcavatorAnchoring());
            machine.Add(new ExcavatorShockWave());
            machine.Add(new ExcavatorShootMissile());
            machine.Add(new ExcavatorBreakFloor());
            machine.Add(new ExcavatorLanding());
            machine.Add(new ExcavatorDie());
            machine.Add(new ExcavatorGroggy());

            machine.Begin(Excavator.c_st_SLEEP);
            return machine;
        }
/*
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
*/
        protected void OnDisable()
        {
            GameManager.instance.generatedBoss.Remove(m_spawnDataNode);
        }
    }
}