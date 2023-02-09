using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class TestMantis : EntityMonster
    {
        public int idleMinFrame = 30;
        public int idleMaxFrame = 50;
        public int walkMinFrame = 30;
        public int walkMaxFrame = 50;

        public float walkSpeed = 6.0f;

        public tMantisFsm fsm;

        protected override void Start()
        {
            base.Start();

            fsm = new tMantisFsm(this, 2);
            fsm.Start(0);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            fsm.OnFixedUpdate();
        }

        protected override void Update()
        {
            base.Update();
            fsm.OnUpdate();
        }
    }

    public class tMantisFsm : EntityFsm<TestMantis>
    {
        public const int c_st_IDLE = 0;
        public const int c_st_WALK = 1;

        public tMantisFsm(TestMantis _instance, int _capacity)
        : base(_instance, _capacity)
        {
            this[0] = new tMantisIdle(_instance);
            this[1] = new tMantisWalk(_instance);
        }
    }

    public abstract class tMantisState : MonsterState<TestMantis>
    {
        protected TestMantis tMantis => instance;
        protected tMantisFsm fsm => instance.fsm;

        private int m_leftFrame;

        public tMantisState(TestMantis _instance)
        : base(_instance)
        {

        }
    }

    public class tMantisIdle : tMantisState
    {
        private int m_leftFrame;

        public tMantisIdle(TestMantis _instance)
        : base(_instance)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            m_leftFrame = tMantis.prng.Next(tMantis.idleMinFrame, tMantis.idleMaxFrame + 1);

            tMantis.vm.FreezePosition(true, false);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            tMantis.vm.SetVelocityY(-1.0f);

            --m_leftFrame;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(m_leftFrame <= 0)
                return tMantisFsm.c_st_WALK;
                
            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }

    public class tMantisWalk : tMantisState
    {
        private int m_leftFrame;

        public tMantisWalk(TestMantis _instance)
        : base(_instance)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            tMantis.axisInput.x = tMantis.prng.Next(-100, 101) < 0 ? -1 : 1;
            m_leftFrame = tMantis.prng.Next(tMantis.walkMinFrame, tMantis.walkMaxFrame + 1);

            tMantis.vm.FreezePosition(false, false);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float ix = tMantis.axisInput.x;
            float vx = ix * tMantis.walkSpeed;
            float vy = -1.0f;

            tMantis.vm.SetVelocityXY(vx, vy);

            if(m_leftFrame > 0)
                --m_leftFrame;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(m_leftFrame <= 0)
                return tMantisFsm.c_st_IDLE;
            
            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}