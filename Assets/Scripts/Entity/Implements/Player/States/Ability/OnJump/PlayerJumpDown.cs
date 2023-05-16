using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class PlayerJumpDown : PlayerJump
    {
        private float vy;

        // public LTRB m_sitRange;
        // public LTRB m_slabRange;

        public override int idConstant => Player.c_st_JUMP_DOWN;
/*
        protected override void OnConstruct()
        {
            m_sitRange = new LTRB()
            {
                left = 0.6f,
                top = -1.63f,
                right = 0.6f,
                bottom = 2.23f
            };
            m_slabRange = new LTRB()
            {
                left = 0.6f,
                top = 2.5f,
                right = 0.6f,
                bottom = 1.87f
            };
        }
*/
        public override void OnStateBegin()
        {
            base.OnStateBegin();

            vy = instance.speed_JumpDown;

            instance.downJumpedSlabs.Clear();

            for(int i = 0; i < instance.sitSlabs.Count; ++i)
                instance.downJumpedSlabs.Add(instance.sitSlabs[i]);

            instance.sitSlabs.Clear();
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = instance.bIsRun ? instance.speed_Run : instance.speed_Walk;
            float ix = instance.iManager.ix;

            instance.moveDir.x = ix;
            instance.moveDir.y = 0;
            vx *= ix;

            instance.vm.SetVelocityXY(vx, vy);

            if(vy > 0)
                vy -= (instance.force_JumpDown * Time.fixedDeltaTime);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(vy <= 0)
                return Player.c_st_FREE_FALL;

            return MachineConstant.c_lt_PASS;
        }
    }
}