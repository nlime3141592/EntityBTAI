using UnityEngine;

namespace Unchord
{
    public class ExcavatorShootMissile : ExcavatorAttack, IBattleState
    {
        public override int idConstant => Excavator.c_st_SHOOT_MISSILE;

        private float m_ang_beg = -15.0f;
        private float m_ang_delta = 7.5f;
        private float m_ang_current;

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.bFixedLookDirByAxis.x = true;

            m_ang_current = m_ang_beg;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(instance.aController.bEndOfAnimation)
                return Excavator.c_st_IDLE;
            
            return MachineConstant.c_lt_PASS;
        }

        public void OnTriggerBattleState(BattleModule _btModule)
        {
            ExcavatorProjectile proj = GameObject.Instantiate<ExcavatorProjectile>(instance.projectile);
            float angle = m_ang_current;
            Vector2 finalVelocity = Quaternion.Euler(0, 0, angle) * instance.projectile.initVelocity;

            finalVelocity.x *= instance.lookDir.fx;
            m_ang_current += m_ang_delta;

            // TODO: 자기 자신에게 데미지를 입히면 안되므로, 전투 모듈에서 자기 자신에게 피해 옵션을 해제하는 것이 필요함.
            proj.initPosition = instance.transform.position;
            proj.initVelocity = finalVelocity;
            proj.bInstanceReady = true;
        }
    }
}