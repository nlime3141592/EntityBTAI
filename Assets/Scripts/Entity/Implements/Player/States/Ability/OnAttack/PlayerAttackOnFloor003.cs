namespace Unchord
{
    public class PlayerAttackOnFloor003 : PlayerAttackOnFloorBase
    {
        public override void OnConstruct()
        {
            base.OnConstruct();

            base.baseDamage = 1.25f;
            base.speed_Step = 2.0f;
            base.coyote = 2.0f;

            idFixed = Player.c_st_ATTACK_ON_FLOOR_003;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.stateNext_AttackOnFloor = Player.c_st_ATTACK_ON_FLOOR_001;
        }
    }
}