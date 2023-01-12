using UnityEngine;

namespace UnchordMetroidvania
{
    public class MentisIdle : TaskNodeBT<Mentis>
    {
        public MentisIdle(Mentis instance)
        : base(instance)
        {
            
        }

        protected override InvokeResult p_Invoke()
        {
            instance.mantisAnimator.SetInteger("state", 0);
            instance.vm.SetVelocityXY(0, -1);
            Debug.Log("정지 중..");
            return InvokeResult.Success;
        }
    }
}