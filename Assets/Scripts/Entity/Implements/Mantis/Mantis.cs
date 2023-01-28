using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class Mantis : EntityMonster
    {
        public BattleModule battleModule;
        public BoxCollider2D terrainCollider; // 할당 필요
        public Transform originFloorL;
        public Transform originFloorR;
        public Transform originCeilL;
        public Transform originCeilR;
        public Transform originWallLT;
        public Transform originWallRT;
        public Transform originWallLB;
        public Transform originWallRB;

        public MantisIdle idle;
        public MantisUpSlice upSlice;
        public MantisFSM fsm;
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
        public MonsterBaseAI<Mantis> ai;

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
            ai = new MonsterBaseAI<Mantis>(this);

            idle = new MantisIdle(this);
            upSlice = new MantisUpSlice(this);

            fsm = new MantisFSM(this);
            fsm[0] = idle;
            fsm[1] = upSlice;

            aController.OnStart();

            ai.Set(fsm);

            hitColliders.Add(GetComponent<BoxCollider2D>());
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            m_FixedUpdateOrigins();
            ai.Invoke();
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

        private void m_FixedUpdateOrigins()
        {
            Bounds box = terrainCollider.bounds;
            float minX = box.min.x;
            float minY = box.min.y;
            float maxX = box.max.x;
            float maxY = box.max.y;
            float y10 = minY + (maxY - minY) * 0.1f;
            float y90 = maxY - (maxY - minY) * 0.1f;

            originFloorL.position = new Vector2(minX, minY);
            originFloorR.position = new Vector2(maxX, minY);
            originCeilL.position = new Vector2(minX, maxY);
            originCeilR.position = new Vector2(maxX, maxY);
            originWallLT.position = new Vector2(minX, y90);
            originWallRT.position = new Vector2(maxX, y90);
            originWallLB.position = new Vector2(minX, y10);
            originWallRB.position = new Vector2(maxX, y10);
        }

        protected void OnDisable()
        {
            GameManager.instance.generatedBoss.Remove(m_spawnDataNode);
        }
    }
}