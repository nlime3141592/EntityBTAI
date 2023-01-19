using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerParrying : _PlayerAbility
    {
        public PlayerParrying(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            // player.SetMoveDirOnFloor();
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.parryingUp || p_bEndOfAnimation)
            {
                fsm.Change(fsm.idleShort);
                return true;
            }

            return false;
        }
    }
}