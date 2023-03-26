namespace Unchord
{
    public class PlayerIdleLong : PlayerIdle
    {
        public override void OnMachineBegin(Player _instance, int _id)
        {
            base.OnMachineBegin(_instance, _id);

            _instance.stateMap.Add(Player.c_st_IDLE_LONG, _id);
        }
    }
}