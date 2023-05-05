using UnityEngine;

namespace Unchord
{
    public class ExcavatorIdle : ExcavatorIdleBase
    {
        public override int idConstant => Excavator.c_st_IDLE;

        // fixed data
        private Timer m_idleTimer;
        private float m_rangeX1 = 10.5f;
        private float m_rangeX2 = 21.0f;
        private float m_rangeY1 = 4.0f;
        private float m_rangeY2 = 8.0f;

        // variable
        private int m_rangeCode = -1;

        protected override void OnConstruct()
        {
            base.OnConstruct();
            m_idleTimer = new Timer(1.2f);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            m_idleTimer.Reset();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.vm.SetVelocityXY(0.0f, -1.0f);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(!m_idleTimer.bEndOfTimer)
                m_idleTimer.OnUpdate();
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_idleTimer.bEndOfTimer)
            {
                Entity target = instance.aggroTargets[0];
                m_rangeCode = m_GetRangeCode(target, instance, m_rangeX1, m_rangeX2, m_rangeY1, m_rangeY2);

                if(instance.phase == 0)
                    return m_ChangeStateByRangeCodePhase1(m_rangeCode);
                else if(instance.phase == 1)
                    return m_ChangeStateByRangeCodePhase2(m_rangeCode);
                else if(instance.phase == 2)
                    return m_ChangeStateByRangeCodePhase3(m_rangeCode);
            }

            return MachineConstant.c_lt_PASS;
        }

        private int m_GetRangeCode(Entity target, Excavator excaavtor, float rx1, float rx2, float ry1, float ry2)
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

        private int m_ChangeStateByRangeCodePhase1(int code)
        {
            int rNumber = instance.prng.Next(0, 10000);

            switch(code)
            {
                case 0:
                    return MachineConstant.c_lt_CONTINUE;
                case 1:
                case 4:
                case 7:
                    return Excavator.c_st_STAMPING;
                case 2:
                case 9:
                    if(rNumber < 3000) return Excavator.c_st_WALK;
                    else return Excavator.c_st_ANCHORING;
                case 3:
                    if(rNumber < 5000) return Excavator.c_st_WALK;
                    else return Excavator.c_st_ANCHORING;
                case 5:
                case 8:
                    if(rNumber < 2000) return Excavator.c_st_WALK;
                    else return Excavator.c_st_ANCHORING;
                case 6:
                    if(rNumber < 4000) return Excavator.c_st_WALK;
                    else return Excavator.c_st_ANCHORING;
                default:
                    return MachineConstant.c_lt_END;
            }
        }

        private int m_ChangeStateByRangeCodePhase2(int code)
        {
            int rNumber = instance.prng.Next(0, 10000);

            switch(code)
            {
                case 0:
                    return MachineConstant.c_lt_CONTINUE;
                case 1:
                    if(rNumber < 6000) return Excavator.c_st_STAMPING;
                    else return Excavator.c_st_SHOCK_WAVE;
                case 2:
                    if(rNumber < 2000) return Excavator.c_st_WALK;
                    else if(rNumber < 6000) return Excavator.c_st_ANCHORING;
                    else return Excavator.c_st_SHOCK_WAVE;
                case 3:
                case 6:
                    if(rNumber < 3000) return Excavator.c_st_WALK;
                    else if(rNumber < 7000) return Excavator.c_st_ANCHORING;
                    else return Excavator.c_st_SHOCK_WAVE;
                case 4:
                    if(rNumber < 7000) return Excavator.c_st_STAMPING;
                    else return Excavator.c_st_SHOCK_WAVE;
                case 5:
                    if(rNumber < 2000) return Excavator.c_st_WALK;
                    else if(rNumber < 7000) return Excavator.c_st_ANCHORING;
                    else return Excavator.c_st_SHOCK_WAVE;
                case 7:
                    if(rNumber < 8000) return Excavator.c_st_STAMPING;
                    else return Excavator.c_st_SHOCK_WAVE;
                case 8:
                    if(rNumber < 2000) return Excavator.c_st_WALK;
                    else if(rNumber < 8000) return Excavator.c_st_ANCHORING;
                    else return Excavator.c_st_SHOCK_WAVE;
                case 9:
                    if(rNumber < 3000) return Excavator.c_st_WALK;
                    else if(rNumber < 8000) return Excavator.c_st_ANCHORING;
                    else return Excavator.c_st_SHOCK_WAVE;
                default:
                    return MachineConstant.c_lt_HALT;
            }
        }
        
        private int m_ChangeStateByRangeCodePhase3(int code)
        {
            int rNumber = instance.prng.Next(0, 10000);

            switch(code)
            {
                case 0:
                    return MachineConstant.c_lt_CONTINUE;
                case 1:
                    if(rNumber < 6000) return Excavator.c_st_STAMPING;
                    else if(rNumber < 9000) return Excavator.c_st_SHOCK_WAVE;
                    else return Excavator.c_st_SHOOT_MISSILE;
                case 2:
                case 5:
                    if(rNumber < 2000) return Excavator.c_st_WALK;
                    else if(rNumber < 5000) return Excavator.c_st_ANCHORING;
                    else if(rNumber < 8000) return Excavator.c_st_SHOCK_WAVE;
                    else return Excavator.c_st_SHOOT_MISSILE;
                case 3:
                    if(rNumber < 2000) return Excavator.c_st_WALK;
                    else if(rNumber < 4000) return Excavator.c_st_ANCHORING;
                    else if(rNumber < 7000) return Excavator.c_st_SHOCK_WAVE;
                    else return Excavator.c_st_SHOOT_MISSILE;
                case 4:
                    if(rNumber < 5000) return Excavator.c_st_WALK;
                    else if(rNumber < 8000) return Excavator.c_st_SHOCK_WAVE;
                    else return Excavator.c_st_SHOOT_MISSILE;
                case 6:
                    if(rNumber < 2000) return Excavator.c_st_WALK;
                    else if(rNumber < 5000) return Excavator.c_st_ANCHORING;
                    else if(rNumber < 7000) return Excavator.c_st_SHOCK_WAVE;
                    else return Excavator.c_st_SHOOT_MISSILE;
                case 7:
                    if(rNumber < 5000) return Excavator.c_st_WALK;
                    else if(rNumber < 7000) return Excavator.c_st_SHOCK_WAVE;
                    else return Excavator.c_st_SHOOT_MISSILE;
                case 8:
                    if(rNumber < 2000) return Excavator.c_st_WALK;
                    else if(rNumber < 6000) return Excavator.c_st_ANCHORING;
                    else if(rNumber < 8000) return Excavator.c_st_SHOCK_WAVE;
                    else return Excavator.c_st_SHOOT_MISSILE;
                case 9:
                    if(rNumber < 2000) return Excavator.c_st_WALK;
                    else if(rNumber < 6000) return Excavator.c_st_ANCHORING;
                    else if(rNumber < 7000) return Excavator.c_st_SHOCK_WAVE;
                    else return Excavator.c_st_SHOOT_MISSILE;
                default:
                    return MachineConstant.c_lt_HALT;
            }
        }
    }
}