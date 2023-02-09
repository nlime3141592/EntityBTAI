using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerRoll : PlayerRush
    {
        private int m_frame = 45;
        private int m_leftFrame;

        public PlayerRoll(Player _player)
        : base(_player)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            m_leftFrame = m_frame;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            RaycastHit2D terrain = Physics2D.Raycast(player.senseData.originFloor.position, Vector2.down, 0.5f, 1 << LayerMask.NameToLayer("Terrain"));

            if(m_leftFrame > 0)
                --m_leftFrame;
            else if(!terrain || player.senseData.bOnWallFrontB || player.senseData.bOnWallFrontT)
            {
                if(!p_bEndOfAbility)
                    p_bEndOfAbility = true;
                return;
            }

            player.moveDir.x = 1.0f;

            if(terrain.normal.y == 0)
                player.moveDir.y = 0;
            else
                player.moveDir.y = -terrain.normal.x / terrain.normal.y;

            float vx = player.lookDir.x * player.moveDir.x * data.rollSpeed;
            float vy = player.lookDir.x * player.moveDir.y * data.rollSpeed;
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

            player.vm.SetVelocityXY(vx, vy);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(player.aController.bEndOfAnimation)
                return PlayerFsm.c_st_IDLE_SHORT;
            else if(player.aController.bEndOfAction && player.parryingDown)
                return PlayerFsm.c_st_EMERGENCY_PARRYING;
            else if(player.jumpDown)
                return PlayerFsm.c_st_JUMP_ON_FLOOR;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}