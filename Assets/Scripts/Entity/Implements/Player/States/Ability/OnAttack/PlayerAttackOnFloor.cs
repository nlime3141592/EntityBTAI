using System;
using UnityEngine;

namespace UnchordMetroidvania
{
    [Serializable]
    public class PlayerAttackOnFloor : PlayerAttack
    {
        public int continuousAttackCount
        {
            get => m_continuousAttackCount;
            set
            {
                if(value < 1)
                    m_continuousAttackCount = 1;
                else
                    m_continuousAttackCount = value;
            }
        }

        private float m_memoryTime = 2.0f;
        private int m_continuousAttackCount = 1;
        private int m_attackPhase = 0;
        private bool m_bCanUpdate;
        private float m_continuousTime;
        private bool m_bGoNextPhase;

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
            player.bFixLookDirX = true;
            player.vm.FreezePositionX();

            player.skAttackOnFloor.cooltime = data.attackOnFloor.cooltime;
            player.battleModule.Reserve(player.skAttackOnFloor, 1);

            m_bCanUpdate = false;
            m_bGoNextPhase = false;
            if(m_attackPhase < m_continuousAttackCount)
                ++m_attackPhase;
            else
                m_attackPhase = 1;

            player.pAnimator.SetInteger("actionPhase", m_attackPhase);

            base.OnStateBegin();
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
            else if(player.rushDown)
            {
                player.fsm.Change(player.roll);
                return true;
            }
            else if(p_bEndOfAction && m_bGoNextPhase)
            {
                player.fsm.Replay();
                return true;
            }
            else if(player.skill00)
            {
                m_bGoNextPhase = true;
            }

            return false;
        }

        public override void OnActionEnd()
        {
            base.OnActionEnd();
            m_continuousTime = m_memoryTime;
            m_bCanUpdate = true;
        }

        public void UpdateContinuous()
        {
            if(m_bCanUpdate)
            {
                m_continuousTime -= Time.deltaTime;

                if(m_continuousTime <= 0)
                {
                    m_continuousTime = 0;
                    m_attackPhase = 0;
                }
            }
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            player.bFixLookDirX = false;
            player.vm.MeltPositionX();

            if(!m_bCanUpdate)
            {
                m_bCanUpdate = true;
                m_continuousTime = m_memoryTime;
            }

            player.pAnimator.SetInteger("actionPhase", 0);
        }
    }
}