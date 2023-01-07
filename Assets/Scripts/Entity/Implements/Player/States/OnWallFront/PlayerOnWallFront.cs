namespace UnchordMetroidvania
{
    public abstract class PlayerOnWallFront : PlayerState
    {
        public PlayerOnWallFront(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.bOnDetectFloor)
            {
                if(player.axisInput.y > 0)
                    player.fsm.Change(player.gliding);
                else
                    player.fsm.Change(player.freeFall);
                return true;
            }
            else if(player.axisInput.y < 0 && player.axisInput.x == 0)
            {
                player.fsm.Change(player.freeFall);
                return true;
            }
            else if(!player.bOnWallFront)
            {
                if(player.axisInput.y > 0)
                    player.fsm.Change(player.gliding);
                else
                    player.fsm.Change(player.freeFall);
                return true;
            }

            return false;
        }
    }
}