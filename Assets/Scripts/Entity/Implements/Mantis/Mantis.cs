using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class Mantis : EntityMonster
    {
        public const int c_st_IDLE                       = 0;
        public const int c_st_WALK_FRONT                 = 1;
        public const int c_st_WALK_BACK                  = 2;
        public const int c_st_PHASE_SHOUT                = 3;
        public const int c_st_KNIFE_GRINDING             = 4;
        public const int c_st_UP_SLICE                   = 5;
        public const int c_st_BACK_SLICE                 = 6;
        public const int c_st_CHOP                       = 7;
        public const int c_st_JUMP_CHOP                  = 8;
        public const int c_st_GROGGY                     = 9;
        public const int c_st_DIE                        = 10;
        public const int c_st_SLEEP                      = 11;
        public const int c_st_COMBO_SHOUT                = 12;

        public Vector2 aiCenterOffset; // Inspector에서 값 할당 필요

        public MantisTerrainSensor senseData;
        public MantisStateRegion3_001 stateAi_001;
        public MantisStateRegion3_002 stateAi_002;

        private EntitySpawnData m_spawnData;
        private LinkedListNode<EntitySpawnData> m_spawnDataNode;

#region 아직 정리 안 함.
        public float wallDetectLength = 0.06f;
        public float hitLength = 0.06f;
        public float walkSpeed = 6.0f;
#endregion

        public virtual bool CanAggro()
        {
            return false;
        }

        protected override void OnAwakeEntity()
        {
            base.OnAwakeEntity();

            senseData = new MantisTerrainSensor();
            stateAi_001 = new MantisStateRegion3_001();
            stateAi_002 = new MantisStateRegion3_002();

            m_spawnData = new EntitySpawnData("사마귀", this);
            // m_spawnDataNode = new LinkedListNode<EntitySpawnData>(m_spawnData);
        }

        protected override void OnStartEntity()
        {
            base.OnStartEntity();

            volumeCollisions.Add(GetComponent<BoxCollider2D>());

            // GameManager.instance.generatedBoss.AddLast(m_spawnDataNode);
        }

        protected override IStateMachineBase InitStateMachine()
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
            machine.Add(new MantisJumpChop());
            machine.Add(new MantisGroggy());
            machine.Add(new MantisDie());

            machine.Begin(Mantis.c_st_SLEEP);
            return machine;
        }
/*
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
*/
        protected void OnDisable()
        {
            // GameManager.instance.generatedBoss.Remove(m_spawnDataNode);
        }
    }
}