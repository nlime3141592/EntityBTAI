using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerAttackOnFloor : PlayerAttack
    {
        public PlayerAttackOnFloor(Player player, PlayerData data, int id, string name)
        : base(player, data, id, name)
        {

        }

        public override bool CanAttack()
        {
            bool canAttack = player.skAttackOnFloor.cooltime <= 0;

            if(!canAttack)
            {
                int cur = player.skAttackOnFloor.cooltime;
                // Debug.Log(string.Format("남은 쿨타임: {0}", cur));
            }

            return canAttack;
        }

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            player.bFixLookDirX = true;
            player.vm.FreezePositionX();

            player.skAttackOnFloor.cooltime = data.attackOnFloor.cooltime;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            player.vm.SetVelocityXY(0.0f, -1.0f);

            if(player.CanReceiveAttackCommand())
            {
                player.battleModule.UseBattleSkill(player.skAttackOnFloor);
            }
        }

        public override bool OnUpdate()
        {
            if(base.OnUpdate())
                return true;

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