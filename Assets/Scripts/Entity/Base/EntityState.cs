using UnityEngine;

namespace UnchordMetroidvania
{
    public abstract class EntityState<T> : UnchordState<T>
    where T : EntityBase
    {
        public EntityState(T _instance, int _id, string _name)
        : base(_instance, _id, _name)
        {

        }

        public sealed override void OnStateBegin()
        {
            base.OnStateBegin();
            instance.aController.Reset();
            p_OnStateBegin();
            p_OnChangeAnimation();
        }

        protected virtual void p_OnStateBegin() {}

        protected virtual void p_OnChangeAnimation()
        {
            instance.aController.ChangeAnimation(base.id);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            instance.lookDir.x = m_GetNextLookDir(instance.axisInput.x, instance.lookDir.x, 1, instance.bFixLookDirX);
            instance.lookDir.y = m_GetNextLookDir(instance.axisInput.y, instance.lookDir.y, 1, instance.bFixLookDirY);

            m_Rotate01();
            // m_Rotate02();
        }

        public virtual void OnAnimationBegin() {}
        public virtual void OnActionBegin() {}
        public virtual void OnActionEnd() {}
        public virtual void OnAnimationEnd() {}

        private void m_Rotate01()
        {
            if(instance.lookDir.x < 0)
                instance.transform.eulerAngles = Vector3.up * 180;
            else
                instance.transform.eulerAngles = Vector3.zero;
        }

        private void m_Rotate02()
        {
            bool flipX = instance.lookDir.x < 0;
            // bool flipY = false;

            foreach(SpriteRenderer r in instance.spRenderers)
            {
                r.flipX = flipX;
                // r.flipY = flipY;
            }
        }

        private float m_GetNextLookDir(float input, float lCurrent, float lDefault, bool bFixed)
        {
            if(lCurrent != -1 && lCurrent != 1)
                return lDefault;
            else if(bFixed)
                return lCurrent;
            else if(input < 0)
                return -1;
            else if(input > 0)
                return 1;
            else
                return lCurrent;
        }
    }
}