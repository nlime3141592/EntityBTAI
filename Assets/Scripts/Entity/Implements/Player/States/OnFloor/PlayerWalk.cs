using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerWalk : PlayerMove
    {
        public PlayerWalk(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = player.axisInput.x * player.moveDir.x * data.walkSpeed;
            float vy = player.axisInput.x * player.moveDir.y * data.walkSpeed - 0.1f;

            player.vm.SetVelocityXY(vx, vy);
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.bIsRun)
            {
                fsm.Change(fsm.run);
                return true;
            }

            return false;
        }
    }
}