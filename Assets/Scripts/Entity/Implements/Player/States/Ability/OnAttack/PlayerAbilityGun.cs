using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerAbilityGun : PlayerAttack
    {
        public PlayerAbilityGun(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override bool CanAttack()
        {
            bool canAttack = player.skAbilityGun.cooltime <= 0;

            if(!canAttack)
            {
                int cur = player.skAbilityGun.cooltime;
                // Debug.Log(string.Format("남은 쿨타임: {0}", cur));
            }

            return canAttack;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.bFixLookDirX = true;
            player.vm.FreezePositionX();

            player.skAbilityGun.cooltime = data.abilityGun.cooltime;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            player.vm.SetVelocityXY(0.0f, -1.0f);

            if(player.CanReceiveAttackCommand())
            {
                player.battleModule.UseBattleSkill(player.skAbilityGun);
            }
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;

            // NOTE: 디버그용 상태 전환 코드.
            else if(Input.GetKeyDown(KeyCode.Q))
                player.PublishAttackCommand();
            else if(Input.GetKeyDown(KeyCode.W))
                p_bEndOfAnimation = true;

            return false;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            player.bFixLookDirX = false;
            player.vm.MeltPositionX();
        }
    }
}