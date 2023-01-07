namespace UnchordMetroidvania
{
    public abstract class _PlayerOnAir : PlayerState
    {
        public _PlayerOnAir(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.bOnFloor)
            {
                if(player.axisInput.y > 0)
                    player.fsm.Change(player.headUp);
                else if(player.axisInput.y < 0)
                    player.fsm.Change(player.sit);
                else if(player.axisInput.x == 0)
                    player.fsm.Change(player.idleShort);
                else if(player.bIsRun)
                    player.fsm.Change(player.run);
                else
                    player.fsm.Change(player.walk);

                return true;
            }
            else if(player.bOnDetectFloor)
            {
                return false;
            }
            else if(player.bOnLedge)
            {
                return true;
            }
            else if(player.bOnWallFront)
            {
                if(player.axisInput.x != 0)
                {
                    player.fsm.Change(player.idleWallFront);
                    return true;
                }
            }

            return false;
        }
    }
}