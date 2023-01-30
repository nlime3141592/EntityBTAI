namespace UnchordMetroidvania
{
    public abstract class PlayerState : EntityState<Player>
    {
        protected Player player => instance;
        protected PlayerData data => instance.data;
        protected PlayerFsm fsm => instance.fsm;

        public PlayerState(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.senseData.UpdateData(instance);
        }
    }
}