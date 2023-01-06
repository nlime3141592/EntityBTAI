using UnityEngine;

namespace UnchordMetroidvania
{
    public class Player : EntityBase
    {
        public ElongatedHexagonCollider2D hCol;
        public Transform originFloor;
        public Transform originCeil;
        public Transform originWallLT;
        public Transform originWallRT;
        public Transform originWallLB;
        public Transform originWallRB;
        public Transform originLedgeLT;
        public Transform originLedgeRT;
        public Transform originLedgeLB;
        public Transform originLedgeRB;

        public TerrainSensor sensor;
        public VelocityModule2D vm;

        public PlayerData data;
        public PlayerIdle idleLong;
        public PlayerIdleShort idleShort;

        public _PlayerFSM fsm;

        protected override void Start()
        {
            hCol = GetComponent<ElongatedHexagonCollider2D>();
            sensor = new TerrainSensor();
            vm = new VelocityModule2D(GetComponent<Rigidbody2D>());

            int state = -1;
            data = new PlayerData();
            idleLong = new PlayerIdle(this, data, ++state, "IdleLong");
            idleShort = new PlayerIdleShort(this, data, ++state, "IdleShort");

            fsm = new _PlayerFSM();

            fsm.Begin(idleShort);
        }

        protected override void p_Debug_OnPostInvoke()
        {
            vm.FixedUpdate(axisInput.x);
            
            m_FixedUpdateOrigins();
            vm.SetVelocityXY(base.axisInput.x * 3.0f, base.axisInput.y * 3.0f);

            sensor.CheckFloor(originFloor.transform.position, 0.04f);
            sensor.CheckCeil(originCeil.transform.position, 0.04f);

            if(vm.lookDirX > 0)
            {
                sensor.CheckWallFront(originWallRT.transform.position, 0.06f, vm.lookDirX);
                sensor.CheckWallFront(originWallRB.transform.position, 0.06f, vm.lookDirX);
                sensor.CheckWallBack(originWallLT.transform.position, 0.06f, vm.lookDirX);
                sensor.CheckWallBack(originWallLB.transform.position, 0.06f, vm.lookDirX);

                sensor.CheckLedgeHorizontal(originLedgeRT.transform.position, 0.5f, vm.lookDirX);
                sensor.CheckLedgeVerticalDown(originLedgeRT.transform.position + Vector3.right * 0.1f, 0.3f);
            }
            else if(vm.lookDirX < 0)
            {
                sensor.CheckWallBack(originWallRT.transform.position, 0.06f, vm.lookDirX);
                sensor.CheckWallBack(originWallRB.transform.position, 0.06f, vm.lookDirX);
                sensor.CheckWallFront(originWallLT.transform.position, 0.06f, vm.lookDirX);
                sensor.CheckWallFront(originWallLB.transform.position, 0.06f, vm.lookDirX);

                sensor.CheckLedgeHorizontal(originLedgeLT.transform.position, 0.5f, vm.lookDirX);
                sensor.CheckLedgeVerticalDown(originLedgeLT.transform.position - Vector3.right * 0.1f, 0.3f);
            }

            fsm.OnFixedUpdate();

            Debug.Log(string.Format("CurrentState: {0}", fsm.state));
        }

        protected override void Update()
        {
            base.Update();

            base.axisInput.x = Input.GetAxisRaw("Horizontal");
            base.axisInput.y = Input.GetAxisRaw("Vertical");

            fsm.OnUpdate();
        }

        private void m_FixedUpdateOrigins()
        {
            Bounds head = hCol.head.bounds;
            Bounds feet = hCol.feet.bounds;

            originFloor.position = new Vector2(feet.center.x, feet.min.y);
            originCeil.position = new Vector2(head.center.x, head.max.y);

            originWallLT.position = new Vector2(head.min.x, head.center.y);
            originWallRT.position = new Vector2(head.max.x, head.center.y);
            originWallLB.position = new Vector2(feet.min.x, feet.center.y);
            originWallRB.position = new Vector2(feet.max.x, feet.center.y);

            originLedgeLT.position = new Vector2(head.min.x, head.max.y);
            originLedgeRT.position = new Vector2(head.max.x, head.max.y);
            originLedgeLB.position = new Vector2(feet.min.x, feet.min.y);
            originLedgeRB.position = new Vector2(feet.max.x, feet.min.y);
        }
    }
}