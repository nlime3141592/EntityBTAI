using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    [AddComponentMenu("Unchord System/Entity Components/Mantis")]
    public class Mantis : Monster
    {
        public const int c_st_IDLE                       = 0;
        public const int c_st_WALK_FRONT                 = 1;
        public const int c_st_WALK_BACK                  = 2;
        public const int c_st_PHASE_SHOUT                = 3;
        public const int c_st_KNIFE_GRINDING             = 4;
        public const int c_st_UP_SLICE                   = 5;
        public const int c_st_BACK_SLICE                 = 6;
        public const int c_st_CHOP                       = 7;
        public const int c_st_JUMP_CHOP_001              = 8;
        public const int c_st_GROGGY                     = 9;
        public const int c_st_DIE                        = 10;
        public const int c_st_SLEEP                      = 11;
        public const int c_st_COMBO_SHOUT                = 12;
        public const int c_st_JUMP_CHOP_002              = 13;

        [Header("Mantis State Transition AI")]
        public Vector2 aiCenterOffset;
        public MantisStateRegion3_001 stateAi_001;
        public MantisStateRegion3_002 stateAi_002;

        public MantisTerrainSensor senseData;

        private EntitySpawnData m_spawnData;
        private LinkedListNode<EntitySpawnData> m_spawnDataNode;

        [Header("Mantis Skill Ranges")]
        public AreaSensorBox skillRange_BackSlice_01;
        public AreaSensorBox skillRange_Chop_01;
        public AreaSensorBox skillRange_JumpChop002_01;
        public AreaSensorBox skillRange_UpSlice_01;

        [Header("Mantis Parameters")]
        public float wallDetectLength = 0.06f;
        public float hitLength = 0.06f;
        public float walkSpeed = 6.0f;

        public int frame_idleTimeMin = 60;
        public int frame_idleTimeMax = 120;
        public int frame_idleAggroDelay = 70;

        public float speed_jumpChopX = 8.0f;
        public float speed_jumpChopY = 4.0f;
        public float force_jumpChopY = 49.5f;

        public bool bAggro;
        public List<Entity> aggroTargets;

        public virtual bool CanAggro()
        {
            return false;
        }

        public override void OnAwakeEntity()
        {
            base.OnAwakeEntity();

            senseData = new MantisTerrainSensor();
            stateAi_001 = new MantisStateRegion3_001();
            stateAi_002 = new MantisStateRegion3_002();

            // m_spawnData = new EntitySpawnData("사마귀", this);
            // m_spawnDataNode = new LinkedListNode<EntitySpawnData>(m_spawnData);
        }

        public override void OnEnableEntity()
        {
            base.OnEnableEntity();

            GameManager.instance.lBoss.Add(this);
        }

        public override void OnDisableEntity()
        {
            base.OnDisableEntity();

            GameManager.instance.lBoss.RemoveAll((e) => e == this);
        }

        public override void OnStartEntity()
        {
            base.OnStartEntity();

            volumeCollisions.Add(GetComponent<BoxCollider2D>());

            // GameManager.instance.generatedBoss.AddLast(m_spawnDataNode);
        }

        public override IStateMachineBase InitStateMachine()
        {
            StateMachine<Mantis> machine = new StateMachine<Mantis>(20);
            machine.instance = this;

            machine.Add(new MantisSleep());
            machine.Add(new MantisIdle());
            machine.Add(new MantisWalkFront());
            machine.Add(new MantisWalkBack());
            machine.Add(new MantisPhaseShout());
            machine.Add(new MantisComboShout());
            machine.Add(new MantisKnifeGrinding());
            machine.Add(new MantisUpSlice());
            machine.Add(new MantisBackSlice());
            machine.Add(new MantisChop());
            machine.Add(new MantisJumpChop001());
            machine.Add(new MantisGroggy());
            machine.Add(new MantisDie());
            machine.Add(new MantisJumpChop002());

            machine.Begin(Mantis.c_st_SLEEP);
            return machine;
        }

        protected void OnDisable()
        {
            // GameManager.instance.generatedBoss.Remove(m_spawnDataNode);
        }
    }
}