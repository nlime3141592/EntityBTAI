namespace Unchord
{
    // NOTE: 도약찍기 제 2상태, 점프 하강 동작
    public class MantisJumpChop_002 : MantisAttack
    {
        public override int idConstant => Mantis.c_st_JUMP_CHOP_002;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.SetVelocityXY(0.0f, -0.1f);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            
                return Mantis.c_st_IDLE;

            // return MachineConstant.c_lt_PASS;
        }
    }
}