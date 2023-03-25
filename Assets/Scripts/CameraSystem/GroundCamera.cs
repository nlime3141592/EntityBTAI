using System.Collections.Generic;
using UnityEngine;

namespace Unchord
{
    public class GroundCamera : MonoBehaviour
    {
        public Vector2 center;

        void Update()
        {
            Vector3 pos = MainCamera.instance.transform.position;
            transform.position = pos;
        }
    }
}