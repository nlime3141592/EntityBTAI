using UnityEngine;

namespace Unchord
{
    public abstract class PlayerOnFloor : PlayerState
    {
        public override void OnStateBegin()
        {
            base.OnStateBegin();
/*
            // NOTE: sitSlabs를 여기서 초기화하는데 slab의 overlap 판정 갱신은 물리 프레임이므로, 이 곳에서 즉시 물리 충돌 활성화하는 코드가 필요할 수도 있음.
            // 필요한 경우, 이 코드를 활성화하면 됨.
            foreach(Slab slab in instance.sitSlabs)
            {
                slab.AcceptCollision(instance.hCol.head);
                slab.AcceptCollision(instance.hCol.body);
                slab.AcceptCollision(instance.hCol.feet);
            }
*/
            instance.sitSlabs.Clear();
            instance.downJumpedSlabs.Clear();
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
            else if(!bCanLandOnFloor())
                return Player.c_st_FREE_FALL;

            return MachineConstant.c_lt_PASS;
        }
    }
}