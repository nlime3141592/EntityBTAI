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

            fsm = new tMantisFsm(this);
            fsm.Begin(fsm.idle);
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

    public class tMantisFsm : UnchordFsm<TestMantis>
    {
        public tMantisIdle idle;
        public tMantisWalk walk;

        public tMantisFsm(TestMantis instance)
        : base(instance, -1, "tMantisFsm")
        {
            idle = new tMantisIdle(instance, 0, "tMantisIdle");
            walk = new tMantisWalk(instance, 1, "tMantisWalk");
        }
    }

    public abstract class tMantisState : MonsterState<TestMantis>
    {
        protected TestMantis tMantis => instance;
        protected tMantisFsm fsm => instance.fsm;

        private int m_leftFrame;

        public tMantisState(TestMantis instance, int id, string name)
        : base(instance, id, name)
        {

        }
    }

    public class tMantisIdle : tMantisState
    {
        private int m_leftFrame;

        public tMantisIdle(TestMantis instance, int id, string name)
        : base(instance, id, name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();
            m_leftFrame = tMantis.prng.Next(tMantis.idleMinFrame, tMantis.idleMaxFrame + 1);

            tMantis.vm.FreezePosition(true, false);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            tMantis.vm.SetVelocityY(-1.0f);

            --m_leftFrame;
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(m_leftFrame <= 0)
            {
                fsm.Change(fsm.walk);
                return true;
            }
            return false;
        }
    }

    public class tMantisWalk : tMantisState
    {
        private int m_leftFrame;

        public tMantisWalk(TestMantis instance, int id, string name)
        : base(instance, id, name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();
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

            --m_leftFrame;
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(m_leftFrame <= 0)
            {
                fsm.Change(fsm.idle);
                return true;
            }
            return false;
        }
    }
}