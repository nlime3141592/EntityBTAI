using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class TestMantis : EntityMonster
    {
        public const int c_st_IDLE = 0;
        public const int c_st_WALK = 1;

        public int idleMinFrame = 30;
        public int idleMaxFrame = 50;
        public int walkMinFrame = 30;
        public int walkMaxFrame = 50;

        public float walkSpeed = 6.0f;

        public float axisX;

        public StateMachine<TestMantis> fsm;
        public Dictionary<int, int> m_stateMap;

        protected override void InitComponents()
        {
            base.InitComponents();

            fsm = new StateMachine<TestMantis>(2);
            m_stateMap = new Dictionary<int, int>(2);

            CompositeState<TestMantis> tree = new CompositeState<TestMantis>(2);
            tree[0] = new tMantisIdle();
            tree[1] = new tMantisWalk();

            fsm.RegisterStateMap(m_stateMap);
            fsm.Begin(this, tree, 0);
            base.RegisterMachineEvent(fsm);
        }
    }

    public abstract class tMantisState : MonsterState<TestMantis>
    {

    }

    public class tMantisIdle : tMantisState
    {
        private int m_leftFrame;

        public override void OnMachineBegin(TestMantis _instance, int _id)
        {
            instance.m_stateMap.Add(TestMantis.c_st_IDLE, id);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            m_leftFrame = instance.prng.Next(instance.idleMinFrame, instance.idleMaxFrame + 1);

            instance.vm.FreezePosition(true, false);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            instance.vm.SetVelocityY(-1.0f);

            --m_leftFrame;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_leftFrame <= 0)
                return TestMantis.c_st_WALK;
                
            return MachineConstant.c_lt_PASS;
        }
    }

    public class tMantisWalk : tMantisState
    {
        private int m_leftFrame;

        public override void OnMachineBegin(TestMantis _instance, int _id)
        {
            instance.m_stateMap.Add(TestMantis.c_st_WALK, id);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.axisX = instance.prng.Next(-100, 101) < 0 ? -1 : 1;
            m_leftFrame = instance.prng.Next(instance.walkMinFrame, instance.walkMaxFrame + 1);

            instance.vm.FreezePosition(false, false);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float ix = instance.axisX;
            float vx = ix * instance.walkSpeed;
            float vy = -1.0f;

            instance.vm.SetVelocityXY(vx, vy);

            if(m_leftFrame > 0)
                --m_leftFrame;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_leftFrame <= 0)
                return TestMantis.c_st_IDLE;
            
            return MachineConstant.c_lt_PASS;
        }
    }
}