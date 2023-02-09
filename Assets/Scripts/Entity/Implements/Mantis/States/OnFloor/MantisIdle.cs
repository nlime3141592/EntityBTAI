using UnityEngine;

namespace UnchordMetroidvania
{
    public class MantisIdle : MantisOnFloor
    {
        // fixed data
        private int m_minIdleTime = 60;
        private int m_maxIdleTime = 120;
        private int m_ixDelayTime = 70; // NOTE: 좌, 우 회전을 자주 하면 안 되기 때문에 쿨타임을 소폭 부여함.
        private float m_rangeX1 = 8.0f; // NOTE: 상태 전이 구간 설정용 변수.
        private float m_rangeX2 = 16.0f;
        private float m_rangeY1 = 4.0f;
        private float m_rangeY2 = 8.0f;

        // variable
        private int m_leftIdleTime = 0;
        private int m_leftIxDelayTime = 0;
        private int m_rangeCode = -1;

        public MantisIdle(Mantis _mantis)
        : base(_mantis)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            mantis.vm.FreezePositionX();
            mantis.vm.MeltPositionY();

            m_leftIdleTime = mantis.prng.Next(m_minIdleTime, m_maxIdleTime + 1);
            m_leftIxDelayTime = mantis.prng.Next(m_ixDelayTime / 4, 1 + m_ixDelayTime / 2);
            mantis.lookDir.x = -mantis.lookDir.x;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            mantis.vm.SetVelocityY(-1.0f);

            if(m_leftIxDelayTime <= 0)
            {
                mantis.axisInput.x = mantis.GetAxisInputX();
                m_leftIxDelayTime = m_ixDelayTime;
            }

            if(m_leftIdleTime > 0)
                --m_leftIdleTime;
            if(m_leftIxDelayTime > 0)
                --m_leftIxDelayTime;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(!mantis.bAggro)
                return FiniteStateMachine.c_st_STATE_CONTINUE;
            else if(m_leftIdleTime <= 0)
            {
                EntityBase target = mantis.aggroTargets[0];
                m_rangeCode = m_GetRangeCode(target, mantis, m_rangeX1, m_rangeX2, m_rangeY1, m_rangeY2);

                if(fsm.mode == 1)
                    return m_ChangeStateByRangeCodePhase1(m_rangeCode);
                else if(fsm.mode == 2)
                    return m_ChangeStateByRangeCodePhase2(m_rangeCode);

                // NOTE: 테스트 코드.
                /*
                if(mantis.senseData.bOnWallFront)
                    fsm.Change(fsm.walkBack);
                else if(mantis.senseData.bOnWallBack)
                    fsm.Change(fsm.walkFront);
                else if(mantis.prng.Next(0, 100) < 50)
                    fsm.Change(fsm.walkFront);
                else
                    fsm.Change(fsm.walkBack);
                return true;
                */
            }

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            mantis.vm.MeltPositionX();
        }

        private int m_GetRangeCode(EntityBase target, Mantis mantis, float rx1, float rx2, float ry1, float ry2)
        {
            Vector2 tDir = target.transform.position - mantis.aiCenter.position;
            float dx = tDir.x * mantis.lookDir.x;
            float dy = tDir.y;
            int code = 0;

            if(dy < 0)
                return -1;
            else if(dx < 0)
                return 0;

            if(dx < rx1)
                code = 1;
            else if(dx < ry2)
                code = 2;
            else
                code = 3;

            if(dy < ry1)
                return code;
            else if(dy < ry2)
                return code + 3;
            else
                return code + 6;
        }

        private int m_ChangeStateByRangeCodePhase1(int code)
        {
            int rNumber = mantis.prng.Next(0, 10000);

            switch(code)
            {
                case 0:
                    if(rNumber < 2000)              return MantisFsm.c_st_WALK_FRONT;
                    else                            return MantisFsm.c_st_BACK_SLICE;

                case 1:
                    if(rNumber < 5000)              return MantisFsm.c_st_UP_SLICE;
                    else if(rNumber < 8000)         return MantisFsm.c_st_CHOP;
                    else                            return MantisFsm.c_st_WALK_BACK;

                case 2:
                    if(rNumber < 5000)              return MantisFsm.c_st_CHOP;
                    else if(rNumber < 7000)         return MantisFsm.c_st_UP_SLICE;
                    else if(rNumber < 9000)         return MantisFsm.c_st_WALK_BACK;
                    else                            return MantisFsm.c_st_WALK_FRONT;

                case 3:
                case 6:
                case 9:
                    if(rNumber < 7000)              return MantisFsm.c_st_WALK_FRONT;
                    else                            return MantisFsm.c_st_WALK_BACK;

                case 4:
                    if(rNumber < 7000)              return MantisFsm.c_st_UP_SLICE;
                    else if(rNumber < 8000)         return MantisFsm.c_st_CHOP;
                    else                            return MantisFsm.c_st_WALK_BACK;

                case 5:
                    if(rNumber < 4000)              return MantisFsm.c_st_CHOP;
                    else if(rNumber < 7000)         return MantisFsm.c_st_UP_SLICE;
                    else if(rNumber < 9000)         return MantisFsm.c_st_WALK_BACK;
                    else                            return MantisFsm.c_st_WALK_FRONT;

                case 7:
                    if(rNumber < 4000)              return MantisFsm.c_st_UP_SLICE;
                    else                            return MantisFsm.c_st_WALK_BACK;

                case 8:
                    if(rNumber < 5000)              return MantisFsm.c_st_UP_SLICE;
                    else                            return MantisFsm.c_st_WALK_BACK;

                default:
                    return FiniteStateMachine.c_st_MACHINE_HALT;
            }
        }

        private int m_ChangeStateByRangeCodePhase2(int code)
        {
            int rNumber = mantis.prng.Next(0, 10000);

            switch(code)
            {
                case 0:
                    if(rNumber < 2000)              return MantisFsm.c_st_WALK_FRONT;
                    else                            return MantisFsm.c_st_BACK_SLICE;

                case 1:
                    if(rNumber < 4000)              return MantisFsm.c_st_UP_SLICE;
                    else if(rNumber < 6000)         return MantisFsm.c_st_CHOP; // TODO: 포효콤보로 변경
                    else if(rNumber < 9000)         return MantisFsm.c_st_CHOP;
                    else                            return MantisFsm.c_st_WALK_BACK;

                case 2:
                    if(rNumber < 4000)              return MantisFsm.c_st_CHOP;
                    else if(rNumber < 6000)         return MantisFsm.c_st_CHOP; // TODO: 포효콤보로 변경
                    else if(rNumber < 8000)         return MantisFsm.c_st_UP_SLICE;
                    else if(rNumber < 9000)         return MantisFsm.c_st_WALK_BACK;
                    else                            return MantisFsm.c_st_WALK_FRONT;

                case 3:
                case 6:
                case 9:
                    if(rNumber < 3000)              return MantisFsm.c_st_WALK_FRONT;
                    else                            return MantisFsm.c_st_JUMP_CHOP;

                case 4:
                    if(rNumber < 5000)              return MantisFsm.c_st_UP_SLICE;
                    else if(rNumber < 7000)         return MantisFsm.c_st_CHOP; // TODO: 포효콤보로 변경
                    else if(rNumber < 8000)         return MantisFsm.c_st_CHOP;
                    else                            return MantisFsm.c_st_WALK_BACK;

                case 5:
                    if(rNumber < 3000)              return MantisFsm.c_st_CHOP;
                    else if(rNumber < 5000)         return MantisFsm.c_st_UP_SLICE; // TODO: 포효콤보로 변경
                    else if(rNumber < 8000)         return MantisFsm.c_st_UP_SLICE;
                    else if(rNumber < 9000)         return MantisFsm.c_st_WALK_BACK;
                    else                            return MantisFsm.c_st_WALK_FRONT;

                case 7:
                    if(rNumber < 4000)              return MantisFsm.c_st_UP_SLICE;
                    else                            return MantisFsm.c_st_WALK_BACK;

                case 8:
                    if(rNumber < 5000)              return MantisFsm.c_st_UP_SLICE;
                    else                            return MantisFsm.c_st_WALK_BACK;

                default:
                    return FiniteStateMachine.c_st_MACHINE_HALT;
            }
        }
    }
}