/*
using UnityEngine;
using UnchordMetroidvania;

public class DebugNode : NodeBT<DebugConfigurationBT>
{
    private string m_msg;

    public DebugNode(DebugConfigurationBT config, string msg)
    : base(config, -1, "DebugNode")
    {
        m_msg = msg;
    }

    public override InvokeResult Invoke()
    {
        Debug.Log(m_msg);
        return InvokeResult.SUCCESS;
    }
}

public class DebugConfigurationBT : ConfigurationBT
{
    
}
*/