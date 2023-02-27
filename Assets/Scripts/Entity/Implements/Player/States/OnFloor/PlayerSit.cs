using UnityEngine;

namespace UnchordMetroidvania
{
    public class PlayerSit : PlayerStand
    {
        private bool m_bOnSlab;
        private Slab m_slab;

        public PlayerSit(Player _player)
        : base(_player)
        {

        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            player.cameraOffset = Vector2.down;

            RaycastHit2D hit = Physics2D.Raycast(player.senseData.originFloor.position, Vector2.down, 0.1f, 1 << LayerMask.NameToLayer("Slab"));
            if(hit)
                m_bOnSlab = hit.collider.gameObject.TryGetComponent<Slab>(out m_slab);
            else
                m_bOnSlab = false;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != FiniteStateMachine.c_st_BASE_IGNORE)
                return transit;
            else if(m_bOnSlab && player.jumpDown)
                return PlayerFsm.c_st_JUMP_DOWN;

            return FiniteStateMachine.c_st_BASE_IGNORE;
        }
    }
}