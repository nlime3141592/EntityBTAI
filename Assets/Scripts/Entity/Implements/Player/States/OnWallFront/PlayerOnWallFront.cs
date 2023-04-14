using UnityEngine;

namespace Unchord
{
    public abstract class PlayerOnWallFront : PlayerState
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
            else if(instance.jumpDown)
                return Player.c_st_JUMP_ON_WALL_FRONT;
            else if(instance.senseData.datFloor.bOnDetected)
                return Player.c_st_FREE_FALL;
            else if(instance.axis.y < 0 && instance.axis.x == 0)
                return Player.c_st_FREE_FALL;
            else if(!instance.senseData.bOnWallFront)
                return Player.c_st_FREE_FALL;

            return MachineConstant.c_lt_PASS;
        }
    }
}