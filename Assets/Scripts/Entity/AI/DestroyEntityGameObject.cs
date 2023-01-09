using UnityEngine;

namespace UnchordMetroidvania
{
    public class DestroyEntityGameObject<T> : TaskNodeBT<T>
    where T : EntityBase
    {
        public DestroyEntityGameObject(ConfigurationBT<T> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            if(config.instance.bEndOfEntity)
            {
                config.instance.OnEntityDestroy();
                GameObject.Destroy(config.instance.gameObject);
                return InvokeResult.Success;
            }
            else
            {
                return InvokeResult.Running;
            }
        }
    }
}