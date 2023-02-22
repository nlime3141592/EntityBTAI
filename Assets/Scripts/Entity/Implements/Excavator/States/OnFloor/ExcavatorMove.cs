namespace UnchordMetroidvania
{
    public abstract class ExcavatorMove : ExcavatorOnFloor
    {
        public ExcavatorMove(Excavator _instance)
        : base(_instance)
        {
            
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            excavator.vm.FreezePosition(false, false);

            excavator.bUpdateAggroDirX = false;
            excavator.bFixLookDirX = true;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            excavator.senseData.UpdateMoveDir(excavator);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(excavator.aController.bEndOfAnimation)
                return ExcavatorFsm.c_st_IDLE;
            // else if(excavator.senseData.bOnWallFront) return ExcavatorFsm.c_st_IDLE;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            excavator.bUpdateAggroDirX = true;
            excavator.bFixLookDirX = false;
        }
    }
}