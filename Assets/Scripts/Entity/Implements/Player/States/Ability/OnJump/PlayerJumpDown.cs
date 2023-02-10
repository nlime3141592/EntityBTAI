using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerJumpDown : PlayerJump
    {
        private float vy;

        private List<Slab> curSlabs;
        private List<Slab> prevSlabs;

        public LTRB m_sitRange;
        public LTRB m_slabRange;

        public PlayerJumpDown(Player _player)
        : base(_player)
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
            vy = data.jumpDownSpeed;
            player.sitSlabs.Clear();
            Collider2D[] colliders = OverlapBox(
                player.transform.position,
                player.lookDir.x < 0,
                false,
                m_sitRange,
                1 << LayerMask.NameToLayer("Slab"));
            player.sitSlabs.GetComponentsFromColliders<Slab>(player.transform, colliders, false);

            for(int i = 0; i < player.sitSlabs.Count; ++i)
            {
                player.sitSlabs[i].IgnoreCollision(player.hCol.head);
                player.sitSlabs[i].IgnoreCollision(player.hCol.body);
                player.sitSlabs[i].IgnoreCollision(player.hCol.feet);
            }
        }

        public override void OnFixedUpdateAlways()
        {
            base.OnFixedUpdateAlways();

            curSlabs.Clear();
            Collider2D[] colliders = OverlapBox(
                player.transform.position,
                player.lookDir.x < 0,
                false,
                m_slabRange,
                1 << LayerMask.NameToLayer("Slab"));
            curSlabs.GetComponentsFromColliders<Slab>(player.transform, colliders, false);

            for(int i = player.sitSlabs.Count - 1; i >= 0; --i)
            {
                if(curSlabs.Contains(player.sitSlabs[i]))
                    player.sitSlabs.RemoveAt(i);
            }

            for(int i = 0; i < curSlabs.Count; ++i)
            {
                if(!prevSlabs.Contains(curSlabs[i]))
                {
                    // curSlabs[i]가 이번 프레임에 인식됨.
                    curSlabs[i].IgnoreCollision(player.hCol.head);
                    curSlabs[i].IgnoreCollision(player.hCol.body);
                    curSlabs[i].IgnoreCollision(player.hCol.feet);
                }
            }
            for(int i = 0; i < prevSlabs.Count; ++i)
            {
                if(!curSlabs.Contains(prevSlabs[i]))
                {
                    // prevSlabs[i]가 이번 프레임에 나감.
                    prevSlabs[i].AcceptCollision(player.hCol.head);
                    prevSlabs[i].AcceptCollision(player.hCol.body);
                    prevSlabs[i].AcceptCollision(player.hCol.feet);
                }
            }

            List<Slab> tmp_slabs = curSlabs;
            curSlabs = prevSlabs;
            prevSlabs = tmp_slabs;
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            float vx = player.bIsRun ? data.runSpeed : data.walkSpeed;
            float ix = player.axisInput.x;

            player.moveDir.x = ix;
            player.moveDir.y = 0;
            vx *= ix;

            player.vm.SetVelocityXY(vx, vy);

            if(vy > 0)
                vy -= (data.jumpDownForce * Time.fixedDeltaTime);
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(vy <= 0)
                return PlayerFsm.c_st_FREE_FALL;
            
            return FiniteStateMachine.c_st_BASE_IGNORE;
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