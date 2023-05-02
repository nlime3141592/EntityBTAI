using UnityEngine;

namespace Unchord
{
    public class MantisIdle : MantisOnFloor
    {
        public override int idConstant => Mantis.c_st_IDLE;

        // fixed data
        private float m_rangeX1 = 8.0f; // NOTE: 상태 전이 구간 설정용 변수.
        private float m_rangeX2 = 16.0f;
        private float m_rangeY1 = 4.0f;
        private float m_rangeY2 = 8.0f;

        // variable
        private int m_leftIdleTime = 0;
        private int m_rangeCode = -1;

        private int m_leftIdleAggroDelay;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePositionX();
            instance.vm.MeltPositionY();

            m_leftIdleTime = instance.prng.Next(instance.frame_idleTimeMin, instance.frame_idleTimeMax + 1);
            m_leftIdleAggroDelay = instance.prng.Next(instance.frame_idleAggroDelay / 4, instance.frame_idleAggroDelay / 2);
            // instance.lookDir.x = (Direction)(-(int)(instance.lookDir.x));
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.vm.SetVelocityY(-1.0f);

            if(m_leftIdleAggroDelay == 0)
            {
                instance.lookDir.x = m_GetLookDirX();
                m_leftIdleAggroDelay = instance.frame_idleAggroDelay;
            }
            else
                --m_leftIdleAggroDelay;

            if(m_leftIdleTime > 0)
                --m_leftIdleTime;
        }

        private Direction m_GetLookDirX()
        {
            if(!instance.bAggro)
                return instance.lookDir.x;

            float tx = instance.aggroTargets[0].transform.position.x;
            float px = instance.transform.position.x;

            if(tx - px < 0)
                return Direction.Negative;
            else
                return Direction.Positive;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(!instance.bAggro)
                return MachineConstant.c_lt_CONTINUE;
            else if(m_leftIdleTime <= 0)
            {
                Entity target = instance.aggroTargets[0];
                m_rangeCode = m_GetRangeCode(target, instance, m_rangeX1, m_rangeX2, m_rangeY1, m_rangeY2);

                if(instance.monsterPhase == 1)
                    return m_ChangeStateByRangeCodePhase1(m_rangeCode);
                else if(instance.monsterPhase == 2)
                    return m_ChangeStateByRangeCodePhase2(m_rangeCode);

                // NOTE: 테스트 코드.
                /*
                if(instance.senseData.bOnWallFront)
                    fsm.Change(fsm.walkBack);
                else if(instance.senseData.bOnWallBack)
                    fsm.Change(fsm.walkFront);
                else if(instance.prng.Next(0, 100) < 50)
                    fsm.Change(fsm.walkFront);
                else
                    fsm.Change(fsm.walkBack);
                return true;
                */
            }

            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            instance.vm.MeltPositionX();
        }

        private int m_GetRangeCode(Entity target, Mantis mantis, float rx1, float rx2, float ry1, float ry2)
        {
            Vector2 tDir = target.transform.position - instance.aiCenter.position;
            float dx = tDir.x * instance.lookDir.fx;
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

        // TODO: xmlx 파일 입출력으로 변경하기.
        private int m_ChangeStateByRangeCodePhase1(int code)
        {
            int rNumber = instance.prng.Next(0, 10000);

            switch(code)
            {
                case 0:
                    if(rNumber < 2000)              return Mantis.c_st_WALK_FRONT;
                    else                            return Mantis.c_st_BACK_SLICE;

                case 1:
                    if(rNumber < 5000)              return Mantis.c_st_UP_SLICE;
                    else if(rNumber < 8000)         return Mantis.c_st_CHOP;
                    else                            return Mantis.c_st_WALK_BACK;

                case 2:
                    if(rNumber < 5000)              return Mantis.c_st_CHOP;
                    else if(rNumber < 7000)         return Mantis.c_st_UP_SLICE;
                    else if(rNumber < 9000)         return Mantis.c_st_WALK_BACK;
                    else                            return Mantis.c_st_WALK_FRONT;

                case 3:
                case 6:
                case 9:
                    if(rNumber < 7000)              return Mantis.c_st_WALK_FRONT;
                    else                            return Mantis.c_st_WALK_BACK;

                case 4:
                    if(rNumber < 7000)              return Mantis.c_st_UP_SLICE;
                    else if(rNumber < 8000)         return Mantis.c_st_CHOP;
                    else                            return Mantis.c_st_WALK_BACK;

                case 5:
                    if(rNumber < 4000)              return Mantis.c_st_CHOP;
                    else if(rNumber < 7000)         return Mantis.c_st_UP_SLICE;
                    else if(rNumber < 9000)         return Mantis.c_st_WALK_BACK;
                    else                            return Mantis.c_st_WALK_FRONT;

                case 7:
                    if(rNumber < 4000)              return Mantis.c_st_UP_SLICE;
                    else                            return Mantis.c_st_WALK_BACK;

                case 8:
                    if(rNumber < 5000)              return Mantis.c_st_UP_SLICE;
                    else                            return Mantis.c_st_WALK_BACK;

                default:
                    return MachineConstant.c_lt_HALT;
            }
        }

        // TODO: xmlx 파일 입출력으로 변경하기.
        private int m_ChangeStateByRangeCodePhase2(int code)
        {
            int rNumber = instance.prng.Next(0, 10000);

            switch(code)
            {
                case 0:
                    if(rNumber < 2000)              return Mantis.c_st_WALK_FRONT;
                    else                            return Mantis.c_st_BACK_SLICE;

                case 1:
                    if(rNumber < 4000)              return Mantis.c_st_UP_SLICE;
                    else if(rNumber < 6000)         return Mantis.c_st_CHOP; // TODO: 포효콤보로 변경
                    else if(rNumber < 9000)         return Mantis.c_st_CHOP;
                    else                            return Mantis.c_st_WALK_BACK;
  
                case 2:
                    if(rNumber < 4000)              return Mantis.c_st_CHOP;
                    else if(rNumber < 6000)         return Mantis.c_st_CHOP; // TODO: 포효콤보로 변경
                    else if(rNumber < 8000)         return Mantis.c_st_UP_SLICE;
                    else if(rNumber < 9000)         return Mantis.c_st_WALK_BACK;
                    else                            return Mantis.c_st_WALK_FRONT;

                case 3:
                case 6:
                case 9:
                    if(rNumber < 3000)              return Mantis.c_st_WALK_FRONT;
                    else                            return Mantis.c_st_JUMP_CHOP;

                case 4:
                    if(rNumber < 5000)              return Mantis.c_st_UP_SLICE;
                    else if(rNumber < 7000)         return Mantis.c_st_CHOP; // TODO: 포효콤보로 변경
                    else if(rNumber < 8000)         return Mantis.c_st_CHOP;
                    else                            return Mantis.c_st_WALK_BACK;

                case 5:
                    if(rNumber < 3000)              return Mantis.c_st_CHOP;
                    else if(rNumber < 5000)         return Mantis.c_st_UP_SLICE; // TODO: 포효콤보로 변경
                    else if(rNumber < 8000)         return Mantis.c_st_UP_SLICE;
                    else if(rNumber < 9000)         return Mantis.c_st_WALK_BACK;
                    else                            return Mantis.c_st_WALK_FRONT;

                case 7:
                    if(rNumber < 4000)              return Mantis.c_st_UP_SLICE;
                    else                            return Mantis.c_st_WALK_BACK;

                case 8:
                    if(rNumber < 5000)              return Mantis.c_st_UP_SLICE;
                    else                            return Mantis.c_st_WALK_BACK;

                default:
                    return MachineConstant.c_lt_HALT;
            }
        }
    }
}