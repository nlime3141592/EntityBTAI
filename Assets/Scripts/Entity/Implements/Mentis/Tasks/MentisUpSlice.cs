using UnityEngine;

namespace UnchordMetroidvania
{
    public class MentisUpSlice : TaskNodeBT<Mentis>
    {
        public MentisUpSlice(ConfigurationBT<Mentis> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            config.instance.bFixLookDirX = true;
            config.instance.mantisAnimator.SetInteger("state", 1);

            if(config.instance.CanReceiveAttackCommand())
            {
                config.instance.battleModule.UseBattleSkill(
                    config.instance.skUpSlice
                );
                return InvokeResult.Running;
            }

            InvokeResult iResult = config.instance.animationResult;

            if(iResult == InvokeResult.Success)
            {
                config.instance.mantisAnimator.SetInteger("state", 0);
                config.instance.animationResult = InvokeResult.Running;
                config.instance.bFixLookDirX = false;
            }

            return iResult;
        }

        public override void ResetNode()
        {
            base.ResetNode();
            config.instance.bFixLookDirX = false;
        }
    }
}