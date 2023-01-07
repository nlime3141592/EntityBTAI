namespace UnchordMetroidvania
{
    public class _PlayerJumpOnFloor : _PlayerJump
    {
        public _PlayerJumpOnFloor(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.vm.MeltPositionX();
            player.vm.MeltPositionY();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            player.vm.SetVelocityY(data.jumpOnFloorSpeed);
            player.fsm.Change(player.freeFall);
        }
    }
}