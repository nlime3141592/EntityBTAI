using UnityEngine;

namespace UnchordMetroidvania
{
    public class EntityMove<T> : EntityTask<T>
    where T : EntityBase
    {
        private Stat m_moveSpeedStat;

        public EntityMove(ConfigurationBT<T> config, int id, string name)
        : base(config, id, name)
        {

        }

        public void SetSpeed(Stat moveSpeedStat)
        {
            m_moveSpeedStat = moveSpeedStat;
        }

        protected override InvokeResult p_Invoke()
        {
            p_Move(m_moveSpeedStat, config.instance.moveDir, 1.0f);
            return InvokeResult.Running;
        }

        protected void p_Move(Stat moveSpeedStat, Vector2 moveDir, float weight)
        {
            float dx = moveDir.x;
            float dy = moveDir.y;
            float velocity = moveSpeedStat.finalValue * weight;
            dx *= velocity;
            dy *= velocity;
            config.instance.vm.SetVelocityXY(dx, dy);
        }
    }
}