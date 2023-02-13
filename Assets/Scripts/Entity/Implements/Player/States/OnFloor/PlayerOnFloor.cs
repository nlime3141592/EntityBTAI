using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerOnFloor : PlayerState
    {
        public PlayerOnFloor(Player _player)
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
            player.leftAirJumpCount = player.data.maxAirJumpCount;
            player.leftDashCount = data.maxDashCount;
            player.leftAirAttackCount = data.maxAirAttackCount;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.skill00)
                return PlayerFsm.c_st_ATTACK_ON_FLOOR;
            else if(player.skill01)
                return PlayerFsm.c_st_ABILITY_SWORD;
            else if(player.skill02)
                return PlayerFsm.c_st_ABILITY_GUN;
            else if(player.parryingDown)
                return PlayerFsm.c_st_BASIC_PARRYING;
            else if(player.rushDown)
                return PlayerFsm.c_st_ROLL;
            else if(!player.senseData.bOnFloor)
                return PlayerFsm.c_st_FREE_FALL;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}