using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerOnWallFront : PlayerState
    {
        public PlayerOnWallFront(Player _player)
        : base(_player)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            foreach(Slab slab in player.sitSlabs)
            {
                slab.AcceptCollision(player.hCol.head);
                slab.AcceptCollision(player.hCol.body);
                slab.AcceptCollision(player.hCol.feet);
            }
            player.sitSlabs.Clear();
            player.leftAirJumpCount = data.maxAirJumpCount;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.jumpDown)
                return PlayerFsm.c_st_JUMP_ON_WALL_FRONT;
            else if(player.senseData.bOnDetectFloor)
                return PlayerFsm.c_st_FREE_FALL;
            else if(player.axisInput.y < 0 && player.axisInput.x == 0)
                return PlayerFsm.c_st_FREE_FALL;
            else if(!player.senseData.bOnWallFront)
                return PlayerFsm.c_st_FREE_FALL;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}