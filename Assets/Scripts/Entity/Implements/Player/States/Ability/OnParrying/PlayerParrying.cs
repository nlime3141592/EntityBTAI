using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerParrying : PlayerAbility
    {
        public PlayerParrying(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            // player.senseData.UpdateMoveDir(player);
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.parryingUp || player.aController.bEndOfAnimation)
            {
                fsm.Change(fsm.idleShort);
                return true;
            }

            return false;
        }
    }
}