namespace Unchord
{
    public class PlayerTakeDown002 : PlayerTakeDownBase
    {
        public override void OnConstruct()
        {
            base.OnConstruct();

            idFixed = Player.c_st_TAKE_DOWN_002;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePosition(false, false);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            instance.vm.SetVelocityY(-instance.speed_TakeDown);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.senseData.datFloor.bOnHit)
                return Player.c_st_TAKE_DOWN_003;

            return MachineConstant.c_lt_PASS;
        }
    }
}