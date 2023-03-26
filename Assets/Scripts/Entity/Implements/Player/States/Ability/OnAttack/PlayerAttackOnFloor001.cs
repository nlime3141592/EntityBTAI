namespace Unchord
{
    public class PlayerAttackOnFloor001 : PlayerAttackOnFloorBase
    {
        public override void OnConstruct()
        {
            base.OnConstruct();

            base.baseDamage = 1.0f;
            base.speed_Step = 1.5f;
            base.coyote = 2.0f;
        }

        public override void OnMachineBegin(Player _instance, int _id)
        {
            base.OnMachineBegin(_instance, _id);

            _instance.stateMap.Add(Player.c_st_ATTACK_ON_FLOOR_001, _id);
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.stateNext_AttackOnFloor = Player.c_st_ATTACK_ON_FLOOR_002;
            --instance.countLeft_AttackOnAir;
        }
    }
}