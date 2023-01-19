using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerMove : _PlayerOnFloor
    {
        public PlayerMove(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.vm.MeltPositionX();
            player.vm.MeltPositionY();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            player.SetMoveDirOnFloor();
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.axisInput.y > 0)
            {
                fsm.Change(fsm.headUp);
                return true;
            }
            else if(player.axisInput.y < 0)
            {
                fsm.Change(fsm.sit);
                return true;
            }
            else if(player.axisInput.x == 0)
            {
                fsm.Change(fsm.idleShort);
                return true;
            }

            return false;
        }
    }
}