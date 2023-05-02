using UnityEngine;

namespace Unchord
{
    public class ExcavatorProjectileFlying : ExcavatorProjectileState
    {
        public override int idConstant => ExcavatorProjectile.c_st_FLYING;

        private int m_cntBounce;

        public override void OnStateBegin()
        {
            base.OnStateBegin();

            instance.vm.FreezePosition(false, false);
            instance.vm.SetVelocityXY(instance.initVelocity.x, instance.initVelocity.y);

            float px = instance.initPosition.x;
            float py = instance.initPosition.y;
            float pz = instance.transform.position.z;

            instance.transform.position = new Vector3(px, py, pz);

            m_cntBounce = instance.cntBounce;
        }

        public override void OnCollisionEnter2D(Collision2D _collider)
        {
            base.OnCollisionEnter2D(_collider);

            --m_cntBounce;
        }

        public override int Transit()
        {
            int transit = base.Transit();

            if(transit != MachineConstant.c_lt_PASS)
                return transit;
            else if(m_cntBounce <= 0)
                return ExcavatorProjectile.c_st_EXPLOSION;
            
            return MachineConstant.c_lt_PASS;
        }
    }
}