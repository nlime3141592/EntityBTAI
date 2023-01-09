using UnityEngine;

namespace UnchordMetroidvania
{
    public class MonsterDebugNode<T> : DecoratorNodeBT<T>
    where T : EntityMonster
    {
        public string message;

        public MonsterDebugNode(ConfigurationBT<T> config, int id, string name)
        : base(config, id, name)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            InvokeResult iResult = child?.Invoke() ?? InvokeResult.Success;
            Debug.Log(message);
            return iResult;
        }
    }
}