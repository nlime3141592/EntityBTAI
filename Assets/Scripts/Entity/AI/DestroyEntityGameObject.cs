using UnityEngine;

namespace UnchordMetroidvania
{
    public class DestroyEntityGameObject<T> : TaskNodeBT<T>
    where T : EntityBase
    {
        public DestroyEntityGameObject(T instance)
        : base(instance)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(instance.bEndOfEntity)
            {
                instance.OnEntityDestroy();
                GameObject.Destroy(instance.gameObject);
                return InvokeResult.Success;
            }
            else
            {
                return InvokeResult.Running;
            }
        }
    }
}