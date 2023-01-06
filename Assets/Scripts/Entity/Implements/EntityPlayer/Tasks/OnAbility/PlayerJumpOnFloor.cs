using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerJumpOnFloor : PlayerJump
    {
        public Stat speedStatX;
        public Stat jumpGravityY;
        public Stat jumpSpeedStatY;
        private bool m_bCanceled;

        public PlayerJumpOnFloor(ConfigurationBT<EntityPlayer> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(config.curTask != this)
            {
                bool bHit = config.instance.terrainPage[EPlayerTerrainCheckResult.Floor].bHit;
                long msgCnt = config.instance.jumpBeginMessenger.Get(false);

                if(!bHit || msgCnt == 0)
                    return InvokeResult.Failure;
            }

            float ix = config.instance.axisInput.x;
            float vx = speedStatX.finalValue * ix;
            float vy = config.instance.velModule.GetVelocityY();

            if(1 == 0)
            {
                m_bCanceled = false;
                config.instance.FixConstraints(false, false);
                vy = jumpSpeedStatY.finalValue;
            }

            if(vy >= 0)
            {
                bool bOnJumpCancel = config.instance.jumpCancelMessenger.Get(false) > 0;

                if(!m_bCanceled && bOnJumpCancel)
                {
                    m_bCanceled = true;
                    vy /= 2;
                }
                else
                {
                    vy -= jumpGravityY.finalValue * Time.fixedDeltaTime;
                }

                config.curTask = this;
                config.instance.velModule.SetVelocityXY(vx, vy);

                if(vy < 0)
                {
                    config.curTask = null;
                    return InvokeResult.Success;
                }
                else
                {
                    return InvokeResult.Running;
                }
            }
            else
            {
                return InvokeResult.Failure;
            }
        }
    }
}