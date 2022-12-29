using System.Collections.Generic;
using UnityEngine;
using UnchordMetroidvania;
using System.Text;

public class Debugger : MonoBehaviour
{
    public static Queue<string> messager;
    private static StringBuilder builder;

    static Debugger()
    {
        messager = new Queue<string>(2);
        builder = new StringBuilder();
    }

    public static void FixedUpdate()
    {
        while(messager.Count > 0)
            builder.AppendFormat("{0}\n", messager.Dequeue());

        Debug.Log(builder.ToString());
        builder.Clear();
    }
}
