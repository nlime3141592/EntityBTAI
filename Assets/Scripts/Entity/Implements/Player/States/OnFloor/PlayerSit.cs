using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerSit : PlayerStand
    {
        public PlayerSit(Player _player)
        : base(_player)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            player.cameraOffset = Vector2.down;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.jumpDown)
                return PlayerFsm.c_st_JUMP_DOWN;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}