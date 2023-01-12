using UnityEngine;

namespace UnchordMetroidvania
{
    public class MonsterDebugNode<T> : DecoratorNodeBT<T>
    where T : EntityMonster
    {
        public string message;

        public MonsterDebugNode(T instance)
        : base(instance)
        {

        }

        protected override InvokeResult p_Invoke()
        {
            InvokeResult iResult = children[0]?.Invoke() ?? InvokeResult.Success;
            Debug.Log(message);
            return iResult;
        }
    }
}