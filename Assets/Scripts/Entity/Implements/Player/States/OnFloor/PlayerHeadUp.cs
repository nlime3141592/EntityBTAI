using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerHeadUp : PlayerStand
    {
        public PlayerHeadUp(Player _player)
        : base(_player)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            player.cameraOffset = Vector2.up;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.jumpDown)
                return PlayerFsm.c_st_JUMP_ON_FLOOR;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}