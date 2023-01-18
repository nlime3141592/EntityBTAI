using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerDash : _PlayerAbility
    {
        public PlayerDash(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if(fsm.nextFps >= data.dashFrame)
            {
                if(!p_bEndOfAbility)
                    p_bEndOfAbility = true;
                return;
            }
            else if(fsm.bOnWallFrontB || fsm.bOnWallFrontT)
            {
                if(!p_bEndOfAbility)
                    p_bEndOfAbility = true;
                return;
            }

            player.moveDir.x = 1;
            player.moveDir.y = 0;

            float vx = player.lookDir.x * player.moveDir.x * data.dashSpeed;
            float vy = 0;

            player.vm.SetVelocityXY(vx, vy);
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(fsm.leftAirJumpCount > 0 && player.jumpDown)
            {
                fsm.Change(fsm.jumpOnAir);
                return true;
            }

            return false;
        }
    }
}
