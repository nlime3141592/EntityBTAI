using System.Collections.Generic;
using UnityEngine;

namespace UnchordMetroidvania
{
    public class GroundCamera : MonoBehaviour
    {
        public Vector2 center;

        void Update()
        {
            Vector3 pos = MainCamera.instance.transform.position;
            transform.position = pos;

            foreach(ParallaxObject pobj in GetComponentsInChildren<ParallaxObject>())
                pobj.OnUpdate(center, pos);
        }
    }
}