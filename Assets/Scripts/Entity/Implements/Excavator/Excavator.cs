using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class Excavator : EntityMonster
    {
        public BattleModule battleModule;
        public BoxCollider2D terrainCollider; // Inspector에서 값 할당 필요
        public Transform aiCenter; // Inspector에서 값 할당 필요
        public Vector2 aiCenterOffset; // Inspector에서 값 할당 필요

        public ExcavatorArm arm;
        public ExcavatorHand hand;
        public GameObject armObj;

        public ExcavatorFsm fsm;
        public ExcavatorData data;
        public ExcavatorTerrainSenseData senseData;

        public int CURRENT_STATE;
        public int BOSS_PHASE;

        public Projectile projectile;
        public Vector2 projVelocity;

        public ShockWave shockwave;
        public LTRB shockRange;

        private EntitySpawnData m_spawnData;
        private LinkedListNode<EntitySpawnData> m_spawnDataNode;

        public virtual bool CanAggro()
        {
            return false;
        }

        protected override void Start()
        {
            base.Start();

            m_spawnData = new EntitySpawnData("굴착 기계", this);
            m_spawnDataNode = new LinkedListNode<EntitySpawnData>(m_spawnData);
            GameManager.instance.generatedBoss.AddLast(m_spawnDataNode);

            battleModule = GetComponent<BattleModule>();

            arm = GetComponentInChildren<ExcavatorArm>();
            hand = GetComponentInChildren<ExcavatorHand>();
            armObj.SetActive(false);

            fsm = new ExcavatorFsm(this, 13);
            fsm.Start(ExcavatorFsm.c_st_SLEEP);

            hitColliders.Add(GetComponent<BoxCollider2D>());
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            aiCenter.localPosition = aiCenterOffset;
            senseData.UpdateOrigins(this);
            fsm.OnFixedUpdate();
        }

        protected override void Update()
        {
            // NOTE: test input code
            float ixn = Input.GetKeyDown(KeyCode.J) ? -1 : 0;
            float ixp = Input.GetKeyDown(KeyCode.L) ? 1 : 0;
            float iyn = Input.GetKeyDown(KeyCode.I) ? -1 : 0;
            float iyp = Input.GetKeyDown(KeyCode.K) ? 1 : 0;
            float ix = ixn + ixp;
            float iy = iyn + iyp;
            axisInput.x = ix;
            axisInput.y = iy;

            base.Update();
            arm.yAngle = transform.eulerAngles.y;
            fsm.OnUpdate();
            CURRENT_STATE = fsm.Transit();
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