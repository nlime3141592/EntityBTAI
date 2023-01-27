using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class PlayerMove : PlayerOnFloor
    {
        public PlayerMove(Player _player, int _id, string _name)
        : base(_player, _id, _name)
        {

        }

        protected override void p_OnStateBegin()
        {
            base.p_OnStateBegin();

            player.vm.MeltPositionX();
            player.vm.MeltPositionY();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            player.senseData.UpdateMoveDir(player);
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