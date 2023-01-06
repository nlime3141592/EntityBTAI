namespace UnchordMetroidvania
{
    public abstract class PlayerState
    {
        protected _PlayerFSM fsm => player.fsm;

        protected readonly Player player;
        protected readonly PlayerData data;
        public readonly int id;
        public readonly string name;

        public PlayerState(Player player, PlayerData data, int id, string name)
        {
            this.player = player;
            this.data = data;
            this.id = id;
            this.name = name;
        }

        public virtual void OnStateBegin()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnFixedUpdate()
        {

        }

        public virtual void OnStateEnd()
        {

        }
    }
}