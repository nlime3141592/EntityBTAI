using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/Entity Components/Excavator")]
    public class Excavator : Monster
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
        public const int c_st_ANCHORING_001             = 13;
        public const int c_st_ANCHORING_002             = 14;
        public const int c_st_ANCHORING_003             = 15;
        public const int c_st_ANCHORING_004             = 16;

        // organ hierarchy
        [Header("Sub Organs")]
        public ExcavatorRightArm rightArm;

        [Header("Excavator State Transition AI")]
        public Vector2 aiCenterOffset;
        public ExcavatorStateRegion3_001 stateAi_001;
        public ExcavatorStateRegion3_002 stateAi_002;
        public ExcavatorStateRegion3_003 stateAi_003;

        public ExcavatorTerrainSensor senseData;

        private EntitySpawnData m_spawnData;
        private LinkedListNode<EntitySpawnData> m_spawnDataNode;

        [Header("Excavator Prefabs")]
        public ExcavatorProjectile projectile;
        public ExcavatorWave wave;

        [Header("Excavator Parameters")]
        public float walkSpeed = 12.0f;

        public float gravity = -49.5f;
        public float speed_freeFallMin = -42.0f;

        public float time_idleMin = 0.8f;
        public float time_idleMax = 1.5f;
        public float time_groggy = 5.0f;

        public float time_idleRotationMin = 0.8f;
        public float time_idleRotationMax = 1.5f;

        public float time_anchor = 2.0f;

        public int waveLength = 15;

        public Vector2 offset_rightArmJoint001;
        public Vector2 position_rightArmJoint001;

        [Header("Skill Ranges")]
        public AreaSensorBox skillRange_stamping_01;

        public bool bAggro;
        public List<Entity> aggroTargets;

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
            StateMachine<Excavator> machine = new StateMachine<Excavator>(20);
            machine.instance = this;

            machine.Add(new ExcavatorSleep());
            machine.Add(new ExcavatorWakeUp());
            machine.Add(new ExcavatorIdle());
            machine.Add(new ExcavatorWalkFront());
            machine.Add(new ExcavatorFreeFall());
            machine.Add(new ExcavatorStamping());
            // machine.Add(new ExcavatorAnchoring());
            machine.Add(new ExcavatorShockWave());
            machine.Add(new ExcavatorShootMissile());
            machine.Add(new ExcavatorBreakFloor());
            machine.Add(new ExcavatorLanding());
            machine.Add(new ExcavatorDie());
            machine.Add(new ExcavatorGroggy());
            machine.Add(new ExcavatorAnchoring001());
            machine.Add(new ExcavatorAnchoring002());
            machine.Add(new ExcavatorAnchoring003());
            machine.Add(new ExcavatorAnchoring004());

            machine.Begin(Excavator.c_st_SLEEP);
            return machine;
        }

        protected void OnDisable()
        {
            // GameManager.instance.generatedBoss.Remove(m_spawnDataNode);
        }
    }
}