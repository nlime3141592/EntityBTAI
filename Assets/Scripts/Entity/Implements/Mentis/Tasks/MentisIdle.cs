using UnityEngine;

namespace UnchordMetroidvania
{
    public class MentisIdle : TaskNodeBT<Mentis>
    {
        public MentisIdle(ConfigurationBT<Mentis> config, int id, string name)
        : base(config, id, name)
        {
            
        }

        protected override InvokeResult p_Invoke()
        {
            config.instance.mantisAnimator.SetInteger("state", 0);
            config.instance.vm.SetVelocityXY(0, -1);
            Debug.Log("정지 중..");
            return InvokeResult.Success;
        }
    }
}