/*
using UnityEngine;

namespace UnchordMetroidvania
{
    public class SandBagHard : SandBagState
    {
        private string m_name = "Hard";

        public SandBagHard(SandBag instance, AnimationController aController)
        : base(instance, aController)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            Debug.Log(string.Format("{0}: 상태 시작", m_name));
        }

        public override void OnAnimationBegin() => Debug.Log(string.Format("{0}: 애니메이션 시작", m_name));
        public override void OnActionBegin() => Debug.Log(string.Format("{0}: 동작 시작", m_name));
        public override void OnActionEnd() => Debug.Log(string.Format("{0}: 동작 종료", m_name));
        public override void OnAnimationEnd() => Debug.Log(string.Format("{0}: 애니메이션 종료", m_name));
        public override void OnStateEnd() => Debug.Log(string.Format("{0}: 상태 종료", m_name));

        public override InvokeResult OnUpdate()
        {
            if(base.OnUpdate() == InvokeResult.Success)
                return InvokeResult.Success;
            else if(!instance.value)
            {
                instance.currentState = instance.stateSoft;
                return InvokeResult.Success;
            }

            return InvokeResult.Failure;
        }
    }

    public class SandBagSoft : SandBagState
    {
        private string m_name = "Soft";

        public SandBagSoft(SandBag instance, AnimationController aController)
        : base(instance, aController)
        {

        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            Debug.Log(string.Format("{0}: 상태 시작", m_name));
        }

        public override void OnAnimationBegin() => Debug.Log(string.Format("{0}: 애니메이션 시작", m_name));
        public override void OnActionBegin() => Debug.Log(string.Format("{0}: 동작 시작", m_name));
        public override void OnActionEnd() => Debug.Log(string.Format("{0}: 동작 종료", m_name));
        public override void OnAnimationEnd() => Debug.Log(string.Format("{0}: 애니메이션 종료", m_name));
        public override void OnStateEnd() => Debug.Log(string.Format("{0}: 상태 종료", m_name));

        public override InvokeResult OnUpdate()
        {
            Debug.Log("Update");
            if(base.OnUpdate() == InvokeResult.Success)
                return InvokeResult.Success;
            else if(instance.value)
            {
                instance.currentState = instance.stateHard;
                return InvokeResult.Success;
            }

            Debug.Log("F");
            return InvokeResult.Failure;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            Debug.Log("FixedUpdate");
        }
    }
}
*/