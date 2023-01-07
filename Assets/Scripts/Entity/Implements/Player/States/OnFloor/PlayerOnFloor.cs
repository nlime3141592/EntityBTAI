using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class _PlayerOnFloor : PlayerState
    {
        public _PlayerOnFloor(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.bJumpedOnFloor) // NOTE: 테스트 조건문, InputHandler를 만들 필요가 있음.
            {
                player.fsm.Change(player.jumpOnFloor);
                return true;
            }
            else if(!player.bOnFloor)
            {
                player.fsm.Change(player.freeFall);
                return true;
            }

            return false;
        }
    }
}