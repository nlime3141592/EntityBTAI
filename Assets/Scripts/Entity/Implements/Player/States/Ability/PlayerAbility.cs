namespace UnchordMetroidvania
{
    public abstract class PlayerAbility : PlayerState
    {
        public PlayerAbility(Player _player)
        : base(_player)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
        }

    }
}