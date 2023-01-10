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
                Debug.Log("공격 성공");
                return InvokeResult.Success;
            }
            else
            {
                Debug.Log("공격 대기 중..");
                return InvokeResult.Running;
            }
        }

        public override void ResetNode()
        {
            base.ResetNode();
            Debug.Log("사마귀 초기화");
            config.instance.bFixLookDirX = false;
        }
    }
}