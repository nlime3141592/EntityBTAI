namespace UnchordMetroidvania
{
    public abstract class _PlayerOnFloor : PlayerState
    {
        public _PlayerOnFloor(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(!player.bOnFloor)
            {
                player.fsm.Change(player.freeFall);
                return true;
            }

            return false;
        }
    }
}