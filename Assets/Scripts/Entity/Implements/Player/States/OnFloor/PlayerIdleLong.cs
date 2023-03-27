namespace Unchord
{
    public class PlayerIdleLong : PlayerIdle
    {
        public override void OnConstruct()
        {
            base.OnConstruct();

            idFixed = Player.c_st_IDLE_LONG;
        }
    }
}