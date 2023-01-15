using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerAttackOnAir : PlayerAttack
    {
        public PlayerAttackOnAir(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override bool CanAttack()
        {
            bool canAttack = player.skAttackOnAir.cooltime <= 0;

            if(!canAttack)
            {
                int cur = player.skAttackOnAir.cooltime;
                // Debug.Log(string.Format("남은 쿨타임: {0}", cur));
            }

            return canAttack;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.bFixLookDirX = true;
            player.vm.FreezePositionX();
            player.vm.FreezePositionY();

            player.skAttackOnAir.cooltime = data.attackOnAir.cooltime;
            // player.battleModule.Reserve(player.skAttackOnAir, 1);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            player.vm.SetVelocityXY(0.0f, -1.0f);
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;

            // NOTE: 디버그용 상태 전환 코드.
            else if(Input.GetKeyDown(KeyCode.W))
                p_bEndOfAnimation = true;

            return false;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            player.bFixLookDirX = false;
            player.vm.MeltPositionX();
            player.vm.MeltPositionY();
        }
    }
}