namespace UnchordMetroidvania
{
    public abstract class PlayerRush : PlayerAbility
    {
        public PlayerRush(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();

            player.bFixLookDirX = true;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            player.bFixLookDirX = false;
        }
    }
}