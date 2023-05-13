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

        // organ hierarchy
        public ExcavatorRightArm rightArm;

        public Vector2 aiCenterOffset; // Inspector에서 값 할당 필요

        public ExcavatorTerrainSensor senseData;
        public ExcavatorStateRegion3_001 stateAi_001;
        public ExcavatorStateRegion3_002 stateAi_002;
        public ExcavatorStateRegion3_003 stateAi_003;

        // Prefabs
        public ExcavatorProjectile projectile;
        public ExcavatorWave wave;

        private EntitySpawnData m_spawnData;
        private LinkedListNode<EntitySpawnData> m_spawnDataNode;

#region 아직 정리 안 함.
        public float walkSpeed = 12.0f;

        public float gravity = -49.5f;
        public float speed_freeFallMin = -42.0f;

        public float time_idleMin = 0.8f;
        public float time_idleMax = 1.5f;
        public float time_groggy = 5.0f;

        public float rangeAi3_rx1 = 10.5f;
        public float rangeAi3_rx2 = 21.0f;
        public float rangeAi3_ry1 = 4.0f;
        public float rangeAi3_ry2 = 8.0f;

        public AreaSensorBox skillRange_stamping_01;

        public int waveLength = 15;
#endregion

        public override void OnAwakeEntity()
        {
            base.OnAwakeEntity();

            rightArm = GetComponentInChildren<ExcavatorRightArm>(true);

            senseData = new ExcavatorTerrainSensor();
            stateAi_001 = new ExcavatorStateRegion3_001();
            stateAi_002 = new ExcavatorStateRegion3_002();
            stateAi_003 = new ExcavatorStateRegion3_003();

            m_spawnData = new EntitySpawnData("굴착 기계", this);

            // m_spawnDataNode = new LinkedListNode<EntitySpawnData>(m_spawnData);
            // GameManager.instance.generatedBoss.AddLast(m_spawnDataNode);
        }

        public override void OnStartEntity()
        {
            base.OnStartEntity();

            volumeCollisions.Add(GetComponent<BoxCollider2D>());
        }

        public override IStateMachineBase InitStateMachine()
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

        protected void OnDisable()
        {
            // GameManager.instance.generatedBoss.Remove(m_spawnDataNode);
        }
    }
}