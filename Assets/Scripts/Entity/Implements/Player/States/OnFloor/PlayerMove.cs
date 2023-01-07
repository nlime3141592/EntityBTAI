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
            player.vm.MeltPositionX();
            player.vm.MeltPositionY();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            RaycastHit2D terrain = Physics2D.Raycast(player.originFloor.position, Vector2.down, 0.5f, 1 << LayerMask.NameToLayer("Terrain"));
            player.moveDir.x = 1.0f;
            player.moveDir.y = -terrain.normal.x / terrain.normal.y;
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.axisInput.y > 0)
            {
                player.fsm.Change(player.headUp);
                return true;
            }
            else if(player.axisInput.y < 0)
            {
                player.fsm.Change(player.sit);
                return true;
            }
            else if(player.axisInput.x == 0)
            {
                player.fsm.Change(player.idleShort);
                return true;
            }

            return false;
        }
    }
}