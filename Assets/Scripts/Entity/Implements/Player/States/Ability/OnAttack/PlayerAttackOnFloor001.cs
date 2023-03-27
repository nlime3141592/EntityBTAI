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

            idFixed = Player.c_st_ATTACK_ON_FLOOR_001;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.stateNext_AttackOnFloor = Player.c_st_ATTACK_ON_FLOOR_002;
            --instance.countLeft_AttackOnAir;
        }
    }
}