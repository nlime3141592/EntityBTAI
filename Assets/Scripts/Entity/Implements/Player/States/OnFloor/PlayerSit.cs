using UnityEngine;

namespace Unchord
{
    public class PlayerSit : PlayerStand
    {
        private bool m_bOnSlab;
        private Slab m_slab;

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            instance.offset_StandCamera = Vector2.down;

            RaycastHit2D hit = Physics2D.Raycast(instance.senseData.originFloor.position, Vector2.down, 0.1f, 1 << LayerMask.NameToLayer("Slab"));
            if(hit)
                m_bOnSlab = hit.collider.gameObject.TryGetComponent<Slab>(out m_slab);
            else
                m_bOnSlab = false;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_bOnSlab && instance.jumpDown)
                return Player.c_st_JUMP_DOWN;

            return MachineConstant.c_lt_PASS;
        }
    }
}