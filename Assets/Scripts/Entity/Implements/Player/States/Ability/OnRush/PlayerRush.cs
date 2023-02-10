namespace UnchordMetroidvania
{
    public abstract class PlayerRush : PlayerAbility
    {
        public PlayerRush(Player _player)
        : base(_player)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            player.bFixLookDirX = true;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();
            player.bFixLookDirX = false;
        }
    }
}