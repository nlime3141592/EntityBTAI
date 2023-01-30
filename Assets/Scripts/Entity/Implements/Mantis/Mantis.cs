using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class Mantis : EntityMonster
    {
        public BattleModule battleModule;
        public BoxCollider2D terrainCollider; // 할당 필요

        public MantisFsm fsm;
        public MantisTerrainSenseData senseData;

        public bool bOnFloor;
        public bool bOnCeil;
        public bool bOnWallFront;
        public bool bOnWallBack;

        public int attackCount;

        public void PublishAttackCommand()
        {
            ++attackCount;
        }

        public bool CanReceiveAttackCommand()
        {
            if(attackCount > 0)
            {
                --attackCount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public MantisData data;

        private EntitySpawnData m_spawnData;
        private LinkedListNode<EntitySpawnData> m_spawnDataNode;

        public virtual bool CanAggro()
        {
            return false;
        }

        protected override void Start()
        {
            base.Start();

            m_spawnData = new EntitySpawnData("사마귀", this);
            m_spawnDataNode = new LinkedListNode<EntitySpawnData>(m_spawnData);
            GameManager.instance.generatedBoss.AddLast(m_spawnDataNode);

            aController = GetComponent<AnimationController>();
            battleModule = GetComponent<BattleModule>();

            fsm = new MantisFsm(this);

            aController.OnStart();
            fsm.Begin(fsm.idle);

            hitColliders.Add(GetComponent<BoxCollider2D>());
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            senseData.UpdateOrigins(this);
            fsm.OnFixedUpdate();
        }

        protected override void Update()
        {
            base.Update();
            fsm.OnUpdate();
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