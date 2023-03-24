using UnityEngine;

namespace Unchord
{
    public abstract class PlayerOnFloor : PlayerState
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();
            foreach(Slab slab in instance.sitSlabs)
            {
                slab.AcceptCollision(instance.hCol.head);
                slab.AcceptCollision(instance.hCol.body);
                slab.AcceptCollision(instance.hCol.feet);
            }
            instance.sitSlabs.Clear();
            instance.countLeft_JumpOnAir = instance.count_JumpOnAir;
            instance.countLeft_Dash = instance.count_Dash;
            instance.countLeft_AttackOnAir = instance.count_AttackOnAir;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.skill00)
                return Player.c_st_ATTACK_ON_FLOOR;
            else if(instance.skill01)
                return Player.c_st_ABILITY_SWORD;
            else if(instance.skill02)
                return Player.c_st_ABILITY_GUN;
            else if(instance.parryingDown)
                return Player.c_st_BASIC_PARRYING;
            else if(instance.rushDown)
                return Player.c_st_ROLL;
            else if(!instance.senseData.bOnFloor)
                return Player.c_st_FREE_FALL;

            return MachineConstant.c_lt_PASS;
        }
    }
}