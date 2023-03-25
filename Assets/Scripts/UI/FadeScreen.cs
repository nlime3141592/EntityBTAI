using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unchord
{
    public class FadeScreen : MonoBehaviour
    {
        private Image img;

        void Start()
        {
            img = GetComponent<Image>();            
        }

        void Update()
        {
            Color c = img.color;
            img.color = new Color(c.r, c.g, c.b, FadeManager.fadeValue);

            img.raycastTarget = img.color.a > 0;
        }
    }
}