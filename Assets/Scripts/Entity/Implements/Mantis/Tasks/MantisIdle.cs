using UnityEngine;

namespace UnchordMetroidvania
{
    public class MantisIdle : TaskNodeBT<Mantis>
    {
        public MantisIdle(Mantis instance)
        : base(instance)
        {
            
        }

        protected override InvokeResult p_Invoke()
        {
            instance.aController.ChangeAnimation(0);
            instance.vm.SetVelocityXY(0, -1);
            Debug.Log("정지 중..");
            return InvokeResult.Success;
        }
    }
}