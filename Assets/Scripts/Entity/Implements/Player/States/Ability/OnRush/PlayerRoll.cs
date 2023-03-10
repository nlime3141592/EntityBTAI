using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerRoll : PlayerRush
    {
        public PlayerRoll(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            RaycastHit2D terrain = Physics2D.Raycast(player.senseData.originFloor.position, Vector2.down, 0.5f, 1 << LayerMask.NameToLayer("Terrain"));

            if(fsm.nextFixedFrameNumber >= data.rollFrame)
            {
                if(!p_bEndOfAbility)
                    p_bEndOfAbility = true;
                return;
            }
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

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.aController.bEndOfAnimation)
            {
                fsm.Change(fsm.idleShort);
                return true;
            }
            else if(player.aController.bEndOfAction && player.parryingDown)
            {
                fsm.Change(fsm.emergencyParrying);
                return true;
            }
            else if(player.jumpDown)
            {
                fsm.Change(fsm.jumpOnFloor);
                return true;
            }

            return false;
        }
    }
}