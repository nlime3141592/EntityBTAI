namespace Unchord
{
    public class PlayerAttackOnFloor002 : PlayerAttackOnFloorBase
    {
        public override void OnConstruct()
        {
            base.OnConstruct();

            base.baseDamage = 1.1f;
            base.speed_Step = 0.8f;
            base.coyote = 2.0f;

            idFixed = Player.c_st_ATTACK_ON_FLOOR_002;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.stateNext_AttackOnFloor = Player.c_st_ATTACK_ON_FLOOR_003;
        }
    }
}