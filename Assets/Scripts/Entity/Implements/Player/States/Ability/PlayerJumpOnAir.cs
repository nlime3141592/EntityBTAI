using UnityEngine;

namespace UnchordMetroidvania
{
    public class _PlayerJumpOnAir : _PlayerJump
    {
        private bool bJumpCanceled;
        private float vy;

        public _PlayerJumpOnAir(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.vm.MeltPositionX();
            player.vm.MeltPositionY();

            --player.leftAirJumpCount;
            bJumpCanceled = false;
            vy = data.jumpOnAirSpeed;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = player.bIsRun ? player.runSpeed : player.walkSpeed;
            float ix = player.axisInput.x;

            player.moveDir.x = ix;
            player.moveDir.y = 0;
            vx *= ix;

            if(vy <= 0)
            {
                p_bEndOfAbility = true;
                return;
            }

            player.vm.SetVelocityXY(vx, vy);
            vy -= (data.jumpOnAirForce * Time.fixedDeltaTime);
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;
            else if(player.bOnCeil)
            {
                player.fsm.Change(player.freeFall);
                return true;
            }
            else if(!bJumpCanceled && Input.GetKeyUp(KeyCode.Space))
            {
                bJumpCanceled = true;
                vy /= 2;
                return false;
            }
            else if(player.leftAirJumpCount > 0 && Input.GetKeyDown(KeyCode.Space))
            {
                player.fsm.Change(player.jumpOnAir);
                return true;
            }

            return false;
        }
    }
}