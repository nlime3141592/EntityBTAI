namespace UnchordMetroidvania
{
    public abstract class PlayerRush : _PlayerAbility
    {
        public PlayerRush(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
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