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
            else if(instance.iManager.active000)
                return instance.stateNext_AttackOnFloor;
            else if(instance.iManager.parryingDown)
                return Player.c_st_BASIC_PARRYING;
            else if(instance.iManager.rushDown)
                return Player.c_st_ROLL;
            else if(!instance.senseData.datFloor.bOnHit)
                return Player.c_st_FREE_FALL;

            return MachineConstant.c_lt_PASS;
        }
    }
}