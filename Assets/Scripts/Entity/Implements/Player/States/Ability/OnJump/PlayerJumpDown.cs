using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class PlayerJumpDown : PlayerJump
    {
        private float vy;

        private List<Slab> curSlabs;
        private List<Slab> prevSlabs;

        public LTRB m_sitRange;
        public LTRB m_slabRange;

        public override int idConstant => Player.c_st_JUMP_DOWN;

        protected override void OnConstruct()
        {
            curSlabs = new List<Slab>(4);
            prevSlabs = new List<Slab>(4);

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

        public override void OnStateBegin()
        {
            base.OnStateBegin();
            vy = instance.speed_JumpDown;
            instance.sitSlabs.Clear();
            Collider2D[] colliders = OverlapBox(
                instance.transform.position,
                instance.lookDir.x < 0,
                false,
                m_sitRange,
                1 << LayerMask.NameToLayer("Slab"));
            instance.sitSlabs.GetComponentsFromColliders<Slab>(instance.transform, colliders, false);

            for(int i = 0; i < instance.sitSlabs.Count; ++i)
            {
                instance.sitSlabs[i].IgnoreCollision(instance.hCol.head);
                instance.sitSlabs[i].IgnoreCollision(instance.hCol.body);
                instance.sitSlabs[i].IgnoreCollision(instance.hCol.feet);
            }
        }

        public override void OnFixedUpdateAlways()
        {
            base.OnFixedUpdateAlways();

            curSlabs.Clear();
            Collider2D[] colliders = OverlapBox(
                instance.transform.position,
                instance.lookDir.x < 0,
                false,
                m_slabRange,
                1 << LayerMask.NameToLayer("Slab"));
            curSlabs.GetComponentsFromColliders<Slab>(instance.transform, colliders, false);

            for(int i = instance.sitSlabs.Count - 1; i >= 0; --i)
            {
                if(curSlabs.Contains(instance.sitSlabs[i]))
                    instance.sitSlabs.RemoveAt(i);
            }

            for(int i = 0; i < curSlabs.Count; ++i)
            {
                if(!prevSlabs.Contains(curSlabs[i]))
                {
                    // curSlabs[i]가 이번 프레임에 인식됨.
                    curSlabs[i].IgnoreCollision(instance.hCol.head);
                    curSlabs[i].IgnoreCollision(instance.hCol.body);
                    curSlabs[i].IgnoreCollision(instance.hCol.feet);
                }
            }
            for(int i = 0; i < prevSlabs.Count; ++i)
            {
                if(!curSlabs.Contains(prevSlabs[i]))
                {
                    // prevSlabs[i]가 이번 프레임에 나감.
                    prevSlabs[i].AcceptCollision(instance.hCol.head);
                    prevSlabs[i].AcceptCollision(instance.hCol.body);
                    prevSlabs[i].AcceptCollision(instance.hCol.feet);
                }
            }

            List<Slab> tmp_slabs = curSlabs;
            curSlabs = prevSlabs;
            prevSlabs = tmp_slabs;
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

        private static Collider2D[] OverlapBox(Vector2 origin, bool flipX, bool flipY, LTRB range, int layerMask)
        {
            Vector2 beg = origin;
            Vector2 end = beg;

            float lx = flipX ? -1 : 1;
            float ly = flipY ? -1 : 1;
            
            beg -= new Vector2(lx * range.left, ly * range.bottom);
            end += new Vector2(lx * range.right, ly * range.top);

            Collider2D[] colliders = Physics2D.OverlapAreaAll(beg, end, layerMask);
            return colliders;
        }
    }
}