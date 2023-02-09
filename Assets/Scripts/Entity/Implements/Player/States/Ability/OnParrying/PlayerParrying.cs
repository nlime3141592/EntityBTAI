using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerParrying : PlayerAbility
    {
        public PlayerParrying(Player _player)
        : base(_player)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            // player.senseData.UpdateMoveDir(player);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.parryingUp || player.aController.bEndOfAnimation)
                return PlayerFsm.c_st_IDLE_SHORT;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}