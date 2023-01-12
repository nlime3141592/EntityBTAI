using UnityEngine;

namespace UnchordMetroidvania
{
    public class MentisUpSlice : TaskNodeBT<Mentis>
    {
        public MentisUpSlice(Mentis instance)
        : base(instance)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            instance.bFixLookDirX = true;
            instance.mantisAnimator.SetInteger("state", 1);

            if(instance.CanReceiveAttackCommand())
            {
                instance.battleModule.UseBattleSkill(
                    instance.skUpSlice
                );
                return InvokeResult.Running;
            }

            InvokeResult iResult = instance.animationResult;

            if(iResult == InvokeResult.Success)
            {
                instance.mantisAnimator.SetInteger("state", 0);
                instance.animationResult = InvokeResult.Running;
                instance.bFixLookDirX = false;
            }

            return iResult;
        }

        public override void ResetNode()
        {
            base.ResetNode();
            instance.bFixLookDirX = false;
        }
    }
}