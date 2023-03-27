using UnityEngine;

namespace Unchord
{
    public class PlayerRoll : PlayerRush
    {
        private bool m_bParryingDown;

        public override void OnConstruct()
        {
            base.OnConstruct();

            idFixed = Player.c_st_ROLL;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            m_bParryingDown = false;

            float ix = instance.axis.x;
            if(ix < 0) instance.lookDir.x = Direction.Negative;
            else if(ix > 0) instance.lookDir.x = Direction.Positive;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            RaycastHit2D terrain = Physics2D.Raycast(instance.senseData.originFloor.position, Vector2.down, 0.5f, 1 << LayerMask.NameToLayer("Terrain"));

            instance.moveDir.x = 1.0f;

            if(terrain.normal.y == 0)
                instance.moveDir.y = 0;
            else
                instance.moveDir.y = -terrain.normal.x / terrain.normal.y;

            float vx = instance.lookDir.fx * instance.moveDir.x * instance.speed_Roll;
            float vy = instance.lookDir.fx * instance.moveDir.y * instance.speed_Roll;
            float addVelocityRatio = 0.25f;

            if(vy < 0)
                vy *= (1 + addVelocityRatio);
            else if(vy > 0)
                vy *= (1 - addVelocityRatio);
            else if(vx < 0)
                vy += vx;
            else if(vx > 0)
                vy -= vx;
            else
                vy -= 1.0f;

            instance.vm.SetVelocityXY(vx, vy);
        }

        public override void OnUpdateAlways()
        {
            base.OnUpdateAlways();
            if(instance.parryingDown)
                m_bParryingDown = true;
            if(instance.aController.bEndOfAction)
            {
                // TODO: 이 블럭 안에 패링 입력을 받을지 고민해보기.
            }
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return Player.c_st_IDLE_SHORT;
            else if(m_bParryingDown)
                return Player.c_st_EMERGENCY_PARRYING;
            else if(instance.jumpDown)
                return Player.c_st_JUMP_ON_FLOOR;

            return MachineConstant.c_lt_PASS;
        }
    }
}