using System.Collections.Generic;

namespace Unchord
{
    public class PlayerTakeDown003 : PlayerTakeDownBase, ISkillEvent
    {
        private bool m_bCapturedParringDown;

        public override int idConstant => Player.c_st_TAKE_DOWN_003;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePosition(true, false);

            m_bCapturedParringDown = false;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            instance.vm.SetVelocityY(-1.0f);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(!instance.bEndOfAction)
                return;

            if(!instance.iManager.parryingDown)
                m_bCapturedParringDown = true;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_bCapturedParringDown)
                return Player.c_st_EMERGENCY_PARRYING;
            else if(instance.bEndOfAnimation)
                return Player.c_st_IDLE_SHORT;

            return MachineConstant.c_lt_PASS;
        }

        public override void OnStateEnd()
        {
            base.OnStateEnd();

            instance.vm.FreezePosition(false, false);
        }

        public void OnSkill(SkillModule _skModule)
        {
            List<Entity> targets = _skModule
                .Reset()
                .SenseColliders(instance.skillRange_TakeDown003_01)
                .GetTargets();

            instance.skillRange_TakeDown003_01.DebugSensor(UnityEngine.Color.magenta, 2.0f);

            foreach(Entity victim in targets)
                _skModule.TakeDamage(victim, 1.0f);
        }
    }
}